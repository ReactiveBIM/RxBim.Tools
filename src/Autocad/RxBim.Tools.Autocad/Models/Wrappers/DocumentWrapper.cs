namespace RxBim.Tools.Autocad;

using Autodesk.AutoCAD.ApplicationServices;

/// <inheritdoc cref="RxBim.Tools.Autocad.IDocumentWrapper" />
public class DocumentWrapper(Document contextObject)
    : Wrapper<Document>(contextObject), IDocumentWrapper;