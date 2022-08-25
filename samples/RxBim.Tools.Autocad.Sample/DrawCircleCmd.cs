namespace RxBim.Tools.Autocad.Sample
{
    using Abstractions;
    using Command.Autocad;
    using JetBrains.Annotations;
    using Shared;

    /// <summary>
    /// Command.
    /// </summary>
    [RxBimCommandClass("RxBimToolsSampleDrawCircle")]
    [PublicAPI]
    public class DrawCircleCmd : RxBimCommand
    {
        /// <summary>
        /// Runs a command and returns the result of execution.
        /// </summary>
        /// <param name="transactionService"><see cref="ITransactionService"/> instance.</param>
        /// <param name="transactionContextService"><see cref="ITransactionContextService{T}"/> instance.</param>
        /// <param name="circleService"><see cref="ICircleService"/> instance.</param>
        public PluginResult ExecuteCommand(
            ITransactionService transactionService,
            ITransactionContextService<DatabaseContext> transactionContextService,
            ICircleService circleService)
        {
            if (!circleService.TryGetCircleParams(out var radius, out var center))
                return PluginResult.Cancelled;

            // Action with transaction param
            transactionService.RunInTransaction<DatabaseContext>((context, transaction) =>
                circleService.AddCircle(context, transaction, center, radius, 1));

            // Action with transaction param and context
            transactionService.RunInTransaction((context, transaction)
                    => circleService.AddCircle(context, transaction, center.OffsetPoint(radius * 2, 0), radius, 2),
                context: transactionContextService.GetDefaultContext());

            // Func<T> with transaction param
            var id = transactionService.RunInDatabaseTransaction(transaction =>
                circleService.AddCircle(transaction, center.OffsetPoint(radius * 6, 0), radius, 4));

            return id.IsFullyValid() ? PluginResult.Succeeded : PluginResult.Failed;
        }
    }
}