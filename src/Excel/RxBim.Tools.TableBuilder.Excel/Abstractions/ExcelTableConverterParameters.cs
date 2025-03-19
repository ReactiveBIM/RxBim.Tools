namespace RxBim.Tools.TableBuilder;

using ClosedXML.Excel;

/// <summary>
/// Excel converter parameters
/// </summary>
public class ExcelTableConverterParameters
{
    /// <summary>
    /// Excel document
    /// </summary>
    public IXLWorkbook? Workbook { get; set; }

    /// <summary>
    /// Worksheet name
    /// </summary>
    public string? WorksheetName { get; set; }

    /// <summary>
    /// Number of freeze rows
    /// </summary>
    public int FreezeRows { get; set; } = 0;

    /// <summary>
    /// Adding an AutoFilter to a Range
    /// </summary>
    public (int FromRow, int FromColumn, int ToRow, int ToColumn) AutoFilterRange { get; set; }
}