namespace RxBim.Tools.TableBuilder;

using System.Collections.Generic;
using System.Linq;
using ClosedXML.Excel;
using ClosedXML.Excel.Drawings;
using JetBrains.Annotations;

/// <summary>
/// Represents a <see cref="Table"/> converter from an Excel workbook.
/// </summary>
[UsedImplicitly]
internal class FromExcelTableConverter : IFromExcelTableConverter
{
    /// <inheritdoc/>
    public Table Convert(IXLWorkbook workbook, FromExcelConverterParameters parameters)
    {
        var sheet = string.IsNullOrWhiteSpace(parameters.WorksheetName)
            ? workbook.Worksheet(1)
            : workbook.Worksheet(parameters.WorksheetName);

        var tableBuilder = new TableBuilder();

        var tableRowIndex = 0;
        var rowsCount = sheet.Rows().Count();
        var columnsCount = sheet.Columns().Count();
        var xlPictures = GetPictures(sheet);

        tableBuilder.AddColumn(count: columnsCount);

        // Read data
        for (var sourceRowIndex = 1; sourceRowIndex <= rowsCount; sourceRowIndex++)
        {
            var xlRow = sheet.Row(sourceRowIndex);
            tableBuilder.AddRow();

            var tableColumnIndex = 0;
            for (var sourceColumnIndex = 1; sourceColumnIndex <= columnsCount; sourceColumnIndex++)
            {
                var xlCell = xlRow.Cell(sourceColumnIndex);
                var cellBuilder = tableBuilder[tableRowIndex, tableColumnIndex];

                if (xlPictures.TryGetValue(xlCell, out var xlPicture))
                    cellBuilder.SetContent(new ImageCellContent(xlPicture.ImageStream.ToArray(), xlCell.Value));
                else
                    xlCell.SetValue(xlCell.Value);

                tableColumnIndex++;
            }

            tableRowIndex++;
        }

        return tableBuilder;
    }

    private Dictionary<IXLCell, IXLPicture> GetPictures(IXLWorksheet sheet) => sheet.Pictures
        .GroupBy(p => p.TopLeftCell)
        .ToDictionary(k => k.Key, v => v.First());
}