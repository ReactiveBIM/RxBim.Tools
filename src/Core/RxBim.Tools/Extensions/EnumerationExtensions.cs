namespace RxBim.Tools;

using System;
using System.ComponentModel;
using System.Linq;
using JetBrains.Annotations;

/// <summary>
/// Extensions for enumerations.
/// </summary>
[PublicAPI]
public static class EnumerationExtensions
{
    /// <summary>
    /// Returns a description for the enum value.
    /// </summary>
    /// <param name="value">Enum value.</param>
    /// <typeparam name="T">Type of enum.</typeparam>
    public static string GetEnumDescription<T>(this T value)
        where T : Enum
    {
        var fi = value.GetType().GetField(value.ToString());

        return fi.GetCustomAttributes(typeof(DescriptionAttribute), false) is DescriptionAttribute[] attributes &&
               attributes.Any()
            ? attributes.First().Description
            : value.ToString();
    }
}