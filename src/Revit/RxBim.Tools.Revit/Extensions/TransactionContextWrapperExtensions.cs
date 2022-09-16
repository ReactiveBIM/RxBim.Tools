namespace RxBim.Tools.Revit.Extensions
{
    using System;
    using Abstractions;
    using Autodesk.Revit.DB;
    using JetBrains.Annotations;
    using Models;

    /// <summary>
    /// Extensions for <see cref="ITransactionContextWrapper"/>.
    /// </summary>
    [PublicAPI]
    public static class TransactionContextWrapperExtensions
    {
        /// <summary>
        /// Returns <see cref="Document"/> from context.
        /// </summary>
        /// <param name="context"><see cref="ITransactionContextWrapper"/> object.</param>
        /// <exception cref="ArgumentException">If context is not <see cref="DocumentWrapper"/>.</exception>
        public static Document GetDocument(this ITransactionContextWrapper context)
        {
            if (context is IDocumentWrapper documentContext)
                return documentContext.Unwrap<Document>();

            throw new ArgumentException($"Must be a {nameof(IDocumentWrapper)}!", nameof(context));
        }
    }
}