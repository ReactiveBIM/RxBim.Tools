namespace RxBim.Tools.TableBuilder.Abstractions;

using ClosedXML.Excel;
using Models;

/// <summary>
/// Represents an interface of a <see cref="Table"/> deserialazer from an Excel workbook.
/// </summary>
public interface IExcelTableDeserializer : ITableDeserializer<IXLWorksheet>
{
}