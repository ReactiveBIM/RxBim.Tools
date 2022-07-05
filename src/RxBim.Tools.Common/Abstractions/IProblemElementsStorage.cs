namespace RxBim.Tools.Abstractions
{
    using System.Collections.Generic;

    /// <summary>
    /// Problem elements storage.
    /// </summary>
    public interface IProblemElementsStorage<T>
        where T : struct
    {
        /// <summary>
        /// Adds the element with the problem to the store.
        /// </summary>
        /// <param name="id">Element ID.</param>
        /// <param name="problem">Problem description.</param>
        void AddProblemElement(T id, string problem);

        /// <summary>
        /// Returns elements IDs combined by problem description.
        /// </summary>
        IDictionary<string, IEnumerable<T>> GetCombinedProblems();

        /// <summary>
        /// Returns a collection of pairs from the storage: element ID and problem description.
        /// </summary>
        IEnumerable<KeyValuePair<T, string>> GetProblems();

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