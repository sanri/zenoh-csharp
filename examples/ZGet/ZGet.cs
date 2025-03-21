using System;
using CommandLine;
using Zenoh;

namespace ZGet;

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

        var getOptions = new GetOptions();

        Console.WriteLine($"Querying keyexpr: {keyStr}");
        r = session.Get(keyexpr, getOptions, null, ChannelType.Fifo, 10, out ChannelReply? channel);
        if (channel is null)
        {
            Console.WriteLine($"Query unsuccessful! result: {r}");
            goto Exit;
        }

        while (true)
        {
            r = channel.Recv(out Reply? reply);
            if (reply is null)
            {
                if (r is Result.ChannelDisconnected)
                {
                    Console.WriteLine("All data of the channel has been read");
                    goto Exit;
                }
                else
                {
                    Console.WriteLine($"Channel recv a null reply! result: {r}");
                    continue;
                }
            }

            if (reply.IsOk())
            {
                var sample = reply.AsOk();
                if (sample is null) continue;

                var k = sample.GetKeyexpr();
                var kStr = k.ToString() ?? "";
                var e = sample.GetEncoding();
                var eStr = e.ToString() ?? "";
                var p = sample.GetPayload();
                var pStr = p.ToZString()?.ToString() ?? "";

                var printStr = $">> Received ({kStr}, {eStr}, {pStr})";
                Console.WriteLine(printStr);
            }
            else
            {
                var replyErr = reply.AsErr();
                if (replyErr is null) continue;

                var e = replyErr.GetEncoding();
                var eStr = e.ToString() ?? "";
                var p = replyErr.GetPayload();
                var pStr = p.ToZString()?.ToString() ?? "";

                var printStr = $">> Received err ({eStr}, {pStr})";
                Console.WriteLine(printStr);
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

        if (config is not null) return config;
        Console.WriteLine("load config file error!");
        return null;
    }
}