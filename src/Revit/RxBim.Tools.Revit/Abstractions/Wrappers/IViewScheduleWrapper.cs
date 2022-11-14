namespace RxBim.Tools.Revit;

/// <summary>
/// Wrapper for ViewSchedule type.
/// </summary>
public interface IViewScheduleWrapper : IViewWrapper
{
    /// <summary>
    /// Settings that define the contents of a schedule.
    /// </summary>
    public IScheduleDefinitionWrapper Definition { get; }
    
    /// <summary>
    /// The schedule writable table data object.
    /// </summary>
    public ITableDataWrapper TableData { get; }
}