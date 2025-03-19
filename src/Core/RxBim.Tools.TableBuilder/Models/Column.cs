﻿namespace RxBim.Tools.TableBuilder;

/// <summary>
/// Table column data.
/// </summary>
public class Column : CellsSet
{
    private double? _width;

    /// <summary>
    /// Initializes a new instance of the <see cref="Column"/> class.
    /// </summary>
    /// <param name="table">The <see cref="Table"/> that this <see cref="Column"/> belongs to.</param>
    internal Column(Table table)
        : base(table)
    {
    }

    /// <summary>
    /// The width of the column.
    /// </summary>
    public double? Width
    {
        get => _width;
        internal set
        {
            IsAdjustedToContent = false;
            _width = value;
        }
    }
}