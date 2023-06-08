namespace RxBim.Tools.Revit.Extensions;

using Abstractions;
using Autodesk.Revit.DB;
using JetBrains.Annotations;

/// <summary>
/// DocumentWrapper extensions.
/// </summary>
[PublicAPI]
public static class DocumentWrapperExtensions
{
    /// <summary>
    /// Returns the path to the document.
    /// </summary>
    /// <param name="doc">Document.</param>
    public static string GetProjectPath(this IDocumentWrapper doc)
    {
        return doc.Unwrap<Document>().GetProjectPath();
    }
}