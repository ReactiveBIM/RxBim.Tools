namespace RxBim.Tools.Revit;

using Autodesk.Revit.DB;

/// <summary>
/// Wrapped <see cref="ScheduleDefinition"/>
/// </summary>
public class ScheduleDefinitionWrapper
    : Wrapper<ScheduleDefinition>, IScheduleDefinitionWrapper
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ScheduleDefinitionWrapper"/> class.
    /// </summary>
    /// <param name="wrappedObject"><see cref="ScheduleDefinition"/></param>
    public ScheduleDefinitionWrapper(ScheduleDefinition wrappedObject)
        : base(wrappedObject)
    {
    }

    /// <inheritdoc />
    public int FieldCount
        => Object.GetFieldCount();

    /// <inheritdoc />
    public IScheduleFieldWrapper GetField(int index)
        => Object.GetField(index).Wrap();
}