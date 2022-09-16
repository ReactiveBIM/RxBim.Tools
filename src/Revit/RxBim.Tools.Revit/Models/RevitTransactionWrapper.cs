namespace RxBim.Tools.Revit.Models
{
    using Autodesk.Revit.DB;

    /// <inheritdoc cref="ITransactionWrapper" />
    internal class RevitTransactionWrapper : Wrapper<Transaction>, ITransactionWrapper
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RevitTransactionWrapper"/> class.
        /// </summary>
        /// <param name="transaction"><see cref="Transaction"/> instance.</param>
        public RevitTransactionWrapper(Transaction transaction)
            : base(transaction)
        {
        }

        /// <inheritdoc />
        public void Dispose()
        {
            Object.Dispose();
        }

        /// <inheritdoc />
        public void Start()
        {
            Object.Start();
        }

        /// <inheritdoc />
        public void RollBack()
        {
            Object.RollBack();
        }

        /// <inheritdoc />
        public bool IsRolledBack()
        {
            return Object.GetStatus() == TransactionStatus.RolledBack;
        }

        /// <inheritdoc />
        public void Commit()
        {
            Object.Commit();
        }
    }
}