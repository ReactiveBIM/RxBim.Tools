namespace RxBim.Tools.Revit.TestablePlugin.Sample
{
    using Abstractions;
    using Autodesk.Revit.Attributes;
    using Autodesk.Revit.UI;
    using CSharpFunctionalExtensions;
    using RxBim.Command.Revit;
    using RxBim.Shared;

    /// <inheritdoc />
    [Regeneration(RegenerationOption.Manual)]
    [Transaction(TransactionMode.Manual)]
    public class Cmd : RxBimCommand
    {
        /// <summary>
        /// Execute command.
        /// </summary>
        /// <param name="collapseSteelConsumptionStatementService"><see cref="ICollapseSteelConsumptionStatementService"/></param>
        public PluginResult ExecuteCommand(
            ICollapseSteelConsumptionStatementService collapseSteelConsumptionStatementService)
        {
            collapseSteelConsumptionStatementService.Collapse()
                .OnFailure(err => TaskDialog.Show("Error", err));
            return PluginResult.Succeeded;
        }
    }
}
