﻿namespace RxBim.Tools.Autocad.Sample.Abstractions;

using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;

/// <summary>
/// Service for <see cref="Circle"/>.
/// </summary>
public interface ICircleService
{
    /// <summary>
    /// Returns true if getting the circle parameters was successful. Otherwise returns false.
    /// </summary>
    /// <param name="radius">Circle radius.</param>
    /// <param name="center">Circle center point.</param>
    bool TryGetCircleParams(out double radius, out Point3d center);

    /// <summary>
    /// Adds a circle to current space.
    /// </summary>
    /// <param name="context">Transaction context.</param>
    /// <param name="transactionWrapper">Transaction.</param>
    /// <param name="center">Center of circle.</param>
    /// <param name="radius">Radius of circle.</param>
    /// <param name="colorIndex">Color index of circle.</param>
    ObjectId AddCircle(
        ITransactionContextWrapper context,
        ITransactionWrapper transactionWrapper,
        Point3d center,
        double radius,
        int colorIndex);
}