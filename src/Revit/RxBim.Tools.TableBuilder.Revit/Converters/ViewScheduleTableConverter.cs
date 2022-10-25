namespace RxBim.Tools.TableBuilder
{
    using System;
    using System.Linq;
    using Autodesk.Revit.DB;
    using JetBrains.Annotations;
    using Revit.Extensions;
    using Revit.Models;
    using Styles;

    /// <summary>
    /// Represents a converter that renders a <see cref="Table"/> object to a Revit ViewSchedule.
    /// </summary>
    [UsedImplicitly]
    internal class ViewScheduleTableConverter : IViewScheduleTableConverter
    {
        private const double FontRatio = 3.77951502;
        private readonly Document _document;
        private readonly ITransactionService _transactionService;

        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="document">A <see cref="Document"/> object.</param>
        /// <param name="transactionService"> TransactionService </param>
        public ViewScheduleTableConverter(Document document, ITransactionService transactionService)
        {
            _document = document;
            _transactionService = transactionService;
        }

        /// <inheritdoc />
        public ViewSchedule Convert(Table table, ViewScheduleTableConverterParameters parameters)
        {
            if (string.IsNullOrWhiteSpace(parameters.Name))
            {
                throw new ArgumentException(
                    "ViewSchedule name not defined.",
                    nameof(parameters));
            }

            return _transactionService.RunInTransaction(
                () =>
                {
                    var id = new ElementId((int)BuiltInCategory.OST_NurseCallDevices);

                    var schedule = ViewSchedule.CreateSchedule(_document, id);
                    schedule.Name = parameters.Name;
                    schedule.Definition.ShowHeaders = false;

                    var field = schedule.Definition
                        .GetSchedulableFields()
                        .FirstOrDefault(x =>
                            x.GetName(_document).ToUpper() == "URL");

                    if (field != null)
                    {
                        schedule.Definition.AddField(field).GridColumnWidth = table.Width.MmToFt();
                    }

                    var tableData = schedule.GetTableData();
                    var headerData = tableData.GetSectionData(SectionType.Header);

                    InsertCells(headerData, table.Rows.Count() - 1, table.Columns.Count());

                    var scheduleCol = headerData.FirstColumnNumber;

                    for (var col = 0;
                         col < table.Columns.Count();
                         col++, scheduleCol++)
                    {
                        var widthInFt = table.Columns[col].Width.MmToFt();
                        headerData.SetColumnWidth(scheduleCol, widthInFt);

                        var scheduleRow = headerData.FirstRowNumber;

                        for (var row = 0;
                             row < table.Rows.Count();
                             row++, scheduleRow++)
                        {
                            var cell = table[row, col];

                            var rowHeight = table.Rows[row].Height;
                            const int defaultRowHeightInMm = 8;
                            rowHeight = rowHeight > 0 ? rowHeight.MmToFt() : defaultRowHeightInMm.MmToFt();

                            headerData.SetRowHeight(scheduleRow, rowHeight);
                            headerData.SetCellText(scheduleRow, scheduleCol, cell.Content.ValueObject?.ToString());
                            headerData.SetCellStyle(scheduleRow,
                                scheduleCol,
                                GetCellStyle(cell.GetComposedFormat(), parameters));
                        }
                    }

                    foreach (var mergeArea in table.MergeAreas)
                    {
                        var tableMergedCell = new TableMergedCell
                        {
                            Bottom = mergeArea.BottomRow,
                            Top = mergeArea.TopRow,
                            Left = mergeArea.LeftColumn,
                            Right = mergeArea.RightColumn
                        };

                        headerData.MergeCells(tableMergedCell);
                    }

                    headerData.RemoveColumn(headerData.LastColumnNumber);
                    return schedule;
                },
                nameof(ViewScheduleTableConverter));
        }

        private TableCellStyle GetCellStyle(
            CellFormatStyle cellStyle,
            ViewScheduleTableConverterParameters parameters)
        {
            var options = new TableCellStyleOverrideOptions
            {
                BorderTopLineStyle = GetCellBorderType(cellStyle.Borders.Top) != CellBorderType.Thin,
                BorderBottomLineStyle = GetCellBorderType(cellStyle.Borders.Bottom) != CellBorderType.Thin,
                BorderLeftLineStyle = GetCellBorderType(cellStyle.Borders.Left) != CellBorderType.Thin,
                BorderRightLineStyle = GetCellBorderType(cellStyle.Borders.Right) != CellBorderType.Thin,
                FontSize = cellStyle.TextFormat.TextSize is > 0,
                Bold = true,
                Italics = true,
                HorizontalAlignment = true,
                VerticalAlignment = true,
                FontColor = true,
                BackgroundColor = true
            };

            var boldLineId = parameters.SpecificationBoldLineId ?? -1;

            var style = new TableCellStyle
            {
                BorderTopLineStyle = GetLineId(GetCellBorderType(cellStyle.Borders.Top), boldLineId),
                BorderBottomLineStyle = GetLineId(GetCellBorderType(cellStyle.Borders.Bottom), boldLineId),
                BorderLeftLineStyle = GetLineId(GetCellBorderType(cellStyle.Borders.Left), boldLineId),
                BorderRightLineStyle = GetLineId(GetCellBorderType(cellStyle.Borders.Right), boldLineId),
                TextSize = cellStyle.TextFormat.TextSize ?? 0 * FontRatio,
                IsFontBold = cellStyle.TextFormat.Bold ?? false,
                IsFontItalic = cellStyle.TextFormat.Italic ?? false,
                TextColor = GetRevitColor(cellStyle.TextFormat.TextColor ?? System.Drawing.Color.Black),
                BackgroundColor = GetRevitColor(cellStyle.BackgroundColor ?? System.Drawing.Color.White),
                FontVerticalAlignment = cellStyle.ContentVerticalAlignment switch
                {
                    CellContentVerticalAlignment.Top => VerticalAlignmentStyle.Top,
                    CellContentVerticalAlignment.Middle => VerticalAlignmentStyle.Middle,
                    _ => VerticalAlignmentStyle.Bottom
                },
                FontHorizontalAlignment = cellStyle.ContentHorizontalAlignment switch
                {
                    CellContentHorizontalAlignment.Right => HorizontalAlignmentStyle.Right,
                    CellContentHorizontalAlignment.Left => HorizontalAlignmentStyle.Left,
                    _ => HorizontalAlignmentStyle.Center
                }
            };

            style.SetCellStyleOverrideOptions(options);

            return style;
        }

        private ElementId GetLineId(CellBorderType cellBorderType, int boldLineId = -1)
        {
            return cellBorderType switch
            {
                CellBorderType.Thin => ElementId.InvalidElementId, // does not apply
                CellBorderType.Hidden => ElementId.InvalidElementId,
                CellBorderType.Bold => new ElementId(boldLineId),
                _ => throw new Exception($"Line {cellBorderType} is not implemented in the converter."),
            };
        }

        private void InsertCells(TableSectionData section, int rows, int cols)
        {
            for (var j = 0; j < rows; j++)
                section.InsertRow(j);

            for (var j = 0; j < cols; j++)
                section.InsertColumn(j);
        }

        private Color GetRevitColor(System.Drawing.Color color)
        {
            return new Color(color.R, color.G, color.B);
        }

        private CellBorderType GetCellBorderType(CellBorderType? cellBorderType)
        {
            return cellBorderType ?? CellBorderType.Thin;
        }
    }
}