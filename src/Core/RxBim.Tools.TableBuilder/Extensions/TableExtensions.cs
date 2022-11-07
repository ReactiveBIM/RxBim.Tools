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
    /// <returns></returns>
    public static double GetAverageRowHeight(this Table table)
    {
        var rowsWithValues =
            table.Rows
                .Where(x => x.Height.HasValue)
                .Select(x => x.Height!.Value)
                .ToList();
        return (table.Height - rowsWithValues.Sum()) / (table.Rows.Count() - rowsWithValues.Count);
    }
}
