namespace RxBim.Tools.Serializer.Excel.Models
{
    using ClosedXML.Excel;

    /// <summary>
    /// Excel serializer parameters
    /// </summary>
    public class ExcelTableSerializerParameters
    {
        /// <summary>
        /// Excel document
        /// </summary>
        public XLWorkbook? Document { get; set; }

        /// <summary>
        /// Worksheet name
        /// </summary>
        public string? WorksheetName { get; set; }

        /// <summary>
        /// Number of freeze rows
        /// </summary>
        public int FreezeRows { get; set; } = 0;

        /// <summary>
        /// Adding an AutoFilter to a Range
        /// </summary>
        public (int FromRow, int FromColumn, int ToRow, int ToColumn) AutoFilterRange { get; set; }
    }
}