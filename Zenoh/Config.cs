using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Zenoh;

public sealed class Config : IDisposable
{
    // z_owned_config*
    internal nint Handle { get; private set; }

    private Config()
    {
        throw new InvalidOperationException();
    }

    private Config(nint config)
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
                r = ZenohC.zc_config_from_file(pOwnedConfig, (nint)pPath);
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
                r = ZenohC.zc_config_from_str(pOwnedConfig, (nint)pS);
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
        if (Handle == nint.Zero) return;

        ZenohC.z_config_drop(Handle);
        Marshal.FreeHGlobal(Handle);
        Handle = nint.Zero;
    }

    public void CheckDisposed()
    {
        if (Handle == nint.Zero)
        {
            throw new InvalidOperationException();
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