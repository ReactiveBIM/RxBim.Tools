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
    using TableBuilder.Models.Contents;
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

            tableBuilder
                .SetFormat(x => x.SetBorders(
                    CellBorderType.Thin,
                    CellBorderType.Thin,
                    CellBorderType.Bold,
                    CellBorderType.Bold))
                .AddColumn(x => x.SetWidth(60))
                .AddColumn(x => x.SetWidth(80))
                .AddColumn(x => x.SetWidth(50))
                .AddColumn(x => x.SetWidth(40))
                .AddRow(r => r.MergeRow() // Title
                    .SetFormat(x => x
                        .SetContentHorizontalAlignment(CellContentHorizontalAlignment.Center)
                        .SetContentVerticalAlignment(CellContentVerticalAlignment.Bottom)
                        .SetContentMargins(1)
                        .SetBorders(
                            CellBorderType.Hidden,
                            CellBorderType.Bold,
                            CellBorderType.Hidden,
                            CellBorderType.Hidden))
                    .ToCells()
                    .First()
                    .SetText("Selected object data table"))
                .AddRow(r => r.SetHeight(15) // Header
                    .SetFormat(f => f.SetAllBorders(CellBorderType.Bold))
                    .ToCells()
                    .First()
                    .SetText("Object class", RotationAngle.Degrees090)
                    .Next()
                    .SetText("Layer", RotationAngle.Degrees090)
                    .Next()
                    .SetText("Id", RotationAngle.Degrees090)
                    .Next()
                    .SetText("Block", RotationAngle.Degrees090));

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
                        .SetText(entity.Id.Handle.ToString())
                        .Next()
                        .SetContent(entity is BlockReference blRef
                            ? new BlockCellContent(blRef.DynamicBlockTableRecord)
                            : new TextCellContent("Not block"));
                });
            }

            tableBuilder.ToRows()
                .ElementAt(1)
                .ToCells()
                .ToList()
                .ForEach(c => new CellFormatStyleBuilder(c.ObjectForBuild.Format).SetAllBorders(CellBorderType.Bold));

            tableBuilder.ToRows().Last().SetFormat(x => x.SetBorders(bottom: CellBorderType.Bold));
            return tableBuilder.Build();
        }
    }
}