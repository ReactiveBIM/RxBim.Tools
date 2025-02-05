namespace RxBim.Tools.TableBuilder;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using ClosedXML.Excel;
using JetBrains.Annotations;

/// <inheritdoc />
[UsedImplicitly]
internal class ExcelTableConverter : IExcelTableConverter
{
    /// <summary>
    /// Standard DPI.
    /// </summary>
    private const double StandardDpi = 96;

    /// <summary>
    /// Is DPI calculated.
    /// </summary>
    private static bool _isDpiCalculated;

    /// <summary>
    /// DPI by X.
    /// </summary>
    private static uint _dpiX;

    /// <summary>
    /// DPI by Y.
    /// </summary>
    private static uint _dpiY;

    /// <inheritdoc />
    public IXLWorkbook Convert(Table table, ExcelTableConverterParameters parameters)
    {
        var workbook = parameters.Workbook ?? new XLWorkbook();
        var worksheet = workbook.Worksheets.Add(parameters.WorksheetName ?? "Sheet1");

        var mergedCells = new List<CellRange>();

        // Setting all columns widths and rows heights.
        ApplyToColumns(table, worksheet, (xlColumn, columnIndex) =>
        {
            if (table.Columns[columnIndex].IsAdjustedToContent)
                xlColumn.AdjustToContents();
            else
                xlColumn.Width = table.Columns[columnIndex].Width ?? table.GetAverageColumnWidth();

            ApplyToRows(
                table,
                worksheet,
                (xlRow, rowIndex) =>
                {
                    if (table.Rows[rowIndex].IsAdjustedToContent)
                        xlRow.AdjustToContents();
                    else
                        xlRow.Height = table.Rows[rowIndex].Height ?? table.GetAverageRowHeight();
                });
        });

        // Fill cells.
        ApplyToColumns(table, worksheet, (_, columnIndex) =>
            ApplyToRows(table, worksheet, (xlRow, rowIndex) =>
                FillRow(table, worksheet, xlRow, columnIndex, rowIndex, mergedCells)));

        if (parameters.FreezeRows > 0)
            worksheet.SheetView.Freeze(parameters.FreezeRows, table.Columns.Count);

        var (fromRow, fromColumn, toRow, toColumn) = parameters.AutoFilterRange;

        if (fromRow > 0 && fromColumn > 0)
            worksheet.Range(fromRow, fromColumn, toRow, toColumn).SetAutoFilter(true);

        return workbook;
    }

    /// <inheritdoc/>
    public double ConvertWidthToPixels(double width)
    {
        // DPI (screen resolution) is used to determine the width and height of Excel in pixels.
        CalculateDpi();

        // Magic constants for determining column width and row height in pixels (from Google).
        return (width * 7 + 5) / (StandardDpi / _dpiX);
    }

    /// <inheritdoc/>
    public double ConvertHeightToPixels(double height)
    {
        CalculateDpi();
        return height / 0.75 / (StandardDpi / _dpiY);
    }

    [DllImport("shcore.dll")]
    private static extern int GetDpiForMonitor(IntPtr hmonitor, int dpiType, out uint dpiX, out uint dpiY);

    [DllImport("user32.dll")]
    private static extern IntPtr MonitorFromWindow(IntPtr hwnd, uint dwFlags);

    [DllImport("user32.dll")]
    private static extern IntPtr GetDesktopWindow();

    private void ApplyToColumns(Table table, IXLWorksheet worksheet, Action<IXLColumn, int> action)
    {
        for (var columnIndex = 0; columnIndex < table.Columns.Count; columnIndex++)
        {
            var xlColumn = worksheet.Column(columnIndex + 1);
            action(xlColumn, columnIndex);
        }
    }

    private void ApplyToRows(
        Table table,
        IXLWorksheet worksheet,
        Action<IXLRow, int> action)
    {
        for (var rowIndex = 0; rowIndex < table.Rows.Count; rowIndex++)
        {
            var xlRow = worksheet.Row(rowIndex + 1);
            action(xlRow, rowIndex);
        }
    }

