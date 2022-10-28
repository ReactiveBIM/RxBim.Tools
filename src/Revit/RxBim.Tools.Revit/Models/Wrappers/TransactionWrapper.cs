namespace RxBim.Tools.Revit.Models
{
    using System;
    using Autodesk.Revit.DB;

    /// <inheritdoc cref="ITransactionWrapper" />
    internal class TransactionWrapper : Wrapper<Transaction>, ITransactionWrapper
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionWrapper"/> class.
        /// </summary>
        /// <param name="transaction"><see cref="Transaction"/> instance.</param>
        public TransactionWrapper(Transaction transaction)
            : base(transaction)
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
            Object.RollBack();
        }

        /// <inheritdoc />
        [Obsolete]
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