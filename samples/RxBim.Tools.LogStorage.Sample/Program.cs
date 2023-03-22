namespace RxBim.Tools.LogStorage.Sample;

using Di;
using Models;
using Tools;

/// <summary>
/// Console application.
/// </summary>
public class Program
{
    /// <summary>
    /// Main.
    /// </summary>
    public static void Main()
    {
        // Create a simple container and register the log storage.
        var container = CreateContainer();

        var logStorage = container.GetService<ILogStorage>();

        // Add text message to storage
        logStorage.AddMessage(new TextMessage("Test message"));

        // For add text with id message you will need to create
        // your own object inherited from IObjectIdWrapper interface
        logStorage.AddMessage(new TextWithIdMessage("Test message", new ObjectIdWrapper(1)));

        // Get messages count
        logStorage.Count();
    }

    private static IContainer CreateContainer()
    {
        var container = new SimpleInjectorContainer();
        container.AddSingleton<ILogStorage, LogStorage>();
        return container;
    }
}