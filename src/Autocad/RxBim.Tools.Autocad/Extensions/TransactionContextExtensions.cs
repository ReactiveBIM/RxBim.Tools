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
    public static class TransactionContextExtensions
    {
        /// <summary>
        /// Returns <see cref="Autodesk.AutoCAD.DatabaseServices.TransactionManager"/> for context.
        /// </summary>
        /// <param name="context"><see cref="ITransactionContextWrapper"/> object.</param>
        /// <exception cref="ArgumentException">
        /// If <paramref name="context"/> is not <see cref="DocumentContextWrapper"/> or <see cref="DatabaseContextWrapper"/>.
        /// </exception>
        public static TransactionManager GetTransactionManager(this ITransactionContextWrapper context)
        {
            return context.GetFromContext(
                x => x.Unwrap<Document>().TransactionManager,
                x => x.Unwrap<Database>().TransactionManager);
        }

        /// <summary>
        /// Returns <see cref="Database"/> from context.
        /// </summary>
        /// <param name="context"><see cref="ITransactionContextWrapper"/> object.</param>
        /// <exception cref="ArgumentException">
        /// If <paramref name="context"/> is not <see cref="DocumentContextWrapper"/> or <see cref="DatabaseContextWrapper"/>.
        /// </exception>
        public static Database GetDatabase(this ITransactionContextWrapper context)
        {
            return context.GetFromContext(x => x.Unwrap<Document>().Database, x => x.Unwrap<Database>());
        }

        /// <summary>
        /// Returns <see cref="ITransactionContextWrapper"/> from database.
        /// </summary>
        /// <param name="database"><see cref="Database"/> object.</param>
        public static ITransactionContextWrapper ToContext(this Database database)
        {
            return new DatabaseContextWrapper(database);
        }

        /// <summary>
        /// Returns <see cref="ITransactionContextWrapper"/> from document.
        /// </summary>
        /// <param name="document"><see cref="Document"/> object.</param>
        public static ITransactionContextWrapper ToContext(this Document document)
        {
            return new DocumentContextWrapper(document);
        }

        private static T GetFromContext<T>(
            this ITransactionContextWrapper context,
            Func<DocumentContextWrapper, T> fromDocCx,
            Func<DatabaseContextWrapper, T> fromDbCx)
        {
            return context switch
            {
                DocumentContextWrapper document => fromDocCx(document),
                DatabaseContextWrapper database => fromDbCx(database),
                _ => throw new ArgumentException("Unknown context type!", nameof(context))
            };
        }
    }
}