namespace RxBim.Tools.TableBuilder.Abstractions
{
    /// <summary>
    /// The content of a cell.
    /// </summary>
    public interface ICellContent
    {
        /// <summary>
        /// Cell content value.
        /// </summary>
        object? ValueObject { get; }
    }
}