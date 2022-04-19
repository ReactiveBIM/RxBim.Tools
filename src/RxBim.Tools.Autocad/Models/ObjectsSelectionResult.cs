namespace RxBim.Tools.Autocad
{
    using System;
    using Autodesk.AutoCAD.DatabaseServices;

    /// <inheritdoc />
    internal class ObjectsSelectionResult : IObjectsSelectionResult
    {
        /// <inheritdoc/>
        public bool IsKeyword { get; set; }

        /// <inheritdoc/>
        public ObjectId[] SelectedObjects { get; set; } = Array.Empty<ObjectId>();

        /// <inheritdoc/>
        public string Keyword { get; set; } = string.Empty;
    }
}