namespace RxBim.Tools.TableBuilder.Tests
{
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using FluentAssertions;
    using Xunit;
    using RxBim.Tools.TableBuilder;

    /// <summary>
    /// Tests for <see cref="TableBuilder"/>
    /// </summary>
    public class TableBuildingTests
    {
        /// <summary> Тест заполнения таблицы по строкам </summary>
        [Fact]
        public void FillRowsTest()
        {
            const int assert = 10;

            var data = GetTestData(assert);

            var result = new TableBuilder().SetWidth(100)
                .AddRowsFromList(data, 0, 0, p => p.Prop1, p => p.Prop2)
                .Build();

            result.Rows.Count().Should().Be(assert);
            result.Columns.Count().Should().Be(2);
        }

        /// <summary> Тест заполнения таблицы по столбцам </summary>
        [Fact]
        public void FillCellsTest()
        {
            const int assert = 10;
            var data = GetTestData(assert);
            var result = new TableBuilder().SetWidth(100)
                .AddColumnsFromList(data, 0, 0, p => p.Prop1, p => p.Prop2)
                .Build();
            result.Rows.Count().Should().Be(2);
            result.Columns.Count().Should().Be(assert);
        }

        /// <summary> Тест получения элемента по индексу </summary>
        [Fact]
        public void GetByIndexTest()
        {
            const int assert = 10;
            var data = GetTestData(assert);
            var table = new TableBuilder().SetWidth(100)
                .AddRowsFromList(data, 1, 1, p => p.Prop1, p => p.Prop2)
                .Build();
            table[0, 0].Content.ValueObject?.ToString().Should().BeEquivalentTo(string.Empty);
            table[0, 1].Content.ValueObject?.ToString().Should().BeEquivalentTo(string.Empty);
            table[1, 0].Content.ValueObject?.ToString().Should().BeEquivalentTo(string.Empty);
            table.Columns.First()
                .Cells
                .Select(x => x.Content.ValueObject?.ToString())
                .Should()
                .OnlyContain(x => x == string.Empty);
            table.Rows.First()
                .Cells
                .Select(x => x.Content.ValueObject?.ToString())
                .Should()
                .OnlyContain(x => x == string.Empty);
            for (var r = 1; r <= assert; r++)
            {
                var cell = table[r, 1];
                var cellData = cell.Content.ValueObject?.ToString();
                var srcData = data[r - 1];
                cellData.Should().BeEquivalentTo(srcData.Prop1.ToString());
                var rightCell = ((CellBuilder)cell).Next().ObjectForBuild;
                cellData = rightCell.Content.ValueObject?.ToString();
                cellData.Should().BeEquivalentTo(srcData.Prop2);
            }
        }

        /// <summary> Тест назначения стилей </summary>
        [Fact]
        public void CellsStyleTest()
        {
            const int assert = 5;
            const int columnWidth = 85;
            const int rowHeight = 95;
            var data = GetTestData(assert);
            var format = GetTestCellFormat();
            var table = new TableBuilder()
                .SetWidth(100)
                .AddColumn(x => x.SetFormat(format).SetWidth(columnWidth))
                .AddColumn(count: data.Count)
                .AddRow(x => x.FromList(data).SetHeight(rowHeight))
                .Build();
            table.Columns.First().GetComposedFormat().Should().BeEquivalentTo(format);
            table.Columns.First().Width.Should().Be(columnWidth);
            foreach (var x in table.Columns.First().Cells)
                x.GetComposedFormat().Should().BeEquivalentTo(format);
            table.Rows.First().Height.Should().Be(rowHeight);
        }

        /// <summary> Тест назначения стилей для всей таблицы </summary>
        [Fact]
        public void TableStyleTest()
        {
            const int assert = 5;
            const int columnWidth = 85;
            const int rowHeight = 95;
            var data = GetTestData(assert);
            var format = GetTestCellFormat();
            var table = new TableBuilder()
                .SetFormat(format)
                .AddColumn(x => x.SetWidth(columnWidth))
                .AddColumn(x => x.SetWidth(columnWidth), count: data.Count)
                .AddRow(x => x.FromList(data).SetHeight(rowHeight))
                .Build();
            table.Columns.Select(e => e.GetComposedFormat()).Should().AllBeEquivalentTo(format);
            table.Columns.Select(e => e.Width).Should().AllBeEquivalentTo(columnWidth);
            table.Columns.SelectMany(e => e.Cells.Select(x => x.GetComposedFormat()))
                .Should()
                .AllBeEquivalentTo(format);
            foreach (var x in table.Columns.First().Cells)
                x.GetComposedFormat().Should().BeEquivalentTo(format);
            table.Rows.Select(e => e.Height).Should().AllBeEquivalentTo(rowHeight);
        }

        /// <summary> Тест назначения стилей для всей таблицы </summary>
        [Fact]
        public void TableRangeStyleTest()
        {
            const int assert = 5;
            const int columnWidth = 85;
            const int rowHeight = 95;
            var data = GetTestData(assert);
            var format = GetTestCellFormat();
            var table = new TableBuilder()
                .AddColumn(x => x.SetWidth(columnWidth))
                .AddColumn(x => x.SetWidth(columnWidth), count: data.Count)
                .AddRow(x => x.FromList(data).SetHeight(rowHeight))
                .SetCellsFormat(format, 0, 1, 2, 1)
                .Build();
            table.Columns.Skip(1)
                .Take(2)
                .SelectMany(x => x.Cells.Select(e => e.GetComposedFormat()))
                .Should()
                .AllBeEquivalentTo(format);
            foreach (var x in table.Columns.First().Cells)
                x.GetComposedFormat().Should().NotBeEquivalentTo(format);
            foreach (var x in table.Columns[3].Cells)
                x.GetComposedFormat().Should().NotBeEquivalentTo(format);
            foreach (var x in table.Columns)
                x.GetComposedFormat().Should().NotBeEquivalentTo(format);
            table.Columns.Select(e => e.Width).Should().AllBeEquivalentTo(columnWidth);
            table.Rows.Select(e => e.Height).Should().AllBeEquivalentTo(rowHeight);
        }

