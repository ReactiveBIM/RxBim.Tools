namespace RxBim.Tools.TableBuilder;

/// <summary>
/// Table row data.
/// </summary>
public class Row : CellsSet
{
    private double? _height;

    /// <summary>
    /// Initializes a new instance of the <see cref="Row"/> class.
    /// </summary>
    /// <param name="table">The <see cref="Table"/> that this <see cref="Row"/> belongs to.</param>
    internal Row(Table table)
        : base(table)
    {
    }

    /// <summary>
    /// The height of the row.
    /// </summary>
    public double? Height
    {
        get => _height;
        internal set
        {
            IsAdjustedToContent = false;
            _height = value;
        }
    }
}