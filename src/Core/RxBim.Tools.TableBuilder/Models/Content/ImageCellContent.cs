namespace RxBim.Tools.TableBuilder;

using JetBrains.Annotations;

/// <summary>
/// The image content of cell.
/// </summary>
[UsedImplicitly]
public class ImageCellContent : ICellContent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ImageCellContent"/> class.
    /// </summary>
    /// <param name="image">Image data.</param>
    /// <param name="value">Value.</param>
    public ImageCellContent(byte[] image, object? value = null)
    {
        Image = image;
        ValueObject = value;
    }

    /// <inheritdoc />
    public object? ValueObject { get; }

    /// <summary>
    /// Image file.
    /// </summary>
    public byte[] Image { get; }
}