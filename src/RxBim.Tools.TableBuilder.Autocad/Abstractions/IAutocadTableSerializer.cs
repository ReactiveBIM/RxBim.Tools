namespace RxBim.Tools.TableBuilder;

/// <summary>
/// Defines an interface of a serializer that renders a <see cref="Table"/> object in Autocad.
/// </summary>
public interface IAutocadTableSerializer : ITableSerializer<AutocadTableSerializerParameters, Autodesk.AutoCAD.DatabaseServices.Table>
{
}