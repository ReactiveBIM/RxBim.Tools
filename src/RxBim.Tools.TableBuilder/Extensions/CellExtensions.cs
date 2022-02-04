namespace RxBim.Tools.TableBuilder.Extensions
{
    using Models;

    /// <summary>
    /// Extensions for <see cref="Cell"/>
    /// </summary>
    public static class CellExtensions
    {
        /// <summary>
        /// Returns the zero-based index of a cell in a table row.
        /// </summary>
        /// <param name="cell"><see cref="Cell"/> object.</param>
        /// <returns></returns>
        public static int GetColumnIndex(this Cell cell)
        {
            return cell.Column.GetIndex();
        }

        /// <summary>
        /// Returns the zero-based index of a cell in a table column.
        /// </summary>
        /// <param name="cell"><see cref="Cell"/> object.</param>
        /// <returns></returns>
        public static int GetRowIndex(this Cell cell)
        {
            return cell.Row.GetIndex();
        }
    }
}