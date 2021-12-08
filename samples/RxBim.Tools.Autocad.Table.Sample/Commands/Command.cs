namespace RxBim.Tools.Autocad.Table.Sample.Commands
{
    using System.Collections.Generic;
    using System.Linq;
    using Abstractions;
    using Autocad.Abstractions;
    using Autodesk.AutoCAD.ApplicationServices;
    using Autodesk.AutoCAD.DatabaseServices;
    using Autodesk.AutoCAD.EditorInput;
    using CSharpFunctionalExtensions;
    using Extensions;
    using RxBim.Command.Autocad;
    using Serializers;
    using Shared;
    using TableBuilder.Abstractions;
    using TableBuilder.Extensions;
    using Result = CSharpFunctionalExtensions.Result;

    /// <inheritdoc />
    [RxBimCommandClass("RxBimTableSample")]
    public class Command : RxBimCommand
    {
        private IObjectsSelectionService _selectionService;
        private ICommandLineService _commandLineService;

        /// <summary>
        /// Command execution
        /// </summary>
        /// <param name="tableDataService"><see cref="ITableDataService"/> object.</param>
        /// <param name="tableSerializer"><see cref="ITableSerializer{T1, T2}"/> object.</param>
        /// <param name="selectionService"><see cref="IObjectsSelectionService"/> object.</param>
        /// <param name="commandLineService"><see cref="ICommandLineService"/> object.</param>
        /// <param name="doc"><see cref="Document"/> object.</param>>
        public PluginResult ExecuteCommand(
            ITableDataService tableDataService,
            ITableSerializer<AutocadTableSerializerParameters, Table> tableSerializer,
            IObjectsSelectionService selectionService,
            ICommandLineService commandLineService,
            Document doc)
        {
            _commandLineService = commandLineService;
            _selectionService = selectionService;

            var parameters = new AutocadTableSerializerParameters
            {
                TargetDatabase = doc.Database
            };

            Result.Try(
                () => SelectObjects()
                    .Bind(tableDataService.GetTable)
                    .Map(table => table.Serialize(tableSerializer, parameters))
                    .Tap(table => InsertTable(doc, table))
                    .OnFailure(ShowError))
                .OnFailure(ShowError);

            return PluginResult.Succeeded;
        }

        private void ShowError(string error)
        {
            _commandLineService?.WriteAsNewLine(error);
        }

        private void InsertTable(Document doc, Table table)
        {
            using (table)
            {
                var promptResult = doc.Editor.GetPoint("\nSpecify the insertion point for the table: ");
                if (promptResult.Status != PromptStatus.OK)
                    return;

                table.Position = promptResult.Value;
                var msId = SymbolUtilityServices.GetBlockModelSpaceId(doc.Database);
                using var ms = msId.OpenAs<BlockTableRecord>(true);
                ms.AppendEntity(table);
            }
        }

        private Result<List<ObjectId>> SelectObjects()
        {
            var selectResult = _selectionService?.RunSelection();
            if (selectResult?.IsEmpty == true)
                return Result.Failure<List<ObjectId>>("No objects selected.");

            return selectResult?.SelectedObjects.ToList() ?? Result.Failure<List<ObjectId>>("Object selection error.");
        }
    }
}