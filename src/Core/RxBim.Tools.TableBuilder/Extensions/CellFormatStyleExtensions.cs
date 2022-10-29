namespace RxBim.Tools.TableBuilder
{
    using Styles;

    /// <summary>
    /// Extensions for <see cref="CellFormatStyle"/>.
    /// </summary>
    public static class CellFormatStyleExtensions
    {
        /// <summary>
        /// bla bla bla
        /// </summary>
        /// <param name="cellFormatStyleBuilder"><see cref="CellFormatStyleBuilder"/></param>
        /// <param name="cellBorderType"><see cref="CellBorderType"/></param>
        public static CellFormatStyleBuilder SetTopBorder(this CellFormatStyleBuilder cellFormatStyleBuilder, CellBorderType cellBorderType)
        {
            cellFormatStyleBuilder.ModifyFormat(f => f.Borders.Top = cellBorderType);
            return cellFormatStyleBuilder;
        }

        /// <summary>
        /// bla bla bla
        /// </summary>
        /// <param name="cellFormatStyleBuilder"><see cref="CellFormatStyleBuilder"/></param>
        /// <param name="cellBorderType"><see cref="CellBorderType"/></param>
        public static CellFormatStyleBuilder SetRightBorder(
            this CellFormatStyleBuilder cellFormatStyleBuilder,
            CellBorderType cellBorderType)
        {
            cellFormatStyleBuilder.ModifyFormat(f => f.Borders.Right = cellBorderType);
            return cellFormatStyleBuilder;
        }

        /// <summary>
        /// bla bla bla
        /// </summary>
        /// <param name="cellFormatStyleBuilder"><see cref="CellFormatStyleBuilder"/></param>
        /// <param name="cellBorderType"><see cref="CellBorderType"/></param>
        public static CellFormatStyleBuilder SetBottomBorder(
            this CellFormatStyleBuilder cellFormatStyleBuilder,
            CellBorderType cellBorderType)
        {
            cellFormatStyleBuilder.ModifyFormat(f => f.Borders.Bottom = cellBorderType);
            return cellFormatStyleBuilder;
        }

        /// <summary>
        /// bla bla bla
        /// </summary>
        /// <param name="cellFormatStyleBuilder"><see cref="CellFormatStyleBuilder"/></param>
        /// <param name="cellBorderType"><see cref="CellBorderType"/></param>
        public static CellFormatStyleBuilder SetLeftBorder(
            this CellFormatStyleBuilder cellFormatStyleBuilder,
            CellBorderType cellBorderType)
        {
            cellFormatStyleBuilder.ModifyFormat(f => f.Borders.Left = cellBorderType);
            return cellFormatStyleBuilder;
        }

        /// <summary>
        /// Returns the result of a combination of two formats.
        /// </summary>
        /// <param name="thisFormat">Own object format.</param>
        /// <param name="ownerFormat">The format of the owner object.</param>
        internal static CellFormatStyle Collect(this CellFormatStyle thisFormat, CellFormatStyle ownerFormat)
        {
            var styleBuilder = new CellFormatStyleBuilder();
            styleBuilder.SetFromFormat(thisFormat, ownerFormat);
            return styleBuilder.Build();
        }
    }
}
