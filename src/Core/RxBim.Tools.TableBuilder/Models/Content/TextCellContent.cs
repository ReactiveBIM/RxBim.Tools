namespace RxBim.Tools.TableBuilder
{
    /// <summary>
    /// The text content of a cell.
    /// </summary>
    public class TextCellContent : CellContent<string>
    {
        /// <inheritdoc />
        public TextCellContent(string value)
            : base(value)
        {
        }

        /// <summary>
        /// Empty text content.
        /// </summary>
        public static TextCellContent Empty { get; } = new(string.Empty);
    }
}