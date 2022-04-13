namespace RxBim.Tools.Autocad.Serializers
{
    using System;
    using System.Linq;
    using Abstractions;
    using Autodesk.AutoCAD.Colors;
    using Autodesk.AutoCAD.DatabaseServices;
    using Extensions.TableBuilder;
    using TableBuilder.Models.Contents;
    using TableBuilder.Models.Styles;
    using BuilderCell = TableBuilder.Models.Cell;

    /// <summary>
    /// Defines a class of a serializer that renders a <see cref="Table"/> object in Autocad.
    /// </summary>
    internal class AutocadTableSerializer : IAutocadTableSerializer
    {
        /// <inheritdoc />
        public Table Serialize(
            TableBuilder.Models.Table tableData,
            AutocadTableSerializerParameters parameters)
        {
            var acadTable = new Table();

            var numRows = tableData.Rows.Count();
            var numCols = tableData.Columns.Count();
            acadTable.SetSize(numRows, numCols);

            var database = parameters.TargetDatabase ?? HostApplicationServices.WorkingDatabase;
            acadTable.SetDatabaseDefaults(database);
            acadTable.TableStyle = parameters.TableStyleId.IsNull
                ? database.Tablestyle
                : parameters.TableStyleId;

            acadTable.ResetTable();

            if (!parameters.TextStyleId.IsNull)
                acadTable.Cells.TextStyleId = parameters.TextStyleId;

            for (var columnIndex = 0; columnIndex < numCols; columnIndex++)
            {
                var acadCol = acadTable.Columns[columnIndex];
                var width = tableData.Columns[columnIndex].Width;
                if (width > 0)
                    acadCol.Width = width;

                for (var rowIndex = 0; rowIndex < numRows; rowIndex++)
                {
                    var acadRow = acadTable.Rows[rowIndex];
                    var acadCell = acadTable.Cells[rowIndex, columnIndex];
                    var cellData = tableData[rowIndex, columnIndex];

                    var rowHeight = tableData.Rows[rowIndex].Height;
                    rowHeight = rowHeight > 0 ? rowHeight : parameters.DefaultRowHeight;
                    acadRow.Height = rowHeight;

                    var format = cellData.GetComposedFormat();
                    SetCellStyle(acadCell, format, parameters);

                    switch (cellData.Content)
                    {
                        case AutocadTextCellContent textCellContent:
                            SetAcadText(acadCell, format, textCellContent);
                            break;
                        case BlockCellContent blockData:
                            SetBlock(acadCell, blockData);
                            break;
                        case TextCellContent textData:
                            SetText(acadCell, textData.Value);
                            break;
                        case CellContent<object> valueData:
                            acadCell.Value = valueData.Value;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException($"Undefined cell data type '{cellData.Content}'.");
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

        private void SetAcadText(Cell cell, CellFormatStyle format, AutocadTextCellContent content)
        {
            if (string.IsNullOrEmpty(content.Value))
                return;
            cell.TextString = content.Value;

            cell.Contents.First().Rotation = content.Rotation;

            if (content.AdjustCellSize)
            {
                var (length, height) =
                    content.Value.GetAutocadTextSize(content.Rotation, cell.TextStyleId, cell.TextHeight);

                var minColumnWidthForText = length + format.GetContentHorizontalMargins() * 2 ?? 0;
                var column = cell.ParentTable.Columns[cell.Column];
                if (column.Width < minColumnWidthForText)
                    column.Width = Math.Ceiling(minColumnWidthForText);

                var minRowHeightForText = height + format.GetContentVerticalMargins() * 2 ?? 0;
                var row = cell.ParentTable.Rows[cell.Row];
                if (row.Height < minRowHeightForText)
                    row.Height = Math.Ceiling(minRowHeightForText);
            }
        }

        private void SetText(Cell cell, string text)
        {
            if (string.IsNullOrEmpty(text))
                return;
            cell.TextString = text;
        }

        private void SetBlock(Cell cell, BlockCellContent blockData)
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

        private void SetCellStyle(Cell cell, CellFormatStyle format, AutocadTableSerializerParameters parameters)
        {
            if (format.TextFormat.TextSize > 0)
                cell.TextHeight = format.TextFormat.TextSize;

            var alignment = GetAlignment(format);
            if (alignment != null)
                cell.Alignment = alignment;

            if (format.TextFormat.TextColor.HasValue)
                cell.ContentColor = Color.FromColor(format.TextFormat.TextColor.Value);

            if (format.BackgroundColor.HasValue)
                cell.BackgroundColor = Color.FromColor(format.BackgroundColor.Value);

            SetBorder(cell.Borders.Bottom, format.Borders.Bottom, parameters.ThinLine, parameters.BoldLine);
            SetBorder(cell.Borders.Top, format.Borders.Top, parameters.ThinLine, parameters.BoldLine);
            SetBorder(cell.Borders.Left, format.Borders.Left, parameters.ThinLine, parameters.BoldLine);
            SetBorder(cell.Borders.Right, format.Borders.Right, parameters.ThinLine, parameters.BoldLine);

            var verticalMargins = format.GetContentVerticalMargins();
            cell.Borders.Top.Margin = verticalMargins;
            cell.Borders.Bottom.Margin = verticalMargins;

            cell.Borders.Horizontal.Margin = format.GetContentHorizontalMargins();
        }

        private void SetBorder(
            CellBorder cellBorder,
            CellBorderType? cellBorderType,
            LineWeight thin,
            LineWeight bold)
        {
            if (cellBorderType is null)
                return;

            switch (cellBorderType)
            {
                case CellBorderType.Thin:
                    cellBorder.LineWeight = thin;
                    break;
                case CellBorderType.Hidden:
                    cellBorder.IsVisible = false;
                    break;
                case CellBorderType.Bold:
                    cellBorder.LineWeight = bold;
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