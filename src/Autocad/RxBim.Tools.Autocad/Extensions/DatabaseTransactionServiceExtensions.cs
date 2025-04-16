namespace RxBim.Tools.Autocad;

using System;

/// <summary>
/// Extensions for <see cref="ITransactionService"/> for <see cref="DatabaseWrapper"/>.
/// </summary>
public static class DatabaseTransactionServiceExtensions
{
    /// <summary>
    /// <inheritdoc cref="ITransactionService.RunInTransaction{T}(Action{T}, string, T)" path="/summary" />
    /// </summary>
    /// <param name="transactionService"><see cref="ITransactionService"/> object.</param>
    /// <param name="action"><inheritdoc cref="ITransactionService.RunInTransaction{T}(Action{T}, string, T)" path="/param[@name='action']" /></param>
    /// <param name="context"><inheritdoc cref="ITransactionService.RunInTransaction{T}(Action{T}, string, T)" path="/param[@name='context']" /></param>
    public static void RunInDatabaseTransaction(
        this ITransactionService transactionService,
        Action action,
        IDatabaseWrapper? context = null)
    {
        transactionService.RunInTransaction(_ => action(), null, context);
    }

    /// <summary>
    /// <inheritdoc cref="ITransactionService.RunInTransaction{T}(Action{T, ITransactionWrapper}, string, T)" path="/summary" />
    /// </summary>
    /// <param name="transactionService"><see cref="ITransactionService"/> object.</param>
    /// <param name="action"><inheritdoc cref="ITransactionService.RunInTransaction{T}(Action{T, ITransactionWrapper}, string, T)" path="/param[@name='action']" /></param>
    /// <param name="context"><inheritdoc cref="ITransactionService.RunInTransaction{T}(Action{T, ITransactionWrapper}, string, T)" path="/param[@name='context']" /></param>
    public static void RunInDatabaseTransaction(
        this ITransactionService transactionService,
        Action<ITransactionWrapper> action,
        IDatabaseWrapper? context = null)
    {
        transactionService.RunInTransaction((_, x) => action(x), null, context);
    }

    /// <summary>
    /// <inheritdoc cref="ITransactionService.RunInTransaction{T}(Action{T, ITransactionWrapper}, string, T)" path="/summary" />
    /// </summary>
    /// <param name="transactionService"><see cref="ITransactionService"/> object.</param>
    /// <param name="action"><inheritdoc cref="ITransactionService.RunInTransaction{T}(Action{T, ITransactionWrapper}, string, T)" path="/param[@name='action']" /></param>
    /// <param name="context"><inheritdoc cref="ITransactionService.RunInTransaction{T}(Action{T, ITransactionWrapper}, string, T)" path="/param[@name='context']" /></param>
    public static void RunInDatabaseTransaction(
        this ITransactionService transactionService,
        Action<ITransactionContextWrapper, ITransactionWrapper> action,
        IDatabaseWrapper? context = null)
    {
        transactionService.RunInTransaction(action, null, context);
    }

    /// <summary>
    /// <inheritdoc cref="ITransactionService.RunInTransaction{T1,T2}(Func{T1,T2}, string, T1)" path="/summary" />
    /// </summary>
    /// <param name="transactionService"><see cref="ITransactionService"/> object.</param>
    /// <param name="func"><inheritdoc cref="ITransactionService.RunInTransaction{T1,T2}(Func{T1,T2}, string, T1)" path="/param[@name='func']" /></param>
    /// <param name="context"><inheritdoc cref="ITransactionService.RunInTransaction{T1,T2}(Func{T1,T2}, string, T1)" path="/param[@name='context']" /></param>
    /// <typeparam name="T"><inheritdoc cref="ITransactionService.RunInTransaction{T1,T2}(Func{T1,T2}, string, T1)" path="/typeparam[@name='TRes']" /></typeparam>
    public static T RunInDatabaseTransaction<T>(
        this ITransactionService transactionService,
        Func<T> func,
        IDatabaseWrapper? context = null)
    {
        return transactionService.RunInTransaction(_ => func(), null, context);
    }

    /// <summary>
    /// <inheritdoc cref="ITransactionService.RunInTransaction{T1,T2}(Func{T1,ITransactionWrapper,T2}, string, T1)" path="/summary" />
    /// </summary>
    /// <param name="transactionService"><see cref="ITransactionService"/> object.</param>
    /// <param name="func"><inheritdoc cref="ITransactionService.RunInTransaction{T1,T2}(Func{T1,ITransactionWrapper,T2}, string, T1)" path="/param[@name='func']" /></param>
    /// <param name="context"><inheritdoc cref="ITransactionService.RunInTransaction{T1,T2}(Func{T1,ITransactionWrapper,T2}, string, T1)" path="/param[@name='context']" /></param>
    /// <typeparam name="T"><inheritdoc cref="ITransactionService.RunInTransaction{T1,T2}(Func{T1,ITransactionWrapper,T2}, string, T1)" path="/typeparam[@name='TRes']" /></typeparam>
    public static T RunInDatabaseTransaction<T>(
        this ITransactionService transactionService,
        Func<ITransactionWrapper, T> func,
        IDatabaseWrapper? context = null)
    {
        return transactionService.RunInTransaction((_, x) => func(x), null, context);
    }

