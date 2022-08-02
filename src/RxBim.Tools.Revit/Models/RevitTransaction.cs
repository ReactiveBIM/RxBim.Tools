namespace RxBim.Tools.Revit.Models
{
    using System;
    using Autodesk.Revit.DB;

    /// <inheritdoc />
    internal class RevitTransaction : ITransaction
    {
        private readonly Transaction _transaction;

        /// <summary>
        /// Initializes a new instance of the <see cref="RevitTransaction"/> class.
        /// </summary>
        /// <param name="transaction"><see cref="Transaction"/> instance.</param>
        public RevitTransaction(Transaction transaction)
        {
            _transaction = transaction;
        }

        /// <inheritdoc />
        public void Dispose()
        {
            _transaction.Dispose();
        }

        /// <inheritdoc />
        public void Start()
        {
            _transaction.Start();
        }

        /// <inheritdoc />
        public void RollBack()
        {
            _transaction.RollBack();
        }

        /// <inheritdoc />
        public bool IsRolledBack()
        {
            return _transaction.GetStatus() == TransactionStatus.RolledBack;
        }

        /// <inheritdoc />
        public void Commit()
        {
            _transaction.Commit();
        }

        /// <inheritdoc />
        public T GetCadTransaction<T>()
            where T : class
        {
            return _transaction as T ??
                   throw new InvalidCastException(
                       $"Can't convert transaction type {_transaction.GetType().Name} to {typeof(T).Name}");
        }
    }
}