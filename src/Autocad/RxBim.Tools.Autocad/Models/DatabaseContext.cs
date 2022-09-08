namespace RxBim.Tools.Autocad
{
    using Autodesk.AutoCAD.DatabaseServices;

    /// <inheritdoc />
    public class DatabaseContext : TransactionContext<Database>
    {
        /// <inheritdoc />
        public DatabaseContext(Database contextObject)
            : base(contextObject)
        {
        }
    }
}