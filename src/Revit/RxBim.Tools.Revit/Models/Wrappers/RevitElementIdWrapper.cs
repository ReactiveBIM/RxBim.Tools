namespace RxBim.Tools.Revit.Models
{
    using Autodesk.Revit.DB;
    using Extensions;

    /// <summary>
    /// Revit element identifier.
    /// </summary>
    public class RevitElementIdWrapper : Wrapper<ElementId>, IObjectIdWrapper
    {
        /// <inheritdoc />
        public RevitElementIdWrapper(ElementId wrappedObject)
            : base(wrappedObject)
        {
        }

        /// <inheritdoc />
        public override bool Equals(object? obj)
        {
            if (obj is not RevitElementIdWrapper wrapper)
                return false;

            return wrapper.Object.GetIdValue() == Object.GetIdValue();
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return Object.GetIdValue().GetHashCode();
        }

        /// <inheritdoc />
        public override string ToString()
            => Object.GetIdValue().ToString();
    }
}