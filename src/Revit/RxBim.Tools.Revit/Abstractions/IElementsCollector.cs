﻿namespace RxBim.Tools.Revit.Abstractions;

using System;
using System.Collections.Generic;
using Autodesk.Revit.DB;
using Models;

/// <summary>
/// Интерфейс коллектора элементов
/// </summary>
public interface IElementsCollector
{
    /// <summary>
    /// Получить отфильтрованную коллекцию элементов
    /// </summary>
    /// <param name="doc">Документ для обработки. Если null, то будет браться текущий документ</param>
    /// <param name="ignoreScope">Игнорировать вариант частей <see cref="ScopeType"/></param>
    /// <param name="includeSubFamilies">Включить в коллектор вложенные семейства.
    /// Опция актуальна при <see cref="ScopeType"/> == SelectedElements</param>
    /// <returns>Отфильтрованная коллекция элементов</returns>
    FilteredElementCollector GetFilteredElementCollector(
        Document? doc = null, bool ignoreScope = false, bool includeSubFamilies = true);

    /// <summary>
    /// Включает в Revit режим выбора элемента в модели текущего документа с учетом заданного фильтра
    /// </summary>
    /// <param name="filterElement">Фильтр для выбора элементов</param>
    /// <param name="statusPrompt">Описание статуса в Revit при выборе</param>
    /// <returns>Выбранный элемент</returns>
    Element? PickElement(Func<Element, bool>? filterElement = null, string statusPrompt = "");

    /// <summary>
    /// Включает в Revit режим выбора элементов в модели текущего документа с учетом заданного фильтра
    /// </summary>
    /// <param name="filterElement">Фильтр для выбора элементов</param>
    /// <param name="statusPrompt">Описание статуса в Revit при выборе</param>
    /// <returns>Выбранный элемент</returns>
    List<Element> PickElements(Func<Element, bool>? filterElement = null, string statusPrompt = "");

    /// <summary>
    /// Включает в Revit режим выбора элемента в модели связанного документа с учетом заданного фильтра
    /// </summary>
    /// <param name="filterElement">Фильтр для выбора элементов</param>
    /// <param name="statusPrompt">Описание статуса в Revit при выборе</param>
    /// <returns>Выбранный элемент</returns>
    LinkedElement? PickLinkedElement(Func<Element, bool>? filterElement = null, string statusPrompt = "");

    /// <summary>
    /// Включает в Revit режим выбора элементов в модели связанного документа с учетом заданного фильтра
    /// </summary>
    /// <param name="filterElement">Фильтр для выбора элементов</param>
    /// <param name="statusPrompt">Описание статуса в Revit при выборе</param>
    /// <returns>Выбранный элемент</returns>
    List<LinkedElement> PickLinkedElements(Func<Element, bool>? filterElement = null, string statusPrompt = "");
}