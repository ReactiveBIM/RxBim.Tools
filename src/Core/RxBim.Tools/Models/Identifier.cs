namespace RxBim.Tools
{
    /// <inheritdoc cref="IIdentifier" />
    public abstract class Identifier<T> : Wrapper<T>, IIdentifier
    {
        /// <inheritdoc />
        protected Identifier(T wrappedObject)
            : base(wrappedObject)
        {
        }
    }
}