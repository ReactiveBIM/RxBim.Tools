namespace RxBim.Tools.Revit.Models
{
    using System;
    using JetBrains.Annotations;

    /// <summary>
    /// Информация об общем параметре
    /// </summary>
    [PublicAPI]
    public record SharedParameterInfo(
        SharedParameterDefinition Definition,
        SharedParameterCreateData CreateData)
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <summary>
        /// Данные общего параметра, описывающие его в ФОП
        /// </summary>
        public SharedParameterDefinition Definition { get; } = Definition ?? throw new NullReferenceException(nameof(Definition) + " not defined");

        /// <summary>
        /// Данные для создания общего параметра в модели
        /// </summary>
        public SharedParameterCreateData CreateData { get; } = CreateData ?? throw new NullReferenceException(nameof(CreateData) + " not defined");
    }
}
