namespace RxBim.Tools.Autocad
{
    using System;
    using Autodesk.AutoCAD.ApplicationServices;
    using Autodesk.AutoCAD.DatabaseServices;
    using JetBrains.Annotations;
    using TransactionManager = Autodesk.AutoCAD.DatabaseServices.TransactionManager;

    /// <summary>
    /// Extensions for <see cref="ITransactionContext"/>.
    /// </summary>
    [PublicAPI]
    public static class TransactionContextExtensions
    {
        /// <summary>
        /// Returns <see cref="Autodesk.AutoCAD.DatabaseServices.TransactionManager"/> for context.
        /// </summary>
        /// <param name="context"><see cref="ITransactionContext"/> object.</param>
        /// <exception cref="ArgumentException">
        /// If <paramref name="context"/> is not <see cref="DocumentContext"/> or <see cref="DatabaseContext"/>.
        /// </exception>
        public static TransactionManager GetTransactionManager(this ITransactionContext context)
        {
            return context.GetFromContext(
                x => x.Unwrap<Document>().TransactionManager,
                x => x.Unwrap<Database>().TransactionManager);
        }

        /// <summary>
        /// Returns <see cref="Database"/> from context.
        /// </summary>
        /// <param name="context"><see cref="ITransactionContext"/> object.</param>
        /// <exception cref="ArgumentException">
        /// If <paramref name="context"/> is not <see cref="DocumentContext"/> or <see cref="DatabaseContext"/>.
        /// </exception>
        public static Database GetDatabase(this ITransactionContext context)
        {
            return context.GetFromContext(x => x.Unwrap<Document>().Database, x => x.Unwrap<Database>());
        }

        /// <summary>
        /// Returns <see cref="ITransactionContext"/> from database.
        /// </summary>
        /// <param name="database"><see cref="Database"/> object.</param>
        public static ITransactionContext ToContext(this Database database)
        {
            return new DatabaseContext(database);
        }

        /// <summary>
        /// Returns <see cref="ITransactionContext"/> from document.
        /// </summary>
        /// <param name="document"><see cref="Document"/> object.</param>
        public static ITransactionContext ToContext(this Document document)
        {
            return new DocumentContext(document);
        }

        private static T GetFromContext<T>(
            this ITransactionContext context,
            Func<DocumentContext, T> fromDocCx,
            Func<DatabaseContext, T> fromDbCx)
        {
            return context switch
            {
                DocumentContext document => fromDocCx(document),
                DatabaseContext database => fromDbCx(database),
                _ => throw new ArgumentException("Unknown context type!", nameof(context))
            };
        }
    }
}