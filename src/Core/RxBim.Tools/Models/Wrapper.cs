namespace RxBim.Tools
{
    using System;
    using JetBrains.Annotations;

    /// <summary>
    /// <see cref="IWrapper"/> realisation.
    /// </summary>
    [PublicAPI]
    public abstract class Wrapper<T> : IWrapper
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Wrapper{T}"/> class.
        /// </summary>
        /// <param name="wrappedObject">Wrapped object.</param>
        protected Wrapper(T wrappedObject)
        {
            Object = wrappedObject;
        }

        /// <summary>
        /// Wrapped object.
        /// </summary>
        public T Object { get; }

        /// <inheritdoc />
        public TWrap Unwrap<TWrap>()
        {
            return Object is TWrap obj
                ? obj
                : throw new InvalidCastException($"Can't cast wrapped object {typeof(T)} to {typeof(TWrap)}");
        }
    }
}