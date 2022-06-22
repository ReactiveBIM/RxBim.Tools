namespace RxBim.Command.TableBuilder.Autocad.Sample.Commands
{
    using System.Collections.Generic;
    using System.Linq;
    using Abstractions;
    using Autodesk.AutoCAD.ApplicationServices;
    using Autodesk.AutoCAD.DatabaseServices;
    using Autodesk.AutoCAD.EditorInput;
    using Autodesk.AutoCAD.Runtime;
    using CSharpFunctionalExtensions;
    using RxBim.Command.Autocad;
    using Shared;
    using Tools.Autocad;
    using Tools.TableBuilder;
    using Result = CSharpFunctionalExtensions.Result;
    using Table = Autodesk.AutoCAD.DatabaseServices.Table;

    /// <inheritdoc />
    [RxBimCommandClass("RxBimTableSample", CommandFlags.UsePickSet)]
    public class Command : RxBimCommand
    {
        private IObjectsSelectionService _selectionService = null!;
        private ICommandLineService _commandLineService = null!;

        /// <summary>
        /// Command execution
        /// </summary>
        /// <param name="tableDataService"><see cref="ITableDataService"/> object.</param>
        /// <param name="tableConverter"><see cref="ITableConverter{TParams,TResult,TSource}"/> object.</param>
        /// <param name="selectionService"><see cref="IObjectsSelectionService"/> object.</param>
        /// <param name="commandLineService"><see cref="ICommandLineService"/> object.</param>
        /// <param name="doc"><see cref="Document"/> object.</param>>
        public PluginResult ExecuteCommand(
            ITableDataService tableDataService,
            IAutocadTableConverter tableConverter,
            IObjectsSelectionService selectionService,
            ICommandLineService commandLineService,
            Document doc)
        {
            _commandLineService = commandLineService;
            _selectionService = selectionService;

            var parameters = new AutocadTableConverterParameters
            {
                TargetDatabase = doc.Database
            };

            Result.Try(
                () => SelectObjects()
                    .Bind(tableDataService.GetTable)
                    .Map(table => tableConverter.Convert(table, parameters))
                    .Tap(table => InsertTable(doc, table))
                    .OnFailure(ShowError))
                .OnFailure(ShowError);

            return PluginResult.Succeeded;
        }

        private void ShowError(string error)
        {
            _commandLineService.WriteAsNewLine(error);
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

        private Result<IEnumerable<ObjectId>> SelectObjects()
        {
            var selectionResult = _selectionService.RunSelection();
            return Result.SuccessIf(!selectionResult.IsEmpty, selectionResult.SelectedObjects, "Objects not selected");
        }
    }
}