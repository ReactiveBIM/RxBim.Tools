namespace RxBim.Command.TableBuilder.Autocad.Sample.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Abstractions;
    using Autodesk.AutoCAD.DatabaseServices;
    using CSharpFunctionalExtensions;
    using JetBrains.Annotations;
    using Tools.Autocad;
    using Tools.TableBuilder;
    using Entity = Autodesk.AutoCAD.DatabaseServices.Entity;
    using Table = Tools.TableBuilder.Table;

    /// <inheritdoc />
    [UsedImplicitly]
    internal class TableDataService : ITableDataService
    {
        /// <inheritdoc />
        public Result<Table> GetTable(IEnumerable<ObjectId> ids)
        {
            var tableBuilder = new TableBuilder();

            tableBuilder
                .SetFormat(x => x
                    .SetBorders(CellBorderType.Thin, CellBorderType.Thin, CellBorderType.Bold, CellBorderType.Bold)
                    .SetContentHorizontalAlignment(CellContentHorizontalAlignment.Center)
                    .SetContentVerticalAlignment(CellContentVerticalAlignment.Middle))
                .AddColumn(x => x.SetWidth(60))
                .AddColumn(x => x.SetWidth(80)
                    .SetFormat(f => f
                        .SetContentVerticalHorizontalMargins(horizontalMargins: 1)
                        .SetContentHorizontalAlignment(CellContentHorizontalAlignment.Left)))
                .AddColumn(x => x.SetWidth(50))
                .AddColumn(x => x.SetWidth(40))

                // Title
                .AddRow(r => r
                    .SetHeight(15)
                    .MergeRow()
                    .SetFormat(x => x
                        .SetBorders(
                            CellBorderType.Hidden,
                            CellBorderType.Bold,
                            CellBorderType.Hidden,
                            CellBorderType.Hidden))
                    .Cells
                    .First()
                    .SetText("Selected object data table"))

                // Header
                .AddRow(r => r
                    .SetHeight(35)
                    .SetFormat(f => f
                        .SetAllBorders(CellBorderType.Bold)
                        .SetContentHorizontalAlignment(CellContentHorizontalAlignment.Center))
                    .Cells
                    .First()
                    .SetAcadTableText(@"Object\Pclass", RotationAngle.Degrees090)
                    .Next()
                    .SetAcadTableText("Layer", RotationAngle.Degrees090)
                    .Next()
                    .SetAcadTableText("Id", RotationAngle.Degrees090)
                    .Next()
                    .SetAcadTableText("Block", RotationAngle.Degrees090));

            // Data
            foreach (var id in ids)
            {
                tableBuilder.AddRow(row =>
                {
                    using var entity = id.OpenAs<Entity>();

                    row.Cells
                        .First()
                        .SetText(entity.GetRXClass().Name)
                        .Next()
                        .SetAcadTableText(entity.Layer, adjustCellSize: true)
                        .Next()
                        .SetText(entity.Id.Handle.ToString())
                        .Next()
                        .SetContent(entity is BlockReference blRef
                            ? new BlockCellContent(blRef.DynamicBlockTableRecord)
                            : new AutocadTextCellContent("Not block"));
                });
            }

            // First data row
            tableBuilder.Rows
                .ElementAt(2)
                .SetFormat(x => x.SetBorders(top: CellBorderType.Bold));

            // Last row
            tableBuilder.Rows
                .Last()
                .SetFormat(x => x.SetBorders(bottom: CellBorderType.Bold));

            return tableBuilder.Build();
        }
    }
}