namespace RxBim.Tools.Revit.Models
{
    using Autodesk.Revit.DB;

    /// <inheritdoc cref="RxBim.Tools.ITransaction" />
    internal class RevitTransaction : Wrapper<Transaction>, ITransaction
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RevitTransaction"/> class.
        /// </summary>
        /// <param name="transaction"><see cref="Transaction"/> instance.</param>
        public RevitTransaction(Transaction transaction)
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