using System;
using CommandLine;
using Zenoh;

namespace ZLiveliness
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

            Console.WriteLine("Opening session...");
            var r = Session.Open(config, out var session);
            if (session is null)
            {
                Console.WriteLine($"Opening session unsuccessful! result: {r}");
                return;
            }

            Console.WriteLine("Opening session successful!\n");

            string keyStr = "group1/zenoh-cs-liveliness";
            var keyexpr = Keyexpr.FromString(keyStr);
            if (keyexpr is null) goto Exit;

            r = session.DeclareLivelinessToken(keyexpr, out LivelinessToken? livelinessToken);
            if (livelinessToken is null)
            {
                Console.WriteLine($"Declare liveliness token unsuccessful! result: {r}");
                goto Exit;
            }

            Console.WriteLine($"Declared liveliness token on {keyStr}");

            Console.WriteLine("Input 'q' to undeclare liveliness token and quit.");
            while (true)
            {
                var input = Console.ReadLine();
                if (input == "q") break;
            }

            livelinessToken.Undeclare();
            livelinessToken.Dispose();

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