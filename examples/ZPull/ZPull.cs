using System;
using CommandLine;
using Zenoh;

namespace ZPull
{
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

            string keyexprStr = "demo/example/**";
            var keyexpr = Keyexpr.FromString(keyexprStr);
            if (keyexpr is null) goto Exit;

            r = session.DeclareSubscriber(keyexpr, ChannelType.Ring, 100, out var handle);
            if (handle is null)
            {
                Console.WriteLine($"Declare subscriber unsuccessful! result: {r}");
                goto Exit;
            }

            var subscriber = handle.Value.Item1;
            var channel = handle.Value.Item2;

            Console.WriteLine("Press <enter> to pull data.");
            Console.WriteLine("Input 'q' to quit.");
            while (true)
            {
                string input = Console.ReadLine() ?? "";
                if (input == "q") break;

                r = channel.TryRecv(out Sample? sample);
                if (sample is null)
                {
                    if (r == Result.ChannelNodata)
                    {
                        Console.WriteLine("All data of the channel has been read");
                        continue;
                    }
                    else
                    {
                        Console.WriteLine($"Channel recv a null sample! result: {r}");
                        break;
                    }
                }

                PrintSample(sample);
            }

            subscriber.Undeclare();

            Exit:
            session.Close();
            Console.WriteLine("exit");
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
                    payloadStr = zString.ToString();
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

            Console.Write(print);
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