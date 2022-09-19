namespace RxBim.Tools.Revit;

using Autodesk.Revit.DB;
using JetBrains.Annotations;

/// <summary>
/// Wrapped <see cref="ViewSchedule"/>.
/// </summary>
public class ViewScheduleWrapper
    : ElementWrapper, IViewScheduleWrapper
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ViewScheduleWrapper"/> class.
    /// </summary>
    /// <param name="wrappedObject"><see cref="ViewSchedule"/></param>
    public ViewScheduleWrapper(ViewSchedule wrappedObject)
        : base(wrappedObject)
    {
    }
}