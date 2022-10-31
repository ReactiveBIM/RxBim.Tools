namespace RxBim.Tools.Revit.Models
{
    using System;
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
        public TransactionStatusEnum Status => (TransactionStatusEnum)Object.GetStatus();

        /// <inheritdoc />
        public void Dispose()
        {
            Object.Dispose();
        }

        /// <inheritdoc />
        public void Start()
        {
            if (Status == TransactionStatusEnum.Uninitialized)
                Object.Start();
        }

        /// <inheritdoc />
        public void RollBack()
        {
            if (Status != TransactionStatusEnum.RolledBack)
                Object.RollBack();
        }

        /// <inheritdoc />
        [Obsolete]
        public bool IsRolledBack()
        {
            return Object.GetStatus() == TransactionStatus.RolledBack;
        }

        /// <inheritdoc />
        public void Assimilate()
        {
            if (Status != TransactionStatusEnum.Committed)
                Object.Assimilate();
        }
    }
}