        /// <summary> Тест объединения ячеек </summary>
        [Fact]
        public void CellMergeTest()
        {
            var data = GetTestData(10);

            var table = new TableBuilder()
                .SetWidth(100)
                .AddColumnsFromList(data, 0, 0, p => p.Prop1, p => p.Prop2)
                .AddColumnsFromList(data, 2, 0, p => p.Prop1, p => p.Prop2)
                .AddColumnsFromList(data, 4, 0, p => p.Prop1, p => p.Prop2)[1, 3]
                .MergeNext(1, (_, _) => { })
                .ToTable()[1, 3]
                .MergeDown(1, (_, _) => { })
                .ToTable()
                .Build();

            table[1, 3].Merged.Should().BeTrue();
            table[1, 4].Merged.Should().BeTrue();
            table[2, 3].Merged.Should().BeTrue();
            table[2, 4].Merged.Should().BeTrue(); // Т.к объединение возможно только в прямоугольник

            var mergedArea = table[2, 4].MergeArea;
            mergedArea.Should().NotBeNull();

            // nullable warning suppression
            if (mergedArea == null)
                return;
            mergedArea.Value.TopRow.Should().Be(1);
            mergedArea.Value.BottomRow.Should().Be(2);
            mergedArea.Value.LeftColumn.Should().Be(3);
            mergedArea.Value.RightColumn.Should().Be(4);
        }

        /// <summary> Тест задает данные объединенной ячейке и они прописываются всем ячейкам в объединении </summary>
        [Fact]
        public void SetDataToMergedCell()
        {
            const int colCount = 4;
            const int rowCount = 4;
            var cellValue = new TextCellContent("test");

            var table = new TableBuilder()
                .AddColumn(count: colCount)
                .AddRow(count: rowCount)
                .Rows
                .First()
                .Cells
                .First()
                .MergeNext(colCount - 1)
                .MergeDown(rowCount - 1)
                .SetContent(cellValue)
                .ToTable()
                .Build();

            for (var i = 0; i < colCount; i++)
            {
                for (var j = 0; j < rowCount; j++)
                    table[j, i].Content.Should().Be(cellValue);
            }
        }

        /// <summary> Тест задает формат объединенной ячейке и они прописываются всем ячейкам в объединении </summary>
        [Fact]
        public void SetFormatToMergedCell()
        {
            const int colCount = 4;
            const int rowCount = 4;
            var format = GetTestCellFormat();

            Table table = new TableBuilder()
                .AddColumn(count: colCount)
                .AddRow(count: rowCount)
                .Rows
                .First()
                .Cells
                .First()
                .MergeNext(colCount - 1)
                .MergeDown(rowCount - 1)
                .SetFormat(format)
                .ToTable();

            for (var i = 0; i < colCount - 1; i++)
            {
                for (var j = 0; j < rowCount - 1; j++)
                    table[j, i].GetComposedFormat().Should().BeEquivalentTo(format);
            }
        }

        /// <summary> Тест при объединении ячеек выдается последняя ячейка в объединении </summary>
        [Fact]
        public void MergeCellReturnLastCell()
        {
            const int colCount = 4;
            const int rowCount = 4;

            var table = new TableBuilder()
                .AddColumn(count: colCount)
                .AddRow(count: rowCount)
                .Build();

            var nextMergedCell = ((CellBuilder)table.Rows.First().Cells.First()).MergeNext().ObjectForBuild;
            nextMergedCell.GetColumnIndex().Should().Be(1);

            var downMergedCell = ((CellBuilder)table.Columns.Last().Cells.First()).MergeDown().ObjectForBuild;
            downMergedCell.Row.GetIndex().Should().Be(1);

            var leftMergedCell = ((CellBuilder)table.Rows.Last().Cells[1]).MergeLeft().ObjectForBuild;
            leftMergedCell.GetColumnIndex().Should().Be(0);
        }

        private static CellFormatStyle GetTestCellFormat()
        {
            var format = new CellFormatStyleBuilder()
                .SetBackgroundColor(Color.Bisque)
                .SetContentHorizontalAlignment(CellContentHorizontalAlignment.Left)
                .SetContentVerticalAlignment(CellContentVerticalAlignment.Top)
                .SetBorders(builder => builder
                    .SetBorders(left: CellBorderType.Hidden, bottom: CellBorderType.Hidden))
                .SetTextFormat(x => x
                    .SetBold(true)
                    .SetItalic(true)
                    .SetTextColor(Color.Blue)
                    .SetTextSize(55))
                .Build();

            return format;
        }

        private List<Example> GetTestData(int count) =>
            Enumerable.Range(0, count)
                .Select(x => new Example { Prop1 = x, Prop2 = nameof(TableBuildingTests) + x })
                .ToList();

        private class Example
        {
            public int Prop1 { get; set; }

            public string Prop2 { get; set; } = string.Empty;
        }
    }
}
