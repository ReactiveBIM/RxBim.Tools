namespace RxBim.Tools.Revit.TestablePlugin.Sample.Services
{
    using System.Collections.Generic;
    using Abstractions;
    using Autodesk.Revit.DB;
    using CSharpFunctionalExtensions;
    using Extensions;
    using JetBrains.Annotations;
    using Models;

    /// <inheritdoc />
    [UsedImplicitly]
    public class CollapseSteelConsumptionStatementService : ICollapseSteelConsumptionStatementService
    {
        private readonly ITransactionService _transactionService;
        private readonly IDocumentsCollector _documentsCollector;
        private readonly PluginSettings _pluginSettings;

        /// <summary>
        /// Create instance of <see cref="CollapseSteelConsumptionStatementService"/>.
        /// </summary>
        /// <param name="transactionService"><see cref="ITransactionService"/></param>
        /// <param name="documentsCollector"><see cref="IDocumentsCollector"/></param>
        /// <param name="pluginSettings"><see cref="PluginSettings"/></param>
        public CollapseSteelConsumptionStatementService(
            ITransactionService transactionService,
            IDocumentsCollector documentsCollector,
            PluginSettings pluginSettings)
        {
            _transactionService = transactionService;
            _documentsCollector = documentsCollector;
            _pluginSettings = pluginSettings;
        }

        /// <inheritdoc />
        public Result Collapse()
        {
            var doc = _documentsCollector.GetCurrentDocument();
            var viewSchedule = doc.ActiveView as IViewScheduleWrapper;

            return Result.SuccessIf(
                viewSchedule is not null && viewSchedule.Name.Contains(_pluginSettings.ScheduleName),
                $"Open specification with name contains \"{_pluginSettings.ScheduleName}\"")
                .Map(() => FindCell(viewSchedule!.Definition))
                .Ensure(
                    foundCell => foundCell.BorderCell is not 0,
                    $"Detection boundary not found (cell starting with \"{_pluginSettings.CellFoundSymbol}\")")
                .Tap(foundCell => _transactionService.RunInTransaction(
                    _ => CollapseInternal(doc, viewSchedule!, foundCell),
                    "Collapse steel consumption statement"));
        }

        private FoundCellParameters FindCell(IScheduleDefinitionWrapper scheduleDefinition)
        {
            var firstWeightCell = 0;
            var startHiddenFields = 0;
            var borderCell = 0;

            // define first and last cells with weight
            for (var i = 0; i < scheduleDefinition.FieldCount; i++)
            {
                var scheduleField = scheduleDefinition.GetField(i);
                var cellName = scheduleField.Name;
                if (firstWeightCell is 0)
                {
                    if (char.IsNumber(cellName[0]))
                    {
                        firstWeightCell = i;
                    }
                    else
                    {
                        if (scheduleField.IsHidden)
                            startHiddenFields++;
                    }
                }

                if (!cellName.StartsWith(_pluginSettings.CellFoundSymbol))
                    continue;
                
                borderCell = i;
                break;
            }

            return new FoundCellParameters
            {
                FirstWeightCell = firstWeightCell,
                StartHiddenFields = startHiddenFields,
                BorderCell = borderCell
            };
        }

        private void CollapseInternal(
            IDocumentWrapper doc,
            IViewScheduleWrapper viewSchedule,
            FoundCellParameters foundCell)
        {
            var scheduleDefinition = viewSchedule.Definition;
            
            // in first hidden all cells
            for (var i = foundCell.FirstWeightCell; i < foundCell.BorderCell; i++)
            {
                var scheduleField = scheduleDefinition.GetField(i);
                scheduleField.IsHidden = false;
            }

            // need for update table
            doc.Regenerate();

            var tableData = viewSchedule.TableData;
            var tableSectionData = tableData.GetSectionData(SectionType.Body)!;
            var firstRowNumber = tableSectionData.FirstRowNumber;
            var lastRowNumber = tableSectionData.LastRowNumber;

            for (var i = foundCell.FirstWeightCell; i < foundCell.BorderCell; i++)
            {
                var scheduleField = scheduleDefinition.GetField(i);

                var values = new List<string>();
                for (var j = firstRowNumber; j <= lastRowNumber; j++)
                {
                    var cellText = tableSectionData.GetCellText(j, i - foundCell.StartHiddenFields);
                    values.Add(cellText);
                }

                if (values.IsOnlyTextAndZeros()
                    || values.IsOnlyEmptyStrings())
                {
                    scheduleField.IsHidden = true;
                }
            }
        }
    }
}
