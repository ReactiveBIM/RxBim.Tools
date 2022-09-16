namespace RxBim.Tools
{
    /// <summary>
    /// Service for <see cref="ITransactionContext"/>.
    /// </summary>
    /// <typeparam name="T">Type of context.</typeparam>
    public interface ITransactionContextService<out T>
        where T : ITransactionContext
    {
        /// <summary>
        /// Returns default context.
        /// </summary>
        T GetDefaultContext();
    }
}