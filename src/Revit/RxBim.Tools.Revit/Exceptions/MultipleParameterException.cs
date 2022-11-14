namespace RxBim.Tools.Revit;

using JetBrains.Annotations;

/// <summary>
/// The exception that is thrown for several parameters.
/// </summary>
[PublicAPI]
public class MultipleParameterException : ParameterException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MultipleParameterException"/> class.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    public MultipleParameterException(string parameterName)
        : base(parameterName, $"There are several parameters {parameterName}.")
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MultipleParameterException"/> class.
    /// </summary>
    /// <param name="parameterName">Definition parameter name.</param>
    /// <param name="message">The message that describes the error.</param>
    public MultipleParameterException(
        string parameterName,
        string message)
        : base(parameterName, message)
    {
    }
}