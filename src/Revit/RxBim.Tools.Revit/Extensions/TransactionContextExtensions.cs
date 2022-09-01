namespace RxBim.Tools.Revit.Extensions
{
    using System;
    using Autodesk.Revit.DB;
    using JetBrains.Annotations;
    using Models;

    /// <summary>
    /// Extensions for <see cref="ITransactionContext"/>.
    /// </summary>
    [PublicAPI]
    public static class TransactionContextExtensions
    {
        /// <summary>
        /// Returns <see cref="Document"/> from context.
        /// </summary>
        /// <param name="context"><see cref="ITransactionContext"/> object.</param>
        /// <exception cref="ArgumentException">If context is not <see cref="DocumentContext"/>.</exception>
        public static Document GetDocument(this ITransactionContext context)
        {
            if (context is DocumentContext documentContext)
                return documentContext.ContextObject;

            throw new ArgumentException($"Must be a {nameof(DocumentContext)}!", nameof(context));
        }

        /// <summary>
        /// Returns <see cref="ITransactionContext"/> from document.
        /// </summary>
        /// <param name="document"><see cref="Document"/> object.</param>
        public static ITransactionContext ToContext(this Document document)
        {
            return new DocumentContext(document);
        }
    }
}