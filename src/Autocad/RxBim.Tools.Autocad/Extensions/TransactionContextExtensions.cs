namespace RxBim.Tools.Autocad
{
    using System;
    using Autodesk.AutoCAD.DatabaseServices;
    using JetBrains.Annotations;

    /// <summary>
    /// Extensions for <see cref="ITransactionContext"/>.
    /// </summary>
    [PublicAPI]
    internal static class TransactionContextExtensions
    {
        /// <summary>
        /// Returns <see cref="TransactionManager"/> for context.
        /// </summary>
        /// <param name="context"><see cref="ITransactionContext"/> object.</param>
        /// <exception cref="ArgumentException">
        /// If <paramref name="context"/> is not <see cref="DocumentContext"/> or <see cref="DatabaseContext"/>.
        /// </exception>
        public static TransactionManager GetTransactionManager(this ITransactionContext context)
        {
            return context.GetFromContext(
                x => x.ContextObject.TransactionManager,
                x => x.ContextObject.TransactionManager);
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
            return context.GetFromContext(x => x.ContextObject.Database, x => x.ContextObject);
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