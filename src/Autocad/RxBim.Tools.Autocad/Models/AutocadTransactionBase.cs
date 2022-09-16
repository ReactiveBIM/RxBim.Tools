namespace RxBim.Tools.Autocad
{
    using Autodesk.AutoCAD.DatabaseServices;

    /// <summary>
    /// Autocad transaction base.
    /// </summary>
    internal abstract class AutocadTransactionBase : Wrapper<Transaction>, ITransaction
    {
        private bool _isRolledBack;

        /// <summary>
        /// Initializes a new instance of the <see cref="AutocadTransactionBase"/> class.
        /// </summary>
        /// <param name="transaction"><see cref="Transaction"/> instance.</param>
        protected AutocadTransactionBase(Transaction transaction)
            : base(transaction)
        {
        }

        /// <inheritdoc/>
        public void Dispose() => Object.Dispose();

        /// <inheritdoc />
        public void Start()
        {
            // Started on creation.
        }

        /// <inheritdoc />
        public void RollBack()
        {
            Object.Abort();
            _isRolledBack = true;
        }

        /// <inheritdoc />
        public bool IsRolledBack() => _isRolledBack;

        /// <inheritdoc />
        public void Commit() => Object.Commit();
    }
}