namespace RxBim.Tools.Revit
{
    using JetBrains.Annotations;

    /// <summary>
    /// Информация об источниках для файлов общих параметров
    /// </summary>
    [PublicAPI]
    public class SharedParameterFileSource
    {
        /// <summary>
        /// Список путей к файлам общих параметров
        /// </summary>
        public string[]? FilePaths { get; set; }
    }
}
