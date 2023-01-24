namespace RxBim.Tools
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Text.RegularExpressions;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// Assembly Extensions.
    /// </summary>
    public static class AssemblyExtensions
    {
        private static readonly Regex AssemblyNameVersionRex = new(
            @"(?<version>\.\d+\.\d+\.\d+(\.\d+)?)?$",
            RegexOptions.Singleline | RegexOptions.CultureInvariant);

        /// <summary>
        /// Returns the configuration for the build from the appsettings.[build name].json
        /// file and additional JSON files (if any).
        /// </summary>
        /// <param name="assembly">Assembly.</param>
        /// <param name="additionalJsonFileNamePattern">Name template for additional uploaded json files.</param>
        public static IConfiguration GetLocalConfig(
            this Assembly assembly,
            string? additionalJsonFileNamePattern = null)
        {
            var basePath = Path.GetDirectoryName(assembly.Location);
            if (basePath is null)
                throw new InvalidOperationException("Failed to get assembly folder path!");

            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .SetFileLoadExceptionHandler(ctx => ctx.Ignore = true)
                .AddJsonFile($"appsettings.{assembly.GetNameWithoutVersion()}.json", true);

            AddJSonFiles(configBuilder, basePath, additionalJsonFileNamePattern);

            return configBuilder.Build();
        }

        /// <summary>
        /// Returns the assembly name without the version number at the end.
        /// </summary>
        /// <param name="assembly">Assembly.</param>
        public static string GetNameWithoutVersion(this Assembly assembly)
        {
            var assemblyName = assembly.GetName().Name;
            var match = AssemblyNameVersionRex.Match(assemblyName);
            return match.Success
                ? assemblyName.Substring(0, assemblyName.Length - match.Groups["version"].Length)
                : assemblyName;
        }

        private static void AddJSonFiles(IConfigurationBuilder configBuilder, string basePath, string? fileNamePattern)
        {
            if (string.IsNullOrEmpty(fileNamePattern))
                return;

            var additionalFilePaths =
                Directory.GetFiles(basePath, fileNamePattern, SearchOption.AllDirectories);

            foreach (var additionalFilePath in additionalFilePaths)
            {
                var jsonFile = PathUtils.GetRelativePath(basePath, additionalFilePath);
                configBuilder.AddJsonFile(jsonFile, true);
            }
        }
    }
}