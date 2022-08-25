namespace RxBim.Tools.Autocad.Sample
{
    using Abstractions;
    using Autodesk.AutoCAD.ApplicationServices.Core;
    using Autodesk.AutoCAD.DatabaseServices;
    using Command.Autocad;
    using JetBrains.Annotations;
    using Shared;

    /// <summary>
    /// Command.
    /// </summary>
    [RxBimCommandClass("RxBimToolsSampleGetLayer")]
    [PublicAPI]
    public class GetLayerCmd : RxBimCommand
    {
        /// <summary>
        /// Runs a command and returns the result of execution.
        /// </summary>
        /// <param name="entityService"><see cref="IEntityService"/> instance.</param>
        /// <param name="transactionService"><see cref="ITransactionService"/> instance.</param>
        public PluginResult ExecuteCommand(IEntityService entityService, ITransactionService transactionService)
        {
            if (!entityService.GetEntity(out var entityId))
                return PluginResult.Cancelled;

            var layerName = transactionService.RunInDocumentTransaction(() =>
            {
                var entity = entityId.GetObjectAs<Entity>();
                return entity.Layer;
            });

            var layerColorIndex = transactionService.RunInDatabaseTransaction(transaction =>
            {
                var entity = transaction.GetObjectAs<Entity>(entityId);
                return entity.ColorIndex;
            });

            Application.ShowAlertDialog($"Object layer is '{layerName}', color is: {layerColorIndex}");
            return PluginResult.Succeeded;
        }
    }
}