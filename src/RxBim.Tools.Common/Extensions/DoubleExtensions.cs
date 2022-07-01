namespace RxBim.Tools.Common.Extensions
{
    using System;

    /// <summary>
    /// Extensions for <see cref="double"/>.
    /// </summary>
    public static class DoubleExtensions
    {
        private const double Epsilon = 1e-6;
        private const double DegreesInRadian = 180D / Math.PI;

        /// <summary>
        /// Returns true if a number is equal to another number with the given precision.
        /// </summary>
        /// <param name="value">Value</param>
        /// <param name="otherValue">Another value</param>
        /// <param name="precision">Comparison accuracy</param>
        public static bool IsEqualTo(this double value, double otherValue, double precision = Epsilon)
        {
            return Math.Abs(value - otherValue) < precision;
        }

        /// <summary>
        /// Returns true if a number is less than or equal to another number with the given precision.
        /// </summary>
        /// <param name="value">Value</param>
        /// <param name="otherValue">Another value</param>
        /// <param name="precision">Comparison accuracy</param>
        public static bool IsEqualOrLess(this double value, double otherValue, double precision = Epsilon)
        {
            return value.IsEqualTo(otherValue, precision) || value < otherValue;
        }

        /// <summary>
        /// Returns true if a number is greater than or equal to another number with the given precision.
        /// </summary>
        /// <param name="value">Value</param>
        /// <param name="otherValue">Another value</param>
        /// <param name="precision">Comparison accuracy</param>
        public static bool IsEqualOrGreater(this double value, double otherValue, double precision = Epsilon)
        {
            return value.IsEqualTo(otherValue, precision) || value > otherValue;
        }

        /// <summary>
        /// Returns true if the number is zero with the given precision.
        /// </summary>
        /// <param name="value">Value</param>
        /// <param name="precision">Comparison accuracy</param>
        public static bool IsZero(this double value, double precision = Epsilon)
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
    }
}