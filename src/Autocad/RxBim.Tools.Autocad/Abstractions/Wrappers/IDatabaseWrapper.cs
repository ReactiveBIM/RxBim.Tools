namespace RxBim.Tools.Autocad;

using Autodesk.AutoCAD.DatabaseServices;

/// <summary>
/// Wrapper for <see cref="Database"/>.
/// </summary>
public interface IDatabaseWrapper : ITransactionContextWrapper;