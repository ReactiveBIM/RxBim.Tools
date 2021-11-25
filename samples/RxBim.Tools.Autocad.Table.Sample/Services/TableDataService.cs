namespace RxBim.Tools.Autocad.Table.Sample.Services
{
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using Abstractions;
    using Autodesk.AutoCAD.DatabaseServices;
    using CSharpFunctionalExtensions;
    using Extensions;
    using Serializers;
    using TableBuilder;
    using TableBuilder.Extensions;
    using CellBorders = TableBuilder.CellBorders;
    using Entity = Autodesk.AutoCAD.DatabaseServices.Entity;
    using Table = TableBuilder.Table;

    /// <inheritdoc />
    public class TableDataService : ITableDataService
    {
        /// <inheritdoc />
        public Result<Table> GetTable(List<ObjectId> ids)
        {
            var table = new Table();

            table.AddRow();
            table.AddRow(r => r.SetHeight(15));

            var cellFormat = GetDefaultFormat();
            cellFormat.Borders = new CellBorders(CellBorderType.Usual)
            {
                Left = CellBorderType.Bold,
                Right = CellBorderType.Bold
            };

            table.AddColumn(c => c.SetWidth(60).SetFormat(cellFormat)
                .Cells[1].SetText("Класс объекта"));
            table.AddColumn(c => c.SetWidth(80).SetFormat(cellFormat)
                .Cells[1].SetText("Слой"));

            var cellCenterFormat = CopyFormat(cellFormat);
            cellCenterFormat.TextHorizontalAlignment = TextHorizontalAlignment.Center;

            table.AddColumn(c => c.SetWidth(50).SetFormat(cellCenterFormat)
                .Cells[1].SetText("Id"));
            table.AddColumn(c => c.SetWidth(40).SetFormat(cellCenterFormat)
                .Cells[1].SetText("Обозначение"));

            var titleFormat = GetDefaultFormat();
            titleFormat.TextHorizontalAlignment = TextHorizontalAlignment.Center;
            titleFormat.Borders = new CellBorders(CellBorderType.Hidden)
            {
                Bottom = CellBorderType.Bold
            };

            table.Rows[0].Cells[0].MergeNext(3).SetText("Данные о выбранных объектах").SetFormat(titleFormat);

            var headerFormat = GetDefaultFormat();
            headerFormat.TextHorizontalAlignment = TextHorizontalAlignment.Center;
            headerFormat.Borders = new CellBorders(CellBorderType.Bold);

            table.Rows[1].Cells.ForEach(c => c.SetFormat(headerFormat));

            foreach (var id in ids)
            {
                table.AddRow(row =>
                {
                    using var entity = id.OpenAs<Entity>();

                    row.Cells[0].SetText(entity.GetRXClass().Name);
                    row.Cells[1].SetText(entity.Layer);
                    row.Cells[2].SetText(entity.Id.ToString());

                    if (entity is BlockReference blRef)
                    {
                        row.Cells[3].SetValue(new BlockCellData
                        {
                            BtrId = blRef.DynamicBlockTableRecord
                        });
                    }
                });
            }

            table.Rows[2].Cells.ForEach(c =>
            {
                var format = CopyFormat(c.Format);
                format.Borders.Top = CellBorderType.Bold;
                c.SetFormat(format);
            });

            table.Rows.Last().Cells.ForEach(c =>
            {
                var format = CopyFormat(c.Format);
                format.Borders.Bottom = CellBorderType.Bold;
                c.SetFormat(format);
            });

            return table;
        }

        private CellFormatStyle CopyFormat(CellFormatStyle format)
        {
            return new CellFormatStyle
            {
                Bold = format.Bold,
                Italic = format.Italic,
                BackgroundColor = format.BackgroundColor,
                FontFamily = format.FontFamily,
                TextColor = format.TextColor,
                TextSize = format.TextSize,
                WrapText = format.WrapText,
                TextHorizontalAlignment = format.TextHorizontalAlignment,
                TextVerticalAlignment = format.TextVerticalAlignment,
                Borders = format.Borders == null
                    ? null
                    : new CellBorders(
                        format.Borders.Top,
                        format.Borders.Bottom,
                        format.Borders.Left,
                        format.Borders.Right)
            };
        }

        private CellFormatStyle GetDefaultFormat()
        {
            return new CellFormatStyle
            {
                Borders = null,
                BackgroundColor = Color.Empty,
                TextColor = Color.Empty,
                FontFamily = string.Empty,
                TextHorizontalAlignment = TextHorizontalAlignment.Left,
                TextVerticalAlignment = TextVerticalAlignment.Middle
            };
        }
    }
}