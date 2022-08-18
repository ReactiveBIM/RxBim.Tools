namespace RxBim.Tools.Autocad
{
    using System;
    using Autodesk.AutoCAD.ApplicationServices;
    using Autodesk.AutoCAD.DatabaseServices;
    using TransactionManager = Autodesk.AutoCAD.DatabaseServices.TransactionManager;

    /// <summary>
    /// Extensions for <see cref="ITransactionContext"/>.
    /// </summary>
    internal static class TransactionContextExtensions
    {
        /// <summary>
        /// Returns transaction manager for this context.
        /// </summary>
        /// <param name="context"><see cref="ITransactionContext"/> object.</param>
        /// <exception cref="ArgumentException"><see cref="ITransactionContext.ContextObject"/> is not Document or Database.</exception>
        public static TransactionManager GetTransactionManager(this ITransactionContext context)
        {
            return context.ContextObject switch
            {
                Document acadDocument => acadDocument.TransactionManager,
                Database database => database.TransactionManager,
                _ => throw new ArgumentException("Must be a Document or Database.", nameof(context.ContextObject))
            };
        }
    }
}