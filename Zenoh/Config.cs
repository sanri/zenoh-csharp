using System;
using System.Runtime.InteropServices;

namespace Zenoh;

public class Config : IDisposable
{
    // ZOwnedConfig
    internal nint Handle { get; private set; }
    private bool _disposed;

    private Config()
    {
    }

    private Config(nint config, bool disposed)
    {
        Handle = config;
        _disposed = disposed;
    }

    public Config(Config config)
    {
        var pLoanedConfig = ZenohC.z_config_loan(config.Handle);
        var pOwnedConfig = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedConfig>());
        ZenohC.z_config_clone(pOwnedConfig, pLoanedConfig);

        Handle = pOwnedConfig;
        _disposed = false;
    }

    public static Config? Default()
    {
        var p = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedConfig>());
        var r = ZenohC.z_config_default(p);
        if (r != ZResult.Ok)
        {
            Marshal.FreeHGlobal(p);
            return null;
        }

        return new Config(p, false);
    }

    public static Config? FromEnv()
    {
        var pOwnedConfig = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedConfig>());
        var r = ZenohC.zc_config_from_env(pOwnedConfig);
        if (r != ZResult.Ok)
        {
            Marshal.FreeHGlobal(pOwnedConfig);
            return null;
        }

        return new Config(pOwnedConfig, false);
    }

    public static Config? FromFile(string path)
    {
        var pOwnedConfig = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedConfig>());
        var pPath = Marshal.StringToHGlobalAnsi(path);
        var r = ZenohC.zc_config_from_file(pOwnedConfig, pPath);
        if (r != ZResult.Ok)
        {
            Marshal.FreeHGlobal(pOwnedConfig);
            Marshal.FreeHGlobal(pPath);
            return null;
        }

        Marshal.FreeHGlobal(pPath);
        return new Config(pOwnedConfig, false);
    }

    public static Config? FromStr(string s)
    {
        var pOwnedConfig = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedConfig>());
        var pS = Marshal.StringToHGlobalAnsi(s);
        var r = ZenohC.zc_config_from_str(pOwnedConfig, pS);
        if (r != ZResult.Ok)
        {
            Marshal.FreeHGlobal(pOwnedConfig);
            Marshal.FreeHGlobal(pS);
            return null;
        }

        Marshal.FreeHGlobal(pS);
        return new Config(pOwnedConfig, false);
    }

    void IDisposable.Dispose()
    {
        if (_disposed) return;

        ZenohC.z_config_drop(Handle);
        Marshal.FreeHGlobal(Handle);
        Handle = nint.Zero;
        _disposed = true;
    }

    public ZString? ToZString()
    {
        var zString = new ZString();
        var pLoanedConfig = ZenohC.z_config_loan(Handle);
        var r = ZenohC.zc_config_to_string(pLoanedConfig, zString.Handle);
        return r != ZResult.Ok ? null : zString;
    }
}