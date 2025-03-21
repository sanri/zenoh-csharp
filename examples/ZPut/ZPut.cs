using System;
using CommandLine;
using Zenoh;

namespace ZPut;

public class Program
{
    static void Main(string[] args)
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

        Keyexpr? keyexpr;
        ZBytes payload;
        PutOptions options;
        string printString;

        // ----------------------------------------------------------------------
        string keyStr = "demo/example/zenoh-cs-put/string";
        string dataStr = "Put from csharp !";
        keyexpr = Keyexpr.FromString(keyStr);
        if (keyexpr is null) goto Exit;
        payload = ZBytes.FromString(dataStr);
        options = new PutOptions(EncodingId.TextPlain);
        options.SetAttachment(ZBytes.FromString("abcd"));

        r = session.Put(keyexpr, payload, options);

        printString = r != Result.Ok
            ? $"Putting data failed!\n result: {r}, key: {keyStr}\n"
            : $"Putting data succeeded!\n key: {keyStr}, payload: {dataStr}\n";
        Console.WriteLine(printString);

        // ----------------------------------------------------------------------
        string keyJson = "demo/example/zenoh-cs-put/json";
        string dataJson = "{\"value\": \"Put from csharp\"}";
        keyexpr = Keyexpr.FromString(keyJson);
        if (keyexpr is null) goto Exit;
        payload = ZBytes.FromString(dataJson);
        options = new PutOptions(EncodingId.TextJson);
        options.SetAttachment(ZBytes.FromString("1234"));

        r = session.Put(keyexpr, payload, options);

        printString = r != Result.Ok
            ? $"Putting data failed!\n result: {r}, key: {keyJson}\n"
            : $"Putting data succeeded!\n key: {keyJson}, payload: {dataJson}\n";
        Console.WriteLine(printString);

        // ----------------------------------------------------------------------
        string keyBin = "demo/example/zenoh-cs-put/bin";
        byte[] dataBin = [0x12, 0x13, 0xa1, 0xb2];
        keyexpr = Keyexpr.FromString(keyBin);
        if (keyexpr is null) goto Exit;
        payload = ZBytes.FromBytes(dataBin);
        options = new PutOptions(EncodingId.ZenohBytes);
        options.SetAttachment(ZBytes.FromString("bin"));

        r = session.Put(keyexpr, payload, options);

        printString = r != Result.Ok
            ? $"Putting data failed!\n result: {r}, key: {keyBin}\n"
            : $"Putting data succeeded!\n key: {keyBin}, payload(Hex): {Convert.ToHexString(dataBin)}\n";
        Console.WriteLine(printString);

        // ----------------------------------------------------------------------
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