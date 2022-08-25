namespace RxBim.Tools.Autocad
{
    using System;
    using Autodesk.AutoCAD.DatabaseServices;

    /// <summary>
    /// Extensions for <see cref="ITransaction"/>.
    /// </summary>
    public static class AutocadTransactionExtensions
    {
        /// <summary>
        /// Returns <see cref="Transaction"/> object from <see cref="ITransaction"/> object.
        /// </summary>
        /// <param name="transaction"><see cref="ITransaction"/> object.</param>
        /// <returns></returns>
        /// <exception cref="InvalidCastException"><paramref name="transaction"/> is not <see cref="AutocadTransactionBase"/>.</exception>
        public static Transaction ToAcadTransaction(this ITransaction transaction)
        {
            return (transaction as AutocadTransactionBase)?.Transaction ??
                   throw new InvalidCastException(
                       $"Can't convert transaction type '{transaction.GetType().FullName}' to '{typeof(Transaction).FullName}'");
        }
    }
}