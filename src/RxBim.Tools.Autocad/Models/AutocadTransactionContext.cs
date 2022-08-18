namespace RxBim.Tools.Autocad
{
    using Autodesk.AutoCAD.ApplicationServices;
    using Autodesk.AutoCAD.DatabaseServices;
    using JetBrains.Annotations;

    /// <summary>
    /// <see cref="ITransactionContext"/> realisation for AutoCAD.
    /// </summary>
    [PublicAPI]
    public class AutocadTransactionContext : ITransactionContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AutocadTransactionContext"/> class.
        /// </summary>
        /// <param name="database"><see cref="Database"/> object.</param>
        public AutocadTransactionContext(Database database)
        {
            ContextObject = database;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AutocadTransactionContext"/> class.
        /// </summary>
        /// <param name="document"><see cref="Document"/> object.</param>
        public AutocadTransactionContext(Document document)
        {
            ContextObject = document;
        }

        /// <inheritdoc />
        public object ContextObject { get; }
    }
}