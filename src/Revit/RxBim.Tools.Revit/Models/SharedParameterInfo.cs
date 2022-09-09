namespace RxBim.Tools.Revit
{
    using System;
    using JetBrains.Annotations;

    /// <summary>
    /// Shared parameter information.
    /// </summary>
    [PublicAPI]
    public class SharedParameterInfo
    {
        /// <summary>
        /// <see cref="SharedParameterDefinition"/>
        /// </summary>
        public SharedParameterDefinition Definition { get; } = new();

        /// <summary>
        /// <see cref="SharedParameterCreateData"/>
        /// </summary>
        public SharedParameterCreateData CreateData { get; } = new();

        /// <summary>
        /// Sets <see cref="SharedParameterDefinition"/>.
        /// </summary>
        /// <param name="definitionAction"><see cref="SharedParameterDefinition"/></param>
        public SharedParameterInfo SetDefinition(
            Action<SharedParameterDefinition> definitionAction)
        {
            definitionAction(Definition);
            return this;
        }

        /// <summary>
        /// Sets <see cref="SharedParameterCreateData"/>.
        /// </summary>
        /// <param name="createDataAction"><see cref="SharedParameterCreateData"/></param>
        public SharedParameterInfo SetCreateData(Action<SharedParameterCreateData> createDataAction)
        {
            createDataAction(CreateData);
            return this;
        }
    }
}
