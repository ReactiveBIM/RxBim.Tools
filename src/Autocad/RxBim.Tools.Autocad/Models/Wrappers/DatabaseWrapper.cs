namespace RxBim.Tools.Autocad
{
    using Autodesk.AutoCAD.DatabaseServices;

    /// <inheritdoc cref="RxBim.Tools.Autocad.IDatabaseWrapper" />
    public class DatabaseWrapper : Wrapper<Database>, IDatabaseWrapper
    {
        /// <inheritdoc />
        public DatabaseWrapper(Database contextObject)
            : base(contextObject)
        {
        }
    }
}