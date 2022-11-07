namespace RxBim.Command.TableBuilder.Revit.Sample.Services
{
    using System;
    using System.Linq;
    using Abstractions;
    using Autodesk.Revit.DB;
    using CSharpFunctionalExtensions;
    using JetBrains.Annotations;
    using Tools;
    using Tools.Revit.Abstractions;
    using Tools.Revit.Extensions;
    using Tools.TableBuilder;
    using Tools.TableBuilder.Styles;
    using Color = System.Drawing.Color;

    /// <inheritdoc />
    [UsedImplicitly]
    internal class ViewScheduleCreator : IViewScheduleCreator
    {
        private readonly IScopedElementsCollector _scopedCollector;
        private readonly ITransactionService _transactionService;
        private readonly IViewScheduleTableConverter _tableConverter;
        private readonly Document _doc;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="scopedCollector"><see cref="IScopedElementsCollector"/></param>
        /// <param name="transactionService"><see cref="ITransactionService"/></param>
        /// <param name="tableConverter">A converter to <see cref="ViewSchedule"/></param>
        /// <param name="doc"><see cref="Document"/></param>
        public ViewScheduleCreator(
            IScopedElementsCollector scopedCollector,
            ITransactionService transactionService,
            IViewScheduleTableConverter tableConverter,
            Document doc)
        {
            _scopedCollector = scopedCollector;
            _transactionService = transactionService;
            _tableConverter = tableConverter;
            _doc = doc;
        }

        /// <inheritdoc />
        public Result CreateSomeViewSchedule(string name, int rowsCount, int columnsCount)
        {
            try
            {
                var tableBuilder = new TableBuilder();
                var baseFormat = new CellFormatStyleBuilder()
                    .SetContentHorizontalAlignment(CellContentHorizontalAlignment.Center)
                    .SetContentVerticalAlignment(CellContentVerticalAlignment.Middle)
                    .SetTextFormat(x => x
                        .SetItalic(true)
                        .SetTextColor(Color.Gray))
                    .Build();

                tableBuilder.SetTableStateStandardFormat(1)
                    .AddRow(row => row.SetHeight(20))
                    .AddRowsFromList(Enumerable.Range(1, rowsCount), 1, 0)
                    .AddColumnsFromList(Enumerable.Range(1, columnsCount), 0, 0)
                    .SetFormat(baseFormat)
                    .SetCellsFormat(
                        new CellFormatStyleBuilder()
                            .SetBorders(builder => builder
                            .SetBorders(
                                bottom: CellBorderType.Bold,
                                left: CellBorderType.Bold,
                                right: CellBorderType.Bold,
                                top: CellBorderType.Bold))
                            .Build(),
                        0,
                        0,
                        columnsCount,
                        1);
                tableBuilder.ToRows().Skip(1).ToList().ForEach(row => row.SetHeight(10));
                tableBuilder.GetColumns().ToList().ForEach(col => col.SetWidth(30));

                var table = tableBuilder.Build();
                var parameters = new ViewScheduleTableConverterParameters
                {
                    Name = name,
                    SpecificationBoldLineId = 571482
                };

                return _transactionService.RunInTransactionGroup(_ =>
                    {
                        return DeleteViewScheduleIfExists(name)
                            .Map(() => _tableConverter.Convert(table, parameters))
                            .Ensure(view => view is not null, "Error creating or converting a specification")
                            .Check(view => PutSpecificationOnSheet(view, _doc.ActiveView.Id, XYZ.Zero))
                            .Check(view => ApplyHeaderStyle(view, columnsCount, 30));
                    },
                    $"Created ViewSchedule: {name}");
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
                .FirstOrDefault(x => x.Name.Contains(name))
                ?.Id;

            if (existedScheduleId is null)
                return Result.Success();

            _transactionService.RunInTransaction(
                () => _doc.Delete(existedScheduleId),
                nameof(DeleteViewScheduleIfExists));

            return Result.Success();
        }

        private Result PutSpecificationOnSheet(ViewSchedule schedule, ElementId viewSheetId, XYZ origin)
        {
            _transactionService.RunInTransaction(
                () => ScheduleSheetInstance.Create(_doc, viewSheetId, schedule.Id, origin),
                nameof(PutSpecificationOnSheet));

            return Result.Success();
        }

        // Revit does not apply styles when putting specification
        private Result ApplyHeaderStyle(ViewSchedule schedule, int columnsCount, int columnWidth)
        {
            var headerData = schedule.GetTableData().GetSectionData(SectionType.Header);

            var style = headerData.GetTableCellStyle(headerData.FirstRowNumber, headerData.FirstColumnNumber);
            var opt = style.GetCellStyleOverrideOptions();
            style.SetCellStyleOverrideOptions(opt);
            opt.Italics = true;
            style.IsFontItalic = true;

            _transactionService.RunInTransaction(() =>
                {
                    for (var i = 0; i < columnsCount; i++)
                        headerData.SetColumnWidth(0, columnWidth.MmToFt());
                    headerData.SetCellStyle(headerData.FirstRowNumber, headerData.FirstColumnNumber, style);
                },
                nameof(ApplyHeaderStyle));

            return Result.Success();
        }
    }
}