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

#if RVT2019 || RVT2020 || RVT2021 || RVT2022 || RVT2023
        /// <summary>
        /// Bold line identifier.
        /// </summary>
        public int? SpecificationBoldLineId { get; set; }
#else
        /// <summary>
        /// Bold line identifier.
        /// </summary>
        public long? SpecificationBoldLineId { get; set; }
#endif
    }
}