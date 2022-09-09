namespace RxBim.Tools.Revit;

using System;
using JetBrains.Annotations;

/// <summary>
/// Base exception with parameter.
/// </summary>
[PublicAPI]
public abstract class ParameterException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ParameterException"/> class.
    /// </summary>
    /// <param name="parameterName">Parameter name.</param>
    /// <param name="message">The message that describes the error.</param>
    protected ParameterException(
        string parameterName,
        string message)
        : base(message)
    {
        ParameterName = parameterName;
    }
    
    /// <summary>
    /// Parameter name.
    /// </summary>
    public string ParameterName { get; }
}