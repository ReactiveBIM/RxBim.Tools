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
    /// Wraps <see cref="Element"/>.
    /// </summary>
    /// <param name="element"><see cref="Element"/></param>
    public static IElementWrapper Wrap(this Element element)
        => new ElementWrapper(element);

    /// <summary>
    /// Wraps <see cref="ElementId"/>.
    /// </summary>
    /// <param name="elementId"><see cref="ElementId"/></param>
    public static IObjectIdWrapper Wrap(this ElementId elementId)
        => new ElementIdWrapper(elementId);

    /// <summary>
    /// Wraps <see cref="View"/>
    /// </summary>
    /// <param name="view"><see cref="View"/></param>
    public static IViewWrapper Wrap(this View view)
        => new ViewWrapper(view);
    
    /// <summary>
    /// Wraps <see cref="ViewSheet"/>.
    /// </summary>
    /// <param name="viewSheet"><see cref="ViewSheet"/></param>
    public static IViewSheetWrapper Wrap(this ViewSheet viewSheet)
        => new ViewSheetWrapper(viewSheet);

    /// <summary>
    /// Wraps <see cref="ViewSchedule"/>.
    /// </summary>
    /// <param name="viewSchedule"><see cref="ViewSchedule"/></param>
    public static IViewScheduleWrapper Wrap(this ViewSchedule viewSchedule)
        => new ViewScheduleWrapper(viewSchedule);

    /// <summary>
    /// Wraps <see cref="TableData"/>.
    /// </summary>
    /// <param name="tableData"><see cref="TableData"/></param>
    public static ITableDataWrapper Wrap(this TableData tableData)
        => new TableDataWrapper(tableData);

    /// <summary>
    /// Wraps <see cref="TableSectionData"/>.
    /// </summary>
    /// <param name="tableSectionData"><see cref="TableSectionData"/></param>
    public static ITableSectionDataWrapper Wrap(this TableSectionData tableSectionData)
        => new TableSectionDataWrapper(tableSectionData);

    /// <summary>
    /// Wraps <see cref="ScheduleDefinition"/>.
    /// </summary>
    /// <param name="scheduleDefinition"><see cref="ScheduleDefinition"/></param>
    public static IScheduleDefinitionWrapper Wrap(this ScheduleDefinition scheduleDefinition)
        => new ScheduleDefinitionWrapper(scheduleDefinition);

    /// <summary>
    /// Wraps <see cref="ScheduleField"/>.
    /// </summary>
    /// <param name="scheduleField"><see cref="ScheduleField"/></param>
    public static IScheduleFieldWrapper Wrap(this ScheduleField scheduleField)
        => new ScheduleFieldWrapper(scheduleField);

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