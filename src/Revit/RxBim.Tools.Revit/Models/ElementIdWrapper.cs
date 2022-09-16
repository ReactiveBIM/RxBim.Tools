namespace RxBim.Tools.Revit.Models
{
    /// <summary>
    /// Revit element identifier.
    /// </summary>
    public class ElementIdWrapper : Wrapper<int>, IIdentifierWrapper
    {
        /// <inheritdoc />
        public ElementIdWrapper(int wrappedObject)
            : base(wrappedObject)
        {
        }
    }
}