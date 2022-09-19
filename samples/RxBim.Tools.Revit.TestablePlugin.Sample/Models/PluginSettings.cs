namespace RxBim.Tools.Revit.TestablePlugin.Sample.Models
{
    using JetBrains.Annotations;

    /// <summary>
    /// Константные значения
    /// </summary>
    [PublicAPI]
    public class PluginSettings
    {
        /// <summary>
        /// Specification name.
        /// </summary>
        public string ScheduleName { get; set; } = string.Empty;
        
        /// <summary>
        /// Symbol for founding cell.
        /// </summary>
        public string CellFoundSymbol { get; set; } = string.Empty;
    }
}
