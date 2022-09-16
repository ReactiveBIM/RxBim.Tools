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
    /// Wraps <see cref="Element"/>
    /// </summary>
    /// <param name="element"><see cref="Element"/></param>
    public static IElementWrapper Wrap(this Element element)
        => new ElementWrapper(element);

    /// <summary>
    /// Wraps <see cref="ViewSheet"/>.
    /// </summary>
    /// <param name="viewSheet"><see cref="ViewSheet"/></param>
    public static IViewSheetWrapper Wrap(this ViewSheet viewSheet)
        => new ViewSheetWrapper(viewSheet);

    /// <summary>
    /// Wraps <see cref="FilteredElementCollector"/>.
    /// </summary>
    /// <param name="filteredElementCollector"><see cref="FilteredElementCollector"/></param>
    public static IFilteredElementCollectorWrapper Wrap(this FilteredElementCollector filteredElementCollector)
        => new FilteredElementCollectorWrapper(filteredElementCollector);

    /// <summary>
    /// Wraps <see cref="ElementFilter"/>.
    /// </summary>
    /// <param name="elementFilter"><see cref="ElementFilter"/></param>
    public static IElementFilterWrapper Wrap(this ElementFilter elementFilter)
        => new ElementFilterWrapper(elementFilter);

    /// <summary>
    /// Wraps <see cref="RevitLinkInstance"/>.
    /// </summary>
    /// <param name="revitLinkInstance"><see cref="RevitLinkInstance"/></param>
    public static IRevitLinkInstanceWrapper Wrap(this RevitLinkInstance revitLinkInstance)
        => new RevitLinkInstanceWrapperWrapper(revitLinkInstance);
}