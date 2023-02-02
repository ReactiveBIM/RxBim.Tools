namespace RxBim.Tools.TableBuilder;

using System;

/// <summary>
/// Extensions for <see cref="Array"/>.
/// </summary>
public static class ArrayExtensions
{
    /// <summary>
    /// ForEach for <see cref="Array"/>.
    /// </summary>
    /// <param name="array"><see cref="Array"/>.</param>
    /// <param name="action">Iterator action.</param>
    public static void ForEach(this Array array, Action<Array, int[]> action)
    {
        if (array.LongLength == 0) 
            return;
        
        var walker = new ArrayTraverse(array);
        do
            action(array, walker.Position);
        while (walker.Step());
    }
}

#pragma warning disable SA1402,SA1600
internal class ArrayTraverse
#pragma warning restore SA1600,SA1402
{
    private readonly int[] _maxLengths;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="ArrayTraverse"/> class.
    /// </summary>
    /// <param name="array"><see cref="Array"/>.</param>
    public ArrayTraverse(Array array)
    {
        _maxLengths = new int[array.Rank];
        for (var i = 0; i < array.Rank; ++i)
            _maxLengths[i] = array.GetLength(i) - 1;
        
        Position = new int[array.Rank];
    }
    
    /// <summary>
    /// Items positions.
    /// </summary>
    public int[] Position { get; }

    /// <summary>
    /// Step.
    /// </summary>
    public bool Step()
    {
        for (var i = 0; i < Position.Length; ++i)
        {
            if (Position[i] >= _maxLengths[i])
                continue;
            
            Position[i]++;
            
            for (var j = 0; j < i; j++)
                Position[j] = 0;
                
            return true;
        }
        
        return false;
    }
}