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
    using Tools.TableBuilder.Styles;
    using Table = Tools.TableBuilder.Table;

    /// <inheritdoc />
    [UsedImplicitly]
    internal class TableDataService : ITableDataService
    {
        /// <inheritdoc />
        public Result<Table> GetTable(List<ObjectId> ids)
        {
            var tableBuilder = new TableBuilder();

            tableBuilder
                .SetFormat(x => x
                    .SetBorders(CellBorderType.Thin, CellBorderType.Thin, CellBorderType.Bold, CellBorderType.Bold)
                    .SetContentHorizontalAlignment(CellContentHorizontalAlignment.Center)
                    .SetContentVerticalAlignment(CellContentVerticalAlignment.Middle))
                .AddColumn(x => x.SetWidth(60))
                .AddColumn(x => x.SetWidth(80).SetFormat(f => f
                        .SetContentVerticalHorizontalMargins(horizontalMargins: 1)
                        .SetContentHorizontalAlignment(CellContentHorizontalAlignment.Left)))
                .AddColumn(x => x.SetWidth(50))
                .AddColumn(x => x.SetWidth(40))
                .AddRow(r => r
                    .SetHeight(15)
                    .MergeRow() // Title
                    .SetFormat(x => x
                        .SetContentHorizontalAlignment(CellContentHorizontalAlignment.Center)
                        .SetContentVerticalAlignment(CellContentVerticalAlignment.Bottom)
                        .SetContentVerticalHorizontalMargins(1)
                        .SetBorders(
                            CellBorderType.Hidden,
                            CellBorderType.Bold,
                            CellBorderType.Hidden,
                            CellBorderType.Hidden))
                    .ToCells()
                    .First()
                    .SetText("Selected object data table"))
                .AddRow(r => r.SetHeight(35) // Header
                    .SetFormat(f => f
                        .SetAllBorders(CellBorderType.Bold)
                        .SetContentHorizontalAlignment(CellContentHorizontalAlignment.Center))
                    .ToCells()
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

                    row.ToCells()
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
            tableBuilder.ToRows().ElementAt(2).SetFormat(x => x.SetBorders(top: CellBorderType.Bold));

            // Last row
            tableBuilder.ToRows().Last().SetFormat(x => x.SetBorders(bottom: CellBorderType.Bold));

            return tableBuilder.Build();
        }
    }
}