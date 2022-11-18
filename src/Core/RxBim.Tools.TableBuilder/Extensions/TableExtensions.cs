namespace RxBim.Tools.TableBuilder;

using System.Linq;

/// <summary>
/// Extensions for <see cref="Table"/>
/// </summary>
public static class TableExtensions
{
    /// <summary>
    /// Returns average row height.
    /// </summary>
    /// <param name="table"><see cref="Table"/></param>
    public static double GetAverageRowHeight(this Table table)
    {
        var rowsWithValues =
            table.Rows
                .Where(x => x.Height.HasValue)
                .Select(x => x.Height!.Value)
                .ToList();
        return (table.Height - rowsWithValues.Sum()) / (table.Rows.Count - rowsWithValues.Count);
    }
    
    /// <summary>
    /// Returns average column width.
    /// </summary>
    /// <param name="table"><see cref="Table"/></param>
    public static double GetAverageColumnWidth(this Table table)
    {
        var columnWithValues =
            table.Columns
                .Where(x => x.Width.HasValue)
                .Select(x => x.Width!.Value)
                .ToList();
        return (table.Width - columnWithValues.Sum()) / (table.Columns.Count - columnWithValues.Count);
    }
}
