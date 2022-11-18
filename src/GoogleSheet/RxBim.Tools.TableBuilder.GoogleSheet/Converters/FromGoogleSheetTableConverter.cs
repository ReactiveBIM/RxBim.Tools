namespace RxBim.Tools.TableBuilder.Converters;

using System.Linq;
using Google.Apis.Sheets.v4.Data;
using JetBrains.Annotations;
using Styles;
using Color = System.Drawing.Color;

/// <summary>
/// Represents a <see cref="Table"/> converter from an Google Sheet workbook.
/// </summary>
[UsedImplicitly]
public class FromGoogleSheetTableConverter : IFromGoogleSheetTableConverter
{
    /// <inheritdoc />
    public Table Convert(Spreadsheet table, FromGoogleSheetConverterParameters parameters)
    {
        var sheet = GetSheet(table, parameters);

        var builder = new TableBuilder();
        var sheetData = sheet.Data.FirstOrDefault();
        if (sheetData?.RowData == null)
            return builder.Build();
        for (var rowIndex = 0; rowIndex < sheetData.RowData.Count; rowIndex++)
        {
            builder.AddRow();
            var rowData = sheetData.RowData[rowIndex];
            var cellsData = rowData.Values;
            if (cellsData == null)
                continue;
            for (var cellIndex = 0; cellIndex < cellsData.Count; cellIndex++)
            {
                if (cellIndex >= builder.GetColumns().Count())
                    builder.AddColumn();
                var cellData = cellsData[cellIndex];
                var builderCell = builder[rowIndex, cellIndex];
                if (cellData.EffectiveValue?.BoolValue.HasValue ?? false)
                {
                    builderCell.SetValue(cellData.EffectiveValue.BoolValue.Value);
                }
                else if (cellData.EffectiveValue?.NumberValue.HasValue ?? false)
                {
                    builderCell.SetValue(cellData.EffectiveValue.NumberValue.Value);
                }
                else
                {
                    builderCell.SetValue(cellData.FormattedValue);
                }

                CopyCellFormat(cellData, builderCell);
            }
        }

        CopyRowFormats(sheetData, builder);
        CopyColumnFormats(sheetData, builder);

        CopyMergedZones(sheet, builder);

        return builder.Build();
    }

    private void CopyMergedZones(Sheet sheet, TableBuilder builder)
    {
        if (sheet.Merges == null || !sheet.Merges.Any())
            return;
        foreach (var sheetMerge in sheet.Merges)
        {
            var startRowIndex = sheetMerge.StartRowIndex;
            var startColumnIndex = sheetMerge.StartColumnIndex;
            var lastRowIndex = sheetMerge.EndRowIndex;
            var lastColumnsIndex = sheetMerge.EndColumnIndex;
            if (startRowIndex == null
                || startColumnIndex == null
                || lastRowIndex == null
                || lastColumnsIndex == null)
                continue;
            var startRow = builder
                .ToRows()
                .ElementAt(startRowIndex.Value);
            var startCell = startRow
                .ToCells()
                .ElementAt(startColumnIndex.Value);
            startCell.MergeNext(lastColumnsIndex.Value - startColumnIndex.Value - 1);
            startCell.MergeDown(lastRowIndex.Value - startRowIndex.Value - 1);
        }
    }

    private Sheet GetSheet(Spreadsheet spreadsheet, FromGoogleSheetConverterParameters parameters)
    {
        var targetSheet =
            spreadsheet.Sheets.FirstOrDefault(sheet => sheet.Properties.Title.Equals(parameters.SheetName)) ??
            spreadsheet.Sheets.FirstOrDefault()!;
        if (targetSheet.Data != null)
            return targetSheet;
        var spreadsheetRequest = parameters.SheetsService.Spreadsheets.Get(spreadsheet.SpreadsheetId);
        spreadsheetRequest.IncludeGridData = true;
        spreadsheet = spreadsheetRequest.Execute();
        return spreadsheet.Sheets.FirstOrDefault(sheet => sheet.Properties.Title.Equals(targetSheet.Properties.Title))!;
    }

