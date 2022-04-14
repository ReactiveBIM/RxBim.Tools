namespace RxBim.Tools.TableBuilder;
using AutocadTable = Autodesk.AutoCAD.DatabaseServices.Table;

/// <summary>
/// Defines an interface of a converter that renders a <see cref="Table"/> object in Autocad.
/// </summary>
public interface IAutocadTableConverter
    : ITableConverter<Table, AutocadTableConverterParameters, AutocadTable>
{
}