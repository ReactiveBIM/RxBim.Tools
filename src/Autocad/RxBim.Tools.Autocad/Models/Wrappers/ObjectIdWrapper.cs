namespace RxBim.Tools.Autocad
{
    using Autodesk.AutoCAD.DatabaseServices;

    /// <summary>
    /// Wrapper for <see cref="ObjectId"/>.
    /// </summary>
    public class ObjectIdWrapper : Wrapper<ObjectId>, IObjectIdWrapper
    {
        /// <inheritdoc />
        public ObjectIdWrapper(ObjectId wrappedObject)
            : base(wrappedObject)
        {
        }

        /// <inheritdoc />
        public override string ToString()
            => Object.ToString();
    }
}