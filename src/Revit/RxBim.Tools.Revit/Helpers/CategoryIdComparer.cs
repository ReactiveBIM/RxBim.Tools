namespace RxBim.Tools.Revit.Helpers;

using System.Collections.Generic;
using Autodesk.Revit.DB;
using Extensions;

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
        return x is not null && y is not null && x.Id.GetIdValue().Equals(y.Id.GetIdValue());
    }

    /// <inheritdoc />
    public int GetHashCode(Category? obj) => obj?.Id.GetIdValue().GetHashCode() ?? 0;
}
