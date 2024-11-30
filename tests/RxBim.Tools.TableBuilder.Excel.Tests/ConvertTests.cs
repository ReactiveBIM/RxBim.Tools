namespace RxBim.Tools.TableBuilder.Excel.Tests
{
    using System.Linq;
    using ClosedXML.Excel;
    using Di;
    using FluentAssertions;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    /// <summary>
    /// Tests for <see cref="IExcelTableConverter"/>
    /// </summary>
    public class ConvertTests : TestsBase
    {
        [Fact]
        public void ConverterTest()
        {
            // Arrange
            const int tableRowsCount = 10;
            var table = GetTestTable(tableRowsCount);
            using IXLWorkbook workbook = new XLWorkbook();
            var converter = Container.GetRequiredService<IExcelTableConverter>();

            // Act
            var result = converter.Convert(
                table,
                new ExcelTableConverterParameters
                {
                    WorksheetName = "Sheet1",
                    Workbook = workbook
                });

            // Assert
            result.Worksheets.First().Rows().Count().Should().Be(tableRowsCount);
            result.Worksheets.First().Columns().Count().Should().Be(2);
        }

        private Table GetTestTable(int count)
        {
            var list = Enumerable.Range(0, count)
                .Select(x => new RowData { Prop1 = x, Prop2 = nameof(ConvertTests) + x })
                .ToList();

            return new TableBuilder()
                .AddRowsFromList(list, 0, 0, p => p.Prop1, p => p.Prop2).Build();
        }

        private class RowData
        {
            public int Prop1 { get; set; }

            public string Prop2 { get; set; } = string.Empty;
        }
    }
}