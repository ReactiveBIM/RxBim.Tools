﻿namespace RxBim.Tools.Revit.Extensions;

using System.Collections.Generic;
using System.IO;
using System.Linq;
using Autodesk.Revit.DB;
using Helpers;
using JetBrains.Annotations;

/// <summary>
/// Revit document extensions.
/// </summary>
[PublicAPI]
public static class DocumentExtensions
{
    /// <summary>
    /// Получить открытый связанный документ Revit
    /// </summary>
    /// <param name="rootDoc">Основной документ Revit</param>
    /// <param name="linkedDocTitle">Название связанного документа</param>
    /// <returns>Открытый связанный документ Revit</returns>
    public static Document? GetOpenedLinkedDocument(this Document rootDoc, string linkedDocTitle)
    {
        var linkedDoc = rootDoc.Application.Documents
            .Cast<Document>()
            .FirstOrDefault(doc => doc.Title.Equals(linkedDocTitle));
        if (linkedDoc == null)
            return null;
        if (!linkedDoc.IsLinked)
            return linkedDoc;

        var linkType = rootDoc.GetLinkType(linkedDocTitle);
        if (linkType == null)
            return null;

        var fileName = string.Empty;
        ModelPath? modelPath = null;

        if (linkedDoc.IsWorkshared)
            modelPath = linkedDoc.GetWorksharingCentralModelPath();

        if (modelPath == null
            || modelPath.Empty)
            fileName = linkedDoc.PathName;

        linkType.Unload(null);

        Document? lDoc = null;
        try
        {
            lDoc = modelPath != null && !modelPath.Empty
                ? rootDoc.Application.OpenDocumentFile(modelPath, new OpenOptions())
                : rootDoc.Application.OpenDocumentFile(fileName);
        }
        catch
        {
            // Подавление исключений
        }

        return lDoc;
    }

    /// <summary>
    /// Закрытие связаного документа
    /// </summary>
    /// <param name="rootDoc">Основной документ Revit</param>
    /// <param name="linkedDocument">Ранее открытый связанный документ</param>
    /// <param name="isSynchronized">Флаг синхронизации рабочих наборов перед закрытием</param>
    public static void CloseLinkedDocument(
        this Document rootDoc,
        Document linkedDocument,
        bool isSynchronized = false)
    {
        var linkType = rootDoc.GetLinkType(linkedDocument.Title);
        if (linkType == null)
            return;

        var transactOptions = new TransactWithCentralOptions();
        var syncOptions = new SynchronizeWithCentralOptions();

        var relOpt = new RelinquishOptions(true);
        syncOptions.SetRelinquishOptions(relOpt);

        try
        {
            if (isSynchronized)
                linkedDocument.SynchronizeWithCentral(transactOptions, syncOptions);

            linkedDocument.Close(true);
        }
        catch
        {
            // Подавление исключений
        }

        // Обновляем связанный документ, чтобы он остался доступен из основного файла
        linkType.Reload();
    }

    /// <summary>
    /// Получить тип связанного документа
    /// </summary>
    /// <param name="rootDoc">Основной документ</param>
    /// <param name="linkedDocTitle">Название связаного документа</param>
    /// <returns>Тип связанного документа</returns>
    public static RevitLinkType? GetLinkType(this Document rootDoc, string linkedDocTitle)
    {
        return new FilteredElementCollector(rootDoc)
                .OfCategory(BuiltInCategory.OST_RvtLinks)
                .OfClass(typeof(RevitLinkType))
                .FirstOrDefault(lt => linkedDocTitle.Equals(Path.GetFileNameWithoutExtension(lt.Name))) as
            RevitLinkType;
    }

    /// <summary>
    /// Получение списка идентификаторов категорий, имеющихся в проекте
    /// </summary>
    /// <param name="doc">Документ</param>
    /// <param name="excludeCategories">Список идентификаторов категорий, которые требуется пропустить</param>
    /// <param name="includeSubCategories">Включая подкатегории</param>
#if RVT2019 || RVT2020 || RVT2021 || RVT2022 || RVT2023
    public static IEnumerable<int> GetCategoriesIds(
        this Document doc,
        IEnumerable<int> excludeCategories,
#else
        public static IEnumerable<long> GetCategoriesIds(
            this Document doc,
            IEnumerable<long> excludeCategories,
#endif
        bool includeSubCategories = true)
    {
        return doc.GetCategoriesIdsIEnumerable(includeSubCategories)
            .Except(excludeCategories)
            .Distinct()
            .ToList();
    }

    /// <summary>
    /// Получить список видов с листами по заданным именам
    /// </summary>
    /// <param name="doc">Документ Revit</param>
    /// <param name="viewsNames">Имена листов в документе</param>
    /// <param name="isOrder">Флаг упорядочивания листов</param>
    /// <returns>Список видов</returns>
    public static IEnumerable<View> GetViews(
        this Document doc,
        IEnumerable<string> viewsNames,
        bool isOrder = true)
    {
        var views = new FilteredElementCollector(doc)
            .OfClass(typeof(ViewSheet))
            .Cast<ViewSheet>()
            .Where(sheet => viewsNames.Contains(sheet.Title));

        if (isOrder)
            views = views.OrderBy(view => view.SheetNumber, new SemiNumericComparer());

        return views;
    }

    /// <summary>
    /// Returns the path to the document.
    /// </summary>
    /// <param name="doc">Document.</param>
    public static string GetProjectPath(this Document doc)
    {
        ModelPath? modelPath = null;

        if (doc.IsWorkshared)
            modelPath = doc.GetWorksharingCentralModelPath();

        return modelPath == null || modelPath.Empty
            ? doc.PathName
            : ModelPathUtils.ConvertModelPathToUserVisiblePath(modelPath);
    }

    /// <summary>
    /// Получение списка идентификаторов категорий, имеющихся в проекте
    /// </summary>
    /// <param name="doc">Документ</param>
    /// <param name="onlyAllowsBoundParameters">Только категории, к которым можно привязать общие параметры</param>
    /// <param name="includeSubCategories">Включая подкатегории</param>
#if RVT2019 || RVT2020 || RVT2021 || RVT2022 || RVT2023
    private static IEnumerable<int> GetCategoriesIdsIEnumerable(
#else
        private static IEnumerable<long> GetCategoriesIdsIEnumerable(
#endif
        this Document doc,
        bool onlyAllowsBoundParameters = false,
        bool includeSubCategories = true)
    {
        foreach (Category category in doc.Settings.Categories)
        {
            if (onlyAllowsBoundParameters)
            {
                if (category.AllowsBoundParameters)
                    yield return category.Id.GetIdValue();
            }
            else
            {
                yield return category.Id.GetIdValue();
            }

            if (!includeSubCategories)
                continue;

            foreach (Category subCategory in category.SubCategories)
            {
                if (onlyAllowsBoundParameters)
                {
                    if (subCategory.AllowsBoundParameters)
                        yield return subCategory.Id.GetIdValue();
                }
                else
                {
                    yield return subCategory.Id.GetIdValue();
                }
            }
        }
    }
}