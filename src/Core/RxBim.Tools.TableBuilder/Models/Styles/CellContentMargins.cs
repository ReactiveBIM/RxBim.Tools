namespace RxBim.Tools.TableBuilder;

/// <summary>
/// Margins size.
/// </summary>
public class CellContentMargins
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CellContentMargins"/> class.
    /// </summary>
    internal CellContentMargins()
    {
    }
        
    /// <summary>
    /// Top margin size.
    /// </summary>
    public double? Top { get; internal set; }

    /// <summary>
    /// Bottom margin size.
    /// </summary>
    public double? Bottom { get; internal set; }

    /// <summary>
    /// Left margin size.
    /// </summary>
    public double? Left { get; internal set; }

    /// <summary>
    /// Right margin size.
    /// </summary>
    public double? Right { get; internal set; }
}