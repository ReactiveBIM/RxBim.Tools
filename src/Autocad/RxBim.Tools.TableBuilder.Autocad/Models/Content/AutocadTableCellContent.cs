namespace RxBim.Tools.TableBuilder
{
    /// <summary>
    /// Cell content for AutoCAD table.
    /// </summary>
    /// <typeparam name="T">Content value type.</typeparam>
    public abstract class AutocadTableCellContent<T> : CellContent<T>
    {
        /// <inheritdoc />
        protected AutocadTableCellContent(T value)
            : base(value)
        {
        }

        /// <summary>
        /// The rotation angle of the content in radians.
        /// </summary>
        public double Rotation { get; set; }
    }
}