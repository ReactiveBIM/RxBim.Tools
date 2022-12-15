namespace RxBim.Tools.TableBuilder.Builders;

using System;
using System.Drawing;
using Styles;

/// <summary>
/// Builder for <see cref="CellFormatStyle"/>.
/// </summary>
public interface ICellFormatStyleBuilder : IBuilder<CellFormatStyle>
{
    /// <summary>
    /// Sets <see cref="CellFormatStyle.TextFormat"/>.
    /// </summary>
    /// <param name="action">Action for text format.</param>
    ICellFormatStyleBuilder SetTextFormat(Action<ICellTextFormatStyleBuilder> action);

    /// <summary>
    /// Sets <see cref="CellFormatStyle.Borders"/>.
    /// </summary>
    /// <param name="action">Action for text format.</param>
    ICellFormatStyleBuilder SetBorders(Action<ICellBordersBuilder> action);

    /// <summary>
    /// Sets <see cref="CellFormatStyle.ContentMargins"/>.
    /// </summary>
    /// <param name="action">Action for text format.</param>
    ICellFormatStyleBuilder SetContentMargins(Action<ICellContentMarginsBuilder> action);

    /// <summary>
    /// Sets <see cref="CellFormatStyle.BackgroundColor"/> property.
    /// </summary>
    /// <param name="color">Property value.</param>
    ICellFormatStyleBuilder SetBackgroundColor(Color? color = null);

    /// <summary>
    /// Sets <see cref="CellFormatStyle.ContentHorizontalAlignment"/> property.
    /// </summary>
    /// <param name="alignment">Property value.</param>
    ICellFormatStyleBuilder SetContentHorizontalAlignment(CellContentHorizontalAlignment? alignment = null);

    /// <summary>
    /// Sets <see cref="CellFormatStyle.ContentVerticalAlignment"/> property.
    /// </summary>
    /// <param name="alignment">Property value.</param>
    ICellFormatStyleBuilder SetContentVerticalAlignment(CellContentVerticalAlignment? alignment = null);

    /// <summary>
    /// Sets properties from another format.
    /// </summary>
    /// <param name="format">Another format.</param>
    /// <param name="defaultFormat">Additional another format.</param>
    /// <param name="useNullValue">If true sets null value from <paramref name="format"/> and <paramref name="defaultFormat"/>.</param>
    /// <remarks>Priority for the <paramref name="format"/>.
    /// If property in <paramref name="format"/> is null sets property from <paramref name="defaultFormat"/>.</remarks>
    ICellFormatStyleBuilder SetFromFormat(
        CellFormatStyle format, CellFormatStyle? defaultFormat = null, bool useNullValue = true);
}
