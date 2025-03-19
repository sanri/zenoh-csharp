using CommandLine;
using Zenoh;
using Queryable = Zenoh.Queryable;

namespace ZQueryable;

public class Program
{
    private const string KeyStr = "demo/example/zenoh-cs-queryable/hello";

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

        var options = new QueryableOptions { Complete = true };

        r = session.DeclareQueryable(keyexpr, options, Callback, out Queryable? queryable);
        if (queryable is null)
        {
            Console.WriteLine($"Declare queryable unsuccessful! result: {r}");
            goto Exit;
        }

        Console.WriteLine($"Declaring queryable on {keyexpr}");

        Console.WriteLine("Input 'q' to quit.");
        while (true)
        {
            var input = Console.ReadLine();
            if (input == "q") break;
        }

        queryable.Undeclare();

        Exit:
        session.Close();
        Console.WriteLine("exit");
    }

    static void Callback(Query query)
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
