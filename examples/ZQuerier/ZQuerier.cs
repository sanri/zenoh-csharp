using System;
using System.Threading;
using CommandLine;
using Zenoh;

namespace ZQuerier
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

            string keyStr = "demo/example/**";
            var keyexpr = Keyexpr.FromString(keyStr);
            if (keyexpr is null) goto Exit;

            var options = new QuerierOptions();

            Console.WriteLine($"Querying querier on {keyStr}");
            r = session.DeclareQuerier(keyexpr, options, out Querier? querier);
            if (querier is null)
            {
                Console.WriteLine($"Declare querier unsuccessful! result: {r}");
                goto Exit;
            }

            Console.WriteLine($"Declared querier on {keyStr}");

            for (int i = 0; i < 100; i++)
            {
                Thread.Sleep(1000);
                var getOptions = new QuerierGetOptions();
                Console.WriteLine($"Querying '{keyStr}'");
                r = querier.Get(getOptions, null, ChannelType.Fifo, 10, out ChannelReply? channel);
                if (channel is null)
                {
                    Console.WriteLine($"Querier get unsuccessful! result: {r}");
                    goto Exit;
                }

                while (true)
                {
                    r = channel.Recv(out Reply? reply);
                    if (reply is null) break;

                    if (reply.IsOk())
                    {
                        var sample = reply.AsOk();
                        if (sample is null) continue;

                        var k = sample.GetKeyexpr();
                        var e = sample.GetEncoding();
                        var p = sample.GetPayload();
                        var pStr = p.ToZString()?.ToString() ?? "";

                        var printStr = $">> Received ({k}, {e}, {pStr})";
                        Console.WriteLine(printStr);
                    }
                    else
                    {
                        var replyErr = reply.AsErr();
                        if (replyErr is null) continue;

                        var e = replyErr.GetEncoding();
                        var p = replyErr.GetPayload();
                        var pStr = p.ToZString()?.ToString() ?? "";

                        var printStr = $">> Received err ({e}, {pStr})";
                        Console.WriteLine(printStr);
                    }
                }
            }

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