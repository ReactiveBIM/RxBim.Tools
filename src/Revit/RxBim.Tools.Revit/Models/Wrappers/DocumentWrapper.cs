namespace RxBim.Tools.Revit;

using Autodesk.Revit.DB;

/// <summary>
/// Wrapped <see cref="Document"/>.
/// </summary>
public class DocumentWrapper : Wrapper<Document>, IDocumentWrapper
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DocumentWrapper"/> class.
    /// </summary>
    /// <param name="wrappedObject"><see cref="Document"/></param>
    public DocumentWrapper(Document wrappedObject)
        : base(wrappedObject)
    {
    }

    /// <inheritdoc />
    public string Title
        => Object.Title;
}