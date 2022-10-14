namespace RxBim.Tools.Revit.Models
{
    using Autodesk.Revit.DB;

    /// <summary>
    /// Revit element identifier.
    /// </summary>
    public class ElementIdWrapper : Wrapper<ElementId>, IObjectIdWrapper
    {
        /// <inheritdoc />
        public ElementIdWrapper(ElementId wrappedObject)
            : base(wrappedObject)
        {
        }

        /// <inheritdoc />
        public override string ToString()
            => Object.IntegerValue.ToString();
    }
}