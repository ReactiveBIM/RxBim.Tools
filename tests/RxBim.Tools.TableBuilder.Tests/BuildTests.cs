namespace RxBim.Tools.TableBuilder.Tests;

using System.Reflection;
using FluentAssertions;
using MoreLinq.Extensions;
using Xunit;

public class BuildTests
{
    /// <summary>
    /// Test for <see cref="CellRangeExtensions.IsInsideFor"/> method.
    /// </summary>
    [Fact]
    public void IsInsideForTest()
    {
        var tableBuilder = new TableBuilder();
        tableBuilder
            .AddColumn(cb => cb.SetWidth(20), 40)
            .AddRow(rb => rb.SetHeight(10).Cells.ForEach((cell, i) => cell.SetText(i.ToString())), 1000);
           

        /*var innerTable = tableBuilder.GetType().GetProperty("Table", BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(tableBuilder) as Table;
        innerTable.Should().NotBeNull();
        var firstCell = innerTable!.Rows[0].Cells[0];
        var firstCell2 = innerTable.Columns[0].Cells[0];
        object.ReferenceEquals(firstCell, firstCell2).Should().BeTrue();*/
        var table = tableBuilder.Build();
    }
}