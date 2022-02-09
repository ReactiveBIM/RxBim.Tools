namespace RxBim.Tools.TableBuilder.Extensions
{
    using Models;

    /// <summary>
    /// Extensions for <see cref="Column"/>
    /// </summary>
    public static class ColumnExtensions
    {
        /// <summary>
        /// Returns the index of this column in the table.
        /// </summary>
        /// <param name="column">Column object.</param>
        public static int GetIndex(this Column column)
        {
            return column.Table.Columns.IndexOf(column);
        }
    }
}