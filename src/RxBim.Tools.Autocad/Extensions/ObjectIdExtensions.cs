namespace RxBim.Tools.Autocad
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Autodesk.AutoCAD.DatabaseServices;
    using Autodesk.AutoCAD.Runtime;

    /// <summary>
    /// Extensions for <see cref="ObjectId"/>
    /// </summary>
    public static class ObjectIdExtensions
    {
        /// <summary>
        /// Returns true if the object identifier is fully valid: the object exists in the database and has not been deleted.
        /// </summary>
        /// <param name="id">Identifier</param>
        public static bool IsFullyValid(this ObjectId id)
        {
            return id.IsValid && !id.IsNull && !id.IsErased && !id.IsEffectivelyErased;
        }

        /// <summary>
        /// Returns true if the object matches the given type.
        /// </summary>
        /// <typeparam name="T">Object type</typeparam>
        /// <param name="id">Identifier</param>
        public static bool Is<T>(this ObjectId id)
            where T : DBObject
        {
            if (!id.IsValid)
                return false;

            RXClass
                rxClass = RXObject.GetClass(typeof(T)),
                objClass = id.ObjectClass;

            return objClass.Equals(rxClass)
                   || objClass.IsDerivedFrom(rxClass);
        }

        /// <summary>
        /// Returns an object opened without using a transaction and cast to the given type
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <param name="forWrite">Open for writing</param>
        /// <param name="openErased">Open even if object is deleted</param>
        /// <param name="forceOpenOnLockedLayer">Open even if the object is on a frozen layer</param>
        /// <typeparam name="T">Object type</typeparam>
        /// <exception cref="Exception">If the object does not match the specified type</exception>
        public static T OpenAs<T>(
            this ObjectId id,
            bool forWrite = false,
            bool openErased = false,
            bool forceOpenOnLockedLayer = true)
            where T : DBObject
        {
            if (id.Is<T>())
            {
#pragma warning disable 618
                return (T)id.Open(
                    forWrite ? OpenMode.ForWrite : OpenMode.ForRead,
                    openErased,
                    forceOpenOnLockedLayer);
#pragma warning restore 618
            }

            throw new Exception(ErrorStatus.WrongObjectType, $"Объект не является типом {typeof(T)}");
        }

        /// <summary>
        /// Try returns an object opened without using a transaction and cast to the given type
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <param name="forWrite">Open for writing</param>
        /// <param name="openErased">Open even if object is deleted</param>
        /// <param name="forceOpenOnLockedLayer">Open even if the object is on a frozen layer</param>
        /// <typeparam name="T">Object type</typeparam>
        public static T? TryOpenAs<T>(
            this ObjectId id,
            bool forWrite = false,
            bool openErased = false,
            bool forceOpenOnLockedLayer = true)
            where T : DBObject
        {
#pragma warning disable 618
            var dbObject = id.Open(
                forWrite ? OpenMode.ForWrite : OpenMode.ForRead,
                openErased,
                forceOpenOnLockedLayer);
#pragma warning restore 618

            if (dbObject is T t)
                return t;

            dbObject.Dispose();
            return null;
        }

        /// <summary>
        /// Returns an object opened using objects and bordering on the given level.
        /// For the method to work, a transaction must be started!
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <param name="forWrite">Open for writing</param>
        /// <param name="openErased">Open even if object is deleted</param>
        /// <param name="forceOpenOnLockedLayer">Open even if the object is on a frozen layer</param>
        /// <typeparam name="T">Object type</typeparam>
        /// <exception cref="Exception">If the object does not match the specified type</exception>
        public static T GetObjectAs<T>(
            this ObjectId id,
            bool forWrite = false,
            bool openErased = false,
            bool forceOpenOnLockedLayer = true)
            where T : DBObject
        {
            if (id.Is<T>())
            {
                return (T)id.GetObject(
                    forWrite ? OpenMode.ForWrite : OpenMode.ForRead,
                    openErased,
                    forceOpenOnLockedLayer);
            }

            throw new Exception(ErrorStatus.WrongObjectType, $"Object is not a type {typeof(T)}");
        }

        /// <summary>
        /// Try returns an object opened using a transaction and cast to the given type.
        /// For the method to work, a transaction must be started!
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <param name="dbObject">Opened object</param>
        /// <param name="forWrite">Open for writing</param>
        /// <param name="forceOpenOnLockedLayer">Open even if the object is on a frozen layer</param>
        /// <typeparam name="T">Object type</typeparam>
        public static bool TryGetObjectAs<T>(
            this ObjectId id,
            out T dbObject,
            bool forWrite = false,
            bool forceOpenOnLockedLayer = true)
            where T : DBObject
        {
            if (!id.IsFullyValid())
            {
                dbObject = null!;
                return false;
            }

            dbObject = (id.GetObject(
                forWrite ? OpenMode.ForWrite : OpenMode.ForRead,
                false,
                forceOpenOnLockedLayer) as T)!;
            return dbObject != null!;
        }

        /// <summary>
        /// Returns objects of the specified type opened using a transaction.
        /// </summary>
        /// <param name="ids">Object identifier</param>
        /// <param name="forWrite">Open for writing</param>
        /// <param name="onLockedLayer">Open even if the object is on a frozen layer</param>
        /// <typeparam name="T">Object type</typeparam>
        public static IEnumerable<T> GetObjectsOf<T>(
            this IEnumerable ids,
            bool forWrite = false,
            bool onLockedLayer = true)
            where T : DBObject
        {
            return ids
                .Cast<ObjectId>()
                .Select(id =>
                {
                    id.TryGetObjectAs<T>(out var dbObject, forWrite, onLockedLayer);
                    return dbObject;
                })
                .Where(t => t != null);
        }
    }
}