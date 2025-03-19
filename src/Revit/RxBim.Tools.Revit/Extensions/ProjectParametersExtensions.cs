﻿namespace RxBim.Tools.Revit.Extensions;

using System;
using System.IO;
using System.Linq;
using Autodesk.Revit.DB;

/// <summary>
/// Расширения для параметров проекта
/// </summary>
public static class ProjectParametersExtensions
{
    /// <summary>
    /// Проверить есть ли параметр проекта по имени
    /// </summary>
    /// <param name="doc">Текущий документ Revit</param>
    /// <param name="parameterName">Название параметра</param>
    /// <returns>true - параметр есть, иначе - false</returns>
    public static bool HasParameter(
        this Document doc,
        string parameterName)
    {
        return new FilteredElementCollector(doc)
            .WhereElementIsNotElementType()
            .OfClass(typeof(ParameterElement))
            .Cast<ParameterElement>()
            .Any(pElem => pElem.GetDefinition().Name.Equals(parameterName));
    }

    /// <summary>
    /// Добавить параметр проекта
    /// </summary>
    /// <param name="doc">Текущий документ Revit</param>
    /// <param name="name">Название параметра</param>
    /// <param name="type">Тип параметра</param>
    /// <param name="visible">Видимость</param>
    /// <param name="cats">Категория</param>
    /// <param name="group">Группировка</param>
    /// <param name="inst">Инстанцированный параметр</param>
    public static bool AddProjectParameter(
        this Document doc,
        string name,
#if RVT2019 || RVT2020 || RVT2021
        ParameterType type,
#else
            ForgeTypeId type,
#endif
        bool visible,
        CategorySet cats,
#if RVT2019 || RVT2020 || RVT2021 || RVT2022 || RVT2023
        BuiltInParameterGroup group,
#else
            ForgeTypeId group,
#endif
        bool inst)
    {
        var app = doc.Application;
        var oriFile = app.SharedParametersFilename;
        ExternalDefinition def;
        try
        {
            var tempFile = $"{Path.Combine(Path.GetTempPath(), Path.GetRandomFileName())}.txt";
            File.Create(tempFile).Close();

            app.SharedParametersFilename = tempFile;

            var defOptions = new ExternalDefinitionCreationOptions(name, type)
            {
                Visible = visible
            };
            def = (ExternalDefinition)app
                .OpenSharedParameterFile()
                .Groups
                .Create("TemporaryDefintionGroup")
                .Definitions
                .Create(defOptions);

            File.Delete(tempFile);
        }
        finally
        {
            app.SharedParametersFilename = oriFile;
        }

        Binding binding = app.Create.NewTypeBinding(cats);
        if (inst)
            binding = app.Create.NewInstanceBinding(cats);

        var map = doc.ParameterBindings;
        return map.Insert(def, binding, group);
    }

    /// <summary>
    /// Удалить параметр
    /// </summary>
    /// <param name="doc">Текущий документ Revit</param>
    /// <param name="parameterName">Название параметра</param>
    /// <param name="useTransaction">Использовать ли транзакцию</param>
    /// <param name="condition">Условие удаления параметра</param>
    public static void DeleteParameter(
        this Document doc,
        string parameterName,
        bool useTransaction = true,
        Func<ParameterElement, bool>? condition = null)
    {
        var pElems = new FilteredElementCollector(doc)
            .WhereElementIsNotElementType()
            .OfClass(typeof(ParameterElement))
            .Cast<ParameterElement>();

        var projParam = pElems.FirstOrDefault(pElem => pElem.GetDefinition().Name.Equals(parameterName));

        if (projParam == null
            || (condition != null
                && !condition.Invoke(projParam)))
        {
            return;
        }

        if (useTransaction)
        {
            using (var t = new Transaction(doc, $"Удаление параметра {parameterName}"))
            {
                t.Start();
                doc.Delete(projParam.Id);
                t.Commit();
            }
        }
        else
        {
            doc.Delete(projParam.Id);
        }
    }

    /// <summary>
    /// Получить ID параметра проекта по имени
    /// </summary>
    /// <param name="doc">Текущий документ Revit</param>
    /// <param name="parameterName">Название параметра</param>
    /// <returns>ID параметра проекта</returns>
    public static ElementId? GetParameterId(
        this Document doc,
        string parameterName)
    {
        return new FilteredElementCollector(doc)
            .WhereElementIsNotElementType()
            .OfClass(typeof(ParameterElement))
            .Cast<ParameterElement>()
            .FirstOrDefault(pElem => pElem.GetDefinition().Name.Equals(parameterName))
            ?.Id;
    }
}