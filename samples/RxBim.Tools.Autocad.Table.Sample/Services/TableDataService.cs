namespace RxBim.Tools.Autocad.Table.Sample.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Abstractions;
    using Autodesk.AutoCAD.DatabaseServices;
    using CSharpFunctionalExtensions;
    using Extensions;
    using Serializers;
    using TableBuilder.Extensions;
    using TableBuilder.Models.Styles;
    using TableBuilder.Services;
    using Table = TableBuilder.Models.Table;

    /// <inheritdoc />
    public class TableDataService : ITableDataService
    {
        /// <inheritdoc />
        public Result<Table> GetTable(List<ObjectId> ids)
        {
            var tableBuilder = new TableBuilder();

            tableBuilder.AddRow();
            tableBuilder.AddRow(r => r.SetHeight(15));

            var cellFormat = GetDefaultFormat();
            cellFormat.Borders.SetBorders(left: CellBorderType.Bold, right: CellBorderType.Bold);
            tableBuilder.AddColumn(c =>
                c.SetWidth(60).SetFormat(cellFormat).ToCells().ElementAt(1).SetText("Object class"));
            tableBuilder.AddColumn(c => c.SetWidth(80)
                .SetFormat(cellFormat)
                .ToCells()
                .ElementAt(1)
                .SetText("Layer"));

            cellFormat.ContentHorizontalAlignment = CellContentHorizontalAlignment.Center;
            tableBuilder.AddColumn(c => c.SetWidth(50).SetFormat(cellFormat).ToCells().ElementAt(1).SetText("Id"));
            tableBuilder.AddColumn(c =>
                c.SetWidth(40).SetFormat(cellFormat).ToCells().ElementAt(1).SetText("Designation"));

            var titleFormat = GetDefaultFormat();
            titleFormat.ContentHorizontalAlignment = CellContentHorizontalAlignment.Center;
            titleFormat.Borders
                .SetBorders(CellBorderType.Hidden, CellBorderType.Bold, CellBorderType.Hidden, CellBorderType.Hidden);

            tableBuilder
                .ToRows()
                .First()
                .ToCells()
                .First()
                .MergeNext(3)
                .SetText("Selected object data")
                .SetFormat(titleFormat);

            var headerFormat = GetDefaultFormat();
            headerFormat.ContentHorizontalAlignment = CellContentHorizontalAlignment.Center;
            headerFormat.Borders.SetBorders(CellBorderType.Bold);
            foreach (var cellBuilder in tableBuilder.ToRows().ElementAt(1).ToCells())
                cellBuilder.SetFormat(headerFormat);

            foreach (var id in ids)
            {
                tableBuilder.AddRow(row =>
                {
                    using var entity = id.OpenAs<Entity>();

                    row.ToCells()
                        .First()
                        .SetText(entity.GetRXClass().Name)
                        .Next()
                        .SetText(entity.Layer)
                        .Next()
                        .SetText(entity.Id.ToString());

                    if (entity is BlockReference blRef)
                    {
                        row.ToCells().ElementAt(3).SetContent(new BlockCellData(blRef.DynamicBlockTableRecord));
                    }
                });
            }

            tableBuilder.ToRows()
                .ElementAt(1)
                .ToCells()
                .ToList()
                .ForEach(c => c.ObjectForBuild.Format.Borders.SetBorders(CellBorderType.Bold));

            tableBuilder.ToRows()
                .Last()
                .ToCells()
                .ToList()
                .ForEach(c => { c.ObjectForBuild.Format.Borders.Bottom = CellBorderType.Bold; });

            return tableBuilder.Build();
        }

        private CellFormatStyle GetDefaultFormat()
        {
            return new CellFormatStyle
            {
                BackgroundColor = null,
                TextFormat = { TextColor = null, FontFamily = string.Empty },
                ContentHorizontalAlignment = CellContentHorizontalAlignment.Left,
                ContentVerticalAlignment = CellContentVerticalAlignment.Middle
            };
        }
    }
}