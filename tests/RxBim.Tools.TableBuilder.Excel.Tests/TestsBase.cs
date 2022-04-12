namespace RxBim.Tools.TableBuilder.Excel.Tests;

using Di;
using Extensions;

public abstract class TestsBase
{
    public TestsBase()
    {
        Container = new SimpleInjectorContainer();
        Container.AddExcelSerialization();
    }

    public IContainer Container { get; }
}