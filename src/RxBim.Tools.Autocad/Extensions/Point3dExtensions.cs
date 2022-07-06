namespace RxBim.Tools.Autocad
{
    using Autodesk.AutoCAD.ApplicationServices.Core;
    using Autodesk.AutoCAD.Geometry;
    using JetBrains.Annotations;

    /// <summary>
    /// Extensions for <see cref="Point3d"/>
    /// </summary>
    [PublicAPI]
    public static class Point3dExtensions
    {
        /// <summary>
        /// Returns a point offset from the original.
        /// </summary>
        /// <param name="basePoint">Point</param>
        /// <param name="x">X offset</param>
        /// <param name="y">Y offset</param>
        /// <param name="z">Z offset</param>
        public static Point3d OffsetPoint(this Point3d basePoint, double x, double y, double z = 0)
        {
            return basePoint + new Vector3d(x, y, z);
        }

        /// <summary>
        /// Returns a 2D point with x and y coordinates of the original 3D point.
        /// </summary>
        /// <param name="point">Origin 3D point</param>
        public static Point2d ConvertTo2d(this Point3d point)
        {
            return new Point2d(point.X, point.Y);
        }

        /// <summary>
        /// Returns a 3D point with the X and Y coordinates of the original 3D point and zero Z coordinate.
        /// </summary>
        /// <param name="pt">Origin 3D point</param>
        public static Point3d Flatten(this Point3d pt)
        {
            return new Point3d(pt.X, pt.Y, 0.0);
        }

        /// <summary>
        /// Returns a point midway between a point and another point.
        /// </summary>
        /// <param name="point">Point</param>
        /// <param name="otherPoint">Other point</param>
        public static Point3d GetMiddlePoint(this Point3d point, Point3d otherPoint)
        {
            return point + point.GetVectorTo(otherPoint).DivideBy(2);
        }

        /// <summary>
        /// Returns a point obtained by transforming a source point from the user coordinate system to the world coordinate system.
        /// </summary>
        /// <param name="pt">Point</param>
        public static Point3d TransformFromUcsToWcs(this Point3d pt)
        {
            return pt.Transform(CoordinateSystemType.UCS, CoordinateSystemType.WCS);
        }

        /// <summary>
        /// Returns a point obtained by transforming a source point from the world coordinate system to the user coordinate system.
        /// </summary>
        /// <param name="pt">Point</param>
        public static Point3d TransformFromWcsToUcs(this Point3d pt)
        {
            return pt.Transform(CoordinateSystemType.WCS, CoordinateSystemType.UCS);
        }

        /// <summary>
        /// Returns a point obtained by transforming a source point from one coordinate system to another.
        /// </summary>
        /// <param name="pt">Point</param>
        /// <param name="from">Initial coordinate system</param>
        /// <param name="to">Target coordinate system</param>
        /// <exception cref="Autodesk.AutoCAD.Runtime.Exception">
        /// eInvalidInput - if transformed from PSDCS to any coordinate system other than DCS.
        /// </exception>
        public static Point3d Transform(this Point3d pt, CoordinateSystemType from, CoordinateSystemType to)
        {
            var ed = Application.DocumentManager.MdiActiveDocument.Editor;
            return ed.TransformPoint(pt, from, to);
        }
    }
}