    private void FillRow(
        Table table,
        IXLWorksheet worksheet,
        IXLRow xlRow,
        int columnIndex,
        int rowIndex,
        List<CellRange> mergedCells)
    {
        var cell = table[rowIndex, columnIndex];
        var sheetCell = worksheet.Cell(rowIndex + 1, columnIndex + 1);

        SetFormat(sheetCell, cell.GetComposedFormat());

        if (table.Rows[rowIndex].IsAdjustedToContent)
            xlRow.AdjustToContents();
        else
            xlRow.Height = table.Rows[rowIndex].Height ?? table.GetAverageRowHeight();

        // Merge logic
        if (!cell.Merged || cell.MergeArea == null)
        {
            SetData(sheetCell, cell.Content);
            return;
        }

        // If such an area already exists, then the data is written only to the first cell.
        if (mergedCells.Exists(x => Equals(x, cell.MergeArea)))
            return;

        mergedCells.Add(cell.MergeArea.Value);

        worksheet.Range(
            cell.MergeArea.Value.TopRow + 1,
            cell.MergeArea.Value.LeftColumn + 1,
            cell.MergeArea.Value.BottomRow + 1,
            cell.MergeArea.Value.RightColumn + 1).Merge();

        SetData(sheetCell, cell.Content);
    }

    private void SetData(IXLCell cell, ICellContent content)
    {
        switch (content)
        {
            case FormulaCellContent formula:
                cell.FormulaA1 = GetFormula(cell.Worksheet, formula);
                break;

            case NumericCellContent numeric:
                cell.Value = numeric.ValueObject;
                cell.Style.NumberFormat.Format = numeric.Format;
                break;

            case ImageCellContent image:
                SetImage(cell, image);
                break;

            default:
                cell.SetValue(content.ValueObject);
                break;
        }
    }

    private void SetImage(IXLCell cell, ImageCellContent image)
    {
        var pictureCell = cell.Worksheet
            .AddPicture(image.ImageFile)
            .MoveTo(cell)
            .Scale(image.Scale, true);

        var left = cell.Style.Alignment.Horizontal switch
        {
            XLAlignmentHorizontalValues.Center =>
                (ConvertWidthToPixels(GetCellWidth(cell)) - pictureCell.Width) / 2,
            XLAlignmentHorizontalValues.Right =>
                ConvertWidthToPixels(GetCellWidth(cell)) - pictureCell.Width,
            _ => 0
        };

        var top = cell.Style.Alignment.Vertical switch
        {
            XLAlignmentVerticalValues.Center =>
                (ConvertHeightToPixels(GetCellHeight(cell)) - pictureCell.Height) / 2,
            XLAlignmentVerticalValues.Bottom =>
                ConvertHeightToPixels(GetCellHeight(cell)) - pictureCell.Height,
            _ => 0
        };

        pictureCell.MoveTo(cell, (int)left, (int)top);
    }

    private double GetCellWidth(IXLCell cell) =>
        cell.IsMerged() ?
            cell.MergedRange().Columns().Sum(c => c.WorksheetColumn().Width)
            : cell.WorksheetColumn().Width;

    private double GetCellHeight(IXLCell cell) =>
        cell.IsMerged() ?
            cell.MergedRange().Rows().Sum(c => c.WorksheetRow().Height)
            : cell.WorksheetRow().Height;

    private string GetFormula(IXLWorksheet ws, FormulaCellContent formula)
    {
        var sFormula = new StringBuilder();

        var (fromRow, fromColumn, toRow, toColumn) = formula.CellRange;

        sFormula.Append(formula.Formula switch
            {
                Formulas.Sum => "SUM",
                _ => throw new NotImplementedException(formula.Formula.ToString())
            })
            .Append("(")
            .Append(ws.Range(fromRow, fromColumn, toRow, toColumn).RangeAddress)
            .Append(")");

        return sFormula.ToString();
    }

    private void SetFormat(IXLCell sheetCell, CellFormatStyle cellFormat)
    {
        var cellStyle = sheetCell.Style;

        SetBackgroundFormat(cellFormat, cellStyle);
        SetTextFormat(cellFormat, cellStyle);
        SetAlignmentFormat(cellFormat, cellStyle);
        SetBordersFormat(cellFormat, cellStyle);
    }

    private void SetBackgroundFormat(CellFormatStyle cellFormat, IXLStyle cellStyle)
    {
        if (cellFormat.BackgroundColor != null)
        {
            cellStyle.Fill.PatternType = XLFillPatternValues.Solid;
            cellStyle.Fill.SetBackgroundColor(XLColor.FromColor(cellFormat.BackgroundColor.Value));
        }
    }

