namespace RxBim.Command.TableBuilder.Autocad.Sample.Abstractions;

using System.Collections.Generic;
using Autodesk.AutoCAD.DatabaseServices;
using CSharpFunctionalExtensions;
using Table = Tools.TableBuilder.Table;

/// <summary>
/// Data service for a table.
/// </summary>
public interface ITableDataService
{
    /// <summary>
    /// Retrieving tabular data.
    /// </summary>
    /// <param name="ids">Drawing object identifiers.</param>
    Result<Table> GetTable(IEnumerable<ObjectId> ids);
}