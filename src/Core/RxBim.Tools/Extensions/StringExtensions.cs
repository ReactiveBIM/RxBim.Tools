namespace RxBim.Tools;

using System;
using JetBrains.Annotations;

/// <summary>
/// Extensions for <see cref="string"/>.
/// </summary>
[PublicAPI]
public static class StringExtensions
{
    /// <summary>
    /// Returns a value indicating whether a specified character occurs within this string, using the specified comparison rules.
    /// </summary>
    /// <param name="value"><see cref="string"/> object.</param>
    /// <param name="part">The string to seek.</param>
    /// <param name="comparison">One of the enumeration values that specifies the rules for the search.</param>
    public static bool Contains(this string value, string part, StringComparison comparison)
    {
        return value.IndexOf(part, comparison) >= 0;
    }
}