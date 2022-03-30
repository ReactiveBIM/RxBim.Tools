namespace RxBim.Tools.Serializer.Excel.Tests
{
    using System.Linq;
    using ClosedXML.Excel;
    using FluentAssertions;
    using Models;
    using Services;
    using TableBuilder.Extensions;
    using TableBuilder.Models;
    using TableBuilder.Services;
    using Xunit;

    /// <summary>
    /// Tests for <see cref="ExcelTableSerializer"/>
    /// </summary>
    public class SerializeTests
    {
        [Fact]
        public void ExcelSerializerTest()
        {
            // Arrange
            const int tableRowsCount = 10;
            var table = GetTestTable(tableRowsCount);
            using var xlPackage = new XLWorkbook();
            var serializer = new ExcelTableSerializer();

            // Act
            var workBook = table.Serialize(
                serializer,
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