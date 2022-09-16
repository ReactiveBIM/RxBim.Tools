namespace RxBim.Tools;

using System;

/// <summary>
/// <see cref="IWrapper"/> realisation.
/// </summary>
public abstract class Wrapper<T> : IWrapper
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

    /// <inheritdoc />
    public TWrap Unwrap<TWrap>()
    {
        return (TWrap)Convert.ChangeType(Object, typeof(TWrap));
    }
}