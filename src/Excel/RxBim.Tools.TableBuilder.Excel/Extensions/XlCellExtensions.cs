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

    /// <summary>
    /// Возвращает значение ячейки.
    /// </summary>
    /// <param name="value">Значение.</param>
    public static XLCellValue FromObject(this object? value) => value switch
    {
        byte b => b,
        sbyte sb => sb,
        short s => s,
        ushort us => us,
        int i => i,
        uint ui => ui,
        long l => l,
        ulong ul => ul,
        float f => f,
        double d => d,
        decimal m => m,
        bool bl => bl,
        DateTime dt => dt,
        TimeSpan ts => ts,
        string s => s,
        _ => Blank.Value
    };
}