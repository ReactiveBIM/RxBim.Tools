namespace RxBim.Tools.TableBuilder
{
    /// <summary>
    /// Contains to Revit converter parameters.
    /// </summary>
    public class ViewScheduleTableConverterParameters
    {
        /// <summary>
        /// The name of a ViewSchedule.
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// Bold line identifier.
        /// </summary>
        public int? SpecificationBoldLineId { get; set; }
    }
}