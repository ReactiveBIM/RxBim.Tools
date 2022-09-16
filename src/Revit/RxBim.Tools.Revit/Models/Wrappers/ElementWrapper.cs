namespace RxBim.Tools.Revit;

using Autodesk.Revit.DB;

/// <summary>
/// Wrapped <see cref="Element"/>.
/// </summary>
public class ElementWrapper
    : Wrapper<Element>, IElementWrapper
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ElementWrapper"/> class.
    /// </summary>
    /// <param name="wrappedObject"><see cref="Element"/></param>
    public ElementWrapper(Element wrappedObject)
        : base(wrappedObject)
    {
    }

    /// <inheritdoc />
    public bool IsValid
        => Object.IsValidObject;

    /// <inheritdoc />
    public T? GetParameterValue<T>(string parameterName)
        => Object.GetParameterValue<T>(parameterName);
}