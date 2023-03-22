namespace RxBim.Tools.Revit.Abstractions;

using System;
using System.Threading.Tasks;
using Autodesk.Revit.UI;
using JetBrains.Annotations;

/// <summary>
/// Интерфейс для <see cref="RevitTask"/>
/// </summary>
/// <remarks>
/// Необходим для обеспечения возможности переопределить логику работы <see cref="RevitTask"/> в тестах.
/// </remarks>
public interface IRevitTask
{
    /// <summary>
    /// Вызывает действие в <see cref="RevitTask"/>.
    /// </summary>
    /// <param name="action">Действие.</param>
    [UsedImplicitly]
    public Task Run(Action<UIApplication> action);

    /// <summary>
    /// Вызывает функцию в <see cref="RevitTask"/>.
    /// </summary>
    /// <param name="func">Функция.</param>
    /// <typeparam name="TResult">Тип возвращаемого объекта.</typeparam>
    [UsedImplicitly]
    public Task<TResult> Run<TResult>(Func<UIApplication, TResult> func);
}