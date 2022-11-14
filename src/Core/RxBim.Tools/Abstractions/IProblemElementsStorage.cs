namespace RxBim.Tools
{
    using System.Collections.Generic;
    using JetBrains.Annotations;

    /// <summary>
    /// Problem elements storage.
    /// </summary>
    [PublicAPI]
    public interface IProblemElementsStorage
    {
        /// <summary>
        /// Adds the element with the problem to the store.
        /// </summary>
        /// <param name="id">Element ID.</param>
        /// <param name="problem">Problem description.</param>
        void AddProblemElement(IObjectIdWrapper id, string problem);

        /// <summary>
        /// Returns elements IDs combined by problem description.
        /// </summary>
        IDictionary<string, IEnumerable<IObjectIdWrapper>> GetCombinedProblems();

        /// <summary>
        /// Returns a collection of pairs from the storage: element ID and problem description.
        /// </summary>
        IEnumerable<KeyValuePair<IObjectIdWrapper, string>> GetProblems();

        /// <summary>
        /// Returns true if the store contains any element with a problem. Otherwise, returns false.
        /// </summary>
        bool HasProblems();

        /// <summary>
        /// Deletes all data about elements with problems from storage.
        /// </summary>
        void Clear();
    }
}