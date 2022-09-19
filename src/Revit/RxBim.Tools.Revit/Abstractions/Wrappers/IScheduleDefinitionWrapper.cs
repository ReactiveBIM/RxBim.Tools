namespace RxBim.Tools.Revit;

/// <summary>
/// Wrapper for ScheduleDefinition type.
/// </summary>
public interface IScheduleDefinitionWrapper : IWrapper
{
    /// <summary>
    /// The number of fields in this ScheduleDefinition.
    /// </summary>
    int FieldCount { get; }

    /// <summary>
    /// Gets a <see cref="IScheduleFieldWrapper"/> by index.
    /// </summary>
    /// <param name="index">The index of the field.</param>
    IScheduleFieldWrapper GetField(int index);
}