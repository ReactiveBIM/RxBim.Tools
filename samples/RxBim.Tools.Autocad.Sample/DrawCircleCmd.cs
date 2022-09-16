namespace RxBim.Tools.Autocad.Sample
{
    using Abstractions;
    using Autodesk.AutoCAD.DatabaseServices;
    using Command.Autocad;
    using JetBrains.Annotations;
    using Shared;

    /// <summary>
    /// Command.
    /// </summary>
    [RxBimCommandClass("RxBimToolsSampleDrawCircle")]
    public class DrawCircleCmd : RxBimCommand
    {
        /// <summary>
        /// Runs a command and returns the result of execution.
        /// </summary>
        /// <param name="transactionService"><see cref="ITransactionService"/> instance.</param>
        /// <param name="database"><see cref="Database"/> instance.</param>
        /// <param name="circleService"><see cref="ICircleService"/> instance.</param>
        [UsedImplicitly]
        public PluginResult ExecuteCommand(
            ITransactionService transactionService,
            Database database,
            ICircleService circleService)
        {
            if (!circleService.TryGetCircleParams(out var radius, out var center))
                return PluginResult.Cancelled;

            // Action with transaction param and context
            transactionService.RunInTransaction<DatabaseWrapper>((context, transaction) =>
                circleService.AddCircle(context, transaction, center, radius, 1));

            // Func with transaction param and context
            var id = transactionService.RunInTransaction((context, transaction)
                    => circleService.AddCircle(context, transaction, center.OffsetPoint(radius * 2, 0), radius, 2),
                context: database.Wrap());

            return id.IsFullyValid() ? PluginResult.Succeeded : PluginResult.Failed;
        }
    }
}