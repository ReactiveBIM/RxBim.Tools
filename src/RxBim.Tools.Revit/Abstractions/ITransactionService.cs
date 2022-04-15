namespace RxBim.Tools.Revit.Abstractions
{
    using System;
    using Autodesk.Revit.DB;
    using CSharpFunctionalExtensions;

    /// <summary>
    /// Сервис по работе с транзакцией
    /// </summary>
    public interface ITransactionService
    {
        /// <summary>
        /// Оборачивает действие в транзакцию
        /// </summary>
        /// <param name="action">Действие</param>
        /// <param name="transactionName">Название транзакции</param>
        /// <param name="document">выбранный документ.
        /// при null - транзакция запустится в текущем документе</param>
        Result RunInTransaction(Action action, string transactionName, Document? document = null);

        /// <summary>
        /// Оборачивает действие в группу транзакций
        /// </summary>
        /// <param name="action">Действие</param>
        /// <param name="transactionGroupName">Название группы транзакций</param>
        /// <param name="document">выбранный документ.
        /// при null - группа транзакций запустится в текущем документе</param>
        Result RunInTransactionGroup(Action action, string transactionGroupName, Document? document = null);

        /// <summary>
        /// Оборачивает действие в транзакцию с возвратом результата
        /// </summary>
        /// <param name="action">Действие с возвратом результата</param>
        /// <param name="transactionName">Название транзакции</param>
        /// <param name="document">выбранный документ.
        /// при null - транзакция запустится в текущем документе</param>
        Result RunInTransaction(Func<Result> action, string transactionName, Document? document = null);

        /// <summary>
        /// Оборачивает действие в группу транзакций с возвратом результата
        /// </summary>
        /// <param name="action">Действие с возвратом результата</param>
        /// <param name="transactionGroupName">Название группы транзакций</param>
        /// <param name="document">выбранный документ.
        /// при null - группа транзакций запустится в текущем документе</param>
        Result RunInTransactionGroup(Func<Result> action, string transactionGroupName, Document? document = null);
    }
}
