namespace RxBim.Tools.Revit.Models
{
    using Autodesk.Revit.DB;

    /// <inheritdoc cref="RxBim.Tools.ITransactionGroup" />
    internal class RevitTransactionGroup : Wrapper<TransactionGroup>, ITransactionGroup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RevitTransactionGroup"/> class.
        /// </summary>
        /// <param name="transactionGroup"><see cref="TransactionGroup"/> instance.</param>
        public RevitTransactionGroup(TransactionGroup transactionGroup)
            : base(transactionGroup)
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
        public void Assimilate()
        {
            Object.Assimilate();
        }
    }
}