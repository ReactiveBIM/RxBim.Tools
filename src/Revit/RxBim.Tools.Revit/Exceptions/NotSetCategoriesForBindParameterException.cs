namespace RxBim.Tools.Revit;

using JetBrains.Annotations;

/// <summary>
/// The exception that is thrown for not set categories for bind parameter in <see cref="SharedParameterCreateData"/>.
/// </summary>
[PublicAPI]
public class NotSetCategoriesForBindParameterException : ParameterException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NotSetCategoriesForBindParameterException"/> class.
    /// </summary>
    /// <param name="parameterName">Definition parameter name.</param>
    public NotSetCategoriesForBindParameterException(string parameterName)
        : base(parameterName, $"Not set categories for bind parameter {parameterName}")
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="NotSetCategoriesForBindParameterException"/> class.
    /// </summary>
    /// <param name="parameterName">Definition parameter name.</param>
    /// <param name="message">The message that describes the error.</param>
    public NotSetCategoriesForBindParameterException(
        string parameterName,
        string message)
        : base(parameterName, message)
    {
    }
}