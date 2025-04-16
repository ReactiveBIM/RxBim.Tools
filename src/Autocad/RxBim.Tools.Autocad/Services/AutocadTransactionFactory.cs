namespace RxBim.Tools.Autocad;

using System;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;

/// <inheritdoc />
[UsedImplicitly]
internal class AutocadTransactionFactory(IServiceProvider serviceProvider) : ITransactionFactory
{
    /// <inheritdoc />
    public ITransactionWrapper CreateTransaction<T>(T context, string? name = null)
        where T : class, ITransactionContextWrapper
    {
        var acadTransaction = context.GetTransactionManager().StartTransaction();
        return new TransactionWrapper(acadTransaction);
    }

    /// <inheritdoc />
    public ITransactionGroupWrapper CreateTransactionGroup<T>(T context, string? name = null)
        where T : class, ITransactionContextWrapper
    {
        var acadTransaction = context.GetTransactionManager().StartTransaction();
        return new TransactionGroupWrapper(acadTransaction);
    }

    /// <inheritdoc />
    public T GetDefaultContext<T>()
        where T : class, ITransactionContextWrapper
    {
        var contextService = serviceProvider.GetService<ITransactionContextService<T>>();
        return contextService.GetDefaultContext();
    }
}