    /// <summary>
    /// <inheritdoc cref="ITransactionService.RunInTransaction{T1,T2}(Func{T1,ITransactionWrapper,T2}, string, T1)" path="/summary" />
    /// </summary>
    /// <param name="transactionService"><see cref="ITransactionService"/> object.</param>
    /// <param name="func"><inheritdoc cref="ITransactionService.RunInTransaction{T1,T2}(Func{T1,ITransactionWrapper,T2}, string, T1)" path="/param[@name='func']" /></param>
    /// <param name="context"><inheritdoc cref="ITransactionService.RunInTransaction{T1,T2}(Func{T1,ITransactionWrapper,T2}, string, T1)" path="/param[@name='context']" /></param>
    /// <typeparam name="T"><inheritdoc cref="ITransactionService.RunInTransaction{T1,T2}(Func{T1,ITransactionWrapper,T2}, string, T1)" path="/typeparam[@name='TRes']" /></typeparam>
    public static T RunInDatabaseTransaction<T>(
        this ITransactionService transactionService,
        Func<ITransactionContextWrapper, ITransactionWrapper, T> func,
        IDatabaseWrapper? context = null)
    {
        return transactionService.RunInTransaction(func, null, context);
    }

    /// <summary>
    /// <inheritdoc cref="ITransactionService.RunInTransactionGroup{T}(Action{T}, string, T)" path="/summary" />
    /// </summary>
    /// <param name="transactionService"><see cref="ITransactionService"/> object.</param>
    /// <param name="action"><inheritdoc cref="ITransactionService.RunInTransactionGroup{T}(Action{T}, string, T)" path="/param[@name='action']" /></param>
    /// <param name="context"><inheritdoc cref="ITransactionService.RunInTransactionGroup{T}(Action{T}, string, T)" path="/param[@name='context']" /></param>
    public static void RunInDatabaseTransactionGroup(
        this ITransactionService transactionService,
        Action action,
        IDatabaseWrapper? context = null)
    {
        transactionService.RunInTransactionGroup(_ => action(), string.Empty, context);
    }

    /// <summary>
    /// <inheritdoc cref="ITransactionService.RunInTransactionGroup{T}(Action{T,ITransactionGroupWrapper}, string, T)"  path="/summary" />
    /// </summary>
    /// <param name="transactionService"><see cref="ITransactionService"/> object.</param>
    /// <param name="action"><inheritdoc cref="ITransactionService.RunInTransactionGroup{T}(Action{T,ITransactionGroupWrapper}, string, T)" path="/param[@name='action']" /></param>
    /// <param name="context"><inheritdoc cref="ITransactionService.RunInTransactionGroup{T}(Action{T,ITransactionGroupWrapper}, string, T)" path="/param[@name='context']" /></param>
    public static void RunInDatabaseTransactionGroup(
        this ITransactionService transactionService,
        Action<ITransactionGroupWrapper> action,
        IDatabaseWrapper? context = null)
    {
        transactionService.RunInTransactionGroup((_, x) => action(x), string.Empty, context);
    }

    /// <summary>
    /// <inheritdoc cref="ITransactionService.RunInTransactionGroup{T1,T2}(Func{T1,T2}, string, T1)" path="/summary" />
    /// </summary>
    /// <param name="transactionService"><see cref="ITransactionService"/> object.</param>
    /// <param name="func"><inheritdoc cref="ITransactionService.RunInTransactionGroup{T1,T2}(Func{T1,T2}, string, T1)" path="/param[@name='func']" /></param>
    /// <param name="context"><inheritdoc cref="ITransactionService.RunInTransactionGroup{T1,T2}(Func{T1,T2}, string, T1)" path="/param[@name='context']" /></param>
    /// <typeparam name="T"><inheritdoc cref="ITransactionService.RunInTransactionGroup{T1,T2}(Func{T1,T2}, string, T1)" path="/typeparam[@name='TRes']" /></typeparam>
    public static T RunInDatabaseTransactionGroup<T>(
        this ITransactionService transactionService,
        Func<T> func,
        IDatabaseWrapper? context = null)
    {
        return transactionService.RunInTransactionGroup(_ => func(), string.Empty, context);
    }

    /// <summary>
    /// <inheritdoc cref="ITransactionService.RunInTransactionGroup{T1,T2}(Func{T1,ITransactionGroupWrapper,T2}, string, T1)" path="/summary"/>
    /// </summary>
    /// <param name="transactionService"><see cref="ITransactionService"/> object.</param>
    /// <param name="func"><inheritdoc cref="ITransactionService.RunInTransactionGroup{T1,T2}(Func{T1,ITransactionGroupWrapper,T2}, string, T1)" path="/param[@name='func']" /></param>
    /// <param name="context"><inheritdoc cref="ITransactionService.RunInTransactionGroup{T1,T2}(Func{T1,ITransactionGroupWrapper,T2}, string, T1)" path="/param[@name='context']" /></param>
    /// <typeparam name="T"><inheritdoc cref="ITransactionService.RunInTransactionGroup{T1,T2}(Func{T1,ITransactionGroupWrapper,T2}, string, T1)" path="/typeparam[@name='TRes']" /></typeparam>
    public static T RunInDatabaseTransactionGroup<T>(
        this ITransactionService transactionService,
        Func<ITransactionGroupWrapper, T> func,
        IDatabaseWrapper? context = null)
    {
        return transactionService.RunInTransactionGroup((_, x) => func(x), string.Empty, context);
    }
}