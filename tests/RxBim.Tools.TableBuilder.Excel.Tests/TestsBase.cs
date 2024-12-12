namespace RxBim.Tools.TableBuilder.Excel.Tests;

using System;
using Microsoft.Extensions.DependencyInjection;

public abstract class TestsBase
{
    public TestsBase()
    {
        var services = new ServiceCollection();
        services.AddExcelTableBuilder();
        Container = services.BuildServiceProvider();
    }

    public IServiceProvider Container { get; }
}