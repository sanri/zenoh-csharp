using System;
using CommandLine;
using Zenoh;

namespace ZScout
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var arguments = Parser.Default.ParseArguments<Args>(args);
            var isOk = true;
            arguments.WithNotParsed(e => { isOk = false; });
            if (!isOk) return;

            var config = arguments.Value.ToConfig();
            if (config is null) return;

            var options = new ScoutOptions
            {
                TimeoutMs = arguments.Value.TimeoutMs,
                What = What.RouterPeer,
            };

            Console.WriteLine("Scouting...");
            var count = 0;
            var r = Scout.Run(config, hello =>
            {
                count++;
                Console.WriteLine(
                    $"Hello {{ zid: {hello.Id.ToHexStr()}, whatami: {hello.Whatami}, locators: [{string.Join(", ", hello.Locators)}] }}");
            }, options);
            config.Dispose();

            if (r != Result.Ok)
            {
                Console.WriteLine($"Scouting failed! result: {r}");
                return;
            }

            if (count == 0) Console.WriteLine("Did not find any zenoh process.");
            Console.WriteLine("exit");
        }
    }

    public class Args
    {
        [Option('c', "config", Required = false, HelpText = "Zenoh config file.")]
        public string? ConfigFilePath { get; set; } = null;

        [Option('t', "timeout", Required = false, HelpText = "Scouting timeout in milliseconds.")]
        public ulong TimeoutMs { get; set; } = 1000;

        public Config? ToConfig()
        {
            var config = ConfigFilePath == null ? Config.Default() : Config.FromFile(ConfigFilePath);

            if (config is null == false) return config;

            Console.WriteLine("load config file error!");
            return null;
        }
    }
}
