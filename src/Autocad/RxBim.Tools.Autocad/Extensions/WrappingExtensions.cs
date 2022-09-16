namespace RxBim.Tools.Autocad
{
    using Autodesk.AutoCAD.DatabaseServices;
    using JetBrains.Annotations;

    /// <summary>
    /// Wrapping extensions.
    /// </summary>
    [PublicAPI]
    public static class WrappingExtensions
    {
        /// <summary>
        /// Return <see cref="IIdentifierWrapper"/> from <see cref="ObjectId"/>.
        /// </summary>
        /// <param name="id"><see cref="ObjectId"/> value.</param>
        public static IIdentifierWrapper Wrap(this ObjectId id)
        {
            return new AutocadObjectIdWrapper(id);
        }
    }
}