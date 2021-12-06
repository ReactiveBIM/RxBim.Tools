namespace RxBim.Tools.TableBuilder.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    /// <summary>
    /// Extensions for formats.
    /// </summary>
    internal static class FormatExtensions
    {
        private static readonly Dictionary<Type, PropertyInfo[]> PropertiesCache = new ();

        /// <summary>
        /// Returns the result of a combination of two formats.
        /// </summary>
        /// <param name="thisFormat">Own object format.</param>
        /// <param name="ownerFormat">The format of the owner object.</param>
        /// <typeparam name="T">Format type.</typeparam>
        public static T Collect<T>(this T thisFormat, T ownerFormat)
            where T : class, new()
        {
            var properties = GetCachedProperties(typeof(T));
            var style = new T();
            foreach (var property in properties)
                CollectProperty(property, style, thisFormat, ownerFormat);

            return style;
        }

        /// <summary>
        /// Applies the values of all format properties from another format.
        /// </summary>
        /// <param name="thisFormat">Format for receiving properties.</param>
        /// <param name="sourceFormat">Format is the source of properties.</param>
        /// <typeparam name="T">Format type.</typeparam>
        public static void CopyProperties<T>(this T thisFormat, T sourceFormat)
            where T : class
        {
            var properties = GetCachedProperties(typeof(T));
            foreach (var property in properties)
                CollectProperty(property, thisFormat, sourceFormat);
        }

        private static void CollectProperty(
            PropertyInfo property,
            object target,
            object source,
            object? additionalSource = null)
        {
            if (property.CanWrite)
            {
                var value = property.GetValue(source);
                if (additionalSource != null)
                    value ??= property.GetValue(additionalSource);
                property.SetValue(target, value);
            }
            else if (!property.PropertyType.IsValueType)
            {
                var innerProps = GetCachedProperties(property.PropertyType);
                var valueForTarget = property.GetValue(target);
                var valueFromSource = property.GetValue(source);
                var valueFromAdditionalSource = additionalSource is null ? null : property.GetValue(additionalSource);

                foreach (var innerProperty in innerProps)
                    CollectProperty(innerProperty, valueForTarget, valueFromSource, valueFromAdditionalSource);
            }
        }

        private static PropertyInfo[] GetCachedProperties(Type type)
        {
            if (!PropertiesCache.TryGetValue(type, out var innerProps))
            {
                innerProps = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                PropertiesCache[type] = innerProps;
            }

            return innerProps;
        }
    }
}