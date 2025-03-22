using System;
using CommandLine;
using Zenoh;

namespace ZDelete
{
    public class Program
    {
        static void Main(string[] args)
        {
            var arguments = Parser.Default.ParseArguments<Args>(args);
            var isOk = true;
            arguments.WithNotParsed(e => { isOk = false; });
            if (!isOk) return;

            var config = arguments.Value.ToConfig();
            if (config is null) return;

            Console.WriteLine("Opening session...");
            var r = Session.Open(config, out var session);
            if (session is null)
            {
                Console.WriteLine($"Opening session unsuccessful! result: {r}");
                return;
            }

            Console.WriteLine("Opening session successful!\n");

            string keyexprStr = "demo/example/zenoh-cs-delete";
            var keyexpr = Keyexpr.FromString(keyexprStr);
            if (keyexpr is null) goto Exit;

            var options = new DeleteOptions();

            r = session.Delete(keyexpr, options);

            var printStr = r != Result.Ok
                ? $"Deleting data failed! result: {r}, key: {keyexprStr}"
                : $"Deleting data succeeded! key: {keyexprStr}";
            Console.WriteLine(printStr);

            Exit:
            session.Close();
            Console.WriteLine("exit");
        }
    }

    public class Args
    {
        [Option('c', "config", Required = false, HelpText = "Zenoh config file.")]
        public string? ConfigFilePath { get; set; } = null;

        public Config? ToConfig()
        {
            var config = ConfigFilePath == null ? Config.Default() : Config.FromFile(ConfigFilePath);

            if (config is null == false) return config;

            Console.WriteLine("load config file error!");
            return null;
        }
    }
}