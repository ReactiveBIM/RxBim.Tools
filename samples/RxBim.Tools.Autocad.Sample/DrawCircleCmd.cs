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
            transactionService.RunInTransaction((transaction, _) =>
            {
                var cadTransaction = transaction.GetCadTransaction<Transaction>();
                circleService.AddCircle(database, center, radius, cadTransaction, 1); // 1 - red
            });

            // Action<T> with transaction param
            transactionService.RunInTransaction((transaction, _) =>
            {
                var cadTransaction = transaction.GetCadTransaction<Transaction>();
                return circleService.AddCircle(database, center.OffsetPoint(radius * 2, 0), radius, cadTransaction, 2); // 2 - yellow
            });

            // Action without transaction param
            transactionService.RunInTransaction(_ =>
            {
                circleService.AddCircle(center.OffsetPoint(radius * 4, 0), radius, 3); // 3 - green
            });

            // Action<T> without transaction param
            var id = transactionService.RunInTransaction(_ =>
                circleService.AddCircle(center.OffsetPoint(radius * 6, 0), radius, 4)); // 4 - cyan

            return PluginResult.Succeeded;
        }
    }
}