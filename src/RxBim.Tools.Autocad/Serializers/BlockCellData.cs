namespace RxBim.Tools.Autocad.Serializers
{
    using Autodesk.AutoCAD.DatabaseServices;
    using TableBuilder.Abstractions;

    /// <summary>
    /// Данные для вставка блока в ячейку
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

        /// <summary>
        /// Установка блока в ячейку
        /// </summary>
        /// <param name="cell">Ячейка</param>
        /// <param name="blockData">Данные блока</param>
        public static Cell SetBlock(Cell cell, BlockCellData blockData)
        {
            if (blockData.BtrId.IsNull)
                return cell;

            cell.BlockTableRecordId = blockData.BtrId;

            var blockContent = cell.Contents[0];
            blockContent.IsAutoScale = blockData.AutoScale;

            if (!blockData.AutoScale)
                blockContent.Scale = blockData.Scale;

            blockContent.Rotation = blockData.Rotation;
            return cell;
        }
    }
}