namespace RxBim.Tools.TableBuilder.Services
{
    using System.Linq;
    using ClosedXML.Excel;

    /// <summary>
    /// Represents a <see cref="Table"/> converter from an Excel workbook.
    /// </summary>
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
                    var cellValue = row.Cell(sourceColumnIndex).Value;
                    builder[tableRowIndex, tableColumnIndex].SetValue(cellValue);
                    tableColumnIndex++;
                }

                tableRowIndex++;
            }

            return builder;
        }
    }
}