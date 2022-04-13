namespace RxBim.Tools.TableBuilder;

using ClosedXML.Excel;

/// <summary>
/// Defines an interface of a <see cref="Table"/> serializer to an Excle workbook.
/// </summary>
public interface IExcelTableSerializer : ITableSerializer<ExcelTableSerializerParameters, IXLWorkbook>
{
}