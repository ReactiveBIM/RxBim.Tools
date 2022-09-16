namespace RxBim.Tools.Revit;

using JetBrains.Annotations;

/// <summary>
/// Wrapper for Element type.
/// </summary>
[PublicAPI]
public interface IElementWrapper : IWrapper
{
    /// <summary>
    /// Specifies whether the .NET object represents a valid Revit entity.
    /// </summary>
    /// <remarks>
    /// If the corresponding Revit native object is destroyed, or creation of the corresponding object is undone,
    /// a managed API object containing it is no longer valid. API methods cannot be called on invalidated wrapper objects.
    /// </remarks>
    bool IsValid { get; }
    
    /// <summary>
    /// Gets value from parameter.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <typeparam name="T">Required type of value.</typeparam>
    T? GetParameterValue<T>(string parameterName);
}