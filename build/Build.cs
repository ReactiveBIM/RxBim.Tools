using System;
using System.Linq;
using System.Text;
using Bimlab.Nuke.Components;
using Nuke.Common;
using Nuke.Common.CI.GitHubActions;
using Nuke.Common.Execution;
using Nuke.Common.IO;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Utilities.Collections;
using static Nuke.Common.IO.PathConstruction;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

[UnsetVisualStudioEnvironmentVariables]
[GitHubActions("CI",
    GitHubActionsImage.WindowsLatest,
    FetchDepth = 0,
    OnPushBranches = new[] { DevelopBranch, FeatureBranches },
    InvokedTargets = new[] { nameof(Test), nameof(IPublish.Publish) },
    ImportSecrets = new[] { "NUGET_API_KEY", "ALL_PACKAGES" })]
/*[GitHubActions("PullRequest",
    GitHubActionsImage.WindowsLatest,
    OnPullRequestBranches = new[] { DevelopBranch, "feature/**" },
    InvokedTargets = new[] { nameof(Test) })]*/
[GitHubActions("Publish",
    GitHubActionsImage.WindowsLatest,
    FetchDepth = 0,
    OnPushBranches = new[] { MasterBranch, "release/**", "hotfix/**" },
    InvokedTargets = new[] { nameof(Test), nameof(IPublish.Publish) },
    ImportSecrets = new[] { "NUGET_API_KEY", "ALL_PACKAGES" })]
class Build : NukeBuild, IPublish, IVersions
{
    const string MasterBranch = "master";
    const string DevelopBranch = "develop";
    const string FeatureBranches = "feature/**";

    public Build()
    {
        Console.OutputEncoding = Encoding.UTF8;
    }

    public static int Main() => Execute<Build>(x => x.From<IVersions>().IncreaseToDevVersions);

    public Target Test => _ => _
        .Before<IRestore>()
        .Executes(() =>
        {
            DotNetTest(settings => settings
                .SetProjectFile(From<IHasSolution>().Solution.Path)
                .SetConfiguration(From<IHasConfiguration>().Configuration));
        });

    T From<T>()
        where T : INukeBuild =>
        (T)(object)this;
}