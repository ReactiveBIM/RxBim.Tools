namespace RxBim.Tools;

/// <summary>
/// <see cref="IWrapper"/> realisation.
/// </summary>
public abstract class Wrapper<T> : IWrapper
    where T : class
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Wrapper{T}"/> class.
    /// </summary>
    /// <param name="wrappedObject">Wrapped object.</param>
    protected Wrapper(T wrappedObject)
    {
        Object = wrappedObject;
    }
    
    /// <summary>
    /// Wrapped object.
    /// </summary>
    public T Object { get; }
}