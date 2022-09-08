namespace RxBim.Tools.TableBuilder
{
    using Styles;

    /// <summary>
    /// Extensions for <see cref="CellFormatStyle"/>.
    /// </summary>
    internal static class CellFormatStyleExtensions
    {
        /// <summary>
        /// Returns the result of a combination of two formats.
        /// </summary>
        /// <param name="thisFormat">Own object format.</param>
        /// <param name="ownerFormat">The format of the owner object.</param>
        public static CellFormatStyle Collect(this CellFormatStyle thisFormat, CellFormatStyle ownerFormat)
        {
            var styleBuilder = new CellFormatStyleBuilder();
            styleBuilder.SetFromFormat(thisFormat, ownerFormat);
            return styleBuilder.Build();
        }
    }
}