namespace RxBim.Tools
{
    using System;
    using System.IO;

    /// <summary>
    /// Path Utilities.
    /// </summary>
    public static class PathUtils
    {
        /// <summary>
        /// Creates a relative path from one file or folder to another.
        /// </summary>
        /// <param name="fromPath">Contains the directory that defines the start of the relative path.</param>
        /// <param name="toPath">Contains the path that defines the endpoint of the relative path.</param>
        /// <returns>The relative path from the start directory to the end path.</returns>
        public static string GetRelativePath(string fromPath, string toPath)
        {
            var fromUri = new Uri(AppendDirectorySeparatorChar(fromPath));
            var toUri = new Uri(AppendDirectorySeparatorChar(toPath));

            if (fromUri.Scheme != toUri.Scheme)
                return toPath;

            var relativeUri = fromUri.MakeRelativeUri(toUri);
            var relativePath = Uri.UnescapeDataString(relativeUri.ToString());

            if (string.Equals(toUri.Scheme, Uri.UriSchemeFile, StringComparison.OrdinalIgnoreCase))
                relativePath = relativePath.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);

            return relativePath;
        }

        private static string AppendDirectorySeparatorChar(string path)
        {
            // Append a slash only if the path is a directory and does not have a slash.
            return !Path.HasExtension(path) &&
                   !path.EndsWith(Path.DirectorySeparatorChar.ToString())
                ? path + Path.DirectorySeparatorChar
                : path;
        }
    }
}