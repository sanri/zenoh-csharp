using CommandLine;
using Zenoh;
using Queryable = Zenoh.Queryable;

namespace ZQueryable;

public class Program
{
    private const string KeyStr1 = "demo/example/zenoh-cs-queryable/ok";
    private const string KeyStr2 = "demo/example/zenoh-cs-queryable/err";
    private const string KeyStr3 = "demo/example/zenoh-cs-queryable/del";

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

        var keyexpr1 = Keyexpr.FromString(KeyStr1);
        if (keyexpr1 is null) goto Exit;

        var keyexpr2 = Keyexpr.FromString(KeyStr2);
        if (keyexpr2 is null) goto Exit;

        var keyexpr3 = Keyexpr.FromString(KeyStr3);
        if (keyexpr3 is null) goto Exit;

        var options = new QueryableOptions { Complete = true };

        r = session.DeclareQueryable(keyexpr1, options, Callback1, out Queryable? queryable1);
        if (queryable1 is null)
        {
            Console.WriteLine($"Declare queryable unsuccessful! result: {r}");
            goto Exit;
        }

        Console.WriteLine($"Declaring queryable on {keyexpr1}");

        r = session.DeclareQueryable(keyexpr2, options, Callback2, out Queryable? queryable2);
        if (queryable2 is null)
        {
            Console.WriteLine($"Declare queryable unsuccessful! result: {r}");
            goto Exit;
        }

        Console.WriteLine($"Declaring queryable on {keyexpr2}");

        r = session.DeclareQueryable(keyexpr3, options, Callback3, out Queryable? queryable3);
        if (queryable3 is null)
        {
            Console.WriteLine($"Declare queryable unsuccessful! result: {r}");
            goto Exit;
        }

        Console.WriteLine($"Declaring queryable on {keyexpr3}");


        Console.WriteLine("Input 'q' to quit.");
        while (true)
        {
            var input = Console.ReadLine();
            if (input == "q") break;
        }

        queryable1.Undeclare();
        queryable2.Undeclare();
        queryable3.Undeclare();

        Exit:
        session.Close();
        Console.WriteLine("exit");
    }

    static void Callback1(Query query)
    {
        var qKeyexpr = query.GetKeyexpr();
        var qKeyexprStr = qKeyexpr.ToString() ?? "";
        var qPayload = query.GetPayload();
        var qPayloadStr = qPayload?.ToZString()?.ToString() ?? "";
        var qEncoding = query.GetEncoding();
        var qEncodingStr = qEncoding?.ToString() ?? "";

        Console.WriteLine($"Received Query ({qKeyexprStr}, {qEncodingStr}, {qPayloadStr})");

        var keyexpr = Keyexpr.FromString(KeyStr1);
        if (keyexpr is null) return;

        var payloadStr = "Queryable from CSharp!";
        var payload = ZBytes.FromString(payloadStr);
        var options = new QueryReplyOptions();
        var encoding = new Encoding(EncodingId.TextPlain);
        options.SetEncoding(encoding);
        var encodingStr = encoding.ToZString()?.ToString() ?? "";

        var r = query.Reply(keyexpr, payload, options);
        if (r == Result.Ok)
        {
            Console.WriteLine($"Responding ({KeyStr1}, {encodingStr}, {payloadStr})");
        }
        else
        {
            Console.WriteLine($"Reply error: {r}");
        }
    }

    static void Callback2(Query query)
    {
        var qKeyexpr = query.GetKeyexpr();
        var qKeyexprStr = qKeyexpr.ToString() ?? "";
        var qPayload = query.GetPayload();
        var qPayloadStr = qPayload?.ToZString()?.ToString() ?? "";
        var qEncoding = query.GetEncoding();
        var qEncodingStr = qEncoding?.ToString() ?? "";

        Console.WriteLine($"Received Query ({qKeyexprStr}, {qEncodingStr}, {qPayloadStr})");

        var keyexpr = Keyexpr.FromString(KeyStr2);
        if (keyexpr is null) return;

        var payloadStr = "Queryable from CSharp!";
        var payload = ZBytes.FromString(payloadStr);
        var options = new QueryReplyErrOptions();
        var encoding = new Encoding(EncodingId.TextPlain);
        options.SetEncoding(encoding);
        var encodingStr = encoding.ToZString()?.ToString() ?? "";

        var r = query.ReplyErr(payload, options);
        if (r == Result.Ok)
        {
            Console.WriteLine($"Responding err ({KeyStr2}, {encodingStr}, {payloadStr})");
        }
        else
        {
            Console.WriteLine($"Reply error: {r}");
        }
    }

    static void Callback3(Query query)
    {
        var qKeyexpr = query.GetKeyexpr();
        var qKeyexprStr = qKeyexpr.ToString() ?? "";
        var qPayload = query.GetPayload();
        var qPayloadStr = qPayload?.ToZString()?.ToString() ?? "";
        var qEncoding = query.GetEncoding();
        var qEncodingStr = qEncoding?.ToString() ?? "";

        Console.WriteLine($"Received Query ({qKeyexprStr}, {qEncodingStr}, {qPayloadStr})");

        var keyexpr = Keyexpr.FromString(KeyStr3);
        if (keyexpr is null) return;

        var options = new QueryReplyDelOptions();

        var r = query.ReplyDel(keyexpr, options);
        if (r == Result.Ok)
        {
            Console.WriteLine($"Responding del ({KeyStr3})");
        }
        else
        {
            Console.WriteLine($"Reply error: {r}");
        }
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