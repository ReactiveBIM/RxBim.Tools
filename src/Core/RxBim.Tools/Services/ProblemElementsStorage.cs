namespace RxBim.Tools
{
    using System.Collections.Generic;
    using System.Linq;
    using JetBrains.Annotations;

    /// <inheritdoc />
    [UsedImplicitly]
    internal class ProblemElementsStorage : IProblemElementsStorage
    {
        private readonly Dictionary<string, List<IObjectIdWrapper>> _storage = new();

        /// <inheritdoc/>
        public void AddProblemElement(IObjectIdWrapper id, string problem)
        {
            if (_storage.ContainsKey(problem))
                _storage[problem].Add(id);
            else
                _storage.Add(problem, new List<IObjectIdWrapper> { id });
        }

        /// <inheritdoc/>
        public IDictionary<string, IEnumerable<IObjectIdWrapper>> GetCombinedProblems()
        {
            return _storage.ToDictionary(
                problem => problem.Key,
                problem => new List<IObjectIdWrapper>(problem.Value) as IEnumerable<IObjectIdWrapper>);
        }

        /// <inheritdoc/>
        public IEnumerable<KeyValuePair<IObjectIdWrapper, string>> GetProblems()
        {
            return _storage.SelectMany(problem =>
                problem.Value.Select(id => new KeyValuePair<IObjectIdWrapper, string>(id, problem.Key)));
        }

        /// <inheritdoc/>
        public bool HasProblems() => _storage.Any();

        /// <inheritdoc/>
        public void Clear() => _storage.Clear();
    }
}