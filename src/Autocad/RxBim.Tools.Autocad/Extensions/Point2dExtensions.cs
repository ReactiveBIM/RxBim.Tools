namespace RxBim.Tools.Autocad
{
    using Autodesk.AutoCAD.Geometry;
    using JetBrains.Annotations;

    /// <summary>
    /// Extensions for <see cref="Point2d"/>
    /// </summary>
    [PublicAPI]
    public static class Point2dExtensions
    {
        /// <summary>
        /// Returns a point offset from the original.
        /// </summary>
        /// <param name="basePoint">Starting point</param>
        /// <param name="x">X offset</param>
        /// <param name="y">Y offset</param>
        public static Point2d OffsetPoint(this Point2d basePoint, double x, double y)
        {
            return basePoint + new Vector2d(x, y);
        }

        /// <summary>
        /// Returns a 3D point with the X and Y coordinates of the original 2D point and zero Z coordinate.
        /// </summary>
        /// <param name="point">Origin 2D point</param>
        public static Point3d ConvertTo3d(this Point2d point)
        {
            return new Point3d(point.X, point.Y, 0d);
        }

        /// <summary>
        /// Returns a point midway between a point and another point.
        /// </summary>
        /// <param name="point">Point</param>
        /// <param name="otherPoint">Other point</param>
        public static Point2d GetMiddlePoint(this Point2d point, Point2d otherPoint)
        {
            return point + point.GetVectorTo(otherPoint).DivideBy(2);
        }
    }
}