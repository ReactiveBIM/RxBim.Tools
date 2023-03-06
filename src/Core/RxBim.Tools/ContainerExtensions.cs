namespace RxBim.Tools
{
    using System;
    using System.Linq;
    using Di;
    using JetBrains.Annotations;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// Extensions for <see cref="IContainer"/>.
    /// </summary>
    [PublicAPI]
    public static class ContainerExtensions
    {
        /// <summary>
        /// Adds tools services.
        /// </summary>
        /// <param name="container">The instance of <see cref="IContainer"/>.</param>
        public static IContainer AddToolsServices(this IContainer container)
        {
            return container.AddSingletonIfNotRegistered<ILogStorage, LogStorage>();
        }

        /// <summary>
        /// Adds transaction services.
        /// </summary>
        /// <param name="container">The instance of <see cref="IContainer"/>.</param>
        /// <typeparam name="TTransactionFactory">
        /// The type of <see cref="ITransactionFactory"/> implementation.
        /// </typeparam>
        public static IContainer AddTransactionServices<TTransactionFactory>(this IContainer container)
            where TTransactionFactory : class, ITransactionFactory
        {
            return container
                .AddSingleton<ITransactionService, TransactionService>()
                .AddSingleton<ITransactionFactory, TTransactionFactory>();
        }

        /// <summary>
        /// Adds a transient object of the type specified in <typeparamref name="T"/> to the DI container.
        /// The object data is loaded from the configuration.
        /// </summary>
        /// <param name="container">The instance of <see cref="IContainer"/>.</param>
        /// <param name="config">Configuration. If null, the configuration is taken from the container.</param>
        /// <param name="sectionName">
        /// The name of the configuration section for the object data.
        /// If null, the name of the type specified in <typeparamref name="T"/> is used.
        /// </param>
        /// <typeparam name="T">The type of the requested object.</typeparam>
        public static IContainer AddTransientFromConfig<T>(
            this IContainer container,
            IConfiguration? config = null,
            string? sectionName = null)
            where T : class
        {
            return container.AddFromConfig<T>(Lifetime.Transient, config, sectionName);
        }

        /// <summary>
        /// Adds a singleton object of the type specified in <typeparamref name="T"/> to the DI container.
        /// The object data is loaded from the configuration.
        /// </summary>
        /// <param name="container">The instance of <see cref="IContainer"/>.</param>
        /// <param name="config">Configuration. If null, the configuration is taken from the container.</param>
        /// <param name="sectionName">
        /// The name of the configuration section for the object data.
        /// If null, the name of the type specified in <typeparamref name="T"/> is used.
        /// </param>
        /// <typeparam name="T">The type of the requested object.</typeparam>
        public static IContainer AddSingletonFromConfig<T>(
            this IContainer container,
            IConfiguration? config = null,
            string? sectionName = null)
            where T : class
        {
            return container.AddFromConfig<T>(Lifetime.Singleton, config, sectionName);
        }

        private static IContainer AddFromConfig<T>(
            this IContainer container,
            Lifetime lifetime,
            IConfiguration? config,
            string? sectionName)
            where T : class
        {
            var section = sectionName ?? typeof(T).Name;
            var implementationFactory = config is null
                ? (Func<T>)(() => container.GetService<IConfiguration>().GetSection(section).Get<T>())
                : () => config.GetSection(section).Get<T>();

            return lifetime switch
            {
                Lifetime.Singleton => container.AddSingleton(implementationFactory),
                Lifetime.Transient => container.AddTransient(implementationFactory),
                Lifetime.Scoped => container.AddScoped(implementationFactory),
                _ => throw new ArgumentOutOfRangeException(nameof(lifetime), lifetime, "Unsupported lifetime!")
            };
        }

        private static IContainer AddSingletonIfNotRegistered<TService, TImplementation>(
            this IContainer container)
            where TService : class
            where TImplementation : class, TService
        {
            if (!container.GetCurrentRegistrations().Select(x => x.ServiceType).Contains(typeof(TService)))
                container.AddSingleton<TService, TImplementation>();
            return container;
        }
    }
}