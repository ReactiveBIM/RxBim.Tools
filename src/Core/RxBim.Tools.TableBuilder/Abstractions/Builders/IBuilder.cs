namespace RxBim.Tools.TableBuilder.Builders;

/// <summary>
/// Generic builder <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="T">Type for build.</typeparam>
public interface IBuilder<out T>
    where T : class
{
    /// <summary>
    /// Returns the built <typeparamref name="T"/>.
    /// </summary>
    T Build();
}