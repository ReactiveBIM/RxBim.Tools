namespace RxBim.Tools.Autocad;

using Autodesk.AutoCAD.ApplicationServices;

/// <summary>
/// AutoCAD documents service
/// </summary>
public interface IDocumentService
{
    /// <summary>
    /// Gets the current document
    /// </summary>
    Document GetActiveDocument();
}