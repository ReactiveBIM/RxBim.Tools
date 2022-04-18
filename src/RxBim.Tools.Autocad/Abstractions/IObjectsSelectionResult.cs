namespace RxBim.Tools.Autocad.Abstractions
{
    using Autodesk.AutoCAD.DatabaseServices;

    /// <summary>
    /// Результат выбора объектов
    /// </summary>
    public interface IObjectsSelectionResult
    {
        /// <summary>
        /// Возвращает истину если было введено ключевое слово вместо выбора объектов
        /// </summary>
        bool IsKeyword { get; }

        /// <summary>
        /// Возвращает коллекцию ObjectId выбранных объектов
        /// </summary>
        ObjectId[] SelectedObjects { get; }

        /// <summary>
        /// Возвращает введённое ключевое слово
        /// </summary>
        string Keyword { get; }
    }
}