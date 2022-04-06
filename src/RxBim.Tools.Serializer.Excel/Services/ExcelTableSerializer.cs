namespace RxBim.Tools.Serializer.Excel.Services
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using ClosedXML.Excel;
    using Models;
    using TableBuilder.Abstractions;
    using TableBuilder.Models;
    using TableBuilder.Models.Styles;

    /// <summary>
    /// Excel serializer to table
    /// </summary>
    internal class ExcelTableSerializer : ITableSerializer<ExcelTableSerializerParameters, IXLWorkbook>
    {
        /// <inheritdoc />
        public IXLWorkbook Serialize(Table table, ExcelTableSerializerParameters parameters)
        {
            var document = parameters.Document ?? new XLWorkbook();
            var worksheet = document.Worksheets.Add(parameters.WorksheetName ?? "Sheet1");

            var mergedCells = new List<CellRange>();

            var sheetColumnIndex = 1;
            for (var currentColumnIndex = 0;
                 currentColumnIndex < table.Columns.Count;
                 currentColumnIndex++, sheetColumnIndex++)
            {
                var column = worksheet.Column(sheetColumnIndex);
                column.Width = table.Columns[currentColumnIndex].Width;

                var sheetRowIndex = 1;
                for (var currentRowIndex = 0; currentRowIndex < table.Rows.Count; currentRowIndex++, sheetRowIndex++)
                {
                    var row = worksheet.Row(sheetRowIndex);
                    row.Height = table.Rows[currentRowIndex].Height;

                    var cell = table[currentRowIndex, currentColumnIndex];
                    var sheetCell = worksheet.Cell(sheetRowIndex, sheetColumnIndex);

                    SetData(sheetCell, cell.Content);
                    SetStyle(sheetCell, cell.Format);

                    // Merge logic
                    if (!cell.Merged || cell.MergeArea == null || mergedCells.Exists(x => Equals(x, cell.MergeArea)))
                        continue;

                    mergedCells.Add(cell.MergeArea.Value);

                    worksheet.Range(
                        cell.MergeArea.Value.TopRow + 1,
                        cell.MergeArea.Value.LeftColumn + 1,
                        cell.MergeArea.Value.BottomRow + 1,
                        cell.MergeArea.Value.RightColumn + 1).Merge();
                }
            }

            AdjustToContents(ref worksheet);

            if (parameters.FreezeRows > 0)
                worksheet.SheetView.Freeze(parameters.FreezeRows, table.Columns.Count);

            var (fromRow, fromColumn, toRow, toColumn) = parameters.AutoFilterRange;
            if (fromRow > 0 && fromColumn > 0)
                worksheet.Range(fromRow, fromColumn, toRow, toColumn).SetAutoFilter(true);

            return document;
        }

        private void SetData(IXLCell cell, ICellContent content)
        {
            switch (content)
            {
                case FormulaCellContent formulaCellContent:
                    cell.FormulaA1 = GetFormula(cell.Worksheet, formulaCellContent);
                    break;

                case NumerateCellContent numerateCellContent:
                    cell.Value = numerateCellContent.ValueObject;
                    cell.Style.NumberFormat.Format = numerateCellContent.Format;
                    break;

                default:
                    cell.Value = content.ValueObject;
                    break;
            }
        }

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

        private void SetStyle(IXLCell sheetCell, CellFormatStyle format)
        {
            var style = sheetCell.Style;

            if (format.BackgroundColor != null)
            {
                style.Fill.PatternType = XLFillPatternValues.Solid;
                style.Fill.SetBackgroundColor(XLColor.FromColor(format.BackgroundColor.Value));
            }

            if (!string.IsNullOrEmpty(format.TextFormat.FontFamily))
                style.Font.FontName = format.TextFormat.FontFamily;

            if (format.TextFormat.Bold != null)
                style.Font.Bold = format.TextFormat.Bold.Value;

            if (format.TextFormat.Italic != null)
                style.Font.Italic = format.TextFormat.Italic.Value;

            if (format.TextFormat.TextColor != null)
                style.Font.SetFontColor(XLColor.FromColor(format.TextFormat.TextColor.Value));

            if (format.TextFormat.TextSize != null)
                style.Font.FontSize = format.TextFormat.TextSize.Value;

            if (format.TextFormat.WrapText != null)
                style.Alignment.WrapText = format.TextFormat.WrapText.Value;

            if (format.TextFormat.ShrinkToFit != null)
                style.Alignment.ShrinkToFit = format.TextFormat.ShrinkToFit.Value;

            if (format.ContentHorizontalAlignment != null)
                style.Alignment.SetHorizontal(GetExcelHorizontalAlignment(format.ContentHorizontalAlignment.Value));

            if (format.ContentVerticalAlignment != null)
                style.Alignment.SetVertical(GetExcelVerticalAlignment(format.ContentVerticalAlignment.Value));

            if (format.Borders.Top != null)
                style.Border.TopBorder = GetExcelBorderStyle(format.Borders.Top.Value);

            if (format.Borders.Right != null)
                style.Border.RightBorder = GetExcelBorderStyle(format.Borders.Right.Value);

            if (format.Borders.Bottom != null)
                style.Border.BottomBorder = GetExcelBorderStyle(format.Borders.Bottom.Value);

            if (format.Borders.Left != null)
                style.Border.LeftBorder = GetExcelBorderStyle(format.Borders.Left.Value);
        }

        private XLAlignmentVerticalValues GetExcelVerticalAlignment(CellContentVerticalAlignment verticalAlignment)
        {
            return verticalAlignment switch
            {
                CellContentVerticalAlignment.Top => XLAlignmentVerticalValues.Top,
                CellContentVerticalAlignment.Middle => XLAlignmentVerticalValues.Center,
                CellContentVerticalAlignment.Bottom => XLAlignmentVerticalValues.Bottom,
                _ => throw new NotImplementedException(verticalAlignment.ToString())
            };
        }

        private XLAlignmentHorizontalValues GetExcelHorizontalAlignment(
            CellContentHorizontalAlignment horizontalAlignment) => horizontalAlignment switch
        {
            CellContentHorizontalAlignment.Center => XLAlignmentHorizontalValues.Center,
            CellContentHorizontalAlignment.Left => XLAlignmentHorizontalValues.Left,
            CellContentHorizontalAlignment.Right => XLAlignmentHorizontalValues.Right,
            _ => throw new NotImplementedException(horizontalAlignment.ToString())
        };

        private XLBorderStyleValues GetExcelBorderStyle(
            CellBorderType borderType) => borderType switch
        {
            CellBorderType.Hidden => XLBorderStyleValues.None,
            CellBorderType.Bold => XLBorderStyleValues.Medium,
            CellBorderType.Thin => XLBorderStyleValues.Thin,
            _ => throw new NotImplementedException(borderType.ToString())
        };

        private void AdjustToContents(ref IXLWorksheet worksheet)
        {
            worksheet.Columns().AdjustToContents();
            worksheet.Rows().AdjustToContents();
        }
    }
}