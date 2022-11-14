namespace RxBim.Tools.Revit;

/// <summary>
/// Wrapper for ScheduleField type. 
/// </summary>
public interface IScheduleFieldWrapper : IWrapper
{
    /// <summary>
    /// Field name.
    /// </summary>
    string Name { get; }
    
    /// <summary>
    /// Is field hidden.
    /// </summary>
    bool IsHidden { get; set; }
}