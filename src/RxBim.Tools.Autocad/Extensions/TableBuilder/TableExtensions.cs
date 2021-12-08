namespace RxBim.Tools.Autocad.Extensions.TableBuilder
{
    using System.Linq;
    using Autodesk.AutoCAD.DatabaseServices;

    /// <summary>
    /// Extensions for <see cref="Table"/>.
    /// </summary>
    internal static class TableExtensions
    {
        /// <summary>
        /// Resets the preset table settings.
        /// </summary>
        /// <param name="acadTable"><see cref="Table"/> object.</param>
        public static void ResetTable(this Table acadTable)
        {
            var lastCellRef = acadTable.Cells.Last();
            var tableCell = acadTable.Cells[lastCellRef.Row, lastCellRef.Column];
            var cellStyleName = tableCell.Style;

            foreach (var cellReference in acadTable.Cells)
            {
                var cell = acadTable.Cells[cellReference.Row, cellReference.Column];
                cell.Style = cellStyleName;
                cell.State = CellStates.None;
                cell.Contents.Clear();
                cell.ResetValue();
            }

            acadTable.UnmergeCells(acadTable.Cells);
            acadTable.FlowDirection = FlowDirection.TopToBottom;

            foreach (var row in acadTable.Rows)
                row.IsMergeAllEnabled = null;

            foreach (var column in acadTable.Columns)
                column.IsMergeAllEnabled = null;
        }
    }
}