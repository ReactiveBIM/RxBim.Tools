namespace RxBim.Tools.Autocad.Serializers
{
    using System;
    using System.Collections.Generic;
    using Autodesk.AutoCAD.Colors;
    using Autodesk.AutoCAD.DatabaseServices;
    using TableBuilder;
    using TableBuilder.Abstractions;
    using Cell = Autodesk.AutoCAD.DatabaseServices.Cell;
    using Table = Autodesk.AutoCAD.DatabaseServices.Table;

    /// <inheritdoc />
    public class TableSerializer : ITableSerializer<Table>
    {
        private const string RowStyleTitle = "_TITLE";
        private const string RowStyleHeader = "_HEADER";

        /// <inheritdoc />
        public Table Serialize(Tools.TableBuilder.Table tableData, ITableSerializerParameters @params)
        {
            var parameters = @params as TableSerializerParameters ??
                             throw new Exception("Не заданы параметры сериализации таблицы.");

            var acadTable = new Table();

            if (parameters.Db != null)
                acadTable.SetDatabaseDefaults(parameters.Db);

            if (!parameters.TableStyleId.IsNull)
                acadTable.TableStyle = parameters.TableStyleId;

            var rowCount = tableData.Rows.Count;
            acadTable.SetSize(rowCount, tableData.Columns.Count);

            CheckTitleRow(tableData, acadTable, parameters, out var headerRow);
            CheckHeaderRow(acadTable, parameters, headerRow);

            var mergedCells = new List<TableMergedArea>();

            for (var col = 0;
                col < tableData.Columns.Count;
                col++)
            {
                var acadCol = acadTable.Columns[col];
                var width = tableData[0, col].Width;
                if (width > 0)
                    acadCol.Width = width;

                for (var row = 0;
                    row < tableData.Rows.Count;
                    row++)
                {
                    var acadRow = acadTable.Rows[row];
                    var acadCell = acadTable.Cells[row, col];
                    var cell = tableData[row, col];

                    var rowHeight = tableData.Rows[row].Height;
                    rowHeight = rowHeight > 0 ? rowHeight : parameters.RowHeightDefault;
                    acadRow.Height = rowHeight;

                    // Merge logic
                    if (cell.Merged && !mergedCells.Exists(x => Equals(x, cell.Area)))
                    {
                        mergedCells.Add(cell.Area);

                        var mergeRange = CellRange.Create(
                            acadTable,
                            cell.Area.TopRow,
                            cell.Area.LeftColumn,
                            cell.Area.BottomRow,
                            cell.Area.RightColumn);

                        acadTable.MergeCells(mergeRange);
                    }

                    SetCellStyle(acadCell, cell.Format, parameters);

                    switch (cell.Data)
                    {
                        case TextCellData textData:
                            SetText(acadCell, textData.Value);
                            break;
                        case ValueCellData valueData:
                            acadCell.Value = valueData.Value;
                            break;
                        case BlockCellData blockData:
                            SetBlock(acadCell, blockData);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException($"Неопределенный тип данных ячейки '{cell.Data}'.");
                    }
                }
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
            if (blockData.BtrId.IsNull)
                return;

            cell.BlockTableRecordId = blockData.BtrId;

            var blockContent = cell.Contents[0];
            blockContent.IsAutoScale = blockData.AutoScale;

            if (!blockData.AutoScale && blockData.Scale > 0)
                blockContent.Scale = blockData.Scale;

            blockContent.Rotation = blockData.Rotation;
        }

        private void CheckTitleRow(
            TableBuilder.Table tableData,
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
                var height = tableData.Rows[0].Height;
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

            if (format.TextSize > 0)
                cell.TextHeight = format.TextSize;

            var alignment = GetAlignment(format);
            if (alignment != null)
                cell.Alignment = alignment;

            if (!format.TextColor.IsEmpty)
                cell.ContentColor = Color.FromColor(format.TextColor);

            if (!format.BackgroundColor.IsEmpty)
                cell.BackgroundColor = Color.FromColor(format.BackgroundColor);

            if (format.Borders != null)
            {
                SetBorder(cell.Borders.Bottom, format.Borders.Bottom, parameters);
                SetBorder(cell.Borders.Top, format.Borders.Top, parameters);
                SetBorder(cell.Borders.Left, format.Borders.Left, parameters);
                SetBorder(cell.Borders.Right, format.Borders.Right, parameters);
            }
        }

        private void SetBorder(
            CellBorder cellBorder,
            CellBorderType borderType,
            TableSerializerParameters parameters)
        {
            switch (borderType)
            {
                case CellBorderType.Usual:
                    cellBorder.LineWeight = parameters.LineUsual;
                    break;
                case CellBorderType.Hidden:
                    cellBorder.IsVisible = false;
                    break;
                case CellBorderType.Bold:
                    cellBorder.LineWeight = parameters.LineBold;
                    break;
                default:
                    throw new Exception($"Линия {borderType} не реализована в сериализаторе");
            }
        }

        private CellAlignment? GetAlignment(CellFormatStyle format)
        {
            return format switch
            {
                _ when format.TextHorizontalAlignment == TextHorizontalAlignment.Center &&
                       format.TextVerticalAlignment == TextVerticalAlignment.Bottom => CellAlignment.BottomCenter,
                _ when format.TextHorizontalAlignment == TextHorizontalAlignment.Center &&
                       format.TextVerticalAlignment == TextVerticalAlignment.Middle => CellAlignment.MiddleCenter,
                _ when format.TextHorizontalAlignment == TextHorizontalAlignment.Center &&
                       format.TextVerticalAlignment == TextVerticalAlignment.Top => CellAlignment.TopCenter,

                _ when format.TextHorizontalAlignment == TextHorizontalAlignment.Left &&
                       format.TextVerticalAlignment == TextVerticalAlignment.Bottom => CellAlignment.BottomLeft,
                _ when format.TextHorizontalAlignment == TextHorizontalAlignment.Left &&
                       format.TextVerticalAlignment == TextVerticalAlignment.Middle => CellAlignment.MiddleLeft,
                _ when format.TextHorizontalAlignment == TextHorizontalAlignment.Left &&
                       format.TextVerticalAlignment == TextVerticalAlignment.Top => CellAlignment.TopLeft,

                _ when format.TextHorizontalAlignment == TextHorizontalAlignment.Right &&
                       format.TextVerticalAlignment == TextVerticalAlignment.Bottom => CellAlignment.BottomRight,
                _ when format.TextHorizontalAlignment == TextHorizontalAlignment.Right &&
                       format.TextVerticalAlignment == TextVerticalAlignment.Middle => CellAlignment.MiddleRight,
                _ when format.TextHorizontalAlignment == TextHorizontalAlignment.Right &&
                       format.TextVerticalAlignment == TextVerticalAlignment.Top => CellAlignment.TopRight,

                _ => null
            };
        }
    }
}