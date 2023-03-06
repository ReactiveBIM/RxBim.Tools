namespace RxBim.Tools.TableBuilder;

using System.Collections.Generic;

/// <inheritdoc />
public class ReferenceEqualityComparer : EqualityComparer<object>
{
    /// <inheritdoc />
    public override bool Equals(object x, object y)
    {
        var typeX = x.GetType();
        var typeY = y.GetType();
        if (typeX != typeY)
            return false;
        if (typeX.IsValueType || x is string)
            return x.Equals(y);
        return ReferenceEquals(x, y);
    }

    /// <inheritdoc />
    public override int GetHashCode(object? obj)
    {
        return obj == null ? 0 : obj.GetHashCode();
    }
}