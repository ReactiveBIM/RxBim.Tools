namespace RxBim.Tools.Autocad
{
    using System;
    using Autodesk.AutoCAD.DatabaseServices;

    /// <summary>
    /// Autocad transaction base.
    /// </summary>
    internal abstract class AutocadTransactionBase : ITransaction
    {
        private readonly Transaction _transaction;
        private bool _isRolledBack;

        /// <summary>
        /// Initializes a new instance of the <see cref="AutocadTransactionBase"/> class.
        /// </summary>
        /// <param name="transaction"><see cref="Transaction"/> instance.</param>
        protected AutocadTransactionBase(Transaction transaction)
        {
            _transaction = transaction;
        }

        /// <inheritdoc/>
        public void Dispose() => _transaction.Dispose();

        /// <inheritdoc />
        public void Start()
        {
            // Started on creation.
        }

        /// <inheritdoc />
        public void RollBack()
        {
            _transaction.Abort();
            _isRolledBack = true;
        }

        /// <inheritdoc />
        public bool IsRolledBack() => _isRolledBack;

        /// <inheritdoc />
        public void Commit() => _transaction.Commit();

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