namespace RxBim.Tools.Revit.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Abstractions;
    using Autodesk.Revit.DB;
    using Autodesk.Revit.UI;
    using CSharpFunctionalExtensions;
    using Extensions;
    using Helpers;
    using JetBrains.Annotations;
    using Models;
    using Result = CSharpFunctionalExtensions.Result;

    /// <summary>
    /// Сервис по работе с общими параметрами
    /// </summary>
    [UsedImplicitly]
    internal class SharedParameterService : ISharedParameterService
    {
        private readonly UIApplication _uiApplication;
        private readonly ITransactionService _transactionService;

        /// <summary>
        /// Initializes a new instance of the <see cref="SharedParameterService"/> class.
        /// </summary>
        /// <param name="uiApplication"><see cref="UIApplication"/></param>
        /// <param name="transactionService"><see cref="ITransactionService"/></param>
        public SharedParameterService(UIApplication uiApplication, ITransactionService transactionService)
        {
            _uiApplication = uiApplication;
            _transactionService = transactionService;
        }

        /// <inheritdoc />
        public Result AddSharedParameter(
            DefinitionFile definitionFile,
            SharedParameterInfo sharedParameterInfo,
            bool fullMatch,
            bool useTransaction = false,
            Document? document = null)
        {
            if (sharedParameterInfo == null!)
                return Result.Failure("Данные об общем параметре не заданы");
            if (sharedParameterInfo.Definition == null!)
                return Result.Failure("Не заданы данные для определения общего параметра");
            if (sharedParameterInfo.Definition.ParameterName == null!)
                return Result.Failure("Не задано название общего параметра");
            if (sharedParameterInfo.CreateData == null!)
                return Result.Failure($"Не заданы данные для создания общего параметра '{sharedParameterInfo.Definition.ParameterName}'");
            if (ParameterExistsInDocument(sharedParameterInfo.Definition, fullMatch, document))
                return Result.Failure($"Параметр '{sharedParameterInfo.Definition.ParameterName}' уже добавлен в модель");
            if (sharedParameterInfo.CreateData.CategoriesForBind?.Any() != true)
                return Result.Failure($"Не указаны категории для привязки параметра '{sharedParameterInfo.Definition.ParameterName}'");

            var doc = document ?? _uiApplication.ActiveUIDocument.Document;

            if (!useTransaction)
                return AddSharedParameter(doc, definitionFile, sharedParameterInfo, fullMatch);

            return _transactionService.RunInTransaction(
                () => AddSharedParameter(doc, definitionFile, sharedParameterInfo, fullMatch),
                "Adding parameters",
                doc.Wrap());
        }

        /// <inheritdoc />
        public Result AddOrUpdateParameter(
            DefinitionFile[] definitionFiles,
            SharedParameterInfo sharedParameterInfo,
            bool fullMatch,
            bool isSavePastValues = false,
            Document? document = null)
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
                .Where(x => x.ExternalDefinition != null)
                .ToList();

            if (!externalDefinitionInFile.Any())
                return Result.Failure($"Параметр '{sharedParameterInfo.Definition.ParameterName}' не найден ни в одном из ФОП");

            if (externalDefinitionInFile.Count > 1 && !fullMatch)
                return Result.Failure($"Параметр '{sharedParameterInfo.Definition.ParameterName}' обнаружен в нескольких ФОП");

            var existsInDocument = ParameterExistsInDocument(
                sharedParameterInfo.Definition,
                fullMatch,
                document);

            if (existsInDocument)
            {
                return UpdateParameterBindings(
                        externalDefinitionInFile[0].ExternalDefinition,
                        sharedParameterInfo.CreateData,
                        document,
                        isSavePastValues)
                    .TapIf(
                        sharedParameterInfo.CreateData.AllowVaryBetweenGroups,
                        () => SetAllowVaryBetweenGroups(sharedParameterInfo.Definition.ParameterName, document));
            }

            return AddSharedParameter(
                externalDefinitionInFile[0].DefinitionFile,
                sharedParameterInfo,
                fullMatch,
                false,
                document);
        }

        /// <inheritdoc />
        public Result<DefinitionFile> GetDefinitionFile(Document? document = null)
        {
            var doc = document ?? _uiApplication.ActiveUIDocument.Document;
            var sharedParameterFilename = doc.Application.SharedParametersFilename;

            if (string.IsNullOrWhiteSpace(sharedParameterFilename))
                return Result.Failure<DefinitionFile>("Файл общих параметров не задан");

            return File.Exists(sharedParameterFilename)
                ? doc.Application.OpenSharedParameterFile()
                : Result.Failure<DefinitionFile>($"Не найден файл общих параметров '{sharedParameterFilename}'");
        }

        /// <inheritdoc/>
        public DefinitionFile[] TryGetDefinitionFiles(SharedParameterFileSource fileSource, Document? doc = null)
        {
            var document = doc ?? _uiApplication.ActiveUIDocument.Document;
            var oldDefinitionFilePath = string.Empty;

            bool initialized;
            try
            {
                oldDefinitionFilePath = document.Application.SharedParametersFilename;
                initialized = true;
            }
            catch
            {
                initialized = false;
            }

            var definitionFiles = new List<DefinitionFile>();

            if (fileSource.FilePaths != null)
            {
                foreach (var filePath in fileSource.FilePaths)
                {
                    try
                    {
                        document.Application.SharedParametersFilename = new FileInfo(filePath).FullName;
                        definitionFiles.Add(document.Application.OpenSharedParameterFile());
                    }
                    catch
                    {
                        // ignore
                    }
                }
            }

            if (initialized)
                document.Application.SharedParametersFilename = oldDefinitionFilePath;

            return definitionFiles.ToArray();
        }

        /// <inheritdoc/>
        public bool ParameterExistsInDocument(SharedParameterDefinition definition, bool fullMatch, Document? document = null)
        {
            var doc = document ?? _uiApplication.ActiveUIDocument.Document;
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

        /// <inheritdoc/>
        public Result ParameterExistsInDocument(
            SharedParameterInfo parameterInfo,
            bool fullMatch,
            Document? document = null,
            IEnumerable<SharedParameterElement>? sharedParameters = null,
            DefinitionFile? definitionFile = null)
        {
            var doc = document ?? _uiApplication.ActiveUIDocument.Document;
            var definition = parameterInfo.Definition;
            var sharedParameter = (sharedParameters ?? new FilteredElementCollector(doc)
                .OfClass<SharedParameterElement>())
                .FirstOrDefault(parameter =>
                    string.Equals(
                        definition.ParameterName,
                        parameter.Name,
                        StringComparison.InvariantCulture));
            if (sharedParameter is null)
                return Result.Failure($"Параметр {definition.ParameterName} отсутствует в проекте");

            if (!fullMatch)
                return Result.Success();

            var actualDefinitionFile = definitionFile;
            if (actualDefinitionFile is null)
            {
                var definitionFileResult = GetDefinitionFile(doc);
                if (definitionFileResult.IsFailure)
                    return definitionFileResult;
                actualDefinitionFile = definitionFileResult.Value;
            }

            var externalDefinition = GetSharedExternalDefinition(definition, false, actualDefinitionFile);
            if (externalDefinition is null)
                return Result.Failure($"Параметр {definition.ParameterName} не является актуальным (не соответствует ФОП)");

#if RVT2019 || RVT2020 || RVT2021
            var dataType = definition.DataType ?? externalDefinition.ParameterType;
#else
            var dataType = definition.DataType ?? externalDefinition.GetDataType();   
#endif
            var actualParameterInfo = new SharedParameterInfo(
                new SharedParameterDefinition()
                {
                    ParameterName = definition.ParameterName,
                    Guid = definition.Guid ?? externalDefinition.GUID,
                    DataType = dataType
                },
                parameterInfo.CreateData);

            return IsFullMatch(doc, actualParameterInfo, sharedParameter);
        }

        /// <inheritdoc />
        public Result ParameterExistsInDefinitionFile(
            DefinitionFile definitionFile,
            SharedParameterInfo sharedParameterInfo,
            bool fullMatch)
        {
            try
            {
                return Result.SuccessIf(
                    GetSharedExternalDefinition(sharedParameterInfo.Definition, fullMatch, definitionFile) != null,
                    "Параметр не найден в ФОП");
            }
            catch (Exception exception)
            {
                return Result.Failure("Ошибка проверки параметра ФОП. " + exception.Message);
            }
        }

        private Result AddSharedParameter(
            Document document,
            DefinitionFile definitionFile,
            SharedParameterInfo sharedParameterInfo,
            bool fullMatch)
        {
            var categorySet = GetCategorySet(
                sharedParameterInfo
                    .CreateData
                    .CategoriesForBind
                    ?.Select(c => Category.GetCategory(document, c)));

            var externalDefinition = GetSharedExternalDefinition(sharedParameterInfo.Definition, fullMatch, definitionFile);
            if (externalDefinition == null)
            {
                return Result.Failure(
                    $"Параметр '{sharedParameterInfo.Definition.ParameterName}' не найден в ФОП '{definitionFile.Filename}'");
            }

            var binding = sharedParameterInfo.CreateData.IsCreateForInstance
                ? (Binding)document.Application.Create.NewInstanceBinding(categorySet)
                : document.Application.Create.NewTypeBinding(categorySet);

            var result = Result.SuccessIf(
                    document.ParameterBindings.Insert(externalDefinition, binding, sharedParameterInfo.CreateData.ParameterGroup),
                    $"Не удалось добавить параметр '{sharedParameterInfo.Definition.ParameterName}'")
                .TapIf(
                    sharedParameterInfo.CreateData.AllowVaryBetweenGroups,
                    () => SetAllowVaryBetweenGroups(sharedParameterInfo.Definition.ParameterName, document));

            return result;
        }

        private Result UpdateParameterBindings(
            Definition? definition,
            SharedParameterCreateData createData,
            Document? doc = null,
            bool isSavePastValues = false)
        {
            if (definition == null)
                return Result.Failure("'definition' argument not defined.");
            doc ??= _uiApplication.ActiveUIDocument.Document;
            var parameterBindings = doc.ParameterBindings;
            ElementBinding? binding = (ElementBinding)parameterBindings.get_Item(definition);
            var existCategories = binding?.Categories ?? new CategorySet();
            var existCategoriesCopy = binding?.Categories ?? new CategorySet();
            var creatingCategories = createData.CategoriesForBind
                ?.Select(bic => Category.GetCategory(doc, bic))
                .ToList() ?? new();
            if (creatingCategories
                    .LastOrDefault(creatingCategory => existCategories.Insert(creatingCategory)) != null)
            {
                if (!isSavePastValues || binding is not InstanceBinding)
                {
                    return Result.SuccessIf(
                        parameterBindings.ReInsert(definition, binding),
                        $"Не удалось обновить параметр '{definition.Name}'");
                }

                // Производится запись параметров-значений для элементов существующих категорий,
                // и их перезапись после биндинга новых категорий.
                // Это необходимо т.к. возможны случаи обнуления данных параметров.
                var categoryFilter =
                    new ElementMulticategoryFilter(existCategoriesCopy.Cast<Category>().Select(cat => cat.Id).ToArray());
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
                return Result.Success();
            }

            return Result.Success();
        }

        /// <summary>
        /// Конвертирование списка категорий в экземпляр <see cref="CategorySet"/>
        /// </summary>
        /// <param name="categories">Список категорий</param>
        private CategorySet GetCategorySet(IEnumerable<Category>? categories)
        {
            var categorySet = new CategorySet();
            if (categories != null)
            {
                foreach (var category in categories)
                {
                    if (category is { AllowsBoundParameters: true })
                        categorySet.Insert(category);
                }
            }

            return categorySet;
        }

        /// <summary>
        /// Возвращает определение общего параметра <see cref="ExternalDefinition"/> из текущего ФОП по имени
        /// </summary>
        /// <param name="sharedParameterDefinition">Данные об общем параметре</param>
        /// <param name="fullMatch">True - параметр ФОП должен совпасть со всеми заполненными
        /// значениями sharedParameterInfo. False - параметр ищется только по имени</param>
        /// <param name="definitionFile">ФОП</param>
        private ExternalDefinition? GetSharedExternalDefinition(
            SharedParameterDefinition sharedParameterDefinition,
            bool fullMatch,
            DefinitionFile definitionFile)
        {
            foreach (var defGroup in definitionFile.Groups)
            {
                foreach (var def in defGroup.Definitions)
                {
                    if (!(def is ExternalDefinition externalDefinition))
                        continue;

                    if (!fullMatch && sharedParameterDefinition.ParameterName == externalDefinition.Name)
                        return externalDefinition;

                    if (fullMatch)
                    {
                        if (!IsFullMatch(sharedParameterDefinition, externalDefinition))
                            continue;

                        return externalDefinition;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Установить для параметров свойство "Значения могут меняться по экземплярам групп".
        /// Метод должен использоваться внутри запущенной транзакции
        /// </summary>
        /// <param name="parameterName">Имя параметра</param>
        /// <param name="document">Текущий документ</param>
        private void SetAllowVaryBetweenGroups(string parameterName, Document? document = null)
        {
            var doc = document ?? _uiApplication.ActiveUIDocument.Document;
            var map = doc.ParameterBindings;
            var it = map.ForwardIterator();
            it.Reset();
            while (it.MoveNext())
            {
                var definition = it.Key;
                if (parameterName != definition.Name)
                    continue;

                if (!(definition is InternalDefinition internalDef))
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

        /// <summary>
        /// Проверка на соответствие параметра заданным в <see cref="SharedParameterInfo"/> параметрам.
        /// Использует проверку IsFullMatch(SharedParameterDefinition, SharedParameterElement),
        /// проверяет наличие заданных категорий в параметре,
        /// проверяет возможность зменения значения параметра по экземплярам группы на соответствие заданной,
        /// проверяет на соответствие заданному типу параметра (параметр экземпляра или параметр типа)
        /// </summary>
        /// <param name="doc">Текущий документ</param>
        /// <param name="sharedParameterInfo">Данные об общем параметре</param>
        /// <param name="sharedParameter">Экземпляр общего параметра</param>
        private Result IsFullMatch(
            Document doc,
            SharedParameterInfo sharedParameterInfo,
            SharedParameterElement sharedParameter)
        {
            var binding = doc.ParameterBindings.get_Item(sharedParameter.GetDefinition());

            var missingCategories = new List<string>();
            var builtInCategories = sharedParameterInfo.CreateData.CategoriesForBind;
            if (builtInCategories is not null)
            {
                var categoriesForBind = doc.Settings.Categories.OfType<Category>()
                    .Where(c => builtInCategories.Contains((BuiltInCategory)c.Id.IntegerValue)).ToHashSet(new CategoryIdComparer());

                var existingCategoriesInDoc = ((ElementBinding)binding).Categories.OfType<Category>().ToHashSet();
                categoriesForBind.ExceptWith(existingCategoriesInDoc);
                missingCategories.AddRange(categoriesForBind.Select(c => c.Name));
            }

            var results = new List<Result>()
            {
                Result.SuccessIf(
                    IsFullMatch(sharedParameterInfo.Definition, sharedParameter),
                    $"Параметр {sharedParameter.Name} не является актуальным (не соответствует ФОП)"),
                Result.SuccessIf(
                    () => sharedParameter.GetDefinition().VariesAcrossGroups ==
                          sharedParameterInfo.CreateData.AllowVaryBetweenGroups,
                    $"Изменение значения параметра {sharedParameter.Name} по экземплярам группы не соответствует заданному поведению"),
                Result.SuccessIf(
                    () => !missingCategories.Any(),
                    $"В параметре {sharedParameter.Name} отсутствуют следующие категории: {string.Join("; ", missingCategories)}"),
                Result.SuccessIf(
                    () => sharedParameterInfo.CreateData.IsCreateForInstance
                        ? binding is InstanceBinding
                        : binding is TypeBinding,
                    $"Параметр {sharedParameter.Name} не является заданным типом параметра (параметром экземпляра или параметром типа)")
            };
            return Result.Combine(results, $"{Environment.NewLine}");
        }

        private bool IsFullMatch(SharedParameterDefinition sharedParameterDefinition, ExternalDefinition externalDefinition)
        {
            if (sharedParameterDefinition.ParameterName != externalDefinition.Name)
                return false;
            if (sharedParameterDefinition.Guid.HasValue
                && externalDefinition.GUID != sharedParameterDefinition.Guid.Value)
                return false;

#if RVT2019 || RVT2020 || RVT2021
            if (sharedParameterDefinition.DataType.HasValue
                && externalDefinition.ParameterType != sharedParameterDefinition.DataType.Value)
                return false;
#else
            if (sharedParameterDefinition.DataType != null
                && externalDefinition.GetDataType() != sharedParameterDefinition.DataType)
                return false;
#endif

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
#if RVT2019 || RVT2020 || RVT2021
            if (sharedParameterDefinition.DataType.HasValue
                && internalDefinition.ParameterType != sharedParameterDefinition.DataType.Value)
                return false;
#else
            if (sharedParameterDefinition.DataType != null
                && internalDefinition.GetDataType() != sharedParameterDefinition.DataType)
                return false;
#endif
            return true;
        }
    }
}
