namespace RxBim.Tools.Revit
{
    using System;
    using Autodesk.Revit.DB;
    using JetBrains.Annotations;

    /// <summary>
    /// Shared parameter data describing it in the definition file
    /// </summary>
    [PublicAPI]
    public class SharedParameterDefinition
    {
        /// <summary>
        /// Parameter name.
        /// </summary>
        public string ParameterName { get; private set; } = null!;

        /// <summary>
        /// Guid.
        /// </summary>
        public Guid? Guid { get; private set; }

        /// <summary>
        /// <see cref="ParameterType"/>.
        /// </summary>
        public ParameterType? DataType { get; private set; }

        /// <summary>
        /// Owner group name for parameter.
        /// </summary>
        public string? OwnerGroupName { get; private set; }

        /// <summary>
        /// Parameter description.
        /// </summary>
        public string? Description { get; private set; }

        /// <summary>
        /// Is parameter visible into project.
        /// </summary>
        public bool? Visible { get; private set; }

        /// <summary>
        /// Allow modify parameter.
        /// </summary>
        public bool? UserModifiable { get; private set; }

        /// <summary>
        /// Sets parameter name.
        /// </summary>
        /// <param name="parameterName">Parameter name.</param>
        public SharedParameterDefinition SetParameterName(string parameterName)
        {
            ParameterName = parameterName;
            return this;
        }

        /// <summary>
        /// Sets guid.
        /// </summary>
        /// <param name="guid">Guid.</param>
        public SharedParameterDefinition SetGuid(Guid guid)
        {
            Guid = guid;
            return this;
        }

        /// <summary>
        /// Sets <see cref="ParameterType"/>.
        /// </summary>
        /// <param name="dataType"><see cref="ParameterType"/></param>
        public SharedParameterDefinition SetDataType(ParameterType dataType)
        {
            DataType = dataType;
            return this;
        }

        /// <summary>
        /// Sets owner group name for parameter.
        /// </summary>
        /// <param name="ownerGroupName">Owner group name for parameter.</param>
        public SharedParameterDefinition SetOwnerGroupName(string ownerGroupName)
        {
            OwnerGroupName = ownerGroupName;
            return this;
        }

        /// <summary>
        /// Sets parameter description.
        /// </summary>
        /// <param name="description">Parameter description.</param>
        public SharedParameterDefinition SetDescription(string description)
        {
            Description = description;
            return this;
        }

        /// <summary>
        /// Enables parameter visible into project.
        /// </summary>
        public SharedParameterDefinition EnableVisible()
        {
            Visible = true;
            return this;
        }

        /// <summary>
        /// Enables modify parameter.
        /// </summary>
        public SharedParameterDefinition EnableUserModifiable()
        {
            UserModifiable = true;
            return this;
        }
    }
}
