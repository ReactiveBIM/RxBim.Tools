namespace RxBim.Tools;

using System;
using JetBrains.Annotations;

/// <inheritdoc />
[UsedImplicitly]
internal class TransactionService : ITransactionService
{
    private readonly ITransactionFactory _transactionFactory;

    /// <summary>
    /// Initializes a new instance of the <see cref="TransactionService"/> class.
    /// </summary>
    /// <param name="transactionFactory"><see cref="ITransactionFactory"/>.</param>
    public TransactionService(ITransactionFactory transactionFactory)
    {
        _transactionFactory = transactionFactory;
    }

    /// <inheritdoc />
    public void RunInTransaction<T>(Action<T> action, string? name = null, T? context = null)
        where T : class, ITransactionContextWrapper
    {
        RunInTransaction(action.ToFunc(), name, context);
    }

    /// <inheritdoc />
    public void RunInTransaction<T>(Action<T, ITransactionWrapper> action, string? name = null, T? context = null)
        where T : class, ITransactionContextWrapper
    {
        RunInTransaction(action.ToFunc(), name, context);
    }

    /// <inheritdoc />
    public TRes RunInTransaction<TContext, TRes>(
        Func<TContext, TRes> func,
        string? name = null,
        TContext? context = null)
        where TContext : class, ITransactionContextWrapper
    {
        var transactionContext = context ?? _transactionFactory.GetDefaultContext<TContext>();
        return RunInTransaction((_, _) => func(transactionContext), name, transactionContext);
    }

    /// <inheritdoc />
    public TRes RunInTransaction<TContext, TRes>(
        Func<TContext, ITransactionWrapper, TRes> func,
        string? name = null,
        TContext? context = null)
        where TContext : class, ITransactionContextWrapper
    {
        var transactionContext = context ?? _transactionFactory.GetDefaultContext<TContext>();
        using var transaction = _transactionFactory.CreateTransaction(transactionContext, name);
        try
        {
            transaction.Start();
            var result = func.Invoke(transactionContext, transaction);

            if (transaction.Status is not TransactionStatusEnum.Committed and not TransactionStatusEnum.RolledBack)
                transaction.Commit();

            return result;
        }
        catch (Exception)
        {
            if (transaction.Status != TransactionStatusEnum.RolledBack)
                transaction.RollBack();
            throw;
        }
    }

    /// <inheritdoc />
    public void RunInTransactionGroup<T>(
        Action<T> action,
        string name,
        T? transactionContext = null)
        where T : class, ITransactionContextWrapper
    {
        RunInTransactionGroup(action.ToFunc(), name, transactionContext);
    }

    /// <inheritdoc />
    public void RunInTransactionGroup<T>(Action<T, ITransactionGroupWrapper> action, string name, T? context = default(T?))
        where T : class, ITransactionContextWrapper
    {
        RunInTransactionGroup(action.ToFunc(), name, context);
    }

    /// <inheritdoc />
    public TRes RunInTransactionGroup<TContext, TRes>(
        Func<TContext, TRes> func,
        string name,
        TContext? context = null)
        where TContext : class, ITransactionContextWrapper
    {
        var transactionContext = context ?? _transactionFactory.GetDefaultContext<TContext>();
        return RunInTransactionGroup((_, _) => func(transactionContext), name, transactionContext);
    }

    /// <inheritdoc />
    public TRes RunInTransactionGroup<TContext, TRes>(
        Func<TContext, ITransactionGroupWrapper, TRes> func,
        string name,
        TContext? context = null)
        where TContext : class, ITransactionContextWrapper
    {
        var transactionContext = context ?? _transactionFactory.GetDefaultContext<TContext>();
        using var transactionGroup =
            _transactionFactory.CreateTransactionGroup(transactionContext, name);
        try
        {
            transactionGroup.Start();
            var result = func.Invoke(transactionContext, transactionGroup);

            if (transactionGroup.Status is not TransactionStatusEnum.Committed and
                not TransactionStatusEnum.RolledBack)
                transactionGroup.Assimilate();

            return result;
        }
        catch (Exception)
        {
            if (transactionGroup.Status != TransactionStatusEnum.RolledBack)
                transactionGroup.RollBack();
            throw;
        }
    }
}