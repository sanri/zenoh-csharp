#nullable enable

using System;
using System.Collections.Generic;
using System.Threading;
using CommandLine;
using Zenoh;

namespace ZPut;

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
            Console.WriteLine("Opening session unsuccessful!");
            return;
        }

        Thread.Sleep(200);
        Console.WriteLine("Opening session successful!");

        string keyStr = "demo/example/zenoh-cs-put/string";
        string dataStr = "Put from csharp !";
        Console.WriteLine(session.PutStr(keyStr, dataStr)
            ? $"Putting data string ('{keyStr}': '{dataStr}')"
            : "Putting data string fault!");

        string keyJson = "demo/example/zenoh-cs-put/json";
        string dataJson = "{\"value\": \"Put from csharp\"}";
        Console.WriteLine(session.PutJson(keyJson, dataJson)
            ? $"Putting data json ('{keyJson}': {dataJson})"
            : "Putting data json fault!");

        string keyInt = "demo/example/zenoh-cs-put/int";
        long dataInt = 965;
        Console.WriteLine(session.PutInt(keyInt, dataInt)
            ? $"Putting data int ('{keyInt}': {dataInt})"
            : "Putting data int fault!");

        string keyFloat = "demo/example/zenoh-cs-put/float";
        double dataFloat = 99.6;
        Console.WriteLine(session.PutFloat(keyFloat, dataFloat)
            ? $"Putting data float ('{keyFloat}': {dataFloat})"
            : "Putting data float fault!");

        string keyBin = "demo/example/zenoh-cs-put/bin";
        byte[] dataBin = { 0x1, 0x2, 0x3, 0x4 };
        Console.WriteLine(
            session.PutData(keyBin, dataBin, ZEncodingPrefix.AppCustom)
                ? $"Putting data bin ('{keyBin}': {dataBin.Length} Byte"
                : "Putting data bin fault!");

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
}