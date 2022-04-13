namespace RxBim.Tools.TableBuilder;

using ClosedXML.Excel;

/// <summary>
/// Represents an interface of a <see cref="Table"/> deserialazer from an Excel workbook.
/// </summary>
public interface IExcelTableDeserializer : ITableDeserializer<IXLWorksheet>
{
}