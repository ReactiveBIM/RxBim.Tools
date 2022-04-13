namespace RxBim.Tools.Autocad.Abstractions;

using Autodesk.AutoCAD.DatabaseServices;
using Serializers;
using TableBuilder.Abstractions;

/// <summary>
/// Defines an interface of a serializer that renders a <see cref="Table"/> object in Autocad.
/// </summary>
public interface IAutocadTableSerializer : ITableSerializer<AutocadTableSerializerParameters, Table>
{
}