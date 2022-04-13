namespace RxBim.Tools.TableBuilder
{
    using Autodesk.Revit.DB;

    /// <summary>
    /// Defines an interface of a serializer that renders a <see cref="Table"/> object as Revit ViewSchedule.
    /// </summary>
    public interface IViewScheduleTableSerializer
        : ITableSerializer<ViewScheduleTableSerializerParameters, ViewSchedule>
    {
    }
}