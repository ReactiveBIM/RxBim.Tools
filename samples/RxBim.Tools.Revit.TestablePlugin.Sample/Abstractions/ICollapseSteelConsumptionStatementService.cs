namespace RxBim.Tools.Revit.TestablePlugin.Sample.Abstractions
{
    using CSharpFunctionalExtensions;

    /// <summary>
    /// Service for collapse steel consumption statement.
    /// </summary>
    public interface ICollapseSteelConsumptionStatementService
    {
        /// <summary>
        /// Collapse steel consumption statement.
        /// </summary>
        Result Collapse();
    }
}
