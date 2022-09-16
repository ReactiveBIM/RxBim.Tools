namespace RxBim.Tools.Revit;

/// <summary>
/// Scope for filtered elements.
/// </summary>
public enum FilteredElementScope
{
    /// <summary>
    /// All elements in model.
    /// </summary>
    AllModel = 0,

    /// <summary>
    /// Only elements from active view.
    /// </summary>
    ActiveView = 1,

    /// <summary>
    /// Only selected elements.
    /// </summary>
    SelectedElements = 2
}