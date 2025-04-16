namespace RxBim.Tools.TableBuilder;

using Autodesk.Revit.DB;

/// <summary>
/// Defines an interface of a converter that renders a <see cref="Table"/> object as Revit ViewSchedule.
/// </summary>
public interface IViewScheduleTableConverter
    : ITableConverter<Table, ViewScheduleTableConverterParameters, ViewSchedule>
{
}