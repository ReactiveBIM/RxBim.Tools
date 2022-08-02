namespace RxBim.Tools.Autocad.Sample
{
    using Abstractions;
    using Di;
    using Services;

    /// <inheritdoc />
    public class Config : ICommandConfiguration
    {
        /// <inheritdoc />
        public void Configure(IContainer container)
        {
            container.AddAutocadHelpers();
            container.AddSingleton<ICircleService, CircleService>();
            container.AddSingleton<IEntityService, EntityService>();
        }
    }
}