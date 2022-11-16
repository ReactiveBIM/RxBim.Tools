namespace RxBim.Tools.TableBuilder;

using Styles;

/// <summary>
/// Extensions for <see cref="CellFormatStyle"/>
/// </summary>
public static class CellFormatStyleExtensions
{
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
