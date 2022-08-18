namespace RxBim.Tools.Autocad
{
    using Autodesk.AutoCAD.ApplicationServices;
    using JetBrains.Annotations;

    /// <summary>
    /// Extensions for <see cref="Document"/>.
    /// </summary>
    [PublicAPI]
    public static class DocumentExtensions
    {
        /// <summary>
        /// Returns <see cref="Document"/> wrapped to <see cref="ITransactionContext"/>.
        /// </summary>
        /// <param name="document"><see cref="Document"/> object.</param>
        public static ITransactionContext ToTransactionContext(this Document document)
        {
            return new TransactionContext(document);
        }
    }
}