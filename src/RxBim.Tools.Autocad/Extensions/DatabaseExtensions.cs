namespace RxBim.Tools.Autocad
{
    using Autodesk.AutoCAD.DatabaseServices;

    /// <summary>
    /// Database extensions
    /// </summary>
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
    }
}