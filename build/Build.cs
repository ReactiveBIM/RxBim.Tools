using System;
using System.Text;
using Bimlab.Nuke.Components;
using Nuke.Common;
using Nuke.Common.CI.GitHubActions;
using Nuke.Common.Execution;
using Nuke.Common.Tools.DotNet;
using RxBim.Nuke.Versions;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

[UnsetVisualStudioEnvironmentVariables]
[GitHubActions("CI",
    GitHubActionsImage.WindowsLatest,
    FetchDepth = 0,
    OnPushBranches = new[] { DevelopBranch, FeatureBranches, BugFixBranches },
    InvokedTargets = new[] { nameof(Test), nameof(IPublish.Publish) },
    ImportSecrets = new[] { "NUGET_API_KEY", "ALL_PACKAGES" })]
[GitHubActions("Publish",
    GitHubActionsImage.WindowsLatest,
    FetchDepth = 0,
    OnPushBranches = new[] { MasterBranch, "release/**", "hotfix/**" },
    InvokedTargets = new[] { nameof(Test), nameof(IPublish.Publish) },
    ImportSecrets = new[] { "NUGET_API_KEY", "ALL_PACKAGES" })]
partial class Build : NukeBuild, IVersions
{
    const string MasterBranch = "master";
    const string DevelopBranch = "develop";
    const string FeatureBranches = "feature/**";
    const string BugFixBranches = "bugfix/**";

    public Build()
    {
        Console.OutputEncoding = Encoding.UTF8;
    }

    public static int Main() => Execute<Build>(x => x.From<IPublish>().PackagesList);

    public Target Test => _ => _
        .Before<IRestore>()
        .Executes(() =>
        {
            DotNetTest(settings => settings
                .SetProjectFile(From<IHasSolution>().Solution.Path)
                .SetConfiguration(From<IHasConfiguration>().Configuration));
        });
    
    string IVersionBuild.ProjectNamePrefix => "RxBim.";

    T From<T>()
        where T : INukeBuild =>
        (T)(object)this;
}