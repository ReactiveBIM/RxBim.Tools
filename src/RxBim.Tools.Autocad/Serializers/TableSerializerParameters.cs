namespace RxBim.Tools.Autocad.Serializers
{
    using Autodesk.AutoCAD.DatabaseServices;
    using TableBuilder.Abstractions;

    /// <summary>
    /// Параметры сериализации в таблицу автокада
    /// </summary>
    public class TableSerializerParameters : ITableSerializerParameters
    {
        /// <summary>
        /// Первая строка названия?
        /// </summary>
        public bool HasTitle { get; set; }

        /// <summary>
        /// Кол-во строк заголовков
        /// </summary>
        public int RowHeadersCount { get; set; } = 1;

        /// <summary>
        /// Стиль таблицы
        /// </summary>
        public ObjectId TableStyleId { get; set; }

        /// <summary>
        /// Чертеж
        /// </summary>
        public Database? Db { get; set; }

        /// <summary>
        /// Вес толстой линии
        /// </summary>
        public LineWeight LineBold { get; set; } = LineWeight.LineWeight050;

        /// <summary>
        /// Вес обычной линии
        /// </summary>
        public LineWeight LineUsual { get; set; } = LineWeight.LineWeight018;
    }
}