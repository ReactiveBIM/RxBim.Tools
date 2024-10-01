namespace RxBim.Tools.Revit.Extensions
{
    using System.Collections.Generic;
    using System.Linq;
    using Autodesk.Revit.DB;
    using Helpers;

    /// <summary>
    /// Расширения для работы с семействами Revit
    /// </summary>
    public static class FamilyExtensions
    {
        /// <summary>
        /// Добавление параметров в семейства
        /// </summary>
        /// <param name="families">Семейства</param>
        /// <param name="doc">Текущий документ Revit</param>
        /// <param name="parameters">Список имен параметров</param>
        public static void AddFamilyParameters(
            this IEnumerable<Family> families,
            Document doc,
            IEnumerable<ExternalDefinition> parameters)
        {
            var parametersList = parameters.ToList();
            foreach (var family in families)
            {
                var familyDoc = doc.EditFamily(family);
                using (var trans = new Transaction(familyDoc, "Добавление параметров в семейство"))
                {
                    trans.Start();

                    var fm = familyDoc.FamilyManager;
                    foreach (var parameter in parametersList)
                    {
                        if (fm.get_Parameter(parameter.Name) == null)
                        {
#if RVT2019 || RVT2020 || RVT2021 || RVT2022 || RVT2023
                            fm.AddParameter(parameter, BuiltInParameterGroup.INVALID, true);
#else
                            fm.AddParameter(parameter, new ForgeTypeId(), true);
#endif
                        }
                    }

                    trans.Commit();
                }

                familyDoc.LoadFamily(doc, new FamilyLoadOptions());
            }
        }
    }
}
