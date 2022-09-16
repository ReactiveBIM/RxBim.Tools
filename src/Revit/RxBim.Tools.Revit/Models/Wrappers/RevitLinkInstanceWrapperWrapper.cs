namespace RxBim.Tools.Revit;

using Autodesk.Revit.DB;

/// <summary>
/// Wrapped <see cref="RevitLinkInstance"/>.
/// </summary>
public class RevitLinkInstanceWrapperWrapper : ElementWrapper, IRevitLinkInstanceWrapper
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RevitLinkInstanceWrapperWrapper"/> class.
    /// </summary>
    /// <param name="wrappedObject"><see cref="RevitLinkInstance"/></param>
    public RevitLinkInstanceWrapperWrapper(RevitLinkInstance wrappedObject)
        : base(wrappedObject)
    {
    }
}