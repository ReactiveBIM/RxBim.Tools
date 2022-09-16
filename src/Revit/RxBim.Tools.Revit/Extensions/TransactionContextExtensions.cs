namespace RxBim.Tools.Revit.Extensions
{
    using System;
    using Autodesk.Revit.DB;
    using JetBrains.Annotations;
    using Models;

    /// <summary>
    /// Extensions for <see cref="ITransactionContextWrapper"/>.
    /// </summary>
    [PublicAPI]
    public static class TransactionContextExtensions
    {
        /// <summary>
        /// Returns <see cref="Document"/> from context.
        /// </summary>
        /// <param name="context"><see cref="ITransactionContextWrapper"/> object.</param>
        /// <exception cref="ArgumentException">If context is not <see cref="DocumentContextWrapper"/>.</exception>
        public static Document GetDocument(this ITransactionContextWrapper context)
        {
            if (context is DocumentContextWrapper documentContext)
                return documentContext.Unwrap<Document>();

            throw new ArgumentException($"Must be a {nameof(DocumentContextWrapper)}!", nameof(context));
        }

        /// <summary>
        /// Returns <see cref="ITransactionContextWrapper"/> from document.
        /// </summary>
        /// <param name="document"><see cref="Document"/> object.</param>
        public static ITransactionContextWrapper ToContext(this Document document)
        {
            return new DocumentContextWrapper(document);
        }
    }
}