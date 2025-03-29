using System;
using CommandLine;
using Zenoh;

namespace ZSubLiveliness
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

            string keyStr = "group1/**";
            var keyexpr = Keyexpr.FromString(keyStr);
            if (keyexpr is null) goto Exit;

            var options = new LivelinessSubscriberOptions();
            options.History = true;

            r = session.DeclareLivelinessSubscriber(keyexpr, options, Callback,
                out LivelinessSubscriber? livelinessSubscriber);
            if (livelinessSubscriber is null)
            {
                Console.WriteLine($"Declare liveliness subscriber unsuccessful! result: {r}");
                goto Exit;
            }

            Console.WriteLine($"Declared liveliness subscriber on {keyStr}");

            Console.WriteLine("Input 'q' to quit.");
            while (true)
            {
                var input = Console.ReadLine();
                if (input == "q") break;
            }

            Exit:
            session.Close();
            Console.WriteLine("exit");
        }

        static void Callback(Sample sample)
        {
            var keyexpr = sample.GetKeyexpr();
            var keyexprStr = keyexpr.ToString();
            var kind = sample.GetKind();

            string print = kind switch
            {
                SampleKind.Put => $"[LivelinessSubscriber] New alive token ({keyexprStr})",
                SampleKind.Delete => $"[LivelinessSubscriber] Dropped token ({keyexprStr})",
                _ => ""
            };

            Console.WriteLine(print);
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