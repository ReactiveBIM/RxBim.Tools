namespace RxBim.Tools
{
    /// <summary>
    /// Service for <see cref="ITransactionContextWrapper"/>.
    /// </summary>
    /// <typeparam name="T">Type of context.</typeparam>
    public interface ITransactionContextService<out T>
        where T : ITransactionContextWrapper
    {
        /// <summary>
        /// Returns default context.
        /// </summary>
        T GetDefaultContext();
    }
}