namespace RxBim.Tools.TableBuilder;

using ClosedXML.Excel;

/// <summary>
/// Represents an interface of a converter from an Excel workbook to a  <see cref="Table"/> object.
/// </summary>
public interface IFromExcelTableConverter 
    : ITableConverter<IXLWorkbook, FromExcelConverterParameters, Table>
{
}