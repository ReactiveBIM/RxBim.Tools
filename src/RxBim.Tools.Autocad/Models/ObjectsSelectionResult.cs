namespace RxBim.Tools.Autocad
{
    using Autodesk.AutoCAD.DatabaseServices;

    /// <summary>
    /// Результат выбора
    /// </summary>
    internal class ObjectsSelectionResult : IObjectsSelectionResult
    {
        /// <summary>
        /// Флаг того, что было введено ключевое слово вместо выбора объектов
        /// </summary>
        public bool IsKeyword { get; set; }

        /// <summary>
        /// Коллекция ObjectId выбранных объектов - если были выбраны объекты
        /// </summary>
        public ObjectId[] SelectedObjects { get; set; } = new ObjectId[0];

        /// <summary>
        /// Ключевое слово - если было введено ключевое слово
        /// </summary>
        public string Keyword { get; set; } = string.Empty;
    }
}