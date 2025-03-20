using System;
using System.Threading;
using CommandLine;
using Zenoh;

namespace ZInfo;

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

        var id = session.GetId();
        Console.WriteLine($"Session ID: {id.ToHexStr()}");

        r = session.RoutersId(PrintRoutersId);
        if (r != Result.Ok)
        {
            Console.WriteLine("Getting routers ID failed!");
            goto Exit;
        }

        r = session.PeersId(PrintPeersId);
        if (r != Result.Ok)
        {
            Console.WriteLine("Getting peers ID failed!");
            goto Exit;
        }

        Thread.Sleep(500);

        Exit:
        session.Close();
        Console.WriteLine("exit");
    }

    static void PrintRoutersId(Id id)
    {
        Console.WriteLine($"Router ID: {id.ToHexStr()}");
    }

    static void PrintPeersId(Id id)
    {
        Console.WriteLine($"Peer ID: {id.ToHexStr()}");
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