namespace RxBim.Tools.Autocad;

using System;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;

/// <summary>
/// Helper class for filtering the object added to the user's selection
/// </summary>
internal class SelectionAddedFilter : IDisposable
{
    private readonly Func<ObjectId, bool> _checkFunc;
    private readonly Editor _editor;

    /// <summary>
    /// Initializes a new instance of the <see cref="SelectionAddedFilter"/> class.
    /// </summary>
    /// <param name="editor">Editor</param>
    /// <param name="checkFunc">
    /// ID validation function that returns true if the object is suitable to add to the selection.
    /// </param>
    public SelectionAddedFilter(Editor editor, Func<ObjectId, bool> checkFunc)
    {
        _editor = editor;
        _checkFunc = checkFunc;
        _editor.SelectionAdded += EditorSelectionAdded;
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        _editor.SelectionAdded -= EditorSelectionAdded;
    }

    /// <summary>
    /// Method for filtering objects selected using GetSelection in real time.
    /// </summary>
    private void EditorSelectionAdded(object sender, SelectionAddedEventArgs e)
    {
        var selIds = e.AddedObjects.GetObjectIds();
        for (var i = 0; i < selIds.Length; i++)
        {
            if (!_checkFunc(selIds[i]))
            {
                e.Remove(i);
            }
        }
    }
}