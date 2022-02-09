namespace RxBim.Tools.Revit.Serializers
{
    /// <summary>
    /// Serialization options to Revit table.
    /// </summary>
    public class ViewScheduleTableSerializerParameters
    {
        /// <summary>
        /// The name of a table (specification).
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Bold line identifier.
        /// </summary>
        public int? SpecificationBoldLineId { get; set; }
    }
}