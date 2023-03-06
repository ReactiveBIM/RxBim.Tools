namespace RxBim.Tools.TableBuilder
{
    /// <summary>
    /// Base class of the table item builder.
    /// </summary>
    public abstract class TableItemEditorBase<TItem, TBuilder>
        where TBuilder : TableItemEditorBase<TItem, TBuilder>
        where TItem : TableItemBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TableItemEditorBase{TItem,TBuilder}"/> class.
        /// </summary>
        /// <param name="objectForBuild">The object to be built.</param>
        protected TableItemEditorBase(TItem objectForBuild)
        {
            ObjectForBuild = objectForBuild;
        }

        /// <summary>
        /// Returns the object to be built.
        /// </summary>
        protected TItem ObjectForBuild { get; }
    }
}