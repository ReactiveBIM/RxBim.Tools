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
    using Result = CSharpFunctionalExtensions.Result;

    /// <inheritdoc />
    [RxBimCommandClass("RxBimTableSample")]
    public class Command : RxBimCommand
    {
        private IObjectsSelectionService? _selectionService;
        private ICommandLineService? _commandLineService;

        /// <summary>
        /// Command execution
        /// </summary>
        /// <param name="tableDataService">Сервис данных для таблицы</param>
        /// <param name="tableSerializer">Сериализатор таблицы</param>
        /// <param name="selectionService">Сервис выбора объектов на чертеже</param>
        /// <param name="commandLineService">Сервис командной строки</param>
        /// <param name="doc">Документ</param>>
        public PluginResult ExecuteCommand(
            ITableDataService tableDataService,
            ITableSerializer<Table> tableSerializer,
            IObjectsSelectionService selectionService,
            ICommandLineService commandLineService,
            Document doc)
        {
            _commandLineService = commandLineService;
            _selectionService = selectionService;

            var parameters = new TableSerializerParameters
            {
                Db = doc.Database
            };

            Result.Try(
                () => SelectObjects()
                    .Bind(tableDataService.GetTable)
                    .Map(table => table.Serialize(parameters, tableSerializer))
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
                var promptResult = doc.Editor.GetPoint("Укажите точку вставки таблицы");
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
                return Result.Failure<List<ObjectId>>("Не выбраны объекты");

            return selectResult?.SelectedObjects.ToList() ?? Result.Failure<List<ObjectId>>("Ошибка выбора объектов");
        }
    }
}