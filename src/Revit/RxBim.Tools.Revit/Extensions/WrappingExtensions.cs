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
    /// Wraps <see cref="DefinitionFile"/>.
    /// </summary>
    /// <param name="definitionFile"><see cref="DefinitionFile"/></param>
    public static IDefinitionFileWrapper Wrap(this DefinitionFile definitionFile)
        => new DefinitionFileWrapper(definitionFile);

    /// <summary>
    /// Wraps <see cref="ViewSheet"/>.
    /// </summary>
    /// <param name="viewSheet"><see cref="ViewSheet"/></param>
    public static IViewSheetWrapper Wrap(this ViewSheet viewSheet)
        => new ViewSheetWrapper(viewSheet);
}