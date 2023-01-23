namespace RxBim.Tools.TableBuilder;

using System.Linq;
using Google.Apis.Sheets.v4.Data;
using JetBrains.Annotations;
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
                if (cellIndex >= builder.ColumnsCount)
                    builder.AddColumn();
                
                var cellData = cellsData[cellIndex];
                var builderCell = builder[rowIndex, cellIndex];
                
                if (cellData.EffectiveValue?.BoolValue.HasValue ?? false)
                    builderCell.SetValue(cellData.EffectiveValue.BoolValue.Value);
                else if (cellData.EffectiveValue?.NumberValue.HasValue ?? false)
                    builderCell.SetValue(cellData.EffectiveValue.NumberValue.Value);
                else
                    builderCell.SetValue(cellData.FormattedValue);

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
                .Rows
                .ElementAt(startRowIndex.Value);
            var startCell = startRow
                .Cells
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

    private void CopyCellFormat(CellData googleCellData, ICellBuilder<Cell> cellBuilder)
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
            .Rows
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

        var tableBuilderColumns = tableBuilder.Columns
            .ToList();

        for (var columnIndex = 0; columnIndex < sizes.Count && columnIndex < tableBuilderColumns.Count; columnIndex++)
        {
            var size = sizes[columnIndex];
            if (size.HasValue)
                tableBuilderColumns[columnIndex].SetWidth(size.Value);
        }
    }

    private void CopyBordersFormat(CellData googleCellData, ICellBuilder<Cell> cellBuilder)
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

        cellBuilder.SetFormat(format => format
            .SetBorders(borderBuilder => borderBuilder
                .SetBorders(cellBorders.Top, cellBorders.Bottom, cellBorders.Left, cellBorders.Right)));
    }

    private void CopyBackgroundColor(CellData googleCellData, ICellBuilder<Cell> cellBuilder)
    {
        var googleBackgroundColor = googleCellData
            .EffectiveFormat?
            .BackgroundColor;
        
        if (googleBackgroundColor == null)
            return;
        
        var color = ConvertArgb(googleBackgroundColor);
        cellBuilder.SetFormat(format => format.SetBackgroundColor(color));
    }

    private void CopyContentMargins(CellData googleCellData, ICellBuilder<Cell> cellBuilder)
    {
        var padding = googleCellData
            .EffectiveFormat?
            .Padding;
        
        if (padding == null)
            return;
        
        cellBuilder.SetFormat(format => format
            .SetContentMargins(marginBuilder => marginBuilder
                    .SetContentMargins(padding.Top, padding.Bottom, padding.Left, padding.Right)));
    }

    private void CopyTextFormat(CellData googleCellData, ICellBuilder<Cell> cellBuilder)
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
            
            if (googleTextFormat.ForegroundColor != null)
            {
                var fontColor = ConvertArgb(googleTextFormat.ForegroundColor);
                textFormat.SetTextColor(fontColor);
            }

            textFormat.SetTextSize(googleTextFormat.FontSize);
            textFormat.SetWrapText(googleCellData.EffectiveFormat?.WrapStrategy.Equals("WRAP"));
        }));
    }

    private void CopyAlignment(CellData googleCellData, ICellBuilder<Cell> cellBuilder)
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

        if (verticalAlignment == null)
            return;
        
        switch (verticalAlignment)
        {
            case "TOP":
                cellBuilder
                    .SetFormat(format => format
                        .SetContentVerticalAlignment(CellContentVerticalAlignment.Top));
                break;
            case "MIDDLE":
                cellBuilder
                    .SetFormat(format => format
                        .SetContentVerticalAlignment(CellContentVerticalAlignment.Middle));
                break;
            case "BOTTOM":
                cellBuilder
                    .SetFormat(format => format
                        .SetContentVerticalAlignment(CellContentVerticalAlignment.Bottom));
                break;
        }
    }

    private Color ConvertArgb(Google.Apis.Sheets.v4.Data.Color color)
    {
        var googleAlphaChannel = color.Alpha ?? 1.0;
        var googleRedChannel = color.Red ?? 0.0;
        var googleGreenChannel = color.Green ?? 0.0;
        var googleBlueChannel = color.Blue ?? 0.0;
        
        var alphaChannel = LinearlyInterpolate(googleAlphaChannel, 1.0, 0.0, 0.0, 255.0);
        var redChannel = LinearlyInterpolate(googleRedChannel, 0.0, 1.0, 0.0, 255.0);
        var greenChannel = LinearlyInterpolate(googleGreenChannel, 0.0, 1.0, 0.0, 255.0);
        var blueChannel = LinearlyInterpolate(googleBlueChannel, 0.0, 1.0, 0.0, 255.0);

        return Color.FromArgb(
            System.Convert.ToInt32(alphaChannel),
            System.Convert.ToInt32(redChannel),
            System.Convert.ToInt32(greenChannel),
            System.Convert.ToInt32(blueChannel));
    }

    private double LinearlyInterpolate(double x, double x0, double x1, double y0, double y1)
    {
        if (x1 - x0 == 0)
            return (y0 + y1) / 2;

        return y0 + (x - x0) * (y1 - y0) / (x1 - x0);
    }
}