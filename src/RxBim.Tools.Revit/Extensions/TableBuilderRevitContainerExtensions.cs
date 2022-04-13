namespace RxBim.Tools.Revit.Extensions;

using Abstractions;
using Di;
using Serializers;

/// <summary>
/// Container extensions
/// </summary>
public static class TableBuilderRevitContainerExtensions
{
    /// <summary>
    /// Adds table serialization to Excel services.
    /// </summary>
    /// <param name="container"><see cref="IContainer"/></param>
    public static IContainer AddTableBuilderRevit(this IContainer container)
    {
        return container
            .AddSingleton<IViewScheduleTableSerializer, ViewScheduleTableSerializer>();
    }
}