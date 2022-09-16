namespace RxBim.Tools.Revit;

using Autodesk.Revit.DB;

/// <summary>
/// Wrapped <see cref="DefinitionFile"/>.
/// </summary>
public class DefinitionFileWrapper
    : Wrapper<DefinitionFile>, IDefinitionFileWrapper
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DefinitionFileWrapper"/> class.
    /// </summary>
    /// <param name="wrappedObject"><see cref="DefinitionFile"/></param>
    public DefinitionFileWrapper(DefinitionFile wrappedObject)
        : base(wrappedObject)
    {
    }
}