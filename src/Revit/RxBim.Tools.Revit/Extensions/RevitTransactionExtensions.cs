namespace RxBim.Tools.Revit
{
    using System;
    using Autodesk.Revit.DB;
    using JetBrains.Annotations;

    /// <summary>
    /// Extensions for <see cref="ITransaction"/>.
    /// </summary>
    [PublicAPI]
    public static class RevitTransactionExtensions
    {
        /// <summary>
        /// Returns <see cref="Transaction"/> object from <see cref="ITransaction"/> object.
        /// </summary>
        /// <param name="transaction"><see cref="ITransaction"/> object.</param>
        /// <returns></returns>
        /// <exception cref="InvalidCastException"><paramref name="transaction"/> is not <see cref="RevitTransaction"/>.</exception>
        public static Transaction ToRvtTransaction(this ITransaction transaction)
        {
            return (transaction as RevitTransaction)?.Transaction ??
                   throw new InvalidCastException(
                       $"Can't convert transaction type {transaction.GetType().FullName} to {typeof(Transaction).FullName}");
        }

        /// <summary>
        /// Returns <see cref="TransactionGroup"/> object from <see cref="ITransactionGroup"/> object.
        /// </summary>
        /// <param name="transactionGroup"><see cref="ITransactionGroup"/> object.</param>
        /// <returns></returns>
        /// <exception cref="InvalidCastException"><paramref name="transactionGroup"/> is not <see cref="RevitTransactionGroup"/>.</exception>
        public static TransactionGroup ToRvtTransactionGroup(this ITransactionGroup transactionGroup)
        {
            return (transactionGroup as RevitTransactionGroup)?.TransactionGroup ??
                   throw new InvalidCastException(
                       $"Can't convert transaction group type {transactionGroup.GetType().FullName} to {typeof(TransactionGroup).FullName}");
        }
    }
}