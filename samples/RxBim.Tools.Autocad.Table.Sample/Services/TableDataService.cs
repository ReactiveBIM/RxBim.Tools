namespace RxBim.Tools.Autocad.Table.Sample.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Abstractions;
    using Autodesk.AutoCAD.DatabaseServices;
    using CSharpFunctionalExtensions;
    using Extensions;
    using Extensions.TableBuilder;
    using Serializers;
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

            var titleFormat = new CellFormatStyleBuilder()
                .SetContentHorizontalAlignment(CellContentHorizontalAlignment.Center)
                .SetBorders(CellBorderType.Hidden, CellBorderType.Bold, CellBorderType.Hidden, CellBorderType.Hidden)
                .Build();

            tableBuilder
                .ToRows()
                .First()
                .ToCells()
                .First()
                .MergeNext(3)
                .SetText("Selected object data")
                .SetFormat(titleFormat);

            tableBuilder
                .AddColumn(x => x.SetWidth(60))
                .AddColumn(x => x.SetWidth(80))
                .AddColumn(x => x.SetWidth(50))
                .AddColumn(x => x.SetWidth(40))
                .AddRow(r => r.MergeRow().SetFormat().ToCells().First().SetText("Selected object data"))
                .AddRow(r => r.SetHeight(15)
                    .ToCells()
                    .First()
                    .SetText("Object class", RotationAngle.Degrees090)
                    .Next()
                    .SetText("Layer", RotationAngle.Degrees090)
                    .Next()
                    .SetText("Id", RotationAngle.Degrees090)
                    .Next()
                    .SetText("Designation", RotationAngle.Degrees090));

            var cellFormat = GetDefaultFormat();
            cellFormat.Borders.Left = CellBorderType.Bold;
            cellFormat.Borders.Right = CellBorderType.Bold;
            tableBuilder.AddColumn(c =>
                c.SetWidth(60)
                    .SetFormat(cellFormat)
                    .ToCells()
                    .ElementAt(1)
                    .SetText("Object class", RotationAngle.Degrees090));

            tableBuilder.AddColumn(c => c.SetWidth(80)
                .SetFormat(cellFormat)
                .ToCells()
                .ElementAt(1)
                .SetText("Layer", RotationAngle.Degrees090));

            cellFormat.ContentHorizontalAlignment = CellContentHorizontalAlignment.Center;
            tableBuilder.AddColumn(c =>
                c.SetWidth(50)
                    .SetFormat(cellFormat)
                    .ToCells()
                    .ElementAt(1)
                    .SetText("Id", RotationAngle.Degrees090));

            tableBuilder.AddColumn(c =>
                c.SetWidth(40)
                    .SetFormat(cellFormat)
                    .ToCells()
                    .ElementAt(1)
                    .SetText("Designation", RotationAngle.Degrees090));

            var headerFormat = new CellFormatStyleBuilder()
                .SetContentHorizontalAlignment(CellContentHorizontalAlignment.Center)
                .SetAllBorders(CellBorderType.Bold)
                .Build();

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
                        row.ToCells().ElementAt(3).SetContent(new BlockCellContent(blRef.DynamicBlockTableRecord));
                });
            }

            tableBuilder.ToRows()
                .ElementAt(1)
                .ToCells()
                .ToList()
                .ForEach(c => new CellFormatStyleBuilder(c.ObjectForBuild.Format).SetAllBorders(CellBorderType.Bold));

            tableBuilder.ToRows()
                .Last()
                .ToCells()
                .ToList()
                .ForEach(c => { c.ObjectForBuild.Format.Borders.Bottom = CellBorderType.Bold; });

            return tableBuilder.Build();
        }

        private CellFormatStyle GetDefaultFormat()
        {
            return new CellFormatStyleBuilder()
                .SetContentHorizontalAlignment(CellContentHorizontalAlignment.Left)
                .SetContentVerticalAlignment(CellContentVerticalAlignment.Middle)
                .Build();
        }
    }
}