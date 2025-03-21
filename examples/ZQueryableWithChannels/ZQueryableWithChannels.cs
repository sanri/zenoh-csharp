using System;
using CommandLine;
using Zenoh;

namespace ZQueryableWithChannels;

public class Program
{
    const string KeyStr = "demo/example/zenoh-cs-queryable/ok";

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

        var keyexpr = Keyexpr.FromString(KeyStr);
        if (keyexpr is null) goto Exit;

        r = session.DeclareQueryable(keyexpr, new QueryableOptions(), ChannelType.Fifo, 100, out var handler);
        if (handler is null)
        {
            Console.WriteLine($"Declare queryable unsuccessful! result: {r}");
            goto Exit;
        }

        Console.WriteLine($"Declaring queryable on {KeyStr}");

        var queryable = handler.Value.Item1;
        var channel = handler.Value.Item2;

        Console.WriteLine("Press Ctrl+C to quit...");
        while (true)
        {
            r = channel.Recv(out Query? query);
            if (query is null)
            {
                Console.WriteLine(r is Result.ChannelDisconnected
                    ? "All data of the channel has been read"
                    : $"Channel recv a null reply! result: {r}");
                break;
            }

            PrintQuery(query);

            var payloadStr = "Queryable from CSharp!";
            var payload = ZBytes.FromString(payloadStr);
            var options = new QueryReplyOptions();
            var encoding = new Encoding(EncodingId.TextPlain);
            options.SetEncoding(encoding);
            var encodingStr = encoding.ToZString()?.ToString() ?? "";

            r = query.Reply(keyexpr, payload, options);
            Console.WriteLine(r == Result.Ok
                ? $"Responding ({KeyStr}, {encodingStr}, {payloadStr})"
                : $"Reply error: {r}");

            query.Dispose();
        }

        queryable.Undeclare();

        Exit:
        session.Close();
        Console.WriteLine("exit");
    }

    static void PrintQuery(Query query)
    {
        var qKeyexpr = query.GetKeyexpr();
        var qKeyexprStr = qKeyexpr.ToString() ?? "";
        var qPayload = query.GetPayload();
        var qPayloadStr = qPayload?.ToZString()?.ToString() ?? "";
        var qEncoding = query.GetEncoding();
        var qEncodingStr = qEncoding?.ToString() ?? "";

        Console.WriteLine($"Received Query ({qKeyexprStr}, {qEncodingStr}, {qPayloadStr})");
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