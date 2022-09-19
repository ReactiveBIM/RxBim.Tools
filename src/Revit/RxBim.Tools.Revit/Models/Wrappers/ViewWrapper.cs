namespace RxBim.Tools.Revit;

using Autodesk.Revit.DB;

/// <summary>
/// Wrapped <see cref="View"/>
/// </summary>
public class ViewWrapper : ElementWrapper, IViewWrapper
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ViewWrapper"/> class.
    /// </summary>
    /// <param name="wrappedObject"><see cref="View"/></param>
    public ViewWrapper(View wrappedObject)
        : base(wrappedObject)
    {
    }
}