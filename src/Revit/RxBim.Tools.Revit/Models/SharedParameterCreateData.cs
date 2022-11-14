namespace RxBim.Tools.Revit
{
    using System.Collections.Generic;
    using System.Linq;
    using Autodesk.Revit.DB;
    using JetBrains.Annotations;

    /// <summary>
    /// Data for creating shared parameter in model.
    /// </summary>
    [PublicAPI]
    public class SharedParameterCreateData
    {
        /// <summary>
        /// Collection of <see cref="BuiltInCategory"/>, for parameter binding.
        /// </summary>
        public IReadOnlyList<BuiltInCategory> CategoriesForBind { get; private set; } = new List<BuiltInCategory>();
        
        /// <summary>
        /// Allow "Values can vary by group instance" for parameter.
        /// </summary>
        public bool AllowVaryBetweenGroups { get; private set; }

        /// <summary>
        /// Is create parameter for instance.
        /// </summary>
        /// <remarks>If true, parameter create for instance, otherwise create for symbol.</remarks>
        public bool IsCreateForInstance { get; private set; } = true;

        /// <summary>
        /// <see cref="BuiltInParameterGroup"/> in which add shared parameter.
        /// </summary>
        public BuiltInParameterGroup ParameterGroup { get; private set; } = BuiltInParameterGroup.INVALID;

        /// <summary>
        /// Sets collection of <see cref="BuiltInCategory"/>, for parameter binding.
        /// </summary>
        /// <param name="categoriesForBind">Collection of <see cref="BuiltInCategory"/>, for parameter binding.</param>
        public SharedParameterCreateData SetCategoriesForBind(IEnumerable<BuiltInCategory> categoriesForBind)
        {
            CategoriesForBind = categoriesForBind.ToList();
            return this;
        }

        /// <summary>
        /// Enables "Values can vary by group instance" for parameter.
        /// </summary>
        public SharedParameterCreateData EnableVaryBetweenGroups()
        {
            AllowVaryBetweenGroups = true;
            return this;
        }

        /// <summary>
        /// Enables create parameter for instance.
        /// </summary>
        public SharedParameterCreateData EnableCreateForInstance()
        {
            IsCreateForInstance = true;
            return this;
        }

        /// <summary>
        /// Sets <see cref="BuiltInParameterGroup"/> in which add shared parameter.
        /// </summary>
        /// <param name="parameterGroup"><see cref="BuiltInParameterGroup"/> in which add shared parameter.</param>
        public SharedParameterCreateData SetParameterGroup(BuiltInParameterGroup parameterGroup)
        {
            ParameterGroup = parameterGroup;
            return this;
        }
    }
}
