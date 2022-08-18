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
            transactionService.RunInTransaction(transaction =>
            {
                var cadTransaction = transaction.ToAcadTransaction();
                circleService.AddCircle(database, center, radius, cadTransaction, 1); // 1 - red
            });

            // Action<T> with transaction param
            transactionService.RunInTransaction(
                transaction =>
                {
                    var cadTransaction = transaction.ToAcadTransaction();
                    circleService.AddCircle(
                        database,
                        center.OffsetPoint(radius * 2, 0),
                        radius,
                        cadTransaction,
                        2); // 2 - yellow
                },
                transactionContext: database.ToTransactionContext());

            // Func without transaction param
            var id1 = transactionService.RunInTransaction(() =>
                circleService.AddCircle(center.OffsetPoint(radius * 4, 0), radius, 3));

            // Func<T> without transaction param
            var id2 = transactionService.RunInTransaction(transaction =>
            {
                var cadTransaction = transaction.ToAcadTransaction();
                return circleService.AddCircle(
                    database,
                    center.OffsetPoint(radius * 6, 0),
                    radius,
                    cadTransaction,
                    4); // 4 - cyan
            });

            return id1.IsFullyValid() && id2.IsFullyValid() ? PluginResult.Succeeded : PluginResult.Failed;
        }
    }
}