    private void CopyCellFormat(CellData googleCellData, CellBuilder cellBuilder)
    {
        CopyBordersFormat(googleCellData, cellBuilder);
        CopyBackgroundColor(googleCellData, cellBuilder);
        CopyContentMargins(googleCellData, cellBuilder);
        CopyTextFormat(googleCellData, cellBuilder);
        CopyAlignment(googleCellData, cellBuilder);
    }

    private void CopyRowFormats(GridData gridData, TableBuilder tableBuilder)
    {
        var sizes = gridData.RowMetadata?
            .Select(metadata => metadata.PixelSize)
            .ToList();
        if (sizes == null || !sizes.Any())
            return;

        var tableBuilderRows = tableBuilder
            .ToRows()
            .ToList();

        for (var rowIndex = 0; rowIndex < sizes.Count && rowIndex < tableBuilderRows.Count; rowIndex++)
        {
            var size = sizes[rowIndex];
            if (size.HasValue)
                tableBuilderRows[rowIndex].SetHeight(size.Value);
        }
    }

    private void CopyColumnFormats(GridData gridData, TableBuilder tableBuilder)
    {
        var sizes = gridData.ColumnMetadata?
            .Select(metadata => metadata.PixelSize)
            .ToList();
        if (sizes == null || !sizes.Any())
            return;

        var tableBuilderColumns = tableBuilder
            .GetColumns()
            .ToList();

        for (var columnIndex = 0; columnIndex < sizes.Count && columnIndex < tableBuilderColumns.Count; columnIndex++)
        {
            var size = sizes[columnIndex];
            if (size.HasValue)
                tableBuilderColumns[columnIndex].SetWidth(size.Value);
        }
    }

    private void CopyBordersFormat(CellData googleCellData, CellBuilder cellBuilder)
    {
        var cellBorders = new CellBorders();
        var properties = cellBorders
            .GetType()
            .GetProperties();

        var googleBorders = googleCellData
            .EffectiveFormat?
            .Borders;
        if (googleBorders == null)
            return;
        var googleProperties = googleBorders
            .GetType()
            .GetProperties();
        foreach (var googleBordersTypeProperty in googleProperties)
        {
            var googleBordersPropertyName = googleBordersTypeProperty.Name;
            if (!properties.Any(cellProperty => cellProperty.Name.Equals(googleBordersPropertyName)))
                continue;
            var targetProperty = properties.FirstOrDefault(property => property.Name.Equals(googleBordersPropertyName));
            var border = googleBordersTypeProperty.GetValue(googleBorders) as Border;
            var borderStyle = border?.Style;
            switch (borderStyle)
            {
                case "SOLID_MEDIUM":
                case "SOLID_THICK":
                    targetProperty?.SetValue(cellBorders, CellBorderType.Bold);
                    break;
                case "SOLID":
                    targetProperty?.SetValue(cellBorders, CellBorderType.Thin);
                    break;
                case "NONE":
                    targetProperty?.SetValue(cellBorders, CellBorderType.Hidden);
                    break;
            }
        }

        cellBuilder.SetFormat(format =>
            format.SetBorders(cellBorders.Top, cellBorders.Bottom, cellBorders.Left, cellBorders.Right));
    }

    private void CopyBackgroundColor(CellData googleCellData, CellBuilder cellBuilder)
    {
        var googleBackgroundColor = googleCellData
            .EffectiveFormat?
            .BackgroundColor;
        if (googleBackgroundColor == null)
            return;
        var colorChannels = ConvertArgb(
            googleBackgroundColor.Alpha ?? 1.0,
            googleBackgroundColor.Red ?? 0.0,
            googleBackgroundColor.Green ?? 0.0,
            googleBackgroundColor.Blue ?? 0.0);
        cellBuilder.SetFormat(format => format.SetBackgroundColor(
            Color.FromArgb(
                colorChannels.A,
                colorChannels.R,
                colorChannels.G,
                colorChannels.B)));
    }

