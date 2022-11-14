namespace RxBim.Tools.Revit.TestablePlugin.Sample.Models;

/// <summary>
/// Parameters for found cell.
/// </summary>
public record FoundCellParameters
{
    /// <summary>
    /// Index of the first cell with weight.
    /// </summary>
    public int FirstWeightCell { get; init; }
    
    /// <summary>
    /// Index of the start hidden fields.
    /// </summary>
    public int StartHiddenFields { get; init; }
    
    /// <summary>
    /// Count of the border cells
    /// </summary>
    public int BorderCell { get; init; }
}