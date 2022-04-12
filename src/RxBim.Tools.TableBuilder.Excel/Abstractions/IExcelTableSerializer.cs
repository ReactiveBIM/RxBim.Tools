namespace RxBim.Tools.TableBuilder.Abstractions;

using ClosedXML.Excel;
using Models;

/// <summary>
/// Defines an interface of a <see cref="Table"/> serializer to an Excle workbook.
/// </summary>
public interface IExcelTableSerializer : ITableSerializer<ExcelTableSerializerParameters, IXLWorkbook>
{
}