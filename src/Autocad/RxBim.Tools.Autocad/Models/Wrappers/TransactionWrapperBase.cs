namespace RxBim.Tools.Autocad;

using System;
using Autodesk.AutoCAD.DatabaseServices;

/// <summary>
/// Autocad transaction base.
/// </summary>
internal abstract class TransactionWrapperBase(Transaction transaction)
    : Wrapper<Transaction>(transaction), ITransactionWrapper
{
    /// <inheritdoc />
    public TransactionStatusEnum Status { get; private set; }

    /// <inheritdoc/>
    public void Dispose() => Object.Dispose();

    /// <inheritdoc />
    public void Start()
    {
        // Started on creation.
        if (Status == TransactionStatusEnum.Uninitialized)
            Status = TransactionStatusEnum.Started;
    }

    /// <inheritdoc />
    public void RollBack()
    {
        if (Status == TransactionStatusEnum.RolledBack)
            return;

        Object.Abort();
        Status = TransactionStatusEnum.RolledBack;
    }

    /// <inheritdoc />
    [Obsolete]
    public bool IsRolledBack() => Status == TransactionStatusEnum.RolledBack;

    /// <inheritdoc />
    public void Commit()
    {
        if (Status == TransactionStatusEnum.Committed)
            return;

        Object.Commit();
        Status = TransactionStatusEnum.Committed;
    }
}