namespace RxBim.Tools.TableBuilder
{
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
                    SetCellContent(builder[tableRowIndex, tableColumnIndex], row.Cell(sourceColumnIndex));

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
    }
}