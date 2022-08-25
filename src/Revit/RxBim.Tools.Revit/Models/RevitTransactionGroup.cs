namespace RxBim.Tools.Revit.Models
{
    using Autodesk.Revit.DB;

    /// <inheritdoc />
    internal class RevitTransactionGroup : ITransactionGroup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RevitTransactionGroup"/> class.
        /// </summary>
        /// <param name="transactionGroup"><see cref="TransactionGroup"/> instance.</param>
        public RevitTransactionGroup(TransactionGroup transactionGroup)
        {
            TransactionGroup = transactionGroup;
        }

        /// <summary>
        /// Revit transaction group.
        /// </summary>
        public TransactionGroup TransactionGroup { get; }

        /// <inheritdoc />
        public void Dispose()
        {
            TransactionGroup.Dispose();
        }

        /// <inheritdoc />
        public void Start()
        {
            TransactionGroup.Start();
        }

        /// <inheritdoc />
        public void RollBack()
        {
            TransactionGroup.RollBack();
        }

        /// <inheritdoc />
        public bool IsRolledBack()
        {
            return TransactionGroup.GetStatus() == TransactionStatus.RolledBack;
        }

        /// <inheritdoc />
        public void Assimilate()
        {
            TransactionGroup.Assimilate();
        }
    }
}