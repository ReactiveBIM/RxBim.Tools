namespace RxBim.Tools.Revit.Models
{
    using Autodesk.Revit.DB;

    /// <inheritdoc />
    internal class RevitTransaction : ITransaction
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RevitTransaction"/> class.
        /// </summary>
        /// <param name="transaction"><see cref="Transaction"/> instance.</param>
        /// <param name="context"><see cref="ITransactionContext"/> instance.</param>
        public RevitTransaction(Transaction transaction, ITransactionContext context)
        {
            Transaction = transaction;
            Context = context;
        }

        /// <summary>
        /// Revit transaction.
        /// </summary>
        public Transaction Transaction { get; }

        /// <inheritdoc />
        public ITransactionContext Context { get; }

        /// <inheritdoc />
        public void Dispose()
        {
            Transaction.Dispose();
        }

        /// <inheritdoc />
        public void Start()
        {
            Transaction.Start();
        }

        /// <inheritdoc />
        public void RollBack()
        {
            Transaction.RollBack();
        }

        /// <inheritdoc />
        public bool IsRolledBack()
        {
            return Transaction.GetStatus() == TransactionStatus.RolledBack;
        }

        /// <inheritdoc />
        public void Commit()
        {
            Transaction.Commit();
        }
    }
}