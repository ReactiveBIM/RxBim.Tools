namespace RxBim.Tools.Revit.Extensions
{
    using Abstractions;
    using Autodesk.Revit.DB;
    using Models;

    /// <summary>
    /// Wrapping extensions.
    /// </summary>
    public static class WrappingExtensions
    {
        /// <summary>
        /// Returns <see cref="IIdentifierWrapper"/> from <see cref="ElementId"/>.
        /// </summary>
        /// <param name="id"><see cref="ElementId"/> object.</param>
        public static IIdentifierWrapper Wrap(this ElementId id)
        {
            return new ElementIdWrapper(id.IntegerValue);
        }

        /// <summary>
        /// Returns <see cref="IDocumentWrapper"/> from document.
        /// </summary>
        /// <param name="document"><see cref="Document"/> object.</param>
        public static IDocumentWrapper Wrap(this Document document)
        {
            return new DocumentWrapper(document);
        }
    }
}