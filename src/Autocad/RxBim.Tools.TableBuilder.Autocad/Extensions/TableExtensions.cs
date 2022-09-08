namespace RxBim.Tools.TableBuilder
{
    using System.Linq;
    using Autodesk.AutoCAD.DatabaseServices;

    /// <summary>
    /// Extensions for <see cref="Autodesk.AutoCAD.DatabaseServices.Table"/>.
    /// </summary>
    internal static class TableExtensions
    {
        /// <summary>
        /// Resets the preset table settings.
        /// </summary>
        /// <param name="acadTable"><see cref="Autodesk.AutoCAD.DatabaseServices.Table"/> object.</param>
        public static void ResetTable(this Autodesk.AutoCAD.DatabaseServices.Table acadTable)
        {
            var acadTableCells = acadTable.Cells;
            var lastCellRef = acadTableCells.Last();
            var lastCell = acadTableCells[lastCellRef.Row, lastCellRef.Column];
            acadTableCells.Style = lastCell.Style;
            acadTableCells.State = CellStates.None;
            acadTable.UnmergeCells(acadTableCells);

            var lastRow = acadTable.Rows[lastCellRef.Row];

            foreach (var row in acadTable.Rows)
            {
                row.IsMergeAllEnabled = false;
                row.Style = lastRow.Style;
            }

            foreach (var column in acadTable.Columns)
                column.IsMergeAllEnabled = false;
        }
    }
}