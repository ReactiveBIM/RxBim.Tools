namespace RxBim.Tools.Serializer.Excel.Services
{
    using System.Linq;
    using ClosedXML.Excel;
    using TableBuilder.Abstractions;
    using TableBuilder.Services;
    using Table = TableBuilder.Models.Table;

    /// <summary>
    /// Excel deserializer to table
    /// </summary>
    internal class ExcelTableDeserializer : ITableDeserializer<IXLWorksheet>
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