namespace RxBim.Tools.TableBuilder.Builders;

using System.Drawing;
using Styles;

/// <summary>
/// Builder for <see cref="CellTextFormatStyle"/>.
/// </summary>
public interface ICellTextFormatStyleBuilder : IBuilder<CellTextFormatStyle>
{
    /// <summary>
    /// Sets <see cref="CellTextFormatStyle.FontFamily"/> property.
    /// </summary>
    /// <param name="fontFamily">Property value.</param>
    ICellTextFormatStyleBuilder SetFontFamily(string? fontFamily);

    /// <summary>
    /// Sets <see cref="CellTextFormatStyle.Bold"/> property.
    /// </summary>
    /// <param name="bold">Property value.</param>
    ICellTextFormatStyleBuilder SetBold(bool? bold);

    /// <summary>
    /// Sets <see cref="CellTextFormatStyle.Italic"/> property.
    /// </summary>
    /// <param name="italic">Property value.</param>
    ICellTextFormatStyleBuilder SetItalic(bool? italic);

    /// <summary>
    /// Sets <see cref="CellTextFormatStyle.TextSize"/> property.
    /// </summary>
    /// <param name="textSize">Property value.</param>
    ICellTextFormatStyleBuilder SetTextSize(double? textSize);

    /// <summary>
    /// Sets <see cref="CellTextFormatStyle.WrapText"/> property.
    /// </summary>
    /// <param name="wrapText">Property value.</param>
    ICellTextFormatStyleBuilder SetWrapText(bool? wrapText);

    /// <summary>
    /// Sets <see cref="CellTextFormatStyle.TextColor"/> property.
    /// </summary>
    /// <param name="color">Property value.</param>
    ICellTextFormatStyleBuilder SetTextColor(Color? color);

    /// <summary>
    /// Sets properties from another format.
    /// </summary>
    /// <param name="textFormat">Another format.</param>
    /// <param name="additionalTextFormat">Additional another format.</param>
    /// <param name="useNullValue">If true sets null value from <paramref name="textFormat"/> and <paramref name="additionalTextFormat"/>.</param>
    ICellTextFormatStyleBuilder SetFromFormat(
        CellTextFormatStyle textFormat,
        CellTextFormatStyle? additionalTextFormat = null,
        bool useNullValue = true);
}
