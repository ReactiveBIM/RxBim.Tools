namespace RxBim.Tools
{
    using System.Collections.Generic;
    using System.Linq;
    using Abstractions;
    using JetBrains.Annotations;

    /// <summary>
    /// Хранилище проблемных элементов
    /// </summary>
    [UsedImplicitly]
    public class ProblemElementsStorage<T> : IProblemElementsStorage<T>
        where T : struct
    {
        private readonly Dictionary<string, List<T>> _storage = new();

        /// <inheritdoc/>
        public void AddProblemElement(T id, string problem)
        {
            if (_storage.ContainsKey(problem))
                _storage[problem].Add(id);
            else
                _storage.Add(problem, new List<T> { id });
        }

        /// <inheritdoc/>
        public IDictionary<string, IEnumerable<T>> GetCombinedProblems()
        {
            return _storage
                .ToDictionary(
                    problem => problem.Key,
                    problem => new List<T>(problem.Value) as IEnumerable<T>);
        }

        /// <inheritdoc/>
        public IEnumerable<KeyValuePair<T, string>> GetProblems()
        {
            return _storage
                .SelectMany(problem => problem.Value.Select(id => new KeyValuePair<T, string>(id, problem.Key)));
        }

        /// <inheritdoc/>
        public bool HasProblems()
            => _storage.Any();

        /// <inheritdoc/>
        public void Clear()
            => _storage.Clear();
    }
}
