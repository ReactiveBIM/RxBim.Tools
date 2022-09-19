namespace RxBim.Tools.Revit.TestablePlugin.Sample.Extensions;

using System.Collections.Generic;
using System.Globalization;
using System.Linq;

/// <summary>
/// Extensions for <see cref="string"/>.
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// Returns true if collection of <see cref="string"/> has only text and zero value.
    /// </summary>
    /// <param name="values">Collection of <see cref="string"/>.</param>
    public static bool IsOnlyTextAndZeros(this IEnumerable<string> values)
    {
        var haveZeros = false;
        var haveNumber = false;
        foreach (var s in values)
        {
            if (string.IsNullOrEmpty(s))
                continue;

            var isNumber = double.TryParse(
                s.Replace(',', '.'), NumberStyles.Number, CultureInfo.InvariantCulture, out var value);
            if (!isNumber)
                continue;

            if (value > 0)
                haveNumber = true;
            else
                haveZeros = true;
        }

        return haveZeros && !haveNumber;
    }
    
    /// <summary>
    /// Returns true if collection of <see cref="string"/> has only empty value.
    /// </summary>
    /// <param name="values">Collection of <see cref="string"/>.</param>
    public static bool IsOnlyEmptyStrings(this IEnumerable<string> values)
        => values.All(string.IsNullOrWhiteSpace);
}