#nullable enable

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using CommandLine;
using Zenoh;
using Encoding = System.Text.Encoding;

namespace ZGet;

class Program
{
    static void Main(string[] args)
    {
        var r = Parser.Default.ParseArguments<ClArgs>(args);
        bool ok = true;
        r.WithNotParsed(e => { ok = false; });
        if (!ok) return;

        ClArgs clArgs = r.Value;
        Config? config = clArgs.ToConfig();
        if (config is null)
            return;

        Console.WriteLine("Opening session...");
        var session = Session.Open(config);
        if (session is null)
        {
            Console.WriteLine("Opening session fault!");
            return;
        }

        Thread.Sleep(200);
        Console.WriteLine("Opening session successful!");

        string keyexpr = clArgs.GetSelector();
        byte[] data = Encoding.UTF8.GetBytes(clArgs.GetValue());
        QueryOptions queryOptions = new QueryOptions(keyexpr, ZEncodingPrefix.TextPlain, data);

        Console.WriteLine($"Sending Query '{keyexpr}'");
        Querier? querier = session.Query(queryOptions);
        if (querier is null)
        {
            Console.WriteLine("Session query fault!");
            goto EXIT;
        }

        QuerierCallback callback = sample =>
        {
            string key = sample.GetKeyexpr();
            ZEncodingPrefix zEncodingPrefix = sample.GetEncodingPrefix();
            string? s = sample.GetString();
            if (s is null)
            {
                byte[] d = sample.GetPayload();
                Console.WriteLine($">> Received ('{key}' '{zEncodingPrefix}': '{d}')");
            }
            else
            {
                Console.WriteLine($">> Received ('{key}' '{zEncodingPrefix}': '{s}')");
            }
        };

        if (!querier.GetSamples(callback))
        {
            Console.WriteLine("querier get sample error!");
        }

        EXIT:
        session.Close();
    }
}

class ClArgs
{
    [Option('c', "config", Required = false, HelpText = "A configuration file.")]
    public string? ConfigFilePath { get; set; } = null;

    [Option('e', "connect", Required = false, HelpText = "Endpoints to connect to. example: tcp/127.0.0.1:7447")]
    public IEnumerable<string> Connects { get; set; } = new List<string>();

    [Option('l', "listen", Required = false, HelpText = "Endpoints to listen on. example: tcp/127.0.0.1:8447")]
    public IEnumerable<string> Listens { get; set; } = new List<string>();

    [Option('m', "mode", Required = false,
        HelpText = "The zenoh session mode (peer by default) [possible values: peer, client]")]
    public string Mode { get; set; } = "peer";

    [Option('s', "selector", Required = false,
        HelpText = "The selection of resources to query [default: demo/example/**]")]
    public string? Keyexpr { get; set; } = null;

    [Option('v', "value", Required = false,
        HelpText = "An optional value to put in the query.")]
    public string? Value { get; set; } = null;

    internal Config? ToConfig()
    {
        if (ConfigFilePath != null)
        {
            Config? c = Config.LoadFromFile(ConfigFilePath);
            if (c is null)
            {
                Console.WriteLine("load config file error!");
                return null;
            }

            return c;
        }

        Config config = new Config();

        config.SetMode(Mode == "client" ? Config.Mode.Client : Config.Mode.Peer);

        List<string> connects = new List<string>();
        foreach (string s in Connects)
        {
            connects.Add(s);
        }

        config.SetConnect(connects.ToArray());

        List<string> listens = new List<string>();
        foreach (string s in Listens)
        {
            listens.Add(s);
        }

        config.SetListen(listens.ToArray());

        return config;
    }

    public string GetSelector()
    {
        return Keyexpr ?? "demo/example/**";
    }

    public string GetValue()
    {
        return Value ?? "hello";
    }
}