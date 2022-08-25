namespace RxBim.Tools.Revit.Helpers
{
    using System.Collections.Generic;
    using Autodesk.Revit.DB;

    /// <summary>
    /// Comparer for <see cref="XYZ"/>.
    /// </summary>
    public class XyzComparer : IComparer<XYZ>
    {
        private const double Tolerance = 0.0001;

        /// <inheritdoc/>
        public int Compare(XYZ x, XYZ y)
        {
            var compareValue = Compare(x.X, y.X);

            if (compareValue != 0)
                return compareValue;

            compareValue = Compare(x.Y, y.Y);

            return compareValue == 0 ? Compare(x.Z, y.Z) : compareValue;
        }

        private static int Compare(double a, double b)
        {
            return a.IsEqualTo(b, Tolerance) ? 0 : (a < b ? -1 : 1);
        }
    }
}