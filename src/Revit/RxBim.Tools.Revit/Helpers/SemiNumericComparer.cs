﻿namespace RxBim.Tools.Revit.Helpers;

using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

/// <summary>
/// Comparator of numbers in string form.
/// </summary>
/// <remarks>https://stackoverflow.com/a/33330715/8252345</remarks>
public class SemiNumericComparer : IComparer<string>
{
    private readonly string _numericPattern;

    /// <summary>
    /// Initializes a new instance of the <see cref="SemiNumericComparer"/> class.
    /// </summary>
    /// <param name="numericPattern">Regular expression to search for a number in text.</param>
    public SemiNumericComparer(string numericPattern = @"[-+]?\d*\.\d+|\d+")
    {
        _numericPattern = numericPattern;
    }

    /// <inheritdoc/>
    public int Compare(string? s1, string? s2)
    {
        var num1 = Regex.Match(s1!, _numericPattern);
        var num2 = Regex.Match(s2!, _numericPattern);

        if (num1.Success && num2.Success)
        {
            var dNum1 = double.Parse(num1.Value, CultureInfo.InvariantCulture);
            var dNum2 = double.Parse(num2.Value, CultureInfo.InvariantCulture);

            // If two numbers are the same, then check the length
            // of the original text (for case: "000" and "0000").
            if (dNum1.Equals(dNum2))
                return s1!.Length - s2!.Length;

            return dNum1.CompareTo(dNum2);
        }

        if (num1.Success)
            return 1;
        if (num2.Success)
            return -1;

        return string.Compare(
            s1, s2, true, CultureInfo.InvariantCulture);
    }
}