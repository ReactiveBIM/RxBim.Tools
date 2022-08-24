namespace RxBim.Tools.Autocad
{
    using System;
    using Autodesk.AutoCAD.ApplicationServices;
    using Autodesk.AutoCAD.DatabaseServices;
    using JetBrains.Annotations;
    using Application = Autodesk.AutoCAD.ApplicationServices.Core.Application;
    using TransactionManager = Autodesk.AutoCAD.DatabaseServices.TransactionManager;

    /// <summary>
    /// Extensions for <see cref="ITransactionContext"/>.
    /// </summary>
    [PublicAPI]
    internal static class TransactionContextExtensions
    {
        /// <summary>
        /// Returns a <see cref="Database"/> from this context.
        /// </summary>
        /// <param name="context"><see cref="ITransactionContext"/> object.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">
        /// <see cref="ITransactionContext.ContextObject"/> value is not Document or Database.
        /// </exception>
        public static Database ToDatabase(this ITransactionContext context)
        {
            return context.ContextObject switch
            {
                Document document => document.Database,
                Database database => database,
                _ => throw GetException()
            };
        }

        /// <summary>
        /// Returns a <see cref="Document"/> from this context.
        /// </summary>
        /// <param name="context"><see cref="ITransactionContext"/> object.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">
        /// <see cref="ITransactionContext.ContextObject"/> value is not Document or Database.
        /// </exception>
        public static Document ToDocument(this ITransactionContext context)
        {
            return context.ContextObject switch
            {
                Document document => document,
                Database database => Application.DocumentManager.GetDocument(database),
                _ => throw GetException()
            };
        }

        private static ArgumentException GetException()
        {
            return new ArgumentException("Must be a AutoCAD Document or AutoCAD Database.",
                $"{nameof(ITransactionContext)}.{nameof(ITransactionContext.ContextObject)}");
        }
    }
}