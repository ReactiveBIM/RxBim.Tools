namespace RxBim.Tools.Autocad;

using System.Collections.Generic;
using System.Linq;
using Autodesk.AutoCAD.DatabaseServices;

/// <inheritdoc />
internal class ObjectsSelectionResult : IObjectsSelectionResult
{
    /// <summary>
    /// Empty result
    /// </summary>
    public static ObjectsSelectionResult Empty { get; } = new();

    /// <inheritdoc/>
    public bool IsKeyword { get; set; }

    /// <inheritdoc/>
    public IEnumerable<ObjectId> SelectedObjects { get; set; } = Enumerable.Empty<ObjectId>();

    /// <inheritdoc/>
    public string Keyword { get; set; } = string.Empty;

    /// <summary>
    /// Empty selection
    /// </summary>
    public bool IsEmpty => ReferenceEquals(this, Empty);
}