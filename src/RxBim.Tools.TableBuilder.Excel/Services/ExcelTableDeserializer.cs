namespace RxBim.Tools.TableBuilder.Services
{
    using System.Linq;
    using ClosedXML.Excel;

    /// <summary>
    /// Represents a <see cref="Table"/> deserialazer from an Excel workbook.
    /// </summary>
    internal class ExcelTableDeserializer : IExcelTableDeserializer
    {
        /// <inheritdoc/>
        public Table Deserialize(IXLWorksheet source)
        {
            var builder = new TableBuilder();

            var tableRowIndex = 0;
            var rowsCount = source.Rows().Count();
            var columnsCount = source.Columns().Count();

            builder.AddColumn(count: columnsCount);

            // Read data
            for (var sourceRowIndex = 1; sourceRowIndex <= rowsCount; sourceRowIndex++)
            {
                var row = source.Row(sourceRowIndex);
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