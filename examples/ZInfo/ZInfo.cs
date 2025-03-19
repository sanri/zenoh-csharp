#nullable enable

using System;
using System.Collections.Generic;
using System.Threading;
using CommandLine;
using Zenoh;

namespace ZInfo;

class Program
{
    static void Main(string[] args)
    {
        // var r = Parser.Default.ParseArguments<ClArgs>(args);
        // bool ok = true;
        // r.WithNotParsed(e => { ok = false; });
        // if (!ok) return;
        //
        // ClArgs clArgs = r.Value;
        // Config? config = clArgs.ToConfig();
        // if (config is null)
        //     return;
        //
        // Console.WriteLine("Opening session...");
        // var session = Session.Open(config);
        // if (session is null)
        // {
        //     Console.WriteLine("Opening session fault!");
        //     return;
        // }
        //
        // Thread.Sleep(200);
        // Console.WriteLine("Opening session successful!");
        //
        // string localId = session.LocalId().ToStr();
        // Id[] routersId = session.RoutersId();
        // List<string> routersIdStr = new List<string>();
        // foreach (var id in routersId)
        // {
        //     routersIdStr.Add(id.ToStr());
        // }
        //
        // Id[] peerId = session.PeersId();
        // List<string> peerIdStr = new List<string>();
        // foreach (var id in peerId)
        // {
        //     peerIdStr.Add(id.ToStr());
        // }
        //
        // Console.WriteLine($"Local ID: {localId}");
        // Console.WriteLine($"PeersPID: {String.Join(',', peerIdStr)}");
        // Console.WriteLine($"Routers ID: {String.Join(',', routersIdStr)}");
        //
        // session.Close();
    }
}

// class ClArgs
// {
//     [Option('c', "config", Required = false, HelpText = "A configuration file.")]
//     public string? ConfigFilePath { get; set; } = null;
//
//     [Option('e', "connect", Required = false, HelpText = "Endpoints to connect to. example: tcp/127.0.0.1:7447")]
//     public IEnumerable<string> Connects { get; set; } = new List<string>();
//
//     [Option('l', "listen", Required = false, HelpText = "Endpoints to listen on. example: tcp/127.0.0.1:8447")]
//     public IEnumerable<string> Listens { get; set; } = new List<string>();
//
//     [Option('m', "mode", Required = false,
//         HelpText = "The zenoh session mode (peer by default) [possible values: peer, client]")]
//     public string Mode { get; set; } = "peer";
//
//     internal Config? ToConfig()
//     {
//         if (ConfigFilePath != null)
//         {
//             Config? c = Config.LoadFromFile(ConfigFilePath);
//             if (c is null)
//             {
//                 Console.WriteLine("load config file error!");
//                 return null;
//             }
//
//             return c;
//         }
//
//         Config config = new Config();
//
//         config.SetMode(Mode == "client" ? Config.Mode.Client : Config.Mode.Peer);
//
//         List<string> connects = new List<string>();
//         foreach (string s in Connects)
//         {
//             connects.Add(s);
//         }
//
//         config.SetConnect(connects.ToArray());
//
//         List<string> listens = new List<string>();
//         foreach (string s in Listens)
//         {
//             listens.Add(s);
//         }
//
//         config.SetListen(listens.ToArray());
//
//         return config;
//     }
// }