using System;
using System.Linq;
using Bimlab.Nuke.Nuget;
using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tools.DotNet;

partial class Build
{
    Target List => _ => _
        .Executes(() =>
        {
            var projects = _packageInfoProvider.Projects;
            Console.WriteLine("\nPackage list:");
            foreach (var projectInfo in projects)
            {
                Console.WriteLine(projectInfo.MenuItem);
            }
        });
    
    Target Pack => _ => _
        .DependsOn(Compile)
        .Executes(() =>
        {
            _packageInfoProvider.GetSelectedProjects(Solution.AllProjects.Select(x => x.Name).ToArray())
                .ForEach(x => PackInternal(Solution, x, OutputDirectory, Configuration));
        });

    /// <summary>
    /// Makes a package
    /// </summary>
    /// <param name="solution">Solution</param>
    /// <param name="project">Project</param>
    /// <param name="outDir">Output directory</param>
    /// <param name="configuration">Build configuration</param>
    static void PackInternal(
        Solution solution,
        ProjectInfo project,
        AbsolutePath outDir,
        string configuration)
    {
        var path = solution.GetProject(project.ProjectName)?.Path;

        if (!string.IsNullOrEmpty(path))
        {
            DotNetTasks.DotNetPack(s => s
                .SetProject(path)
                .SetOutputDirectory(outDir)
                .SetConfiguration(configuration)
                .EnableNoBuild()
                .EnableNoRestore());
        }
    }

    Target CheckUncommitted => _ => _
        .After(Pack)
        .Executes(() =>
        {
            PackageExtensions.CheckUncommittedChanges(Solution);
        });

    Target Push => _ => _
        .Requires(() => NugetApiKey)
        .Unlisted()
        .DependsOn(Pack, CheckUncommitted)
        .Executes(() =>
        {
            var nugetApiKey = Environment.GetEnvironmentVariable("NUGET_API_KEY");
            if (string.IsNullOrWhiteSpace(nugetApiKey))
            {
                throw new ArgumentException("NUGET_API_KEY variable is not setted");
            }

            _packageInfoProvider.GetSelectedProjects(Solution.AllProjects.Select(x => x.Name).ToArray())
                .ForEach(x => PackageExtensions.PushPackage(Solution, x, OutputDirectory, nugetApiKey, NugetSource));
        });

    Target Tag => _ => _
        .DependsOn(Push)
        .Executes(() =>
        {
            _packageInfoProvider.GetSelectedProjects()
                .ForEach(x => PackageExtensions.TagPackage(Solution, x));
        });

    Target Publish => _ => _
        .Description("Публикует Nuget-пакеты")
        .DependsOn(Tag);
}