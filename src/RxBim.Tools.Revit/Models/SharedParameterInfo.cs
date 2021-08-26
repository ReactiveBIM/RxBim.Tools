namespace RxBim.Tools.Revit.Models
{
    /// <summary>
    /// Информация об общем параметре
    /// </summary>
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
