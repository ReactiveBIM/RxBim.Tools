namespace RxBim.Tools.TableBuilder;

using System;
using ClosedXML.Excel;

/// <summary>
/// Расширения для ячейки <see cref="IXLCell"/>.
/// </summary>
public static class XlCellExtensions
{
    /// <summary>
    /// Возвращает значение ячейки как объект.
    /// </summary>
    /// <param name="cell">Ячейка.</param>
    public static object GetValueAsObject(this IXLCell cell) => cell.DataType switch
    {
        XLDataType.Number => cell.GetValue<double>(),
        XLDataType.Text => cell.GetValue<string>(),
        XLDataType.Boolean => cell.GetValue<bool>(),
        XLDataType.DateTime => cell.GetValue<DateTime>(),
        XLDataType.TimeSpan => cell.GetValue<TimeSpan>(),
        XLDataType.Error => cell.GetValue<XLError>(),
        XLDataType.Blank => string.Empty,
        _ => cell.Value
    };
}