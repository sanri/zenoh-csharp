using System;
using System.Runtime.InteropServices;
using System.Threading;
using CommandLine;
using Zenoh;

namespace ZPub
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

            string keyStr = "demo/example/zenoh-cs-pub/string";
            var keyexpr = Keyexpr.FromString(keyStr);
            if (keyexpr is null) goto Exit;

            var publisherOptions = new PublisherOptions();
            publisherOptions.Encoding = new Encoding(EncodingId.TextPlain);

            r = session.DeclarePublisher(keyexpr, publisherOptions, out Publisher? publisher);
            if (publisher is null)
            {
                Console.WriteLine($"Declare publisher unsuccessful! result: {r}");
                goto Exit;
            }

            Console.WriteLine($"Declaring publisher on {keyexpr}");

            for (int i = 0; i < 100; i++)
            {
                Thread.Sleep(1000);
                var payloadStr = $"[{i}] Pub from CS!";
                var payload = ZBytes.FromString(payloadStr);
                r = publisher.Put(payload, new PublisherPutOptions());
                if (r != Result.Ok)
                {
                    Console.WriteLine($"Publisher put unsuccessful! result: {r}");
                    goto Exit;
                }

                Console.WriteLine($"Publisher put data {payloadStr}");
            }

            publisher.Undeclare();

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