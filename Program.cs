namespace sciism.github.io
{
    internal class Program
    {
        public static async Task<int> Main(string[] args) => await Bootstrapper.Factory
            .CreateWeb(args)
            .DeployToGitHubPages(
            "sciism",
            "sciism.github.io",
            Config.FromSetting<string>("GITHUB_TOKEN"))
            .RunAsync();
    }
}