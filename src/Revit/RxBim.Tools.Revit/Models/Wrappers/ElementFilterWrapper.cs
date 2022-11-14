namespace RxBim.Tools.Revit;

using Autodesk.Revit.DB;

/// <summary>
/// Wrapped <see cref="ElementFilter"/>
/// </summary>
public class ElementFilterWrapper : Wrapper<ElementFilter>, IElementFilterWrapper
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ElementFilterWrapper"/> class.
    /// </summary>
    /// <param name="wrappedObject"><see cref="ElementFilter"/></param>
    public ElementFilterWrapper(ElementFilter wrappedObject)
        : base(wrappedObject)
    {
    }
}