namespace RxBim.Tools.Tests.Models;

/// <summary>
/// Wrapper for integer value.
/// </summary>
public class ObjectIdWrapper : Wrapper<int>, IObjectIdWrapper
{
    public ObjectIdWrapper(int wrappedObject) : base(wrappedObject)
    {
    }
}