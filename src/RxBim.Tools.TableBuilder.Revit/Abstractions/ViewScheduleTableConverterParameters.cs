namespace RxBim.Tools.TableBuilder
{
    /// <summary>
    /// Contains to Revit converter parameters.
    /// </summary>
    public class ViewScheduleTableConverterParameters
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