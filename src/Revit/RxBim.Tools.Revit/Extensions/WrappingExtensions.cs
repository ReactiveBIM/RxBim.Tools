namespace RxBim.Tools.Revit;

using Autodesk.Revit.DB;

/// <summary>
/// Extensions for wrapped objects.
/// </summary>
public static class WrappingExtensions
{
    /// <summary>
    /// Wraps <see cref="Document"/>.
    /// </summary>
    /// <param name="document"><see cref="Document"/></param>
    public static IDocumentWrapper Wrap(this Document document)
        => new DocumentWrapper(document);

    /// <summary>
    /// Gets <see cref="Document"/> from wrapped object.
    /// </summary>
    /// <param name="documentWrapper"><see cref="IDocumentWrapper"/></param>
    /// <remarks>Cast from <see cref="Wrapper{T}"/>.</remarks>
    public static Document UnWrap(this IDocumentWrapper documentWrapper)
        => (documentWrapper as Wrapper<Document>)!.Object;

    /// <summary>
    /// Wraps <see cref="DefinitionFile"/>.
    /// </summary>
    /// <param name="definitionFile"><see cref="DefinitionFile"/></param>
    public static IDefinitionFileWrapper Wrap(this DefinitionFile definitionFile)
        => new DefinitionFileWrapper(definitionFile);

    /// <summary>
    /// Gets <see cref="DefinitionFile"/> from wrapped object.
    /// </summary>
    /// <param name="definitionFileWrapper"><see cref="IDefinitionFileWrapper"/></param>
    /// <remarks>Cast from <see cref="Wrapper{T}"/>.</remarks>
    public static DefinitionFile UnWrap(this IDefinitionFileWrapper definitionFileWrapper)
        => (definitionFileWrapper as Wrapper<DefinitionFile>)!.Object;
}