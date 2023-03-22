namespace RxBim.Tools.LogStorage.Sample.Models;

using RxBim.Tools;

/// <summary>
/// Wrapper for integer value.
/// </summary>
public class ObjectIdWrapper : Wrapper<int>, IObjectIdWrapper
{
    /// <inheritdoc />
    public ObjectIdWrapper(int wrappedObject)
        : base(wrappedObject)
    {
    }
}