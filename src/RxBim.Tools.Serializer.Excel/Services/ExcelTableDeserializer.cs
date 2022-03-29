namespace RxBim.Tools.Serializer.Excel.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using ClosedXML.Excel;
    using TableBuilder.Abstractions;
    using TableBuilder.Models.Contents;
    using TableBuilder.Services;
    using Table = TableBuilder.Models.Table;

    /// <summary>
    /// Excel deserializer to table
    /// </summary>
    internal class ExcelTableDeserializer : ITableDeserializer<IXLWorksheet>
    {
        /// <inheritdoc/>
        public (Table Data, List<string> Headers) DeserializeTable(IXLWorksheet source)
        {
            const int headersRow = 1;
            const int firstValueRow = 3;

            var builder = new TableBuilder();
            var headers = new List<string>();

            var headerRow = source.Row(headersRow);
            var cellCount = headerRow.CellCount();

            // Set columns and headers
            for (var cellIndex = 0; cellIndex < cellCount; cellIndex++)
            {
                builder.AddColumn();

                var cell = headerRow.Cell(cellIndex);
                if (cell == null || string.IsNullOrWhiteSpace(cell.ToString()))
                {
                    headers.Add(string.Empty);
                    continue;
                }

                headers.Add(cell.ToString());
            }

            var rowIndex = 0;

            // Read data
            for (var tableRowIndex = firstValueRow; tableRowIndex <= source.RowCount(); tableRowIndex++)
            {
                var row = source.Row(tableRowIndex);
                if (row == null || row.Cells().All(d => d.IsEmpty()))
                    break;

                builder.AddRow();

                for (var columnIndex = 0; columnIndex < cellCount; columnIndex++)
                {
                    var cellValue = row.Cell(columnIndex).Value?.ToString();
                    if (!string.IsNullOrEmpty(cellValue))
                        builder[rowIndex, columnIndex].SetContent(new TextCellContent(cellValue!));
                }

                rowIndex++;
            }

            return (builder, headers);
        }
    }
}