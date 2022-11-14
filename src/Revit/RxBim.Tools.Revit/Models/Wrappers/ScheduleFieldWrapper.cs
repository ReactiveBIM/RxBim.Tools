namespace RxBim.Tools.Revit;

using Autodesk.Revit.DB;

/// <summary>
/// Wrapped <see cref="ScheduleField"/>
/// </summary>
public class ScheduleFieldWrapper
    : Wrapper<ScheduleField>, IScheduleFieldWrapper
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ScheduleFieldWrapper"/> class.
    /// </summary>
    /// <param name="wrappedObject"><see cref="ScheduleField"/></param>
    public ScheduleFieldWrapper(ScheduleField wrappedObject)
        : base(wrappedObject)
    {
    }

    /// <inheritdoc />
    public string Name
        => Object.GetName();

    /// <inheritdoc />
    public bool IsHidden
    {
        get => Object.IsHidden;
        set => Object.IsHidden = value;
    }
}