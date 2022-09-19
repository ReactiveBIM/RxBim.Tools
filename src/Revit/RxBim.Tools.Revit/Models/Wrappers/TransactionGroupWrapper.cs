namespace RxBim.Tools.Revit.Models
{
    using Autodesk.Revit.DB;

    /// <inheritdoc cref="ITransactionGroupWrapper" />
    internal class TransactionGroupWrapper : Wrapper<TransactionGroup>, ITransactionGroupWrapper
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionGroupWrapper"/> class.
        /// </summary>
        /// <param name="transactionGroup"><see cref="TransactionGroup"/> instance.</param>
        public TransactionGroupWrapper(TransactionGroup transactionGroup)
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