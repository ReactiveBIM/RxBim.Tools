namespace RxBim.Tools.Revit.Extensions
{
    using System.Collections.Generic;
    using System.Linq;
    using Autodesk.Revit.DB;

    /// <summary>
    /// Расширения для Коллетора
    /// </summary>
    public static class FilterElementCollectorExtensions
    {
        /// <summary>
        /// Фильтрует элементы по типу и приводит к данному типу
        /// </summary>
        /// <param name="collector">FilteredElementCollector</param>
        /// <typeparam name="T">Тип для фильтрации и приведения</typeparam>
        /// <returns></returns>
        public static IEnumerable<T> OfClassType<T>(this FilteredElementCollector collector)
            where T : Element
        {
            return collector.OfClass(typeof(T)).Cast<T>();
        }
    }
}