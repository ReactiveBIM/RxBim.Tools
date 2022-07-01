namespace RxBim.Tools.Common.Extensions
{
    using System;
    using Di;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// Extensions for <see cref="IContainer"/>.
    /// </summary>
    public static class ContainerExtensions
    {
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
            return container.AddFromConfig<T>(true, config, sectionName);
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
            return container.AddFromConfig<T>(false, config, sectionName);
        }

        private static IContainer AddFromConfig<T>(
            this IContainer container,
            bool addAsTransient,
            IConfiguration? config,
            string? sectionName)
            where T : class
        {
            var section = sectionName ?? typeof(T).Name;
            var factory = config is null
                ? (Func<T>)(() => container.GetService<IConfiguration>().GetSection(section).Get<T>())
                : () => config.GetSection(section).Get<T>();

            return container.Add(addAsTransient, factory);
        }

        private static IContainer Add<T>(this IContainer container, bool transient, Func<T> func)
            where T : class
        {
            return transient ? container.AddTransient(func) : container.AddSingleton(func);
        }
    }
}