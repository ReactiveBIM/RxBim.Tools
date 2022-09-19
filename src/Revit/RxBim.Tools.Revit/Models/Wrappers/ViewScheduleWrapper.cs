namespace RxBim.Tools.Revit;

using Autodesk.Revit.DB;

/// <summary>
/// Wrapped <see cref="ViewSchedule"/>.
/// </summary>
public class ViewScheduleWrapper
    : ViewWrapper, IViewScheduleWrapper
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ViewScheduleWrapper"/> class.
    /// </summary>
    /// <param name="wrappedObject"><see cref="ViewSchedule"/></param>
    public ViewScheduleWrapper(ViewSchedule wrappedObject)
        : base(wrappedObject)
    {
    }

    /// <inheritdoc />
    public IScheduleDefinitionWrapper Definition
        => ((ViewSchedule)Object).Definition.Wrap();

    /// <inheritdoc />
    public ITableDataWrapper TableData
        => ((ViewSchedule)Object).GetTableData().Wrap();
}