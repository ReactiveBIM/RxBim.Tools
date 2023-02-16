namespace RxBim.Tools.TableBuilder;

using System.Collections.Generic;

/// <inheritdoc />
public class ReferenceEqualityComparer : EqualityComparer<object>
{
    /// <inheritdoc />
    public override bool Equals(object x, object y)
    {
        return ReferenceEquals(x, y);
    }

    /// <inheritdoc />
    public override int GetHashCode(object? obj)
    {
        return obj == null ? 0 : obj.GetHashCode();
    }
}