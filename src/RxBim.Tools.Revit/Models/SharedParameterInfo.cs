namespace RxBim.Tools.Revit.Models
{
    using JetBrains.Annotations;

    /// <summary>
    /// Информация об общем параметре
    /// </summary>
    [PublicAPI]
    public class SharedParameterInfo
    {
        /// <summary>
        /// Данные общего параметра, описывающие его в ФОП
        /// </summary>
        public SharedParameterDefinition Definition { get; set; }

        /// <summary>
        /// Данные для создания общего параметра в модели
        /// </summary>
        public SharedParameterCreateData CreateData { get; set; }
    }
}
