namespace RxBim.Tools.Autocad.Serializers
{
    using Autodesk.AutoCAD.DatabaseServices;
    using TableBuilder.Abstractions;

    /// <summary>
    /// Данные для вставки блока в ячейку
    /// </summary>
    public class BlockCellData : ICellData
    {
        /// <summary>
        /// Id блока
        /// </summary>
        public ObjectId BtrId { get; set; }

        /// <summary>
        /// Вписывание
        /// </summary>
        public bool AutoScale { get; set; }

        /// <summary>
        /// Масштаб
        /// </summary>
        public double Scale { get; set; }

        /// <summary>
        /// Поворот радиан
        /// </summary>
        public double Rotation { get; set; }
    }
}