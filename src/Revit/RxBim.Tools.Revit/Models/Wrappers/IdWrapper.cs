namespace RxBim.Tools.Revit.Models
{
    /// <summary>
    /// Revit element identifier.
    /// </summary>
    public class IdWrapper : Wrapper<int>, IIdWrapper
    {
        /// <inheritdoc />
        public IdWrapper(int wrappedObject)
            : base(wrappedObject)
        {
        }
    }
}