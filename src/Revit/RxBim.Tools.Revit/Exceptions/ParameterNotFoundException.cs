namespace RxBim.Tools.Revit;

using JetBrains.Annotations;

/// <summary>
/// The exception that is thrown for not found parameter.
/// </summary>
[PublicAPI]
public class ParameterNotFoundException : ParameterException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ParameterNotFoundException"/> class.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    public ParameterNotFoundException(string parameterName)
        : base(parameterName, $"Not found parameter {parameterName}.")
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ParameterNotFoundException"/> class.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <param name="message">The message that describes the error.</param>
    public ParameterNotFoundException(
        string parameterName,
        string message)
        : base(parameterName, message)
    {
    }
}