// ReSharper disable CheckNamespace
namespace System.Runtime.CompilerServices 
{
    using ComponentModel;
    
    /// <summary>
    /// Reserved to be used by the compiler for tracking metadata.
    /// This class should not be used by developers in source code.
    /// </summary>
    /// <remarks>https://stackoverflow.com/a/64749403</remarks>
    [EditorBrowsable(EditorBrowsableState.Never)]
    internal static class IsExternalInit 
    {
    }
}