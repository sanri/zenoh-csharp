using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Zenoh
{
    // z_loaned_hello_t
    /// <summary>
    ///     A hello message received as a response to a scout.
    /// </summary>
    public sealed class Hello
    {
        /// <summary>
        ///     Callback invoked for each received hello.
        /// </summary>
        public delegate void Cb(Hello hello);

        internal static ZenohC.Cb2 CbCall = _Call;
        internal static ZenohC.Cb1 CbDrop = _Drop;

        private Hello(Id id, Whatami whatami, IReadOnlyList<string> locators)
        {
            Id = id;
            Whatami = whatami;
            Locators = locators;
        }

        /// <summary>
        ///     Zenoh ID of the node that sent the hello.
        /// </summary>
        public Id Id { get; }

        /// <summary>
        ///     Mode of the node that sent the hello.
        /// </summary>
        public Whatami Whatami { get; }

        /// <summary>
        ///     Locators advertised by the node, e.g. "tcp/192.168.1.10:7447".
        /// </summary>
        public IReadOnlyList<string> Locators { get; }

        internal static Hello FromLoaned(IntPtr pLoanedHello)
        {
            var id = new Id(ZenohC.z_hello_zid(pLoanedHello));
            var whatami = ZenohC.z_hello_whatami(pLoanedHello);

            var locators = new List<string>();
            var pOwnedStringArray = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedStringArray>());
            try
            {
                ZenohC.z_internal_string_array_null(pOwnedStringArray);
                ZenohC.z_hello_locators(pLoanedHello, pOwnedStringArray);

                var pLoanedStringArray = ZenohC.z_string_array_loan(pOwnedStringArray);
                var len = (ulong)ZenohC.z_string_array_len(pLoanedStringArray);
                for (ulong i = 0; i < len; i++)
                {
                    var pLoanedString = ZenohC.z_string_array_get(pLoanedStringArray, (UIntPtr)i);
                    locators.Add(ZString.CreateLoaned(pLoanedString).ToString());
                }

                ZenohC.z_string_array_drop(pOwnedStringArray);
            }
            finally
            {
                Marshal.FreeHGlobal(pOwnedStringArray);
            }

            return new Hello(id, whatami, locators);
        }

        private static void _Call(IntPtr hello, IntPtr context)
        {
            var gcHandle = GCHandle.FromIntPtr(context);
            if (!(gcHandle.Target is Cb callback)) return;

            callback(FromLoaned(hello));
        }

        private static void _Drop(IntPtr context)
        {
            var gcHandle = GCHandle.FromIntPtr(context);
            gcHandle.Free();
        }
    }

    /// <summary>
    ///     Scouting options.
    /// </summary>
    public sealed class ScoutOptions
    {
        /// <summary>
        ///     How long to wait for hello replies, in milliseconds. Default is 1000.
        /// </summary>
        public ulong TimeoutMs { get; set; } = 1000;

        /// <summary>
        ///     Which node kinds should reply. Default is routers and peers.
        /// </summary>
        public What What { get; set; } = What.RouterPeer;
    }

    /// <summary>
    ///     Discovery of Zenoh routers and peers (z_scout).
    /// </summary>
    public static class Scout
    {
        /// <summary>
        /// <para>
        /// Scouts for routers and/or peers using the scouting settings of <paramref name="config"/>
        /// (e.g. "scouting/multicast/address", "scouting/multicast/interface").
        /// </para>
        /// <para>
        /// Blocks until the timeout elapses. <paramref name="callback"/> is called once per received hello,
        /// on the calling thread, and is guaranteed to be dropped before this function returns.
        /// The same node may reply more than once (e.g. once per network interface).
        /// </para>
        /// </summary>
        /// <param name="config">Zenoh config. It is copied, the caller keeps ownership.</param>
        /// <param name="callback">Called for every hello received.</param>
        /// <param name="options">Scouting options, or null for defaults.</param>
        /// <returns>
        /// Result.Ok on success, others values on failure.
        /// </returns>
        public static Result Run(Config config, Hello.Cb callback, ScoutOptions? options = null)
        {
            config.CheckDisposed();

            var configCopy = new Config(config);
            var gcHandle = GCHandle.Alloc(callback);
            var pOwnedClosureHello = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedClosureHello>());
            var pOptions = Marshal.AllocHGlobal(Marshal.SizeOf<ZScoutOptions>());
            try
            {
                ZenohC.z_closure_hello(pOwnedClosureHello, Hello.CbCall, Hello.CbDrop, GCHandle.ToIntPtr(gcHandle));

                ZenohC.z_scout_options_default(pOptions);
                if (options != null)
                {
                    var zOptions = Marshal.PtrToStructure<ZScoutOptions>(pOptions);
                    zOptions.timeout_ms = options.TimeoutMs;
                    zOptions.What = options.What;
                    Marshal.StructureToPtr(zOptions, pOptions, false);
                }

                // z_scout consumes both the config and the closure (the closure drop frees gcHandle).
                return ZenohC.z_scout(configCopy.Handle, pOwnedClosureHello, pOptions);
            }
            finally
            {
                configCopy.Dispose();
                Marshal.FreeHGlobal(pOptions);
                Marshal.FreeHGlobal(pOwnedClosureHello);
            }
        }

        /// <summary>
        ///     Scouts and returns all hellos received before the timeout.
        /// </summary>
        public static Result Run(Config config, out List<Hello> hellos, ScoutOptions? options = null)
        {
            var list = new List<Hello>();
            var r = Run(config, h => list.Add(h), options);
            hellos = list;
            return r;
        }
    }
}
