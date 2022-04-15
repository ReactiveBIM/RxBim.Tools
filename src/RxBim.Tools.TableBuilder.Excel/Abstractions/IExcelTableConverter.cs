namespace RxBim.Tools.TableBuilder;

using ClosedXML.Excel;

/// <summary>
/// Defines an interface of a <see cref="Table"/> converter to an Excle workbook.
/// </summary>
public interface IExcelTableConverter
    : ITableConverter<Table, ExcelTableConverterParameters, IXLWorkbook>
{
}