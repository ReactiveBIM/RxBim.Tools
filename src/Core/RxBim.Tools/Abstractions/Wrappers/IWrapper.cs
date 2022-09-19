namespace RxBim.Tools;

/// <summary>
/// Wrapper.
/// </summary>
public interface IWrapper
{
    /// <summary>
    /// Gets real object from wrapped object.
    /// </summary>
    /// <typeparam name="TWrap">Wrapping object type.</typeparam>
    TWrap Unwrap<TWrap>();
}