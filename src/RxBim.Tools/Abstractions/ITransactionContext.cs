namespace RxBim.Tools
{
    /// <summary>
    /// Transaction context.
    /// </summary>
    public interface ITransactionContext
    {
        /// <summary>
        /// Transaction context object.
        /// </summary>
        object ContextObject { get; }
    }
}