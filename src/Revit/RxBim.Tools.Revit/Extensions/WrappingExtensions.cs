namespace RxBim.Tools.Revit.Extensions
{
    using Abstractions;
    using Autodesk.Revit.DB;
    using JetBrains.Annotations;
    using Models;

    /// <summary>
    /// Wrapping extensions.
    /// </summary>
    [PublicAPI]
    public static class WrappingExtensions
    {
        /// <summary>
        /// Returns <see cref="IObjectIdWrapper"/> from <see cref="ElementId"/>.
        /// </summary>
        /// <param name="id"><see cref="ElementId"/> object.</param>
        public static IObjectIdWrapper Wrap(this ElementId id)
        {
            return new RevitElementIdWrapper(id);
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