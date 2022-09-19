namespace RxBim.Tools.Autocad
{
    using Autodesk.AutoCAD.DatabaseServices;

    /// <summary>
    /// Wrapper for <see cref="ObjectId"/>.
    /// </summary>
    public class ObjectIdWrapper : Wrapper<ObjectId>, IIdWrapper
    {
        /// <inheritdoc />
        public ObjectIdWrapper(ObjectId wrappedObject)
            : base(wrappedObject)
        {
        }
    }
}