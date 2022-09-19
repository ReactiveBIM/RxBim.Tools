namespace RxBim.Tools.Autocad
{
    using Autodesk.AutoCAD.ApplicationServices;
    using Autodesk.AutoCAD.DatabaseServices;
    using JetBrains.Annotations;

    /// <summary>
    /// Wrapping extensions.
    /// </summary>
    [PublicAPI]
    public static class WrappingExtensions
    {
        /// <summary>
        /// Return <see cref="IIdWrapper"/> from <see cref="ObjectId"/>.
        /// </summary>
        /// <param name="id"><see cref="ObjectId"/> value.</param>
        public static IIdWrapper Wrap(this ObjectId id)
        {
            return new ObjectIdWrapper(id);
        }

        /// <summary>
        /// Returns <see cref="IDatabaseWrapper"/> from <see cref="Database"/>.
        /// </summary>
        /// <param name="database"><see cref="Database"/> object.</param>
        public static IDatabaseWrapper Wrap(this Database database)
        {
            return new DatabaseWrapper(database);
        }

        /// <summary>
        /// Returns <see cref="IDocumentWrapper"/> from <see cref="Document"/>.
        /// </summary>
        /// <param name="document"><see cref="Document"/> object.</param>
        public static IDocumentWrapper Wrap(this Document document)
        {
            return new DocumentWrapper(document);
        }
    }
}