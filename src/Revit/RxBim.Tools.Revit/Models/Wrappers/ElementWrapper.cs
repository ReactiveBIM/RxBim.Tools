namespace RxBim.Tools.Revit;

using Autodesk.Revit.DB;

/// <summary>
/// Wrapped <see cref="Element"/>.
/// </summary>
public abstract class ElementWrapper : Wrapper<Element>, IElementWrapper
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ElementWrapper"/> class.
    /// </summary>
    /// <param name="wrappedObject"><see cref="Element"/></param>
    protected ElementWrapper(Element wrappedObject)
        : base(wrappedObject)
    {
    }

    /// <inheritdoc />
    public T? GetParameterValue<T>(string parameterName)
        => Object.GetParameterValue<T>(parameterName);
}