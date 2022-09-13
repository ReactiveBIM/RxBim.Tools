namespace RxBim.Tools.Revit;

using JetBrains.Annotations;

/// <summary>
/// Wrapper for Element type.
/// </summary>
[PublicAPI]
public interface IElementWrapper : IWrapper
{
    /// <summary>
    /// Gets value from parameter.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <typeparam name="T">Required type of value.</typeparam>
    T? GetParameterValue<T>(string parameterName);
}