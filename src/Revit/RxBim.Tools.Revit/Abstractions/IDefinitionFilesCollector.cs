namespace RxBim.Tools.Revit;

using System.Collections.Generic;
using JetBrains.Annotations;

/// <summary>
/// Collector of <see cref="IDefinitionFileWrapper"/>.
/// </summary>
[PublicAPI]
public interface IDefinitionFilesCollector
{
    /// <summary>
    /// Gets <see cref="IDefinitionFileWrapper"/> for document.
    /// </summary>
    /// <param name="document"><see cref="IDocumentWrapper"/>.
    /// If null, <see cref="IDefinitionFileWrapper"/> gets from current document.</param>
    IDefinitionFileWrapper GetDefinitionFile(
        IDocumentWrapper? document = null);

    /// <summary>
    /// Gets collection of <see cref="IDefinitionFileWrapper"/> from files path.
    /// </summary>
    /// <param name="filesSource">Collection of files path.</param>
    IEnumerable<IDefinitionFileWrapper> GetDefinitionFiles(
        IEnumerable<string> filesSource);
}