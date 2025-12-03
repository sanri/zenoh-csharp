using System;
using System.Threading;
using CommandLine;
using Zenoh;

namespace ZPubShm
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

            var shmProvider = new ShmProvider(1024 * 1024 * 4);

            var keyStr = "demo/example/zenoh-cs-pub-shm/string";
            var keyexpr = Keyexpr.FromString(keyStr);
            if (keyexpr is null) goto Exit;

            var publisherOptions = new PublisherOptions();
            publisherOptions.Encoding = new Encoding(EncodingId.TextPlain);

            r = session.DeclarePublisher(keyexpr, publisherOptions, out var publisher);
            if (publisher is null)
            {
                Console.WriteLine($"Declare publisher unsuccessful! result: {r}");
                goto Exit;
            }

            Console.WriteLine($"Declared publisher on {keyexpr}");

            for (var i = 0; i < 100; i++)
            {
                Thread.Sleep(1000);
                var payloadStr = $"[{i}] Pub from CS!";
                var payloadBytes = System.Text.Encoding.UTF8.GetBytes(payloadStr);

                var status = shmProvider.AllocGcDefragBlocking((UIntPtr)payloadBytes.Length, out var shm);
                if (status != BufLayoutAllocStatus.Ok)
                {
                    Console.WriteLine("SHM Provider alloc failed");
                    break;
                }

                if (shm != null)
                {
                    shm.WriteData(payloadBytes);
                    var payload = shm.ToZBytes();
                    r = publisher.Put(payload, new PublisherPutOptions());
                    if (r != Result.Ok)
                    {
                        Console.WriteLine($"Publisher put unsuccessful! result: {r}");
                        goto Exit;
                    }

                    Console.WriteLine($"Publisher put data {payloadStr}");
                }
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