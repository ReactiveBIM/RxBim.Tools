﻿namespace RxBim.Tools.TableBuilder;

/// <summary>
/// Range of table cells.
/// </summary>
public readonly struct CellRange
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CellRange"/> class.
    /// </summary>
    /// <param name="topRow"><see cref="TopRow"/></param>
    /// <param name="bottomRow"><see cref="BottomRow"/></param>
    /// <param name="leftColumn"><see cref="LeftColumn"/></param>
    /// <param name="rightColumn"><see cref="RightColumn"/></param>
    public CellRange(int topRow, int bottomRow, int leftColumn, int rightColumn)
    {
        TopRow = topRow;
        BottomRow = bottomRow;
        LeftColumn = leftColumn;
        RightColumn = rightColumn;
    }

    /// <summary>
    /// The top row index of the range.
    /// </summary>
    public int TopRow { get; }

    /// <summary>
    /// The bottom row index of the range.
    /// </summary>
    public int BottomRow { get; }

    /// <summary>
    /// The left column index of the range.
    /// </summary>
    public int LeftColumn { get; }

    /// <summary>
    /// The right column index of the range.
    /// </summary>
    public int RightColumn { get; }

    /// <summary>
    /// The range indexes contain valid values.
    /// </summary>
    public bool IsValid => TopRow >= 0 && LeftColumn >= 0 && BottomRow >= TopRow && RightColumn >= LeftColumn;
}