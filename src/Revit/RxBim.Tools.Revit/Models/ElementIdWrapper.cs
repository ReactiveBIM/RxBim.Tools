namespace RxBim.Tools.Revit.Models
{
    /// <summary>
    /// Revit element identifier.
    /// </summary>
    public class ElementIdWrapper : IdentifierWrapper<int>
    {
        /// <inheritdoc />
        public ElementIdWrapper(int wrappedObject)
            : base(wrappedObject)
        {
        }
    }
}