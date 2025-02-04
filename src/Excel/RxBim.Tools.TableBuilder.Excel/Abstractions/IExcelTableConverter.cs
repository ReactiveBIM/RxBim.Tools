namespace RxBim.Tools.TableBuilder;

using ClosedXML.Excel;
using JetBrains.Annotations;

/// <summary>
/// Defines an interface of a <see cref="Table"/> converter to an Excel workbook.
/// </summary>
[PublicAPI]
public interface IExcelTableConverter
    : ITableConverter<Table, ExcelTableConverterParameters, IXLWorkbook>
{
    /// <summary>
    /// Returns the width in pixels.
    /// </summary>
    /// <param name="width">Width in Excel points.</param>
    double ConvertWidthToPixels(double width);

    /// <summary>
    /// Returns the height in pixels.
    /// </summary>
    /// <param name="height">Height in Excel points.</param>
    double ConvertHeightToPixels(double height);
}