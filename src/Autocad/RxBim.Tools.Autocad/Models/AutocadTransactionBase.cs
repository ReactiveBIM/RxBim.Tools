namespace RxBim.Tools.Autocad
{
    using Autodesk.AutoCAD.DatabaseServices;

    /// <summary>
    /// Autocad transaction base.
    /// </summary>
    internal abstract class AutocadTransactionBase : ITransaction
    {
        private bool _isRolledBack;

        /// <summary>
        /// Initializes a new instance of the <see cref="AutocadTransactionBase"/> class.
        /// </summary>
        /// <param name="transaction"><see cref="Transaction"/> instance.</param>
        protected AutocadTransactionBase(Transaction transaction)
        {
            Transaction = transaction;
        }

        /// <summary>
        /// Autocad transaction object.
        /// </summary>
        public Transaction Transaction { get; }

        /// <inheritdoc/>
        public void Dispose() => Transaction.Dispose();

        /// <inheritdoc />
        public void Start()
        {
            // Started on creation.
        }

        /// <inheritdoc />
        public void RollBack()
        {
            Transaction.Abort();
            _isRolledBack = true;
        }

        /// <inheritdoc />
        public bool IsRolledBack() => _isRolledBack;

        /// <inheritdoc />
        public void Commit() => Transaction.Commit();
    }
}