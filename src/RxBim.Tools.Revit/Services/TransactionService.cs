namespace RxBim.Tools.Revit.Services
{
    using System;
    using Abstractions;
    using Autodesk.Revit.DB;
    using Autodesk.Revit.UI;
    using JetBrains.Annotations;
    using Result = CSharpFunctionalExtensions.Result;

    /// <inheritdoc/>
    [UsedImplicitly]
    internal class TransactionService : ITransactionService
    {
        private readonly UIApplication _uiApplication;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionService"/> class.
        /// </summary>
        /// <param name="uiApplication"><see cref="UIApplication"/></param>
        public TransactionService(UIApplication uiApplication)
        {
            _uiApplication = uiApplication;
        }

        private Document CurrentDocument => _uiApplication.ActiveUIDocument.Document;

        /// <inheritdoc/>
        public Result RunInTransaction(Action action, string transactionName, Document? document = null)
        {
            return RunInTransaction(FuncWithResult, transactionName, document);

            Result FuncWithResult()
            {
                action.Invoke();
                return Result.Success();
            }
        }

        /// <inheritdoc/>
        public Result RunInTransactionGroup(Action action, string transactionGroupName, Document? document = null)
        {
            Func<Result> funcWithResult = () =>
            {
                action.Invoke();
                return Result.Success();
            };

            return RunInTransactionGroup(funcWithResult, transactionGroupName, document);
        }

        /// <inheritdoc />
        public Result RunInTransaction(Func<Result> func, string transactionName, Document? document = null)
        {
            if (func == null)
                return Result.Failure("Не задано действие для транзакции");

            Result result;

            using var tr = new Transaction(document ?? CurrentDocument, transactionName);
            try
            {
                tr.Start();
                result = func.Invoke();
                tr.Commit();
            }
            catch (Exception exception)
            {
                if (tr.GetStatus() != TransactionStatus.RolledBack)
                    tr.RollBack();
                return Result.Failure(exception.ToString());
            }

            return result;
        }

        /// <inheritdoc />
        public Result RunInTransactionGroup(Func<Result> func, string transactionGroupName, Document? document = null)
        {
            if (func == null)
                return Result.Failure("Не задано действие для транзакции");

            Result result;
            using var tr = new TransactionGroup(document ?? CurrentDocument, transactionGroupName);
            try
            {
                tr.Start();
                result = func.Invoke();
                tr.Assimilate();
            }
            catch (Exception exception)
            {
                if (tr.GetStatus() != TransactionStatus.RolledBack)
                    tr.RollBack();
                return Result.Failure(exception.ToString());
            }

            return result;
        }
    }
}
