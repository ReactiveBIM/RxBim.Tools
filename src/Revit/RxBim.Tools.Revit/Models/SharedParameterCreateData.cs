﻿namespace RxBim.Tools.Revit.Models;

using System.Collections.Generic;
using Autodesk.Revit.DB;
using JetBrains.Annotations;

/// <summary>
/// Данные для создания общего параметра в модели
/// </summary>
[PublicAPI]
public class SharedParameterCreateData
{
    /// <summary>
    /// Коллекция категорий, представленных типом <see cref="BuiltInCategory"/>, для привязки параметра
    /// </summary>
    public List<BuiltInCategory>? CategoriesForBind { get; set; }

    /// <summary>
    /// Установить для параметров свойство "Значения могут меняться по экземплярам групп"
    /// </summary>
    public bool AllowVaryBetweenGroups { get; set; }

    /// <summary>
    /// True - параметр создается для экземпляра, False - для типа
    /// </summary>
    public bool IsCreateForInstance { get; set; } = true;

#if RVT2019 || RVT2020 || RVT2021 || RVT2022 || RVT2023
    /// <summary>
    /// Группа, в которую требуется добавить общие параметры
    /// </summary>
    public BuiltInParameterGroup ParameterGroup { get; set; } = BuiltInParameterGroup.INVALID;
#else
        /// <summary>
        /// Группа, в которую требуется добавить общие параметры
        /// </summary>
        public ForgeTypeId ParameterGroup { get; set; } = new();
#endif
}