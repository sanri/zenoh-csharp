using System;
using System.Runtime.InteropServices;

namespace Zenoh
{
    /// <summary>
    /// Zenoh session config.
    /// </summary>
    public sealed class Config : IDisposable
    {
        // z_owned_config*
        internal IntPtr Handle { get; private set; }

        private Config()
        {
            throw new InvalidOperationException();
        }

        private Config(IntPtr config)
        {
            Handle = config;
        }

        public Config(Config other)
        {
            other.CheckDisposed();

            var pOwnedConfig = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedConfig>());
            ZenohC.z_internal_config_null(pOwnedConfig);

            var pLoanedConfig = ZenohC.z_config_loan(other.Handle);
            ZenohC.z_config_clone(pOwnedConfig, pLoanedConfig);
            Handle = pOwnedConfig;
        }

        public static Config? Default()
        {
            var pOwnedConfig = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedConfig>());
            ZenohC.z_internal_config_null(pOwnedConfig);

            var r = ZenohC.z_config_default(pOwnedConfig);
            if (r == Result.Ok) return new Config(pOwnedConfig);
            Marshal.FreeHGlobal(pOwnedConfig);
            return null;
        }

        public static Config? FromEnv()
        {
            var pOwnedConfig = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedConfig>());
            ZenohC.z_internal_config_null(pOwnedConfig);

            var r = ZenohC.zc_config_from_env(pOwnedConfig);
            if (r == Result.Ok) return new Config(pOwnedConfig);
            Marshal.FreeHGlobal(pOwnedConfig);
            return null;
        }

        public static Config? FromFile(string path)
        {
            var pOwnedConfig = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedConfig>());
            ZenohC.z_internal_config_null(pOwnedConfig);

            var utf8BytesPath = System.Text.Encoding.UTF8.GetBytes(path + "\0");
            Result r;
            unsafe
            {
                fixed (byte* pPath = utf8BytesPath)
                {
                    r = ZenohC.zc_config_from_file(pOwnedConfig, (IntPtr)pPath);
                }
            }

            if (r == Result.Ok) return new Config(pOwnedConfig);

            Marshal.FreeHGlobal(pOwnedConfig);
            return null;
        }

        public static Config? FromStr(string s)
        {
            var pOwnedConfig = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedConfig>());
            ZenohC.z_internal_config_null(pOwnedConfig);

            var utf8BytesS = System.Text.Encoding.UTF8.GetBytes(s + "\0");
            Result r;
            unsafe
            {
                fixed (byte* pS = utf8BytesS)
                {
                    r = ZenohC.zc_config_from_str(pOwnedConfig, (IntPtr)pS);
                }
            }

            if (r == Result.Ok) return new Config(pOwnedConfig);
            Marshal.FreeHGlobal(pOwnedConfig);
            return null;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~Config() => Dispose(false);

        private void Dispose(bool disposing)
        {
            if (Handle == IntPtr.Zero) return;

            ZenohC.z_config_drop(Handle);
            Marshal.FreeHGlobal(Handle);
            Handle = IntPtr.Zero;
        }

        public void CheckDisposed()
        {
            if (Handle == IntPtr.Zero)
            {
                throw new ObjectDisposedException(nameof(Config));
            }
        }

        public ZString? ToZString()
        {
            CheckDisposed();

            var zString = new ZString();
            var pLoanedConfig = ZenohC.z_config_loan(Handle);
            var r = ZenohC.zc_config_to_string(pLoanedConfig, zString.Handle);
            return r == Result.Ok ? zString : null;
        }
    }
}