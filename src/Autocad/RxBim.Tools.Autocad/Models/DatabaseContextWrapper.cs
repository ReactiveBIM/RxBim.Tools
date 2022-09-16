namespace RxBim.Tools.Autocad
{
    using Autodesk.AutoCAD.DatabaseServices;

    /// <inheritdoc />
    public class DatabaseContextWrapper : TransactionContextWrapper<Database>
    {
        /// <inheritdoc />
        public DatabaseContextWrapper(Database contextObject)
            : base(contextObject)
        {
        }
    }
}