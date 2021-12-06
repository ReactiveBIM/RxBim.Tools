namespace RxBim.Command.TableBuilder.Revit.Sample.Services
{
    using System;
    using System.Linq;
    using Abstractions;
    using Autodesk.Revit.DB;
    using CSharpFunctionalExtensions;
    using RxBim.Tools.TableBuilder.Extensions;
    using Tools.Revit.Abstractions;
    using Tools.Revit.Serializers;
    using Tools.TableBuilder.Abstractions;
    using Tools.TableBuilder.Models.Styles;
    using Tools.TableBuilder.Services;
    using Color = System.Drawing.Color;

    /// <inheritdoc />
    public class ViewScheduleCreator : IViewScheduleCreator
    {
        private readonly IScopedElementsCollector _scopedCollector;
        private readonly ITransactionService _transactionService;
        private readonly ITableSerializer<ViewSchedule> _tableSerializer;
        private readonly Document _doc;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="scopedCollector"><see cref="IScopedElementsCollector"/></param>
        /// <param name="transactionService"><see cref="ITransactionService"/></param>
        /// <param name="tableSerializer">Serializer to <see cref="ViewSchedule"/></param>
        /// <param name="doc"><see cref="Document"/></param>
        public ViewScheduleCreator(
            IScopedElementsCollector scopedCollector,
            ITransactionService transactionService,
            ITableSerializer<ViewSchedule> tableSerializer,
            Document doc)
        {
            _scopedCollector = scopedCollector;
            _transactionService = transactionService;
            _tableSerializer = tableSerializer;
            _doc = doc;
        }

        /// <inheritdoc />
        public Result CreateSomeViewSchedule(string name, int rowsCount, int columnsCount)
        {
            try
            {
                var tableBuilder = new TableBuilder();
                var baseFormat = new CellFormatStyle
                {
                    ContentHorizontalAlignment = CellContentHorizontalAlignment.Center,
                    ContentVerticalAlignment = CellContentVerticalAlignment.Middle,
                    TextFormat =
                    {
                        Italic = true,
                        TextColor = Color.Gray
                    }
                };

                tableBuilder.SetTableStateStandardFormat(1)
                    .AddRow(row => row.SetHeight(20))
                    .AddRowsFromList(Enumerable.Range(1, rowsCount), 1, 0)
                    .AddColumnsFromList(Enumerable.Range(1, columnsCount), 0, 0)
                    .SetFormat(baseFormat)
                    .SetCellsFormat(
                        new CellFormatStyle
                            {
                                ContentHorizontalAlignment = CellContentHorizontalAlignment.Center,
                                ContentVerticalAlignment = CellContentVerticalAlignment.Middle,
                                TextFormat =
                                {
                                    Bold = true,
                                    TextColor = Color.Red
                                },
                                Borders =
                                {
                                    Bottom = CellBorderType.Bold,
                                    Left = CellBorderType.Bold,
                                    Right = CellBorderType.Bold,
                                    Top = CellBorderType.Bold
                                }
                            },
                        0,
                        0,
                        columnsCount,
                        1);
                tableBuilder.ToRows().Skip(1).ToList().ForEach(row => row.SetHeight(10));
                tableBuilder.GetColumns().ToList().ForEach(col => col.SetWidth(30));

                var table = tableBuilder.Build();
                var serializeParams = new ViewScheduleTableSerializerParameters
                {
                    Name = name,
                    SpecificationBoldLineId = 571482
                };

                return _transactionService.RunInTransactionGroup(() =>
                {
                    return DeleteViewScheduleIfExists(name)
                        .Map(() => table.Serialize(_tableSerializer, serializeParams))
                        .Ensure(view => view is not null, "Error when created or serialized specification")
                        .Bind(view => PutSpecificationOnSheet(view, _doc.ActiveView.Id, XYZ.Zero));
                }, $"Created ViewSchedule: {name}");
            }
            catch (Exception e)
            {
                return Result.Failure(e.Message);
            }
        }

        private Result DeleteViewScheduleIfExists(string name)
        {
            var existedScheduleId = _scopedCollector.GetFilteredElementCollector(ignoreScope: true)
                .WhereElementIsNotElementType()
                .OfType<ViewSchedule>()
                .FirstOrDefault(x => x.Name.Contains(name))?.Id;

            if (existedScheduleId is null)
                return Result.Success();

            return _transactionService.RunInTransaction(
                () => _doc.Delete(existedScheduleId),
                nameof(DeleteViewScheduleIfExists));
        }

        private Result PutSpecificationOnSheet(ViewSchedule schedule, ElementId viewSheetId, XYZ origin)
        {
            return _transactionService.RunInTransaction(
                () => ScheduleSheetInstance.Create(_doc, viewSheetId, schedule.Id, origin),
                nameof(PutSpecificationOnSheet));
        }
    }
}