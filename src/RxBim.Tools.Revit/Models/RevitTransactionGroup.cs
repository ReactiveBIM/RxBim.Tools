namespace RxBim.Tools.Revit.Models
{
    using Autodesk.Revit.DB;

    /// <inheritdoc />
    internal class RevitTransactionGroup : ITransactionGroup
    {
        private readonly TransactionGroup _transactionGroup;

        /// <summary>
        /// Initializes a new instance of the <see cref="RevitTransactionGroup"/> class.
        /// </summary>
        /// <param name="transactionGroup"><see cref="TransactionGroup"/> instance.</param>
        public RevitTransactionGroup(TransactionGroup transactionGroup)
        {
            _transactionGroup = transactionGroup;
        }

        /// <inheritdoc />
        public void Dispose()
        {
            _transactionGroup.Dispose();
        }

        /// <inheritdoc />
        public void Start()
        {
            _transactionGroup.Start();
        }

        /// <inheritdoc />
        public void RollBack()
        {
            _transactionGroup.RollBack();
        }

        /// <inheritdoc />
        public bool IsRolledBack()
        {
            return _transactionGroup.GetStatus() == TransactionStatus.RolledBack;
        }

        /// <inheritdoc />
        public void Assimilate()
        {
            _transactionGroup.Assimilate();
        }
    }
}