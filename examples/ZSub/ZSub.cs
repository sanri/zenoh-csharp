using System;
using CommandLine;
using Zenoh;

namespace ZSub
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

            string keyexprStr = "demo/example/**";
            var keyexpr = Keyexpr.FromString(keyexprStr);
            if (keyexpr is null) goto Exit;

            r = session.DeclareSubscriber(keyexpr, new SubscriberOptions(), Callback, out Subscriber? subscriber);
            if (subscriber is null)
            {
                Console.WriteLine($"Declare subscriber unsuccessful! result: {r}");
                goto Exit;
            }

            Console.WriteLine($"Declaring subscriber on {keyexpr}");

            Console.WriteLine("Input 'q' to quit.");
            while (true)
            {
                var input = Console.ReadLine();
                if (input == "q") break;
            }

            subscriber.Undeclare();

            Exit:
            session.Close();
            Console.WriteLine("exit");
        }

        static void Callback(Sample sample)
        {
            PrintSample(sample);
        }

        static void PrintSample(Sample sample)
        {
            var keyexpr = sample.GetKeyexpr();
            var keyexprStr = keyexpr.ToString();
            var encoding = sample.GetEncoding();
            var encodingStr = encoding.ToString();
            var payload = sample.GetPayload();
            var kind = sample.GetKind();

            string payloadStr;
            switch (encoding.GetEncodingId())
            {
                case EncodingId.ZenohString:
                case EncodingId.TextPlain:
                case EncodingId.ApplicationJson:
                case EncodingId.TextJson:
                case EncodingId.TextJson5:
                    var zString = payload.ToZString() ?? new ZString();
                    payloadStr = zString.ToString() ?? "";
                    break;
                default:
                    payloadStr = "";
                    break;
            }

            string print = kind switch
            {
                SampleKind.Put =>
                    $">> [Subscriber] Received PUT key: {keyexprStr}, encoding: {encodingStr}, length: {payload.Length()}, payload:\n   {payloadStr}",
                SampleKind.Delete => $">> [Subscriber] Received DELETE key: {keyexprStr}",
                _ => ""
            };

            Console.WriteLine(print);
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