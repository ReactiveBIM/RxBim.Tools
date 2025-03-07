﻿namespace RxBim.Tools.Autocad;

using Autodesk.AutoCAD.DatabaseServices;

/// <summary>
/// Extensions for <see cref="ITransactionWrapper"/>.
/// </summary>
public static class TransactionExtensions
{
    /// <summary>
    /// Appends new <see cref="Entity"/> to current space.
    /// </summary>
    /// <param name="transactionWrapper"><see cref="ITransactionWrapper"/> object.</param>
    /// <param name="context"><see cref="ITransactionContextWrapper"/> object.</param>
    /// <param name="entity">New entity.</param>
    public static ObjectId AppendToCurrentSpace(this ITransactionWrapper transactionWrapper, ITransactionContextWrapper context, Entity entity)
    {
        var acadTransaction = transactionWrapper.Unwrap<Transaction>();
        var database = context.GetDatabase();
        var blockTableRecord = acadTransaction.GetObjectAs<BlockTableRecord>(database.CurrentSpaceId, true);
        var id = blockTableRecord.AppendEntity(entity);
        acadTransaction.AddNewlyCreatedDBObject(entity, true);
        return id;
    }

    /// <summary>
    /// Returns an object opened using a transaction and cast to the given type
    /// </summary>
    /// <param name="transactionWrapper"><see cref="ITransactionWrapper"/> object.</param>
    /// <param name="id">Object ID.</param>
    /// <param name="forWrite">If true, the object is opened for writing. If false, opens for reading.</param>
    /// <param name="openErased">
    /// If true, it is allowed to open an object that has been deleted.
    /// If false, objects that have been deleted cannot be opened.
    /// </param>
    /// <param name="forceOpenOnLockedLayer">
    /// If true, the object on a locked layer can be opened.
    /// If false, the object on a locked layer cannot be opened.
    /// </param>
    /// <typeparam name="T">Object type.</typeparam>
    public static T GetObjectAs<T>(
        this ITransactionWrapper transactionWrapper,
        ObjectId id,
        bool forWrite = false,
        bool openErased = false,
        bool forceOpenOnLockedLayer = true)
        where T : DBObject
    {
        return transactionWrapper.Unwrap<Transaction>().GetObjectAs<T>(id, forWrite, openErased, forceOpenOnLockedLayer);
    }
}