namespace RxBim.Tools.TableBuilder.Builders;

using System.Drawing;
using Styles;

/// <summary>
/// Builder for <see cref="CellTextFormatStyle"/>.
/// </summary>
public interface ICellTextFormatStyleBuilder
{
    /// <summary>
    /// Sets <see cref="CellTextFormatStyle.FontFamily"/> property.
    /// </summary>
    /// <param name="fontFamily">Property value.</param>
    public ICellTextFormatStyleBuilder SetFontFamily(string? fontFamily);

    /// <summary>
    /// Sets <see cref="CellTextFormatStyle.Bold"/> property.
    /// </summary>
    /// <param name="bold">Property value.</param>
    public ICellTextFormatStyleBuilder SetBold(bool? bold);

    /// <summary>
    /// Sets <see cref="CellTextFormatStyle.Italic"/> property.
    /// </summary>
    /// <param name="italic">Property value.</param>
    public ICellTextFormatStyleBuilder SetItalic(bool? italic);

    /// <summary>
    /// Sets <see cref="CellTextFormatStyle.TextSize"/> property.
    /// </summary>
    /// <param name="textSize">Property value.</param>
    public ICellTextFormatStyleBuilder SetTextSize(double? textSize);

    /// <summary>
    /// Sets <see cref="CellTextFormatStyle.WrapText"/> property.
    /// </summary>
    /// <param name="wrapText">Property value.</param>
    public ICellTextFormatStyleBuilder SetWrapText(bool? wrapText);

    /// <summary>
    /// Sets <see cref="CellTextFormatStyle.TextColor"/> property.
    /// </summary>
    /// <param name="color">Property value.</param>
    public ICellTextFormatStyleBuilder SetTextColor(Color? color);

    /// <summary>
    /// Sets properties from another format.
    /// </summary>
    /// <param name="textFormat">Another format.</param>
    /// <param name="additionalTextFormat">Additional another format.</param>
    /// <param name="useNullValue">If true sets null value from <paramref name="textFormat"/> and <paramref name="additionalTextFormat"/>.</param>
    public ICellTextFormatStyleBuilder SetFromFormat(
        CellTextFormatStyle textFormat,
        CellTextFormatStyle? additionalTextFormat = null,
        bool useNullValue = true);

    /// <summary>
    /// Returns the built <see cref="CellTextFormatStyle"/>.
    /// </summary>
    public CellTextFormatStyle Build();
}
