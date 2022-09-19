namespace RxBim.Tools.Revit;

using Autodesk.Revit.DB;

/// <summary>
/// Wrapped <see cref="ElementId"/>.
/// </summary>
public class ElementIdWrapper : Wrapper<ElementId>, IObjectIdWrapper
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ElementIdWrapper"/> class.
    /// </summary>
    /// <param name="wrappedObject"><see cref="ElementId"/></param>
    public ElementIdWrapper(ElementId wrappedObject)
        : base(wrappedObject)
    {
    }
}