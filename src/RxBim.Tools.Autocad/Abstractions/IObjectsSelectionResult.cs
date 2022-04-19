namespace RxBim.Tools.Autocad
{
    using Autodesk.AutoCAD.DatabaseServices;

    /// <summary>
    /// Result of object selection
    /// </summary>
    public interface IObjectsSelectionResult
    {
        /// <summary>
        /// Returns true if the keyword was entered instead of selecting objects.
        /// </summary>
        bool IsKeyword { get; }

        /// <summary>
        /// Returns the ObjectId collection of the selected objects.
        /// </summary>
        ObjectId[] SelectedObjects { get; }

        /// <summary>
        /// Returns the entered keyword.
        /// </summary>
        string Keyword { get; }
    }
}