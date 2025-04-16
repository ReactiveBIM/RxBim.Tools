namespace RxBim.Command.TableBuilder.Revit.Sample.Abstractions;

using CSharpFunctionalExtensions;

/// <summary>
/// Creator of ViewSchedule in Revit document
/// </summary>
public interface IViewScheduleCreator
{
    /// <summary>
    /// Creates ViewSchedule
    /// </summary>
    /// <param name="name">ViewSchedule name</param>
    /// <param name="rowsCount">Rows count</param>
    /// <param name="columnsCount">Columns count</param>
    Result CreateSomeViewSchedule(string name, int rowsCount, int columnsCount);
}