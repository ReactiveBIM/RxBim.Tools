namespace RxBim.Tools.Autocad;

using Autodesk.AutoCAD.DatabaseServices;

/// <summary>
/// Wrapper for <see cref="ObjectId"/>.
/// </summary>
public class ObjectIdWrapper(ObjectId wrappedObject)
    : Wrapper<ObjectId>(wrappedObject), IObjectIdWrapper
{
    /// <inheritdoc />
    public override string ToString()
        => Object.ToString();
}