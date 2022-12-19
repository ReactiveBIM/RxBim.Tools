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
        /// <inheritdoc />
        public bool Equals(XYZ x, XYZ y)
        {
            if (ReferenceEquals(x, y))
                return true;
            if (ReferenceEquals(x, null))
                return false;
            if (ReferenceEquals(y, null))
                return false;
            if (x.GetType() != y.GetType())
                return false;
            return x.IsAlmostEqualTo(y);
        }

        /// <inheritdoc />
        public int GetHashCode(XYZ obj)
        {
            unchecked
            {
                var hashCode = Math.Round(obj.Z, 4).GetHashCode();
                hashCode = (hashCode * 397) ^ Math.Round(obj.Y, 4).GetHashCode();
                hashCode = (hashCode * 397) ^ Math.Round(obj.Z, 4).GetHashCode();
                return hashCode;
            }
        }

        /// <inheritdoc />
        public int Compare(XYZ x, XYZ y)
        {
            if (ReferenceEquals(x, y))
                return 0;
            if (ReferenceEquals(null, y))
                return 1;
            if (ReferenceEquals(null, x))
                return -1;
            var zComparison = x.Z.CompareTo(y.Z);
            if (zComparison != 0)
                return zComparison;
            var yComparison = x.Y.CompareTo(y.Y);
            if (yComparison != 0)
                return yComparison;
            return x.X.CompareTo(y.X);
        }
    }
}