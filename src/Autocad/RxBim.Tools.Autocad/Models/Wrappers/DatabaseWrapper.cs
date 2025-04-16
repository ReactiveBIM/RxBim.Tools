namespace RxBim.Tools.Autocad;

using Autodesk.AutoCAD.DatabaseServices;

/// <inheritdoc cref="RxBim.Tools.Autocad.IDatabaseWrapper" />
public class DatabaseWrapper(Database contextObject)
    : Wrapper<Database>(contextObject), IDatabaseWrapper;