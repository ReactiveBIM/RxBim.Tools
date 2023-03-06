namespace RxBim.Tools
{
    using System;
    using JetBrains.Annotations;

    /// <summary>
    /// Represents base type for objects in log storage.
    /// </summary>
    public interface IMessageData : IComparable
    {
        /// <summary>
        /// The object used for the description.
        /// </summary>
        [UsedImplicitly]
        object DescriptionObject { get; }

        /// <summary>
        /// Returns the ID of the object.
        /// </summary>
        [UsedImplicitly]
        IObjectIdWrapper GetId();
    }
}
