﻿namespace RxBim.Tools.TableBuilder.Models.Contents
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
        public static TextCellContent Empty { get; } = new (string.Empty);

        /// <inheritdoc />
        public override string ToString()
        {
            return Value;
        }
    }
}