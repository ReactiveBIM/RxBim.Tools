namespace RxBim.Tools.Autocad
{
    using Autodesk.AutoCAD.DatabaseServices;
    using Autodesk.AutoCAD.Geometry;
    using JetBrains.Annotations;

    /// <summary>
    /// Extensions for <see cref="Extents3d"/>.
    /// </summary>
    [PublicAPI]
    public static class Extents3dExtensions
    {
        /// <summary>
        /// Returns a new extents according to the specified zoom factor.
        /// </summary>
        /// <param name="extents">Extents.</param>
        /// <param name="zoomFactor">Zoom factor.</param>
        public static Extents3d Zoom(this Extents3d extents, double zoomFactor = 0.25)
        {
            var dX = extents.GetLength() / zoomFactor * 0.5;
            var dY = extents.GetHeight() / zoomFactor * 0.5;
            return new Extents3d(
                new Point3d(extents.MinPoint.X - dX, extents.MinPoint.Y - dY, 0),
                new Point3d(extents.MaxPoint.X + dX, extents.MaxPoint.Y + dY, 0));
        }

        /// <summary>
        /// The height of the extents (along the Y-axis).
        /// </summary>
        /// <param name="extents">Extents.</param>
        public static double GetHeight(this Extents3d extents)
        {
            return extents.MaxPoint.Y - extents.MinPoint.Y;
        }

        /// <summary>
        /// The length of the extents (along the X-axis).
        /// </summary>
        /// <param name="extents">Extents.</param>
        public static double GetLength(this Extents3d extents)
        {
            return extents.MaxPoint.X - extents.MinPoint.X;
        }
    }
}