namespace RxBim.Tools.Common.Extensions
{
    using System;

    /// <summary>
    /// Extensions for <see cref="string"/>.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Returns a value indicating whether a specified character occurs within this string, using the specified comparison rules.
        /// </summary>
        /// <param name="value"><see cref="string"/> object.</param>
        /// <param name="part">The string to seek.</param>
        /// <param name="comparison">One of the enumeration values that specifies the rules for the search.</param>
        public static bool ContainsWithComparison(this string value, string part, StringComparison comparison)
        {
            return value.IndexOf(part, comparison) >= 0;
        }
    }
}