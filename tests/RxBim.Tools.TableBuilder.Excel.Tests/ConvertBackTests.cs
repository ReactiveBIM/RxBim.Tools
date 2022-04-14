namespace RxBim.Tools.TableBuilder.Excel.Tests
{
    using System.Linq;
    using ClosedXML.Excel;
    using Di;
    using FluentAssertions;
    using Services;
    using Xunit;

    /// <summary>
    /// Tests for <see cref="FromExcelTableConverter"/>
    /// </summary>
    public class ConvertBackTests : TestsBase
    {
        [Fact]
        public void ConverterTest()
        {
            // Arrange
            const int rowCount = 10;
            const int columnCount = 5;
            var converter = Container.GetRequiredService<IFromExcelTableConverter>();
            var workbook = GetTestWorkbook(rowCount, columnCount);

            // Act
            var table = converter.Convert(workbook, new FromExcelConverterParameters());

            // Assert
            table.Columns.Count.Should().Be(columnCount);
            table.Rows.Count.Should().Be(rowCount);
        }

        private IXLWorkbook GetTestWorkbook(int rowCount, int columnCount)
        {
            var workbook = new XLWorkbook();
            var worksheet = workbook.AddWorksheet();

            var data = Enumerable.Range(0, rowCount)
                .Select(r => Enumerable.Range(0, columnCount).Select(c => $"Row{r}-Column{c}").ToArray()).ToList();

            worksheet.Cell(1, 1).InsertData(data);
            return workbook;
        }
    }
}