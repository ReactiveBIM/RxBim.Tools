namespace RxBim.Tools.Autocad
{
    using Abstractions;
    using Autodesk.AutoCAD.DatabaseServices;
    using Di;
    using Serializers;
    using Services;
    using TableBuilder.Abstractions;

    /// <summary>
    /// Расширения для контейнера
    /// </summary>
    public static class ContainerExtensions
    {
        /// <summary>
        /// Добавляет сервисы работы с AutoCAD в контейнер
        /// </summary>
        /// <param name="container">Контейнер</param>
        public static void AddAutocadHelpers(this IContainer container)
        {
            container.AddSingleton<IObjectsSelectionService, ObjectsSelectionService>();
            container.AddSingleton<ICommandLineService, CommandLineService>();
        }

        /// <summary>
        /// Добавляет сериализатор таблицы в автокад
        /// </summary>
        /// <param name="container">Контейнер</param>
        public static IContainer AddTableSerializer(this IContainer container)
        {
            container.AddSingleton<ITableSerializer<Table>, TableSerializer>();
            return container;
        }
    }
}