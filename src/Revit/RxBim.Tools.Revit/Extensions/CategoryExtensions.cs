namespace RxBim.Tools.Revit;

using System.Collections.Generic;
using Autodesk.Revit.DB;

/// <summary>
/// Extensions for <see cref="Category"/>.
/// </summary>
public static class CategoryExtensions
{
    /// <summary>
    /// Convert collection of <see cref="Category"/> to <see cref="CategorySet"/>.
    /// </summary>
    /// <param name="categories">Collection of <see cref="Category"/>.</param>
    public static CategorySet ToCategorySet(this IEnumerable<Category>? categories)
    {
        var categorySet = new CategorySet();
        if (categories == null)
            return categorySet;
            
        foreach (var category in categories)
        {
            if (category is { AllowsBoundParameters: true })
                categorySet.Insert(category);
        }

        return categorySet;
    }
}