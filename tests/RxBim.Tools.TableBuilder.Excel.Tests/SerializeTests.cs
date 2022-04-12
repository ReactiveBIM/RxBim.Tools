namespace RxBim.Tools.TableBuilder.Excel.Tests
{
    using System.Linq;
    using Abstractions;
    using ClosedXML.Excel;
    using Di;
    using FluentAssertions;
    using TableBuilder.Models;
    using TableBuilder.Services;
    using Xunit;

    /// <summary>
    /// Tests for <see cref="ExcelTableSerializer"/>
    /// </summary>
    public class SerializeTests : TestsBase
    {
        [Fact]
        public void ExcelSerializerTest()
        {
            // Arrange
            const int tableRowsCount = 10;
            var table = GetTestTable(tableRowsCount);
            using var xlPackage = new XLWorkbook();
            var serializer = Container.GetRequiredService<IExcelTableSerializer>();

            // Act
            var workBook = serializer.Serialize(
                table,
                new ExcelTableSerializerParameters
                {
                    WorksheetName = "Sheet1",
                    Document = xlPackage
                });

            // Assert
            workBook.Worksheets.First().Rows().Count().Should().Be(tableRowsCount);
            workBook.Worksheets.First().Columns().Count().Should().Be(2);
        }

        private Table GetTestTable(int count)
        {
            var list = Enumerable.Range(0, count)
                .Select(x => new RowData { Prop1 = x, Prop2 = nameof(SerializeTests) + x })
                .ToList();

            return new TableBuilder()
                .AddRowsFromList(list, 0, 0, p => p.Prop1, p => p.Prop2);
        }

        private class RowData
        {
            public int Prop1 { get; set; }

            public string Prop2 { get; set; } = string.Empty;
        }
    }
}