namespace RxBim.Tools.TableBuilder
{
    using Autocad.Extensions;
    using Autodesk.AutoCAD.DatabaseServices;
    using Autodesk.AutoCAD.Geometry;

    /// <summary>
    /// Extensions for <see cref="string"/>.
    /// </summary>
    internal static class TextExtensions
    {
        /// <summary>
        /// Returns the size of the text, in drawing units, for a given string.
        /// </summary>
        /// <param name="value">String value.</param>
        /// <param name="rotation">Text rotation.</param>
        /// <param name="styleId">Text stile identifier.</param>
        /// <param name="height">Text height.</param>
        public static (double Length, double Height) GetAutocadTextSize(
            this string value,
            double rotation,
            ObjectId? styleId,
            double? height)
        {
            using var text = new MText
            {
                Normal = Vector3d.ZAxis,
                Contents = value,
                Rotation = rotation
            };

            if (styleId != null)
                text.TextStyleId = styleId.Value;

            using var txtStyle = text.TextStyleId.OpenAs<TextStyleTableRecord>();

            // If the text is not forced to set the height.
            if (txtStyle.TextSize.IsZero() && height != null)
                text.TextHeight = height.Value; // Set the height of the text.

            // Calculate the size of the text.
            return
                (text.GeometricExtents.MaxPoint.X - text.GeometricExtents.MinPoint.X,
                text.GeometricExtents.MaxPoint.Y - text.GeometricExtents.MinPoint.Y);
        }
    }
}