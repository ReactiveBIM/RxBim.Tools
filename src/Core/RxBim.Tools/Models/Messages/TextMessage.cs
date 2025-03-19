namespace RxBim.Tools;

using JetBrains.Annotations;

/// <summary>
/// Class for text messages.
/// </summary>
[PublicAPI]
public class TextMessage : MessageBase
{
    /// <inheritdoc />
    public TextMessage(string text, bool isDebugMessage = false)
        : base(text, isDebugMessage)
    {
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return Text;
    }
}