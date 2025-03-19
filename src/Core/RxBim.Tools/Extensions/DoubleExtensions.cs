namespace RxBim.Tools;

using System;
using JetBrains.Annotations;

/// <summary>
/// Extensions for <see cref="double"/>.
/// </summary>
[PublicAPI]
public static class DoubleExtensions
{
    /// <summary>
    /// Double precision by default.
    /// </summary>
    public const double DefaultPrecision = 1e-6;

    private const int DefaultDigits = 4;

    private const double DegreesInRadian = 180D / Math.PI;

    /// <summary>
    /// Returns true if a number is equal to another number with the given precision.
    /// </summary>
    /// <param name="value">Value</param>
    /// <param name="otherValue">Another value</param>
    /// <param name="precision">Comparison accuracy</param>
    public static bool IsEqualTo(this double value, double otherValue, double precision = DefaultPrecision)
    {
        return Math.Abs(value - otherValue) < precision;
    }

    /// <summary>
    /// Returns true if a number is less than or equal to another number with the given precision.
    /// </summary>
    /// <param name="value">Value</param>
    /// <param name="otherValue">Another value</param>
    /// <param name="precision">Comparison accuracy</param>
    public static bool IsEqualOrLess(this double value, double otherValue, double precision = DefaultPrecision)
    {
        return value.IsEqualTo(otherValue, precision) || value < otherValue;
    }

    /// <summary>
    /// Returns true if a number is greater than or equal to another number with the given precision.
    /// </summary>
    /// <param name="value">Value</param>
    /// <param name="otherValue">Another value</param>
    /// <param name="precision">Comparison accuracy</param>
    public static bool IsEqualOrGreater(this double value, double otherValue, double precision = DefaultPrecision)
    {
        return value.IsEqualTo(otherValue, precision) || value > otherValue;
    }

    /// <summary>
    /// Returns true if the number is zero with the given precision.
    /// </summary>
    /// <param name="value">Value</param>
    /// <param name="precision">Comparison accuracy</param>
    public static bool IsZero(this double value, double precision = DefaultPrecision)
    {
        return value.IsEqualTo(0D, precision);
    }

    /// <summary>
    /// Returns the value of the angle converted from radians to degrees.
    /// </summary>
    /// <param name="radians">Angle value in radians</param>
    public static double RadiansToDegrees(this double radians)
    {
        return radians * DegreesInRadian;
    }

    /// <summary>
    /// Returns the value of the angle converted from degrees to radians.
    /// </summary>
    /// <param name="degrees">Angle value in degrees</param>
    public static double DegreesToRadians(this double degrees)
    {
        return degrees / DegreesInRadian;
    }

    /// <summary>
    /// Rounds a double-precision floating-point value to a specified number of fractional digits.
    /// </summary>
    /// <param name="value">A double-precision floating-point number to be rounded.</param>
    /// <param name="digits">The number of fractional digits in the return value.</param>
    /// <returns>
    /// The number nearest to value that has a number of fractional digits equal to digits.
    /// If value has fewer fractional digits than digits, value is returned unchanged.
    /// </returns>
    /// <remarks>Internally, the System.Math.Round method is used with Mode=MidpointRounding.AwayFromZero.</remarks>
    public static double Round(this double value, int digits = DefaultDigits)
    {
        return Math.Round(value, digits, MidpointRounding.AwayFromZero);
    }
}