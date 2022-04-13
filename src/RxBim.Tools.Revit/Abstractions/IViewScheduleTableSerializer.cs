namespace RxBim.Tools.Revit.Abstractions;

using Autodesk.Revit.DB;
using Serializers;
using TableBuilder.Abstractions;
using TableBuilder.Models;

/// <summary>
/// Defines an interface of a serializer that renders a <see cref="Table"/> object as Revit ViewSchedule.
/// </summary>
public interface
    IViewScheduleTableSerializer : ITableSerializer<ViewScheduleTableSerializerParameters, ViewSchedule>
{
}