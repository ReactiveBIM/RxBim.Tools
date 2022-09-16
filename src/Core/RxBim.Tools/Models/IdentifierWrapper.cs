namespace RxBim.Tools
{
    /// <inheritdoc cref="IIdentifierWrapper" />
    public abstract class IdentifierWrapper<T> : Wrapper<T>, IIdentifierWrapper
    {
        /// <inheritdoc />
        protected IdentifierWrapper(T wrappedObject)
            : base(wrappedObject)
        {
        }
    }
}