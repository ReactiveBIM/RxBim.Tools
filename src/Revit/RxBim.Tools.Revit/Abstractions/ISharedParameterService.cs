namespace RxBim.Tools.Revit
{
    using System.Collections.Generic;
    using JetBrains.Annotations;

    /// <summary>
    /// Shared parameters service.
    /// </summary>
    [PublicAPI]
    public interface ISharedParameterService
    {
        /// <summary>
        /// Adds shared parameter from <see cref="IDefinitionFileWrapper"/>.
        /// </summary>
        /// <remarks>If parameter already exists, returns true.
        /// Verify exists parameter use <paramref name="fullMatch"/> argument.
        /// Without transaction.</remarks>
        /// <param name="definitionFile"><see cref="IDefinitionFileWrapper"/></param>
        /// <param name="sharedParameterInfo"><see cref="SharedParameterInfo"/></param>
        /// <param name="fullMatch">If true, find parameter by all required values from <see cref="SharedParameterDefinition"/>,
        /// otherwise parameter find only by name.
        /// (find in document only by <see cref="SharedParameterDefinition.ParameterName"/>,
        /// <see cref="SharedParameterDefinition.Guid"/>
        /// and <see cref="SharedParameterDefinition.DataType"/>)</param>
        /// <param name="document"><see cref="IDocumentWrapper"/>.
        /// If null, <see cref="IDocumentWrapper"/> gets from current document.</param>
        /// <exception cref="NotSetCategoriesForBindParameterException">
        /// Thrown if not set categories for bind parameter in <see cref="SharedParameterCreateData"/>.</exception>
        /// <exception cref="ParameterNotFoundException">
        /// Thrown if not found parameter in <paramref name="definitionFile"/>.</exception>
        bool AddSharedParameter(
            IDefinitionFileWrapper definitionFile,
            SharedParameterInfo sharedParameterInfo,
            bool fullMatch,
            IDocumentWrapper? document = null);

        /// <summary>
        /// Adds shared parameter from <see cref="IDefinitionFileWrapper"/>,
        /// or updates bindings for categories from <paramref name="sharedParameterInfo"/>
        /// (<see cref="SharedParameterCreateData.CategoriesForBind"/>).
        /// </summary>
        /// <remarks>Verify exists parameter use <paramref name="fullMatch"/> argument.
        /// Without transaction.</remarks>
        /// <param name="definitionFiles">Collection of <see cref="IDefinitionFileWrapper"/></param>
        /// <param name="sharedParameterInfo"><see cref="SharedParameterInfo"/></param>
        /// <param name="fullMatch">If true, find parameter by all required values from <see cref="SharedParameterDefinition"/>,
        /// otherwise parameter find only by name.
        /// (find in document only by <see cref="SharedParameterDefinition.ParameterName"/>,
        /// <see cref="SharedParameterDefinition.Guid"/>
        /// and <see cref="SharedParameterDefinition.DataType"/>)</param>
        /// <param name="isSavePastValues">If true,
        /// saves the values of the parameters of the elements of the existing linked categories
        /// and their further setting after updating the binding.</param>
        /// <param name="document"><see cref="IDocumentWrapper"/>.
        /// If null, <see cref="IDefinitionFileWrapper"/> gets from current document.</param>
        /// <exception cref="ParameterNotFoundException">
        /// Thrown if not found parameter in any <paramref name="definitionFiles"/>.</exception>
        /// <exception cref="MultipleParameterException">
        /// Thrown if found several parameters in <paramref name="definitionFiles"/>.</exception>
        /// <exception cref="NotSetCategoriesForBindParameterException">
        /// Thrown if not set categories for bind parameter in <see cref="SharedParameterCreateData"/>.</exception>
        bool AddOrUpdateParameter(
            IEnumerable<IDefinitionFileWrapper> definitionFiles,
            SharedParameterInfo sharedParameterInfo,
            bool fullMatch,
            bool isSavePastValues = false,
            IDocumentWrapper? document = null);

        /// <summary>
        /// Exists parameter in <see cref="IDefinitionFileWrapper"/>.
        /// </summary>
        /// <param name="definition"><see cref="SharedParameterDefinition"/></param>
        /// <param name="fullMatch">If true, find parameter by all required values from <see cref="SharedParameterDefinition"/>,
        /// otherwise parameter find only by name.</param>
        /// <param name="definitionFile"><see cref="IDefinitionFileWrapper"/></param>
        bool ExistsParameterInDefinitionFile(
            SharedParameterDefinition definition,
            bool fullMatch,
            IDefinitionFileWrapper definitionFile);

        /// <summary>
        /// Exists parameter in <see cref="IDocumentWrapper"/>.
        /// </summary>
        /// <param name="definition"><see cref="SharedParameterDefinition"/></param>
        /// <param name="fullMatch">If true, find parameter by all required values from <see cref="SharedParameterDefinition"/>,
        /// otherwise parameter find only by name.</param>
        /// <param name="document"><see cref="IDocumentWrapper"/>.
        /// If null, <see cref="IDefinitionFileWrapper"/> gets from current document.</param>
        bool ExistsParameterInDocument(
            SharedParameterDefinition definition,
            bool fullMatch,
            IDocumentWrapper? document = null);
    }
}
