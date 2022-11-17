namespace RxBim.Tools.Revit.Helpers;

using System.Collections.Generic;
using Autodesk.Revit.DB;

/// <summary>
/// Comparer for <see cref="Category"/>.
/// </summary>
public class CategoryIdComparer : IEqualityComparer<Category?>
{
    /// <inheritdoc />
    public bool Equals(Category? x, Category? y)
    {
        if (x == y)
            return true;
        return x is not null && y is not null && x.Id.IntegerValue.Equals(y.Id.IntegerValue);
    }

    /// <inheritdoc />
    public int GetHashCode(Category? obj) => obj?.Id.IntegerValue.GetHashCode() ?? 0;
}
