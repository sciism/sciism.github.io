namespace sciism.github.io
{
    internal class Program
    {
        public static async Task<int> Main(string[] args) => await Bootstrapper.Factory.CreateWeb(args).RunAsync();
    }
}