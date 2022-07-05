namespace RxBim.Tools.Autocad
{
    using Autodesk.AutoCAD.DatabaseServices;
    using Autodesk.AutoCAD.Geometry;

    /// <summary>
    /// Extensions for <see cref="Extents3d"/>.
    /// </summary>
    public static class Extents3dExtensions
    {
        /// <summary>
        /// Отступ границ
        /// </summary>
        /// <param name="ext">Границы</param>
        /// <param name="zoomFactor">Отступ в процентах</param>
        /// <returns></returns>
        public static Extents3d Offset(this Extents3d ext, double zoomFactor = 0.25)
        {
            var dX = ext.GetLength() / zoomFactor * 0.5;
            var dY = ext.GetHeight() / zoomFactor * 0.5;
            return new Extents3d(
                new Point3d(ext.MinPoint.X - dX, ext.MinPoint.Y - dY, 0),
                new Point3d(ext.MaxPoint.X + dX, ext.MaxPoint.Y + dY, 0));
        }

        /// <summary>
        /// Расстояние по Y
        /// </summary>
        /// <param name="ext">Граница</param>
        public static double GetHeight(this Extents3d ext)
        {
            return ext.MaxPoint.Y - ext.MinPoint.Y;
        }

        /// <summary>
        /// Расстояние по X
        /// </summary>
        /// <param name="ext">Граница</param>
        public static double GetLength(this Extents3d ext)
        {
            return ext.MaxPoint.X - ext.MinPoint.X;
        }
    }
}