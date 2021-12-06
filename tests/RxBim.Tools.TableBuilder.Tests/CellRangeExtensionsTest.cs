namespace RxBim.Tools.TableBuilder.Tests
{
    using Extensions;
    using FluentAssertions;
    using Models;
    using Xunit;

    /// <summary>
    /// Tests for <see cref="CellRangeExtensions"/>
    /// </summary>
    public class CellRangeExtensionsTest
    {
        /// <summary>
        /// Test for <see cref="CellRangeExtensions.IsInsideFor"/> method.
        /// </summary>
        /// <param name="topRow">The index of the top row of the range to be checked.</param>
        /// <param name="leftColumn">The index of the left column of the range to be checked.</param>
        /// <param name="bottomRow">The index of the bottom row of the range to be checked.</param>
        /// <param name="rightColumn">The index of the right column of the range to be checked.</param>
        /// <param name="result">Expected result.</param>
        [Theory]
        [InlineData(0, 0, 0, 0, false)]
        [InlineData(2, 2, 6, 6, false)]
        [InlineData(2, 2, 4, 4, true)]
        [InlineData(0, 0, 4, 4, false)]
        [InlineData(1, 1, 5, 5, true)]
        public void IsInsideForTest(int topRow, int leftColumn, int bottomRow, int rightColumn, bool result)
        {
            var range = GetTestRange();
            new CellRange(topRow, leftColumn, bottomRow, rightColumn).IsInsideFor(range).Should().Be(result);
        }

        /// <summary>
        /// Test for <see cref="CellRangeExtensions.IsIntersectWith"/> method.
        /// </summary>
        /// <param name="topRow">The index of the top row of the range to be checked.</param>
        /// <param name="leftColumn">The index of the left column of the range to be checked.</param>
        /// <param name="bottomRow">The index of the bottom row of the range to be checked.</param>
        /// <param name="rightColumn">The index of the right column of the range to be checked.</param>
        /// <param name="result">Expected result.</param>
        [Theory]
        [InlineData(0, 0, 1, 1, false)]
        [InlineData(1, 2, 3, 4, true)]
        [InlineData(0, 0, 4, 4, true)]
        [InlineData(3, 3, 7, 7, true)]
        [InlineData(5, 0, 7, 5, false)]
        public void IsIntersectWithTest(int topRow, int leftColumn, int bottomRow, int rightColumn, bool result)
        {
            var range = GetTestRange();
            new CellRange(topRow, leftColumn, bottomRow, rightColumn).IsIntersectWith(range).Should().Be(result);
        }

        private static CellRange GetTestRange()
        {
            return new CellRange(1, 1, 5, 5);
        }
    }
}