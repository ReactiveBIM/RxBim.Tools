namespace RxBim.Tools.Autocad
{
    using System;
    using Autodesk.AutoCAD.ApplicationServices;
    using Autodesk.AutoCAD.DatabaseServices;
    using JetBrains.Annotations;
    using TransactionManager = Autodesk.AutoCAD.DatabaseServices.TransactionManager;

    /// <summary>
    /// Extensions for <see cref="ITransactionContextWrapper"/>.
    /// </summary>
    [PublicAPI]
    public static class TransactionContextWrapperExtensions
    {
        /// <summary>
        /// Returns <see cref="Autodesk.AutoCAD.DatabaseServices.TransactionManager"/> for context.
        /// </summary>
        /// <param name="context"><see cref="ITransactionContextWrapper"/> object.</param>
        /// <exception cref="ArgumentException">
        /// If <paramref name="context"/> is not <see cref="DocumentWrapper"/> or <see cref="DatabaseWrapper"/>.
        /// </exception>
        public static TransactionManager GetTransactionManager(this ITransactionContextWrapper context)
        {
            return context.GetFromContext(x => x.TransactionManager, x => x.TransactionManager);
        }

        /// <summary>
        /// Returns <see cref="Database"/> from context.
        /// </summary>
        /// <param name="context"><see cref="ITransactionContextWrapper"/> object.</param>
        /// <exception cref="ArgumentException">
        /// If <paramref name="context"/> is not <see cref="DocumentWrapper"/> or <see cref="DatabaseWrapper"/>.
        /// </exception>
        public static Database GetDatabase(this ITransactionContextWrapper context)
        {
            return context.GetFromContext(x => x.Database, x => x);
        }

        private static T GetFromContext<T>(
            this ITransactionContextWrapper context,
            Func<Document, T> fromDocCx,
            Func<Database, T> fromDbCx)
        {
            return context switch
            {
                IDocumentWrapper document => fromDocCx(document.Unwrap<Document>()),
                IDatabaseWrapper database => fromDbCx(database.Unwrap<Database>()),
                _ => throw new ArgumentException(
                    $"Unknown context type: {context.GetType().FullName}!",
                    nameof(context))
            };
        }
    }
}