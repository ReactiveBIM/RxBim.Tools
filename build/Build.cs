using System;
using System.Linq;
using Bimlab.Nuke.Nuget;
using Nuke.Common;
using Nuke.Common.CI;
using Nuke.Common.Execution;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Utilities;
using Nuke.Common.Utilities.Collections;
using static Nuke.Common.EnvironmentInfo;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.IO.PathConstruction;

[CheckBuildProjectConfigurations]
partial class Build : NukeBuild
{
    private string _project;

    public static int Main() => Execute<Build>(x => x.Compile);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    [Solution] readonly Solution Solution;
    PackageInfoProvider PackageInfoProvider;

    public Build()
    {
        PackageInfoProvider = new PackageInfoProvider(() => Solution);
    }

    Target Clean => _ => _
        .Before(Restore)
        .Executes(() =>
        {
            GlobDirectories(Solution.Directory, "**/bin", "**/obj")
                .Where(x => !IsDescendantPath(BuildProjectDirectory, x))
                .ForEach(DeleteDirectory);
        });

    Target Restore => _ => _
        .DependsOn(Clean)
        .Executes(() =>
        {
            DotNetTasks.DotNetRestore(settings => settings
                .SetProjectFile(Solution));
        });

    Target Compile => _ => _
        .DependsOn(Restore)
        .Executes(() =>
        {
            DotNetTasks.DotNetBuild(settings => settings
                .SetProjectFile(Solution)
                .EnableNoRestore());
        });

    /// <summary>
    /// Selected project
    /// </summary>
    [Parameter("Select project")]
    public virtual string Project
    {
        get
        {
            if (_project == null)
            {
                var result = ConsoleUtility.PromptForChoice(
                    "Select project:",
                    Solution.AllProjects
                        .Select(x => (x.Name, x.Name))
                        .Append((nameof(Solution), "All"))
                        .ToArray());

                _project = result == nameof(Solution)
                    ? Solution.Name
                    : Solution.AllProjects.FirstOrDefault(x => x.Name == result)?.Name;
            }

            return _project;
        }
        set => _project = value;
    }
}