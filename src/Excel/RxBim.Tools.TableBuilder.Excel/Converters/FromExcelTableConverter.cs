namespace RxBim.Tools.TableBuilder
{
    using System;
    using System.Drawing;
    using System.Linq;
    using ClosedXML.Excel;
    using JetBrains.Annotations;

    /// <summary>
    /// Represents a <see cref="Table"/> converter from an Excel workbook.
    /// </summary>
    [UsedImplicitly]
    internal class FromExcelTableConverter : IFromExcelTableConverter
    {
        /// <inheritdoc/>
        public Table Convert(IXLWorkbook source, FromExcelConverterParameters parameters)
        {
            var sheet = string.IsNullOrWhiteSpace(parameters.WorksheetName)
                ? source.Worksheet(1)
                : source.Worksheet(parameters.WorksheetName);

            var builder = new TableBuilder();

            var tableRowIndex = 0;
            var rowsCount = sheet.Rows().Count();
            var columnsCount = sheet.Columns().Count();

            builder.AddColumn(count: columnsCount);

            // Read data
            for (var sourceRowIndex = 1; sourceRowIndex <= rowsCount; sourceRowIndex++)
            {
                var row = sheet.Row(sourceRowIndex);
                builder.AddRow();

                var tableColumnIndex = 0;
                for (var sourceColumnIndex = 1; sourceColumnIndex <= columnsCount; sourceColumnIndex++)
                {
                    var sheetCell = row.Cell(sourceColumnIndex);
                    var cell = builder[tableRowIndex, tableColumnIndex];

                    SetCellContent(cell, sheetCell);
                    SetCellFormat(cell, sheetCell);

                    tableColumnIndex++;
                }

                tableRowIndex++;
            }

            return builder;
        }

        private void SetCellContent(ICellEditor cellEditor, IXLCell cell)
        {
            if (cell.HasFormula)
                cellEditor.SetFormula(cell.FormulaA1);
            else
                cellEditor.SetValue(cell.Value);
        }

        private void SetCellFormat(ICellEditor cellEditor, IXLCell cell)
        {
            var cellStyle = cell.Style;
            cellEditor.SetFormat(format =>
            {
                format.SetTextFormat(textFormat =>
                {
                    var fontStyle = cellStyle.Font;
                    textFormat.SetBold(fontStyle.Bold);
                    textFormat.SetTextSize(fontStyle.FontSize);
                    textFormat.SetTextColor(GetColorFromExcel(fontStyle.FontColor));
                    textFormat.SetWrapText(cellStyle.Alignment.WrapText);
                    textFormat.SetItalic(fontStyle.Italic);
                    textFormat.SetFontFamily(fontStyle.FontName);
                });
                format.SetBorders(
                    GetBorderTypeFromExcel(cellStyle.Border.TopBorder),
                    GetBorderTypeFromExcel(cellStyle.Border.BottomBorder),
                    GetBorderTypeFromExcel(cellStyle.Border.LeftBorder),
                    GetBorderTypeFromExcel(cellStyle.Border.RightBorder));
                format.SetBackgroundColor(GetColorFromExcel(cellStyle.Fill.BackgroundColor));
                format.SetContentVerticalAlignment(GetVerticalAlignmentFromExcel(cellStyle.Alignment.Vertical));
                format.SetContentHorizontalAlignment(GetHorizontalAlignmentFromExcel(cellStyle.Alignment.Horizontal));
            });
        }

        private Color? GetColorFromExcel(XLColor color)
        {
            return color.ColorType is XLColorType.Color
                ? color.Color
                : null;
        }

        private CellContentVerticalAlignment? GetVerticalAlignmentFromExcel(XLAlignmentVerticalValues verticalAlignment)
            => verticalAlignment switch
            {
                XLAlignmentVerticalValues.Top => CellContentVerticalAlignment.Top,
                XLAlignmentVerticalValues.Center => CellContentVerticalAlignment.Middle,
                XLAlignmentVerticalValues.Bottom => CellContentVerticalAlignment.Bottom,
                XLAlignmentVerticalValues.Justify => null,
                _ => throw new NotImplementedException(verticalAlignment.ToString())
            };

        private CellContentHorizontalAlignment? GetHorizontalAlignmentFromExcel(
            XLAlignmentHorizontalValues horizontalAlignment)
            => horizontalAlignment switch
            {
                XLAlignmentHorizontalValues.Center => CellContentHorizontalAlignment.Center,
                XLAlignmentHorizontalValues.Left => CellContentHorizontalAlignment.Left,
                XLAlignmentHorizontalValues.Right => CellContentHorizontalAlignment.Right,
                XLAlignmentHorizontalValues.General or XLAlignmentHorizontalValues.Justify => null,
                _ => throw new NotImplementedException(horizontalAlignment.ToString())
            };

        private CellBorderType GetBorderTypeFromExcel(XLBorderStyleValues borderStyle)
            => borderStyle switch
            {
                XLBorderStyleValues.None => CellBorderType.Hidden,
                XLBorderStyleValues.Medium => CellBorderType.Bold,
                XLBorderStyleValues.Thin => CellBorderType.Thin,
                _ => throw new NotImplementedException(borderStyle.ToString())
            };
    }
}