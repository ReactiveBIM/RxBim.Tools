namespace RxBim.Tools.TableBuilder;

/// <summary>
/// Extensions for <see cref="Row"/>
/// </summary>
public static class RowExtensions
{
    /// <summary>
    /// Returns the index of this row in the table.
    /// </summary>
    /// <param name="row">Row object.</param>
    public static int GetIndex(this Row row)
    {
        return row.Table.Rows.IndexOf(row);
    }
}