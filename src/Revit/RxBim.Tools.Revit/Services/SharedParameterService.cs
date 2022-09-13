namespace RxBim.Tools.Revit
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Autodesk.Revit.DB;
    using Autodesk.Revit.UI;
    using JetBrains.Annotations;

    /// <inheritdoc />
    [UsedImplicitly]
    internal class SharedParameterService : ISharedParameterService
    {
        private readonly UIApplication _uiApplication;

        /// <summary>
        /// Initializes a new instance of the <see cref="SharedParameterService"/> class.
        /// </summary>
        /// <param name="uiApplication"><see cref="UIApplication"/></param>
        public SharedParameterService(UIApplication uiApplication)
        {
            _uiApplication = uiApplication;
        }

        private Document Document => _uiApplication.ActiveUIDocument.Document;

        /// <inheritdoc />
        public bool AddSharedParameter(
            IDefinitionFileWrapper definitionFile,
            SharedParameterInfo sharedParameterInfo,
            bool fullMatch,
            IDocumentWrapper? document = null)
        {
            if (ExistsParameterInDocument(sharedParameterInfo.Definition, fullMatch, document))
                return true;
            if (sharedParameterInfo.CreateData.CategoriesForBind.Any() != true)
                throw new NotSetCategoriesForBindParameterException(sharedParameterInfo.Definition.ParameterName);

            return AddSharedParameterInternal(
                document,
                definitionFile,
                sharedParameterInfo,
                fullMatch);
        }

        /// <inheritdoc />
        public bool AddOrUpdateParameter(
            IEnumerable<IDefinitionFileWrapper> definitionFiles,
            SharedParameterInfo sharedParameterInfo,
            bool fullMatch,
            bool isSavePastValues = false,
            IDocumentWrapper? document = null)
        {
            var externalDefinitionInFile = definitionFiles
                .Select(df => new
                {
                    ExternalDefinition = GetSharedExternalDefinition(
                        sharedParameterInfo.Definition,
                        fullMatch,
                        df),
                    DefinitionFile = df
                })
                .Where(x => x.ExternalDefinition is not null)
                .ToList();

            if (!externalDefinitionInFile.Any())
                throw new ParameterNotFoundException(sharedParameterInfo.Definition.ParameterName);

            if (externalDefinitionInFile.Count > 1 && !fullMatch)
                throw new MultipleParameterException(sharedParameterInfo.Definition.ParameterName);

            var existsInDocument = ExistsParameterInDocument(
                sharedParameterInfo.Definition,
                fullMatch,
                document);

            if (!existsInDocument)
            {
                return AddSharedParameter(
                    externalDefinitionInFile.First().DefinitionFile,
                    sharedParameterInfo,
                    fullMatch,
                    document);
            }

            var result = UpdateParameterBindings(
                externalDefinitionInFile.First().ExternalDefinition!,
                sharedParameterInfo.CreateData,
                document,
                isSavePastValues);
            if (result && sharedParameterInfo.CreateData.AllowVaryBetweenGroups)
                SetAllowVaryBetweenGroups(sharedParameterInfo.Definition.ParameterName, document);
            
            return result;
        }

        /// <inheritdoc/>
        public bool ExistsParameterInDocument(
            SharedParameterDefinition definition,
            bool fullMatch,
            IDocumentWrapper? document = null)
        {
            var doc = document?.Unwrap<Document>() ?? Document;
            foreach (var sharedParameterElement in new FilteredElementCollector(doc)
                         .OfClass(typeof(SharedParameterElement))
                         .Cast<SharedParameterElement>())
            {
                switch (fullMatch)
                {
                    case false when string.Equals(
                        sharedParameterElement.Name,
                        definition.ParameterName,
                        StringComparison.InvariantCultureIgnoreCase):
                    case true when IsFullMatch(definition, sharedParameterElement):
                    {
                        var parameterBindings = doc.ParameterBindings;
                        var binding = (ElementBinding)parameterBindings.get_Item(sharedParameterElement.GetDefinition());
                        return binding != null!;
                    }
                }
            }

            return false;
        }

        /// <inheritdoc />
        public bool ExistsParameterInDefinitionFile(
            SharedParameterDefinition definition,
            bool fullMatch,
            IDefinitionFileWrapper definitionFile)
        {
            return GetSharedExternalDefinition(definition, fullMatch, definitionFile) != null;
        }

        private bool AddSharedParameterInternal(
            IDocumentWrapper? document,
            IDefinitionFileWrapper definitionFile,
            SharedParameterInfo sharedParameterInfo,
            bool fullMatch)
        {
            var doc = document?.Unwrap<Document>() ?? Document;
            var categorySet = sharedParameterInfo
                .CreateData
                .CategoriesForBind
                .Select(c => Category.GetCategory(doc, c))
                .ToCategorySet();

            var externalDefinition = GetSharedExternalDefinition(
                sharedParameterInfo.Definition, fullMatch, definitionFile);
            if (externalDefinition is null)
                throw new ParameterNotFoundException(sharedParameterInfo.Definition.ParameterName);

            var binding = sharedParameterInfo.CreateData.IsCreateForInstance
                ? (Binding)doc.Application.Create.NewInstanceBinding(categorySet)
                : doc.Application.Create.NewTypeBinding(categorySet);

            var result = doc.ParameterBindings.Insert(
                externalDefinition, binding, sharedParameterInfo.CreateData.ParameterGroup);
            if (result && sharedParameterInfo.CreateData.AllowVaryBetweenGroups)
                SetAllowVaryBetweenGroups(sharedParameterInfo.Definition.ParameterName);

            return result;
        }

        private bool UpdateParameterBindings(
            Definition definition,
            SharedParameterCreateData createData,
            IDocumentWrapper? document = null,
            bool isSavePastValues = false)
        {
            var doc = document?.Unwrap<Document>() ?? Document;
            var parameterBindings = doc.ParameterBindings;
            var binding = (ElementBinding)parameterBindings.get_Item(definition);
            var existCategories = binding.Categories ?? new CategorySet();
            var existCategoriesCopy = binding.Categories ?? new CategorySet();
            var creatingCategories = createData.CategoriesForBind
                .Select(bic => Category.GetCategory(doc, bic))
                .ToList();
            if (creatingCategories.LastOrDefault(
                    creatingCategory => existCategories.Insert(creatingCategory)) is null)
                return true;
            
            if (!isSavePastValues || binding is not InstanceBinding)
                return parameterBindings.ReInsert(definition, binding);

            // Parameter-values are recorded for elements of existing categories,
            // and they are overwritten after binding new categories.
            // This is necessary because cases of zeroing of these parameters are possible.
            var categoryFilter = new ElementMulticategoryFilter(
                existCategoriesCopy
                    .Cast<Category>()
                    .Select(cat => cat.Id)
                    .ToArray());
            var paramValuePairs = new FilteredElementCollector(doc)
                .WherePasses(categoryFilter)
                .WhereElementIsNotElementType()
                .Select(element =>
                {
                    var param = element.get_Parameter(definition);
                    if (param is null || param.IsReadOnly || !param.HasValue)
                        return null;
                    return new
                        { Param = param, Value = param.GetParameterValue() };
                })
                .Where(paramValuePair => paramValuePair?.Value is not null)
                .ToArray();
            parameterBindings.ReInsert(definition, binding);
            doc.Regenerate();
            foreach (var pair in paramValuePairs)
                pair!.Param.SetParameterValue(pair.Value);
            return true;
        }

        /// <summary>
        /// Gets <see cref="ExternalDefinition"/> by parameter name from <see cref="IDefinitionFileWrapper"/>.
        /// </summary>
        /// <param name="sharedParameterDefinition"><see cref="SharedParameterDefinition"/></param>
        /// <param name="fullMatch">If true, find parameter by all required values from <see cref="SharedParameterDefinition"/>,
        /// otherwise parameter find only by name.</param>
        /// <param name="definitionFile"><see cref="IDefinitionFileWrapper"/></param>
        private ExternalDefinition? GetSharedExternalDefinition(
            SharedParameterDefinition sharedParameterDefinition,
            bool fullMatch,
            IDefinitionFileWrapper definitionFile)
        {
            foreach (var defGroup in definitionFile.Unwrap<DefinitionFile>()!.Groups)
            {
                foreach (var def in defGroup.Definitions)
                {
                    if (def is not ExternalDefinition externalDefinition)
                        continue;

                    switch (fullMatch)
                    {
                        case false when sharedParameterDefinition.ParameterName == externalDefinition.Name:
                            return externalDefinition;
                        case true when !IsFullMatch(sharedParameterDefinition, externalDefinition):
                            continue;
                        case true:
                            return externalDefinition;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Sets "Values can vary by group instance" for parameter.
        /// </summary>
        /// <param name="parameterName">Parameter name.</param>
        /// <param name="document"><see cref="IDocumentWrapper"/></param>
        /// <remarks>Method must invoke into started transaction.</remarks>
        private void SetAllowVaryBetweenGroups(
            string parameterName,
            IDocumentWrapper? document = null)
        {
            var doc = document?.Unwrap<Document>() ?? Document;
            var map = doc.ParameterBindings;
            var it = map.ForwardIterator();
            it.Reset();
            while (it.MoveNext())
            {
                var definition = it.Key;
                if (parameterName != definition.Name)
                    continue;

                if (definition is not InternalDefinition internalDef)
                    continue;

                try
                {
                    internalDef.SetAllowVaryBetweenGroups(doc, true);
                }
                catch
                {
                    // ignore
                }
            }
        }

        private bool IsFullMatch(
            SharedParameterDefinition sharedParameterDefinition,
            ExternalDefinition externalDefinition)
        {
            if (sharedParameterDefinition.ParameterName != externalDefinition.Name)
                return false;
            if (sharedParameterDefinition.Guid.HasValue
                && externalDefinition.GUID != sharedParameterDefinition.Guid.Value)
                return false;
            if (sharedParameterDefinition.DataType.HasValue
                && externalDefinition.ParameterType != sharedParameterDefinition.DataType.Value)
                return false;
            if (!string.IsNullOrEmpty(sharedParameterDefinition.OwnerGroupName)
                && !externalDefinition.OwnerGroup.Name.Equals(sharedParameterDefinition.OwnerGroupName, StringComparison.OrdinalIgnoreCase))
                return false;
            if (sharedParameterDefinition.Visible.HasValue
                && externalDefinition.Visible != sharedParameterDefinition.Visible.Value)
                return false;
            if (sharedParameterDefinition.UserModifiable.HasValue
                && externalDefinition.UserModifiable != sharedParameterDefinition.UserModifiable.Value)
                return false;
            if (!string.IsNullOrEmpty(sharedParameterDefinition.Description)
                && !externalDefinition.Description.Equals(sharedParameterDefinition.Description, StringComparison.OrdinalIgnoreCase))
                return false;

            return true;
        }

        private bool IsFullMatch(
            SharedParameterDefinition sharedParameterDefinition,
            SharedParameterElement sharedParameterElement)
        {
            var internalDefinition = sharedParameterElement.GetDefinition();
            if (internalDefinition.Name != sharedParameterDefinition.ParameterName)
                return false;
            if (sharedParameterDefinition.Guid.HasValue
                && sharedParameterElement.GuidValue != sharedParameterDefinition.Guid.Value)
                return false;
            if (sharedParameterDefinition.DataType.HasValue
                && internalDefinition.ParameterType != sharedParameterDefinition.DataType.Value)
                return false;

            return true;
        }
    }
}
