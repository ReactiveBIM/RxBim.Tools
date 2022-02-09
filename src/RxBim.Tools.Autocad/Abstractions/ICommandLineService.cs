namespace RxBim.Tools.Autocad.Abstractions
{
    /// <summary>
    /// Command line service
    /// </summary>
    public interface ICommandLineService
    {
        /// <summary>
        /// Outputs a text message as a new line
        /// </summary>
        /// <param name="message">Text message</param>
        void WriteAsNewLine(string message);
    }
}