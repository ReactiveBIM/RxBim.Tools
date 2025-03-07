namespace RxBim.Tools.TableBuilder.Tests;

using FluentAssertions;
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
        new CellRange(topRow, bottomRow, leftColumn, rightColumn).IsInsideFor(range).Should().Be(result);
    }

    /// <summary>
    /// Test for <see cref="CellRangeExtensions.IsIntersectWith"/> method.
    /// </summary>
    /// <param name="topRow">The index of the top row of the range to be checked.</param>
    /// <param name="bottomRow">The index of the bottom row of the range to be checked.</param>
    /// <param name="leftColumn">The index of the left column of the range to be checked.</param>
    /// <param name="rightColumn">The index of the right column of the range to be checked.</param>
    /// <param name="result">Expected result.</param>
    [Theory]
    [InlineData(0, 0, 0, 10, false)]
    [InlineData(1, 2, 3, 4, true)]
    [InlineData(0, 4, 0, 4, true)]
    [InlineData(3, 3, 6, 9, false)]
    [InlineData(6, 7, 0, 10, false)]
    public void IsIntersectWithTest(int topRow, int bottomRow, int leftColumn, int rightColumn, bool result)
    {
        var range = GetTestRange();
        new CellRange(topRow, bottomRow, leftColumn, rightColumn).IsIntersectWith(range).Should().Be(result);
    }

    private static CellRange GetTestRange()
    {
        return new CellRange(1, 5, 1, 5);
    }
}