namespace RxBim.Tools;

using System;
using System.ComponentModel;
using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Extensions for <see cref="IServiceCollection"/>.
/// </summary>
[PublicAPI]
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds tools services.
    /// </summary>
    /// <param name="services">The instance of <see cref="IServiceCollection"/>.</param>
    public static IServiceCollection AddToolsServices(this IServiceCollection services)
    {
        return services.AddSingleton<ILogStorage, LogStorage>();
    }

    /// <summary>
    /// Adds transaction services.
    /// </summary>
    /// <param name="services">The instance of <see cref="IContainer"/>.</param>
    /// <typeparam name="TTransactionFactory">
    /// The type of <see cref="ITransactionFactory"/> implementation.
    /// </typeparam>
    public static IServiceCollection AddTransactionServices<TTransactionFactory>(this IServiceCollection services)
        where TTransactionFactory : class, ITransactionFactory
    {
        return services
            .AddSingleton<ITransactionService, TransactionService>()
            .AddSingleton<ITransactionFactory, TTransactionFactory>();
    }

    /// <summary>
    /// Adds a transient object of the type specified in <typeparamref name="T"/> to the DI.
    /// The object data is loaded from the configuration.
    /// </summary>
    /// <param name="services">The instance of <see cref="IContainer"/>.</param>
    /// <param name="config">Configuration. If null, the configuration is taken from the services.</param>
    /// <param name="sectionName">
    /// The name of the configuration section for the object data.
    /// If null, the name of the type specified in <typeparamref name="T"/> is used.
    /// </param>
    /// <typeparam name="T">The type of the requested object.</typeparam>
    public static IServiceCollection AddTransientFromConfig<T>(
        this IServiceCollection services,
        IConfiguration? config = null,
        string? sectionName = null)
        where T : class
    {
        return services.AddFromConfig<T>(ServiceLifetime.Transient, config, sectionName);
    }

    /// <summary>
    /// Adds a singleton object of the type specified in <typeparamref name="T"/> to the DI.
    /// The object data is loaded from the configuration.
    /// </summary>
    /// <param name="services">The instance of <see cref="IContainer"/>.</param>
    /// <param name="config">Configuration. If null, the configuration is taken from the services.</param>
    /// <param name="sectionName">
    /// The name of the configuration section for the object data.
    /// If null, the name of the type specified in <typeparamref name="T"/> is used.
    /// </param>
    /// <typeparam name="T">The type of the requested object.</typeparam>
    public static IServiceCollection AddSingletonFromConfig<T>(
        this IServiceCollection services,
        IConfiguration? config = null,
        string? sectionName = null)
        where T : class
    {
        return services.AddFromConfig<T>(ServiceLifetime.Singleton, config, sectionName);
    }

    private static IServiceCollection AddFromConfig<T>(
        this IServiceCollection services,
        ServiceLifetime lifetime,
        IConfiguration? config,
        string? sectionName)
        where T : class
    {
        var section = sectionName ?? typeof(T).Name;
        var implementationFactory = config is null
            ? (Func<IServiceProvider, T>)(sp => sp.GetService<IConfiguration>().GetSection(section).Get<T>())
            : _ => config.GetSection(section).Get<T>();

        return lifetime switch
        {
            ServiceLifetime.Singleton => services.AddSingleton(implementationFactory),
            ServiceLifetime.Transient => services.AddTransient(implementationFactory),
            ServiceLifetime.Scoped => services.AddScoped(implementationFactory),
            _ => throw new ArgumentOutOfRangeException(nameof(lifetime), lifetime, "Unsupported lifetime!")
        };
    }
}