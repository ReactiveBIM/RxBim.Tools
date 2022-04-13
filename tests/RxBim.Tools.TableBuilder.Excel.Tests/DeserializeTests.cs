namespace RxBim.Tools.TableBuilder.Excel.Tests
{
    using System.Linq;
    using ClosedXML.Excel;
    using Di;
    using FluentAssertions;
    using Services;
    using Xunit;

    /// <summary>
    /// Tests for <see cref="ExcelTableDeserializer"/>
    /// </summary>
    public class DeserializeTests : TestsBase
    {
        [Fact]
        public void ExcelSerializerTest()
        {
            // Arrange
            const int rowCount = 10;
            const int columnCount = 5;
            var excelDeserializer = Container.GetRequiredService<IExcelTableDeserializer>();
            var workSheet = GetTestWorkSheet(rowCount, columnCount);

            // Act
            var table = excelDeserializer.Deserialize(workSheet);

            // Assert
            table.Columns.Count.Should().Be(columnCount);
            table.Rows.Count.Should().Be(rowCount);
        }

        private IXLWorksheet GetTestWorkSheet(int rowCount, int columnCount)
        {
            var workBook = new XLWorkbook();
            var workSheet = workBook.AddWorksheet();

            var data = Enumerable.Range(0, rowCount)
                .Select(r => Enumerable.Range(0, columnCount).Select(c => $"Row{r}-Column{c}").ToArray()).ToList();

            workSheet.Cell(1, 1).InsertData(data);
            return workSheet;
        }
    }
}