namespace RxBim.Tools.Autocad
{
    using Autodesk.AutoCAD.DatabaseServices;

    /// <summary>
    /// Wrapper for <see cref="ObjectId"/>.
    /// </summary>
    public class AutocadObjectIdWrapper : Wrapper<ObjectId>, IIdentifierWrapper
    {
        /// <inheritdoc />
        public AutocadObjectIdWrapper(ObjectId wrappedObject)
            : base(wrappedObject)
        {
        }
    }
}