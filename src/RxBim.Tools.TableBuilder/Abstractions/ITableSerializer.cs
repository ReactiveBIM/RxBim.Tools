namespace RxBim.Tools.TableBuilder.Abstractions
{
    using Models;

    /// <summary>
    /// Serializer of a <see cref="Table"/> object to object with type <typeparamref name="TTable"/>.
    /// </summary>
    /// <typeparam name="TParams">Serialization parameter type.</typeparam>
    /// <typeparam name="TTable">The target type of a table.</typeparam>
    public interface ITableSerializer<in TParams, out TTable>
    {
        /// <summary>
        /// Serializes a <see cref="Table"/> object to another type of table.
        /// </summary>
        /// <param name="table">Source <see cref="Table"/> object.</param>
        /// <param name="parameter">Serialization parameter.</param>
        TTable Serialize(Table table, TParams parameter);
    }

    /// <summary>
    /// Serializer of a <see cref="Table"/> object to object with type <typeparamref name="TTable"/>.
    /// </summary>
    /// <typeparam name="TParams1">First serialization parameter type.</typeparam>
    /// <typeparam name="TParams2">Second serialization parameter type.</typeparam>
    /// <typeparam name="TTable">The target type of a table.</typeparam>
    public interface ITableSerializer<in TParams1, in TParams2, out TTable>
    {
        /// <summary>
        /// Serializes a <see cref="Table"/> object to another type of table.
        /// </summary>
        /// <param name="table">Source <see cref="Table"/> object.</param>
        /// <param name="parameter1">First serialization parameter.</param>
        /// <param name="parameter2">Second serialization parameter.</param>
        TTable Serialize(Table table, TParams1 parameter1, TParams2 parameter2);
    }

    /// <summary>
    /// Serializer of a <see cref="Table"/> object to object with type <typeparamref name="TTable"/>.
    /// </summary>
    /// <typeparam name="TParams1">First serialization parameter type.</typeparam>
    /// <typeparam name="TParams2">Second serialization parameter type.</typeparam>
    /// <typeparam name="TParams3">Third serialization parameter type.</typeparam>
    /// <typeparam name="TTable">The target type of a table.</typeparam>
    public interface ITableSerializer<in TParams1, in TParams2, in TParams3, out TTable>
    {
        /// <summary>
        /// Serializes a <see cref="Table"/> object to another type of table.
        /// </summary>
        /// <param name="table">Source <see cref="Table"/> object.</param>
        /// <param name="parameter1">First serialization parameter.</param>
        /// <param name="parameter2">Second serialization parameter.</param>
        /// <param name="parameter3">Third serialization parameter.</param>
        TTable Serialize(Table table, TParams1 parameter1, TParams2 parameter2, TParams3 parameter3);
    }

    /// <summary>
    /// Serializer of a <see cref="Table"/> object to object with type <typeparamref name="TTable"/>.
    /// </summary>
    /// <typeparam name="TParams1">First serialization parameter type.</typeparam>
    /// <typeparam name="TParams2">Second serialization parameter type.</typeparam>
    /// <typeparam name="TParams3">Third serialization parameter type.</typeparam>
    /// <typeparam name="TParams4">Fourth serialization parameter type.</typeparam>
    /// <typeparam name="TTable">The target type of a table.</typeparam>
    public interface ITableSerializer<in TParams1, in TParams2, in TParams3, in TParams4, out TTable>
    {
        /// <summary>
        /// Serializes a <see cref="Table"/> object to another type of table.
        /// </summary>
        /// <param name="table">Source <see cref="Table"/> object.</param>
        /// <param name="parameter1">First serialization parameter.</param>
        /// <param name="parameter2">Second serialization parameter.</param>
        /// <param name="parameter3">Third serialization parameter.</param>
        /// <param name="parameter4">Fourth serialization parameter.</param>
        TTable Serialize(
            Table table,
            TParams1 parameter1,
            TParams2 parameter2,
            TParams3 parameter3,
            TParams4 parameter4);
    }

    /// <summary>
    /// Serializer of a <see cref="Table"/> object to object with type <typeparamref name="TTable"/>.
    /// </summary>
    /// <typeparam name="TParams1">First serialization parameter type.</typeparam>
    /// <typeparam name="TParams2">Second serialization parameter type.</typeparam>
    /// <typeparam name="TParams3">Third serialization parameter type.</typeparam>
    /// <typeparam name="TParams4">Fourth serialization parameter type.</typeparam>
    /// <typeparam name="TParams5">Fifth serialization parameter type.</typeparam>
    /// <typeparam name="TTable">The target type of a table.</typeparam>
    public interface ITableSerializer<in TParams1, in TParams2, in TParams3, in TParams4, in TParams5, out TTable>
    {
        /// <summary>
        /// Serializes a <see cref="Table"/> object to another type of table.
        /// </summary>
        /// <param name="table">Source <see cref="Table"/> object.</param>
        /// <param name="parameter1">First serialization parameter.</param>
        /// <param name="parameter2">Second serialization parameter.</param>
        /// <param name="parameter3">Third serialization parameter.</param>
        /// <param name="parameter4">Fourth serialization parameter.</param>
        /// <param name="parameter5">Fifth serialization parameter.</param>
        TTable Serialize(
            Table table,
            TParams1 parameter1,
            TParams2 parameter2,
            TParams3 parameter3,
            TParams4 parameter4,
            TParams5 parameter5);
    }

    /// <summary>
    /// Serializer of a <see cref="Table"/> object to object with type <typeparamref name="TTable"/>.
    /// </summary>
    /// <typeparam name="TParams1">First serialization parameter type.</typeparam>
    /// <typeparam name="TParams2">Second serialization parameter type.</typeparam>
    /// <typeparam name="TParams3">Third serialization parameter type.</typeparam>
    /// <typeparam name="TParams4">Fourth serialization parameter type.</typeparam>
    /// <typeparam name="TParams5">Fifth serialization parameter type.</typeparam>
    /// <typeparam name="TParams6">Sixth serialization parameter type.</typeparam>
    /// <typeparam name="TTable">The target type of a table.</typeparam>
    public interface ITableSerializer<in TParams1, in TParams2, in TParams3, in TParams4, in TParams5, in TParams6,
        out TTable>
    {
        /// <summary>
        /// Serializes a <see cref="Table"/> object to another type of table.
        /// </summary>
        /// <param name="table">Source <see cref="Table"/> object.</param>
        /// <param name="parameter1">First serialization parameter.</param>
        /// <param name="parameter2">Second serialization parameter.</param>
        /// <param name="parameter3">Third serialization parameter.</param>
        /// <param name="parameter4">Fourth serialization parameter.</param>
        /// <param name="parameter5">Fifth serialization parameter.</param>
        /// <param name="parameter6">Sixth serialization parameter.</param>
        TTable Serialize(
            Table table,
            TParams1 parameter1,
            TParams2 parameter2,
            TParams3 parameter3,
            TParams4 parameter4,
            TParams5 parameter5,
            TParams6 parameter6);
    }

    /// <summary>
    /// Serializer of a <see cref="Table"/> object to object with type <typeparamref name="TTable"/>.
    /// </summary>
    /// <typeparam name="TParams1">First serialization parameter type.</typeparam>
    /// <typeparam name="TParams2">Second serialization parameter type.</typeparam>
    /// <typeparam name="TParams3">Third serialization parameter type.</typeparam>
    /// <typeparam name="TParams4">Fourth serialization parameter type.</typeparam>
    /// <typeparam name="TParams5">Fifth serialization parameter type.</typeparam>
    /// <typeparam name="TParams6">Sixth serialization parameter type.</typeparam>
    /// <typeparam name="TParams7">Seventh serialization parameter type.</typeparam>
    /// <typeparam name="TTable">The target type of a table.</typeparam>
    public interface ITableSerializer<in TParams1, in TParams2, in TParams3, in TParams4, in TParams5, in TParams6,
        in TParams7, out TTable>
    {
        /// <summary>
        /// Serializes a <see cref="Table"/> object to another type of table.
        /// </summary>
        /// <param name="table">Source <see cref="Table"/> object.</param>
        /// <param name="parameter1">First serialization parameter.</param>
        /// <param name="parameter2">Second serialization parameter.</param>
        /// <param name="parameter3">Third serialization parameter.</param>
        /// <param name="parameter4">Fourth serialization parameter.</param>
        /// <param name="parameter5">Fifth serialization parameter.</param>
        /// <param name="parameter6">Sixth serialization parameter.</param>
        /// <param name="parameter7">Seventh serialization parameter.</param>
        TTable Serialize(
            Table table,
            TParams1 parameter1,
            TParams2 parameter2,
            TParams3 parameter3,
            TParams4 parameter4,
            TParams5 parameter5,
            TParams6 parameter6,
            TParams7 parameter7);
    }

    /// <summary>
    /// Serializer of a <see cref="Table"/> object to object with type <typeparamref name="TTable"/>.
    /// </summary>
    /// <typeparam name="TParams1">First serialization parameter type.</typeparam>
    /// <typeparam name="TParams2">Second serialization parameter type.</typeparam>
    /// <typeparam name="TParams3">Third serialization parameter type.</typeparam>
    /// <typeparam name="TParams4">Fourth serialization parameter type.</typeparam>
    /// <typeparam name="TParams5">Fifth serialization parameter type.</typeparam>
    /// <typeparam name="TParams6">Sixth serialization parameter type.</typeparam>
    /// <typeparam name="TParams7">Seventh serialization parameter type.</typeparam>
    /// <typeparam name="TParams8">Eighth serialization parameter type.</typeparam>
    /// <typeparam name="TTable">The target type of a table.</typeparam>
    public interface ITableSerializer<in TParams1, in TParams2, in TParams3, in TParams4, in TParams5, in TParams6,
        in TParams7, in TParams8, out TTable>
    {
        /// <summary>
        /// Serializes a <see cref="Table"/> object to another type of table.
        /// </summary>
        /// <param name="table">Source <see cref="Table"/> object.</param>
        /// <param name="parameter1">First serialization parameter.</param>
        /// <param name="parameter2">Second serialization parameter.</param>
        /// <param name="parameter3">Third serialization parameter.</param>
        /// <param name="parameter4">Fourth serialization parameter.</param>
        /// <param name="parameter5">Fifth serialization parameter.</param>
        /// <param name="parameter6">Sixth serialization parameter.</param>
        /// <param name="parameter7">Seventh serialization parameter.</param>
        /// <param name="parameter8">Eighth serialization parameter.</param>
        TTable Serialize(
            Table table,
            TParams1 parameter1,
            TParams2 parameter2,
            TParams3 parameter3,
            TParams4 parameter4,
            TParams5 parameter5,
            TParams6 parameter6,
            TParams7 parameter7,
            TParams8 parameter8);
    }
}