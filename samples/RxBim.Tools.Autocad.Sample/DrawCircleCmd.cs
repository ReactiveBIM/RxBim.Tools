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
    [PublicAPI]
    public class DrawCircleCmd : RxBimCommand
    {
        /// <summary>
        /// Runs a command and returns the result of execution.
        /// </summary>
        /// <param name="database"><see cref="Database"/> instance.</param>
        /// <param name="transactionService"><see cref="ITransactionService"/> instance.</param>
        /// <param name="circleService"><see cref="ICircleService"/> instance.</param>
        public PluginResult ExecuteCommand(
            Database database,
            ITransactionService transactionService,
            ICircleService circleService)
        {
            if (!circleService.TryGetCircleParams(out var radius, out var center))
                return PluginResult.Cancelled;

            // Action with transaction param
            transactionService.RunInTransaction<DatabaseContext>((context, transaction) =>
                circleService.AddCircle(context, transaction, center, radius, 1));

            // Action with transaction param and context
            transactionService.RunInTransaction((context, transaction) => circleService.AddCircle(context,
                    transaction,
                    center.OffsetPoint(radius * 2, 0),
                    radius,
                    2),
                context: new DatabaseContext(database));

            // Func<T> with transaction param
            var id = transactionService.RunInTransaction<DatabaseContext, ObjectId>((context, transaction) =>
                circleService.AddCircle(context,
                    transaction,
                    center.OffsetPoint(radius * 6, 0),
                    radius,
                    4));

            return id.IsFullyValid() ? PluginResult.Succeeded : PluginResult.Failed;
        }
    }
}