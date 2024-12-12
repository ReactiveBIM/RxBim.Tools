namespace RxBim.Tools.Revit.Helpers
{
    using System;
    using System.Collections.Generic;
    using Autodesk.Revit.DB;

    /// <summary>
    /// Comparer for <see cref="XYZ"/>.
    /// </summary>
    public class XyzComparer : IEqualityComparer<XYZ>, IComparer<XYZ>
    {
        private const double Tolerance = 0.0001;

        /// <inheritdoc />
        public bool Equals(XYZ? x, XYZ? y)
        {
            if (ReferenceEquals(x, y))
                return true;
            if (ReferenceEquals(x, null))
                return false;
            if (ReferenceEquals(y, null))
                return false;
            return Compare(x, y) == 0;
        }

        /// <inheritdoc />
        public int GetHashCode(XYZ obj)
        {
            unchecked
            {
                var hashCode = Math.Round(obj.X, 4).GetHashCode();
                hashCode = (hashCode * 397) ^ Math.Round(obj.Y, 4).GetHashCode();
                hashCode = (hashCode * 397) ^ Math.Round(obj.Z, 4).GetHashCode();
                return hashCode;
            }
        }

        /// <inheritdoc />
        public int Compare(XYZ? x, XYZ? y)
        {
            if (ReferenceEquals(x, y))
                return 0;
            if (ReferenceEquals(null, y))
                return 1;
            if (ReferenceEquals(null, x))
                return -1;

            var compareValue = GetCompareValue(x.X, y.X);

            if (compareValue != 0)
                return compareValue;

            compareValue = GetCompareValue(x.Y, y.Y);

            return compareValue == 0 ? GetCompareValue(x.Z, y.Z) : compareValue;
        }

        private int GetCompareValue(double a, double b)
        {
            return a.IsEqualTo(b, Tolerance) ? 0 : a < b ? -1 : 1;
        }
    }
}