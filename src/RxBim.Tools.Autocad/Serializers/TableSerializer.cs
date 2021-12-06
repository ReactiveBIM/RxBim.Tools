namespace RxBim.Tools.Autocad.Serializers
{
    using System;
    using System.Linq;
    using Autodesk.AutoCAD.Colors;
    using Autodesk.AutoCAD.DatabaseServices;
    using TableBuilder.Abstractions;
    using TableBuilder.Models.Contents;
    using TableBuilder.Models.Styles;

    /// <inheritdoc />
    public class TableSerializer : ITableSerializer<Table>
    {
        private const string RowStyleTitle = "_TITLE";
        private const string RowStyleHeader = "_HEADER";

        /// <inheritdoc />
        public Table Serialize(TableBuilder.Models.Table tableData, params object[] parameters)
        {
            var serializerParameters = parameters[0] as TableSerializerParameters ??
                                       throw new Exception("Serialization options for the table were not specified.");

            var acadTable = new Table();

            if (serializerParameters.TargetDatabase != null)
                acadTable.SetDatabaseDefaults(serializerParameters.TargetDatabase);

            if (!serializerParameters.TableStyleId.IsNull)
                acadTable.TableStyle = serializerParameters.TableStyleId;

            var rowCount = tableData.Rows.Count();
            acadTable.SetSize(rowCount, tableData.Columns.Count());

            CheckTitleRow(tableData, acadTable, serializerParameters, out var headerRow);
            CheckHeaderRow(acadTable, serializerParameters, headerRow);

            for (var col = 0;
                col < tableData.Columns.Count();
                col++)
            {
                var acadCol = acadTable.Columns[col];
                var width = tableData.Columns.ElementAt(col).Width;
                if (width > 0)
                    acadCol.Width = width;

                for (var row = 0;
                    row < tableData.Rows.Count();
                    row++)
                {
                    var acadRow = acadTable.Rows[row];
                    var acadCell = acadTable.Cells[row, col];
                    var cell = tableData[row, col];

                    var rowHeight = tableData.Rows.ElementAt(row).Height;
                    rowHeight = rowHeight > 0 ? rowHeight : serializerParameters.RowHeightDefault;
                    acadRow.Height = rowHeight;

                    SetCellStyle(acadCell, cell.GetComposedFormat(), serializerParameters);

                    switch (cell.Content)
                    {
                        case TextCellContent textData:
                            SetText(acadCell, textData.Value);
                            break;
                        case CellContent<object> valueData:
                            acadCell.Value = valueData.Value;
                            break;
                        case BlockCellData blockData:
                            SetBlock(acadCell, blockData);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException($"Undefined cell data type '{cell.Content}'.");
                    }
                }
            }

            foreach (var mergeArea in tableData.MergeAreas)
            {
                var mergeRange = CellRange.Create(
                    acadTable,
                    mergeArea.TopRow,
                    mergeArea.LeftColumn,
                    mergeArea.BottomRow,
                    mergeArea.RightColumn);

                acadTable.MergeCells(mergeRange);
            }

            acadTable.GenerateLayout();
            return acadTable;
        }

        private void SetText(Cell cell, string text)
        {
            if (string.IsNullOrEmpty(text))
                return;
            cell.TextString = text;
        }

        private void SetBlock(Cell cell, BlockCellData blockData)
        {
            if (blockData.Value.IsNull)
                return;

            cell.BlockTableRecordId = blockData.Value;

            var blockContent = cell.Contents[0];
            blockContent.IsAutoScale = blockData.AutoScale;

            if (!blockData.AutoScale && blockData.Scale > 0)
                blockContent.Scale = blockData.Scale;

            blockContent.Rotation = blockData.Rotation;
        }

        private void CheckTitleRow(
            TableBuilder.Models.Table tableData,
            Table acadTable,
            TableSerializerParameters parameters,
            out int headerRow)
        {
            var rowTitle = acadTable.Rows[0];
            if (parameters.HasTitle)
            {
                if (rowTitle.Style != RowStyleTitle)
                    rowTitle.Style = RowStyleTitle;

                if (rowTitle.IsMerged == true)
                    acadTable.UnmergeCells(rowTitle);

                headerRow = 1;
                return;
            }

            headerRow = 0;
            if (rowTitle.IsMerged == true || rowTitle.Style == RowStyleTitle)
            {
                var height = tableData.Rows.First().Height;
                if (height == 0)
                    height = parameters.RowHeightDefault;

                acadTable.DeleteRows(0, 1);
                acadTable.InsertRows(acadTable.Rows.Count - 1, height, 1);
            }
        }

        private void CheckHeaderRow(Table acadTable, TableSerializerParameters parameters, int firstHeaderRowIndex)
        {
            if (parameters.RowHeadersCount <= 1)
                return;

            for (var i = 1; i < parameters.RowHeadersCount; i++)
            {
                var row = acadTable.Rows[firstHeaderRowIndex + i];
                row.Style = RowStyleHeader;
            }
        }

        private void SetCellStyle(Cell cell, CellFormatStyle? format, TableSerializerParameters parameters)
        {
            if (format == null)
                return;

            if (format.TextFormat.TextSize > 0)
                cell.TextHeight = format.TextFormat.TextSize;

            var alignment = GetAlignment(format);
            if (alignment != null)
                cell.Alignment = alignment;

            if (format.TextFormat.TextColor.HasValue)
                cell.ContentColor = Color.FromColor(format.TextFormat.TextColor.Value);

            if (format.BackgroundColor.HasValue)
                cell.BackgroundColor = Color.FromColor(format.BackgroundColor.Value);

            SetBorder(cell.Borders.Bottom, format.Borders.Bottom, parameters);
            SetBorder(cell.Borders.Top, format.Borders.Top, parameters);
            SetBorder(cell.Borders.Left, format.Borders.Left, parameters);
            SetBorder(cell.Borders.Right, format.Borders.Right, parameters);
        }

        private void SetBorder(
            CellBorder cellBorder,
            CellBorderType? cellBorderType,
            TableSerializerParameters parameters)
        {
            if (cellBorderType is null)
                return;

            switch (cellBorderType)
            {
                case CellBorderType.Thin:
                    cellBorder.LineWeight = parameters.ThinLine;
                    break;
                case CellBorderType.Hidden:
                    cellBorder.IsVisible = false;
                    break;
                case CellBorderType.Bold:
                    cellBorder.LineWeight = parameters.BoldLine;
                    break;
                default:
                    throw new Exception($"Line {cellBorderType} is not implemented in the serializer.");
            }
        }

        private CellAlignment? GetAlignment(CellFormatStyle format)
        {
            return format switch
            {
                _ when format.ContentHorizontalAlignment == CellContentHorizontalAlignment.Center &&
                       format.ContentVerticalAlignment == CellContentVerticalAlignment.Bottom =>
                    CellAlignment.BottomCenter,
                _ when format.ContentHorizontalAlignment == CellContentHorizontalAlignment.Center &&
                       format.ContentVerticalAlignment == CellContentVerticalAlignment.Middle =>
                    CellAlignment.MiddleCenter,
                _ when format.ContentHorizontalAlignment == CellContentHorizontalAlignment.Center &&
                       format.ContentVerticalAlignment == CellContentVerticalAlignment.Top => CellAlignment.TopCenter,

                _ when format.ContentHorizontalAlignment == CellContentHorizontalAlignment.Left &&
                       format.ContentVerticalAlignment == CellContentVerticalAlignment.Bottom =>
                    CellAlignment.BottomLeft,
                _ when format.ContentHorizontalAlignment == CellContentHorizontalAlignment.Left &&
                       format.ContentVerticalAlignment == CellContentVerticalAlignment.Middle =>
                    CellAlignment.MiddleLeft,
                _ when format.ContentHorizontalAlignment == CellContentHorizontalAlignment.Left &&
                       format.ContentVerticalAlignment == CellContentVerticalAlignment.Top => CellAlignment.TopLeft,

                _ when format.ContentHorizontalAlignment == CellContentHorizontalAlignment.Right &&
                       format.ContentVerticalAlignment == CellContentVerticalAlignment.Bottom =>
                    CellAlignment.BottomRight,
                _ when format.ContentHorizontalAlignment == CellContentHorizontalAlignment.Right &&
                       format.ContentVerticalAlignment == CellContentVerticalAlignment.Middle =>
                    CellAlignment.MiddleRight,
                _ when format.ContentHorizontalAlignment == CellContentHorizontalAlignment.Right &&
                       format.ContentVerticalAlignment == CellContentVerticalAlignment.Top => CellAlignment.TopRight,

                _ => null
            };
        }
    }
}