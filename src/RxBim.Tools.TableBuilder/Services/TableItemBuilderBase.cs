namespace RxBim.Tools.TableBuilder
{
    /// <summary>
    /// Base class of the table item builder.
    /// </summary>
    public abstract class TableItemBuilderBase<TItem, TBuilder>
        where TBuilder : TableItemBuilderBase<TItem, TBuilder>
        where TItem : TableItemBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TableItemBuilderBase{TItem, TBuilder}"/> class.
        /// </summary>
        /// <param name="objectForBuild">The object to be built.</param>
        protected TableItemBuilderBase(TItem objectForBuild)
        {
            ObjectForBuild = objectForBuild;
        }

        /// <summary>
        /// Returns the object to be built.
        /// </summary>
        public TItem ObjectForBuild { get; }

        /// <summary>
        /// Returns new <see cref="TableBuilder"/> for <see cref="Table"/> to be build.
        /// </summary>
        public TableBuilder ToTable() => new(ObjectForBuild.Table);
    }
}