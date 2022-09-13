namespace RxBim.Tools.Revit;

using Autodesk.Revit.DB;

/// <summary>
/// Wrapped <see cref="ViewSheet"/>.
/// </summary>
public class ViewSheetWrapper : ElementWrapper, IViewSheetWrapper
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ViewSheetWrapper"/> class.
    /// </summary>
    /// <param name="wrappedObject"><see cref="ViewSheet"/></param>
    public ViewSheetWrapper(ViewSheet wrappedObject)
        : base(wrappedObject)
    {
    }
}