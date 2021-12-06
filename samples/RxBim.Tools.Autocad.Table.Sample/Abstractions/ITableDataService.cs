namespace RxBim.Tools.Autocad.Table.Sample.Abstractions
{
    using System.Collections.Generic;
    using Autodesk.AutoCAD.DatabaseServices;
    using CSharpFunctionalExtensions;
    using Table = TableBuilder.Models.Table;

    /// <summary>
    /// Data service for a table.
    /// </summary>
    public interface ITableDataService
    {
        /// <summary>
        /// Retrieving tabular data.
        /// </summary>
        /// <param name="ids">Drawing object identifiers.</param>
        Result<Table> GetTable(List<ObjectId> ids);
    }
}