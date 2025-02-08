using System;
using System.Runtime.InteropServices;

namespace Zenoh;

public class Config : IDisposable
{
    // z_owned_config*
    internal nint HandleZOwnedConfig { get; private set; }

    private Config()
    {
    }

    private Config(nint config)
    {
        HandleZOwnedConfig = config;
    }

    public Config(Config source)
    {
        var pOwnedConfig = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedConfig>());
        ZenohC.z_internal_config_null(pOwnedConfig);

        var pLoanedConfig = ZenohC.z_config_loan(source.HandleZOwnedConfig);
        ZenohC.z_config_clone(pOwnedConfig, pLoanedConfig);
        HandleZOwnedConfig = pOwnedConfig;
    }

    public static Config? Default()
    {
        var pOwnedConfig = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedConfig>());
        ZenohC.z_internal_config_null(pOwnedConfig);

        var r = ZenohC.z_config_default(pOwnedConfig);
        if (r == ZResult.Ok) return new Config(pOwnedConfig);
        Marshal.FreeHGlobal(pOwnedConfig);
        return null;
    }

    public static Config? FromEnv()
    {
        var pOwnedConfig = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedConfig>());
        ZenohC.z_internal_config_null(pOwnedConfig);

        var r = ZenohC.zc_config_from_env(pOwnedConfig);
        if (r == ZResult.Ok) return new Config(pOwnedConfig);
        Marshal.FreeHGlobal(pOwnedConfig);
        return null;
    }

    public static Config? FromFile(string path)
    {
        var pOwnedConfig = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedConfig>());
        ZenohC.z_internal_config_null(pOwnedConfig);

        var pPath = Marshal.StringToHGlobalAnsi(path);
        var r = ZenohC.zc_config_from_file(pOwnedConfig, pPath);
        Marshal.FreeHGlobal(pPath);
        if (r == ZResult.Ok) return new Config(pOwnedConfig);
        Marshal.FreeHGlobal(pOwnedConfig);
        return null;
    }

    public static Config? FromStr(string s)
    {
        var pOwnedConfig = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedConfig>());
        ZenohC.z_internal_config_null(pOwnedConfig);

        var pS = Marshal.StringToHGlobalAnsi(s);
        var r = ZenohC.zc_config_from_str(pOwnedConfig, pS);
        Marshal.FreeHGlobal(pS);
        if (r == ZResult.Ok) return new Config(pOwnedConfig);
        Marshal.FreeHGlobal(pOwnedConfig);
        return null;
    }

    public void Dispose()
    {
        if (HandleZOwnedConfig == nint.Zero) return;

        ZenohC.z_config_drop(HandleZOwnedConfig);
        Marshal.FreeHGlobal(HandleZOwnedConfig);
        HandleZOwnedConfig = nint.Zero;
    }

    public ZString? ToZString()
    {
        var zString = new ZString();
        var pLoanedConfig = ZenohC.z_config_loan(HandleZOwnedConfig);
        var r = ZenohC.zc_config_to_string(pLoanedConfig, zString.HandleZOwnedString);
        return r == ZResult.Ok ? zString : null;
    }
}