namespace RxBim.Tools.Autocad
{
    using Autodesk.AutoCAD.DatabaseServices;
    using JetBrains.Annotations;

    /// <summary>
    /// Database extensions
    /// </summary>
    [PublicAPI]
    public static class DatabaseExtensions
    {
        /// <summary>
        /// Returns the id of the text style by name.
        /// If there is no style with that name in the drawing, the ID of the current style is returned.
        /// </summary>
        /// <param name="db">Database</param>
        /// <param name="textStyleName">Text style name</param>
        public static ObjectId GetTextStyleId(this Database db, string textStyleName)
        {
            using var txtStylesTable = db.TextStyleTableId.OpenAs<TextStyleTable>();
            return txtStylesTable.Has(textStyleName) ? txtStylesTable[textStyleName] : db.Textstyle;
        }

        /// <summary>
        /// Returns <see cref="Database"/> wrapped to <see cref="ITransactionContext"/>.
        /// </summary>
        /// <param name="database"><see cref="Database"/> object.</param>
        public static ITransactionContext ToTransactionContext(this Database database)
        {
            return new TransactionContext(database);
        }
    }
}