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
    /// <param name="imageFile">Image file path.</param>
    /// <param name="scale">Scale.</param>
    public ImageCellContent(
        string imageFile,
        double scale = 1)
    {
        ImageFile = imageFile;
        Scale = scale;
    }

    /// <inheritdoc />
    public object? ValueObject => null;

    /// <summary>
    /// Image file.
    /// </summary>
    public string ImageFile { get; }

    /// <summary>
    /// Scale.
    /// </summary>
    public double Scale { get; }
}