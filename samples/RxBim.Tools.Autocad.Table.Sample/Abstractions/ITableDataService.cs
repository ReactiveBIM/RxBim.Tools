namespace RxBim.Tools.Autocad.Table.Sample.Abstractions
{
    using System.Collections.Generic;
    using Autodesk.AutoCAD.DatabaseServices;
    using CSharpFunctionalExtensions;
    using Table = TableBuilder.Table;

    /// <summary>
    /// Сервис данных для таблицы
    /// </summary>
    public interface ITableDataService
    {
        /// <summary>
        /// Получение табличных данных
        /// </summary>
        /// <param name="ids">Объекты чертежа</param>
        Result<Table> GetTable(List<ObjectId> ids);
    }
}