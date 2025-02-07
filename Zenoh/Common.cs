using System;
using System.Runtime.InteropServices;

namespace Zenoh;

public class ZString : IDisposable
{
    // ZOwnedString
    internal nint Handle { get; private set; }
    private bool _disposed;


    public ZString()
    {
        var pOwnedString = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedString>());
        ZenohC.z_string_empty(pOwnedString);
        Handle = pOwnedString;
        _disposed = false;
    }

    public ZString(ZString zString)
    {
        var pOwnedString = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedString>());
        var pLoanedString = ZenohC.z_string_loan(zString.Handle);
        ZenohC.z_string_clone(pOwnedString, pLoanedString);
        Handle = pOwnedString;
        _disposed = false;
    }

    public ZString(string str)
    {
        var pOwnedString = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedString>());
        var pStr = Marshal.StringToHGlobalAnsi(str);
        var r = ZenohC.z_string_copy_from_str(pOwnedString, pStr);
        if (r != ZResult.Ok) ZenohC.z_string_empty(pOwnedString);
        Marshal.FreeHGlobal(pStr);
        Handle = pOwnedString;
        _disposed = false;
    }

    public bool IsEmpty()
    {
        var pLoanedString = ZenohC.z_string_loan(Handle);
        return ZenohC.z_string_is_empty(pLoanedString);
    }

    public nuint Length()
    {
        var pLoanedString = ZenohC.z_string_loan(Handle);
        return ZenohC.z_string_len(pLoanedString);
    }

    public override string ToString()
    {
        var pLoanedString = ZenohC.z_string_loan(Handle);
        var pS = ZenohC.z_string_data(pLoanedString);
        var s = Marshal.PtrToStringAnsi(pS);
        return s ?? string.Empty;
    }

    void IDisposable.Dispose()
    {
        if(_disposed) return;
        
        ZenohC.z_string_drop(Handle);
        Marshal.FreeHGlobal(Handle);
        Handle = nint.Zero;
        _disposed = true;
    }
}

public class ZBytes : IDisposable
{
    // ZOwnedBytes
    internal nint Handle { get; private set; }
    private bool _disposed;

    public ZBytes()
    {
        var pOwnedBytes = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedBytes>());
        ZenohC.z_bytes_empty(pOwnedBytes);
        Handle = pOwnedBytes;
        _disposed = false;
    }
    
    
    void IDisposable.Dispose()
    {
        if(_disposed) return;
        ZenohC.z_bytes_drop(Handle);
        Marshal.FreeHGlobal(Handle);
        Handle = nint.Zero;
        _disposed = true;
    }
}

public class ZSlice : IDisposable
{
    // ZOwnedSlice
    internal nint Handle { get; private set; }
    private bool _disposed;
    
    void IDisposable.Dispose()
    {
        
    }
}