    private void CopyContentMargins(CellData googleCellData, CellBuilder cellBuilder)
    {
        var padding = googleCellData
            .EffectiveFormat?
            .Padding;
        if (padding == null)
            return;
        cellBuilder.SetFormat(format => format.SetContentMargins(
            padding.Top,
            padding.Bottom,
            padding.Left,
            padding.Right));
    }

    private void CopyTextFormat(CellData googleCellData, CellBuilder cellBuilder)
    {
        var googleTextFormat = googleCellData
            .EffectiveFormat?
            .TextFormat;
        if (googleTextFormat == null)
            return;
        cellBuilder.SetFormat(format => format.SetTextFormat(textFormat =>
        {
            textFormat.SetBold(googleTextFormat.Bold ?? false);
            textFormat.SetItalic(googleTextFormat.Italic ?? false);
            textFormat.SetFontFamily(googleTextFormat.FontFamily);
            var fontColor = ConvertArgb(
                googleTextFormat.ForegroundColor.Alpha ?? 1.0,
                googleTextFormat.ForegroundColor.Red ?? 0.0,
                googleTextFormat.ForegroundColor.Green ?? 0.0,
                googleTextFormat.ForegroundColor.Blue ?? 0);
            textFormat.SetTextColor(
                Color.FromArgb(
                    fontColor.A,
                    fontColor.R,
                    fontColor.G,
                    fontColor.B));
            textFormat.SetTextSize(googleTextFormat.FontSize);
            textFormat.SetWrapText(googleCellData.EffectiveFormat?.WrapStrategy.Equals("WRAP"));
        }));
    }

    private void CopyAlignment(CellData googleCellData, CellBuilder cellBuilder)
    {
        var horizontalAlignment = googleCellData
            .EffectiveFormat?
            .HorizontalAlignment;
        if (horizontalAlignment != null)
        {
            switch (horizontalAlignment)
            {
                case "LEFT":
                    cellBuilder.SetFormat(format =>
                        format.SetContentHorizontalAlignment(CellContentHorizontalAlignment.Left));
                    break;
                case "RIGHT":
                    cellBuilder.SetFormat(format =>
                        format.SetContentHorizontalAlignment(CellContentHorizontalAlignment.Right));
                    break;
                case "CENTER":
                    cellBuilder.SetFormat(format =>
                        format.SetContentHorizontalAlignment(CellContentHorizontalAlignment.Center));
                    break;
            }
        }

        var verticalAlignment = googleCellData
            .EffectiveFormat?
            .VerticalAlignment;
        if (verticalAlignment != null)
        {
            switch (verticalAlignment)
            {
                case "TOP":
                    cellBuilder
                        .SetFormat(format => format.SetContentVerticalAlignment(CellContentVerticalAlignment.Top));
                    break;
                case "MIDDLE":
                    cellBuilder
                        .SetFormat(format => format.SetContentVerticalAlignment(CellContentVerticalAlignment.Middle));
                    break;
                case "BOTTOM":
                    cellBuilder
                        .SetFormat(format => format.SetContentVerticalAlignment(CellContentVerticalAlignment.Bottom));
                    break;
            }
        }
    }

    private (int A, int R, int G, int B) ConvertArgb(double a, double r, double g, double b)
    {
        var alphaChannel = LinearlyInterpolate(a, 1.0, 0.0, 0.0, 255.0);
        var redChannel = LinearlyInterpolate(r, 0.0, 1.0, 0.0, 255.0);
        var greenChannel = LinearlyInterpolate(g, 0.0, 1.0, 0.0, 255.0);
        var blueChannel = LinearlyInterpolate(b, 0.0, 1.0, 0.0, 255.0);

        return (
            System.Convert.ToInt32(alphaChannel),
            System.Convert.ToInt32(redChannel),
            System.Convert.ToInt32(greenChannel),
            System.Convert.ToInt32(blueChannel));
    }

    private double LinearlyInterpolate(double x, double x0, double x1, double y0, double y1)
    {
        if (x1 - x0 == 0)
        {
            return (y0 + y1) / 2;
        }

        return y0 + (x - x0) * (y1 - y0) / (x1 - x0);
    }
}