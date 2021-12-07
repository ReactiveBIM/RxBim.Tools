namespace RxBim.Tools.TableBuilder.Extensions
{
    using Abstractions;
    using Models;

    /// <summary>
    /// Extensions for <see cref="Table"/>
    /// </summary>
    public static class TableExtensions
    {
        /// <summary>
        /// Serializes a <see cref="Table"/> object to another type of table.
        /// </summary>
        /// <param name="table">Source <see cref="Table"/> object.</param>
        /// <param name="tableSerializer"><see cref="ITableSerializer{TParam, TTable}"/> object.</param>
        /// <param name="parameter">Serialization parameter.</param>
        /// <typeparam name="TParam">Serialization parameter type.</typeparam>
        /// <typeparam name="TTable">The target type of a table.</typeparam>
        public static TTable Serialize<TParam, TTable>(
            this Table table,
            ITableSerializer<TParam, TTable> tableSerializer,
            TParam parameter)
        {
            return tableSerializer.Serialize(table, parameter);
        }

        /// <summary>
        /// Serializes a <see cref="Table"/> object to another type of table.
        /// </summary>
        /// <param name="table">Source <see cref="Table"/> object.</param>
        /// <param name="tableSerializer"><see cref="ITableSerializer{TParam, TTable}"/> object.</param>
        /// <param name="parameter1">First serialization parameter.</param>
        /// <param name="parameter2">Second serialization parameter.</param>
        /// <typeparam name="TParam1">First serialization parameter type.</typeparam>
        /// <typeparam name="TParam2">Second serialization parameter type.</typeparam>
        /// <typeparam name="TTable">The target type of a table.</typeparam>
        public static TTable Serialize<TParam1, TParam2, TTable>(
            this Table table,
            ITableSerializer<TParam1, TParam2, TTable> tableSerializer,
            TParam1 parameter1,
            TParam2 parameter2)
        {
            return tableSerializer.Serialize(table, parameter1, parameter2);
        }

        /// <summary>
        /// Serializes a <see cref="Table"/> object to another type of table.
        /// </summary>
        /// <param name="table">Source <see cref="Table"/> object.</param>
        /// <param name="tableSerializer"><see cref="ITableSerializer{TParam, TTable}"/> object.</param>
        /// <param name="parameter1">First serialization parameter.</param>
        /// <param name="parameter2">Second serialization parameter.</param>
        /// <param name="parameter3">Third serialization parameter.</param>
        /// <typeparam name="TParam1">First serialization parameter type.</typeparam>
        /// <typeparam name="TParam2">Second serialization parameter type.</typeparam>
        /// <typeparam name="TParam3">Third serialization parameter type.</typeparam>
        /// <typeparam name="TTable">The target type of a table.</typeparam>
        public static TTable Serialize<TParam1, TParam2, TParam3, TTable>(
            this Table table,
            ITableSerializer<TParam1, TParam2, TParam3, TTable> tableSerializer,
            TParam1 parameter1,
            TParam2 parameter2,
            TParam3 parameter3)
        {
            return tableSerializer.Serialize(table, parameter1, parameter2, parameter3);
        }

        /// <summary>
        /// Serializes a <see cref="Table"/> object to another type of table.
        /// </summary>
        /// <param name="table">Source <see cref="Table"/> object.</param>
        /// <param name="tableSerializer"><see cref="ITableSerializer{TParam, TTable}"/> object.</param>
        /// <param name="parameter1">First serialization parameter.</param>
        /// <param name="parameter2">Second serialization parameter.</param>
        /// <param name="parameter3">Third serialization parameter.</param>
        /// <param name="parameter4">Fourth serialization parameter.</param>
        /// <typeparam name="TParam1">First serialization parameter type.</typeparam>
        /// <typeparam name="TParam2">Second serialization parameter type.</typeparam>
        /// <typeparam name="TParam3">Third serialization parameter type.</typeparam>
        /// <typeparam name="TParam4">Fourth serialization parameter type.</typeparam>
        /// <typeparam name="TTable">The target type of a table.</typeparam>
        public static TTable Serialize<TParam1, TParam2, TParam3, TParam4, TTable>(
            this Table table,
            ITableSerializer<TParam1, TParam2, TParam3, TParam4, TTable> tableSerializer,
            TParam1 parameter1,
            TParam2 parameter2,
            TParam3 parameter3,
            TParam4 parameter4)
        {
            return tableSerializer.Serialize(table, parameter1, parameter2, parameter3, parameter4);
        }

        /// <summary>
        /// Serializes a <see cref="Table"/> object to another type of table.
        /// </summary>
        /// <param name="table">Source <see cref="Table"/> object.</param>
        /// <param name="tableSerializer"><see cref="ITableSerializer{TParam, TTable}"/> object.</param>
        /// <param name="parameter1">First serialization parameter.</param>
        /// <param name="parameter2">Second serialization parameter.</param>
        /// <param name="parameter3">Third serialization parameter.</param>
        /// <param name="parameter4">Fourth serialization parameter.</param>
        /// <param name="parameter5">Fifth serialization parameter.</param>
        /// <typeparam name="TParam1">First serialization parameter type.</typeparam>
        /// <typeparam name="TParam2">Second serialization parameter type.</typeparam>
        /// <typeparam name="TParam3">Third serialization parameter type.</typeparam>
        /// <typeparam name="TParam4">Fourth serialization parameter type.</typeparam>
        /// <typeparam name="TParam5">Fifth serialization parameter type.</typeparam>
        /// <typeparam name="TTable">The target type of a table.</typeparam>
        public static TTable Serialize<TParam1, TParam2, TParam3, TParam4, TParam5, TTable>(
            this Table table,
            ITableSerializer<TParam1, TParam2, TParam3, TParam4, TParam5, TTable> tableSerializer,
            TParam1 parameter1,
            TParam2 parameter2,
            TParam3 parameter3,
            TParam4 parameter4,
            TParam5 parameter5)
        {
            return tableSerializer.Serialize(table, parameter1, parameter2, parameter3, parameter4, parameter5);
        }

        /// <summary>
        /// Serializes a <see cref="Table"/> object to another type of table.
        /// </summary>
        /// <param name="table">Source <see cref="Table"/> object.</param>
        /// <param name="tableSerializer"><see cref="ITableSerializer{TParam, TTable}"/> object.</param>
        /// <param name="parameter1">First serialization parameter.</param>
        /// <param name="parameter2">Second serialization parameter.</param>
        /// <param name="parameter3">Third serialization parameter.</param>
        /// <param name="parameter4">Fourth serialization parameter.</param>
        /// <param name="parameter5">Fifth serialization parameter.</param>
        /// <param name="parameter6">Sixth serialization parameter.</param>
        /// <typeparam name="TParam1">First serialization parameter type.</typeparam>
        /// <typeparam name="TParam2">Second serialization parameter type.</typeparam>
        /// <typeparam name="TParam3">Third serialization parameter type.</typeparam>
        /// <typeparam name="TParam4">Fourth serialization parameter type.</typeparam>
        /// <typeparam name="TParam5">Fifth serialization parameter type.</typeparam>
        /// <typeparam name="TParam6">Sixth serialization parameter type.</typeparam>
        /// <typeparam name="TTable">The target type of a table.</typeparam>
        public static TTable Serialize<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TTable>(
            this Table table,
            ITableSerializer<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TTable> tableSerializer,
            TParam1 parameter1,
            TParam2 parameter2,
            TParam3 parameter3,
            TParam4 parameter4,
            TParam5 parameter5,
            TParam6 parameter6)
        {
            return tableSerializer.Serialize(table,
                parameter1,
                parameter2,
                parameter3,
                parameter4,
                parameter5,
                parameter6);
        }

        /// <summary>
        /// Serializes a <see cref="Table"/> object to another type of table.
        /// </summary>
        /// <param name="table">Source <see cref="Table"/> object.</param>
        /// <param name="tableSerializer"><see cref="ITableSerializer{TParam, TTable}"/> object.</param>
        /// <param name="parameter1">First serialization parameter.</param>
        /// <param name="parameter2">Second serialization parameter.</param>
        /// <param name="parameter3">Third serialization parameter.</param>
        /// <param name="parameter4">Fourth serialization parameter.</param>
        /// <param name="parameter5">Fifth serialization parameter.</param>
        /// <param name="parameter6">Sixth serialization parameter.</param>
        /// <param name="parameter7">Seventh serialization parameter.</param>
        /// <typeparam name="TParam1">First serialization parameter type.</typeparam>
        /// <typeparam name="TParam2">Second serialization parameter type.</typeparam>
        /// <typeparam name="TParam3">Third serialization parameter type.</typeparam>
        /// <typeparam name="TParam4">Fourth serialization parameter type.</typeparam>
        /// <typeparam name="TParam5">Fifth serialization parameter type.</typeparam>
        /// <typeparam name="TParam6">Sixth serialization parameter type.</typeparam>
        /// <typeparam name="TParam7">Seventh serialization parameter type.</typeparam>
        /// <typeparam name="TTable">The target type of a table.</typeparam>
        public static TTable Serialize<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TTable>(
            this Table table,
            ITableSerializer<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TTable> tableSerializer,
            TParam1 parameter1,
            TParam2 parameter2,
            TParam3 parameter3,
            TParam4 parameter4,
            TParam5 parameter5,
            TParam6 parameter6,
            TParam7 parameter7)
        {
            return tableSerializer.Serialize(table,
                parameter1,
                parameter2,
                parameter3,
                parameter4,
                parameter5,
                parameter6,
                parameter7);
        }

        /// <summary>
        /// Serializes a <see cref="Table"/> object to another type of table.
        /// </summary>
        /// <param name="table">Source <see cref="Table"/> object.</param>
        /// <param name="tableSerializer"><see cref="ITableSerializer{TParam, TTable}"/> object.</param>
        /// <param name="parameter1">First serialization parameter.</param>
        /// <param name="parameter2">Second serialization parameter.</param>
        /// <param name="parameter3">Third serialization parameter.</param>
        /// <param name="parameter4">Fourth serialization parameter.</param>
        /// <param name="parameter5">Fifth serialization parameter.</param>
        /// <param name="parameter6">Sixth serialization parameter.</param>
        /// <param name="parameter7">Seventh serialization parameter.</param>
        /// <param name="parameter8">Eighth serialization parameter.</param>
        /// <typeparam name="TParam1">First serialization parameter type.</typeparam>
        /// <typeparam name="TParam2">Second serialization parameter type.</typeparam>
        /// <typeparam name="TParam3">Third serialization parameter type.</typeparam>
        /// <typeparam name="TParam4">Fourth serialization parameter type.</typeparam>
        /// <typeparam name="TParam5">Fifth serialization parameter type.</typeparam>
        /// <typeparam name="TParam6">Sixth serialization parameter type.</typeparam>
        /// <typeparam name="TParam7">Seventh serialization parameter type.</typeparam>
        /// <typeparam name="TParam8">Eighth serialization parameter type.</typeparam>
        /// <typeparam name="TTable">The target type of a table.</typeparam>
        public static TTable Serialize<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TTable>(
            this Table table,
            ITableSerializer<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TTable>
                tableSerializer,
            TParam1 parameter1,
            TParam2 parameter2,
            TParam3 parameter3,
            TParam4 parameter4,
            TParam5 parameter5,
            TParam6 parameter6,
            TParam7 parameter7,
            TParam8 parameter8)
        {
            return tableSerializer.Serialize(table,
                parameter1,
                parameter2,
                parameter3,
                parameter4,
                parameter5,
                parameter6,
                parameter7,
                parameter8);
        }
    }
}