    private void SetAlignmentFormat(CellFormatStyle cellFormat, IXLStyle cellStyle)
    {
        if (cellFormat.ContentHorizontalAlignment != null)
            cellStyle.Alignment.SetHorizontal(GetExcelHorizontalAlignment(cellFormat.ContentHorizontalAlignment.Value));

        if (cellFormat.ContentVerticalAlignment != null)
            cellStyle.Alignment.SetVertical(GetExcelVerticalAlignment(cellFormat.ContentVerticalAlignment.Value));
    }

    private void SetBordersFormat(CellFormatStyle cellFormat, IXLStyle cellStyle)
    {
        if (cellFormat.Borders.Top != null)
            cellStyle.Border.TopBorder = GetExcelBorderStyle(cellFormat.Borders.Top.Value);

        if (cellFormat.Borders.Right != null)
            cellStyle.Border.RightBorder = GetExcelBorderStyle(cellFormat.Borders.Right.Value);

        if (cellFormat.Borders.Bottom != null)
            cellStyle.Border.BottomBorder = GetExcelBorderStyle(cellFormat.Borders.Bottom.Value);

        if (cellFormat.Borders.Left != null)
            cellStyle.Border.LeftBorder = GetExcelBorderStyle(cellFormat.Borders.Left.Value);
    }

    private void SetTextFormat(CellFormatStyle cellFormat, IXLStyle cellStyle)
    {
        if (!string.IsNullOrEmpty(cellFormat.TextFormat.FontFamily))
            cellStyle.Font.FontName = cellFormat.TextFormat.FontFamily;

        if (cellFormat.TextFormat.Bold != null)
            cellStyle.Font.Bold = cellFormat.TextFormat.Bold.Value;

        if (cellFormat.TextFormat.Italic != null)
            cellStyle.Font.Italic = cellFormat.TextFormat.Italic.Value;

        if (cellFormat.TextFormat.TextColor != null)
            cellStyle.Font.SetFontColor(XLColor.FromColor(cellFormat.TextFormat.TextColor.Value));

        if (cellFormat.TextFormat.TextSize != null)
            cellStyle.Font.FontSize = cellFormat.TextFormat.TextSize.Value;

        if (cellFormat.TextFormat.WrapText != null)
            cellStyle.Alignment.WrapText = cellFormat.TextFormat.WrapText.Value;

        if (cellFormat.TextFormat.ShrinkToFit != null)
            cellStyle.Alignment.ShrinkToFit = cellFormat.TextFormat.ShrinkToFit.Value;
    }

    private XLAlignmentVerticalValues GetExcelVerticalAlignment(CellContentVerticalAlignment verticalAlignment) =>
        verticalAlignment switch
        {
            CellContentVerticalAlignment.Top => XLAlignmentVerticalValues.Top,
            CellContentVerticalAlignment.Middle => XLAlignmentVerticalValues.Center,
            CellContentVerticalAlignment.Bottom => XLAlignmentVerticalValues.Bottom,
            _ => throw new NotImplementedException(verticalAlignment.ToString())
        };

    private XLAlignmentHorizontalValues GetExcelHorizontalAlignment(CellContentHorizontalAlignment horizontalAlignment) =>
        horizontalAlignment switch
        {
            CellContentHorizontalAlignment.Center => XLAlignmentHorizontalValues.Center,
            CellContentHorizontalAlignment.Left => XLAlignmentHorizontalValues.Left,
            CellContentHorizontalAlignment.Right => XLAlignmentHorizontalValues.Right,
            _ => throw new NotImplementedException(horizontalAlignment.ToString())
        };

    private XLBorderStyleValues GetExcelBorderStyle(CellBorderType borderType) =>
        borderType switch
        {
            CellBorderType.Hidden => XLBorderStyleValues.None,
            CellBorderType.Bold => XLBorderStyleValues.Medium,
            CellBorderType.Thin => XLBorderStyleValues.Thin,
            _ => throw new NotImplementedException(borderType.ToString())
        };

    private void CalculateDpi()
    {
        if (_isDpiCalculated)
            return;

        // Determining dpi via Graphics does not work (does not find the System.Drawing.Common assembly).
        var hwnd = GetDesktopWindow();
        var hMonitor = MonitorFromWindow(hwnd, 0);
        GetDpiForMonitor(hMonitor, 0, out _dpiX, out _dpiY);
        _isDpiCalculated = true;
    }
}