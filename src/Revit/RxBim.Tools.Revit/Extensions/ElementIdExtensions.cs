namespace RxBim.Tools.Revit.Extensions;

using Autodesk.Revit.DB;

/// <summary>
/// Расширения для <see cref="ElementId"/>.
/// </summary>
public static class ElementIdExtensions
{
#if RVT2019 || RVT2020 || RVT2021 || RVT2022 || RVT2023
    /// <summary>
    /// Возвращает целочисленной представление Id элемента.
    /// </summary>
    /// <param name="elementId"><see cref="ElementId"/>.</param>
    public static int GetIdValue(this ElementId elementId)
    {
        return elementId.IntegerValue;
    }
#else
    /// <summary>
    /// Возвращает целочисленной представление Id элемента.
    /// </summary>
    /// <param name="elementId"><see cref="ElementId"/>.</param>
    public static long GetIdValue(this ElementId elementId)
    {
        return elementId.Value;
    }
#endif
}