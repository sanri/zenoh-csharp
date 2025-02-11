using System;
using System.Runtime.InteropServices;

namespace Zenoh;

public sealed class ZString : IDisposable
{
    // z_owned_string*
    internal nint HandleZOwnedString { get; private set; }

    public ZString()
    {
        var pOwnedString = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedString>());
        ZenohC.z_string_empty(pOwnedString);
        HandleZOwnedString = pOwnedString;
    }

    public ZString(ZString other)
    {
        var pOwnedString = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedString>());
        var pLoanedString = ZenohC.z_string_loan(other.HandleZOwnedString);
        ZenohC.z_string_clone(pOwnedString, pLoanedString);
        HandleZOwnedString = pOwnedString;
    }

    public ZString(string str)
    {
        var pOwnedString = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedString>());
        var pStr = Marshal.StringToHGlobalAnsi(str);
        var r = ZenohC.z_string_copy_from_str(pOwnedString, pStr);
        if (r != ZResult.Ok) ZenohC.z_string_empty(pOwnedString);
        Marshal.FreeHGlobal(pStr);
        HandleZOwnedString = pOwnedString;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~ZString() => Dispose(false);

    private void Dispose(bool disposing)
    {
        if (HandleZOwnedString == nint.Zero) return;

        ZenohC.z_string_drop(HandleZOwnedString);
        Marshal.FreeHGlobal(HandleZOwnedString);
        HandleZOwnedString = nint.Zero;
    }

    public bool IsEmpty()
    {
        var pLoanedString = ZenohC.z_string_loan(HandleZOwnedString);
        return ZenohC.z_string_is_empty(pLoanedString);
    }

    public nuint Length()
    {
        var pLoanedString = ZenohC.z_string_loan(HandleZOwnedString);
        return ZenohC.z_string_len(pLoanedString);
    }

    public override string? ToString()
    {
        var pLoanedString = ZenohC.z_string_loan(HandleZOwnedString);
        var pS = ZenohC.z_string_data(pLoanedString);
        var s = Marshal.PtrToStringAnsi(pS);
        return s;
    }
}

internal sealed class ViewString : IDisposable
{
    internal nint HandleViewString { get; private set; }

    public ViewString()
    {
        var pViewString = Marshal.AllocHGlobal(Marshal.SizeOf<ZViewString>());
        ZenohC.z_view_string_empty(pViewString);
        HandleViewString = pViewString;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~ViewString() => Dispose(false);

    private void Dispose(bool disposing)
    {
        if (HandleViewString == nint.Zero) return;

        Marshal.FreeHGlobal(HandleViewString);
        HandleViewString = nint.Zero;
    }

    public override string? ToString()
    {
        var pLoanedString = ZenohC.z_view_string_loan(HandleViewString);
        var pS = ZenohC.z_string_data(pLoanedString);
        return Marshal.PtrToStringAnsi(pS);
    }
}

public sealed class ZBytes : IDisposable
{
    // z_owned_bytes_t*
    internal nint HandleZOwnedBytes { get; private set; }

    public ZBytes()
    {
        var pOwnedBytes = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedBytes>());
        ZenohC.z_bytes_empty(pOwnedBytes);
        HandleZOwnedBytes = pOwnedBytes;
    }
    
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    
    ~ZBytes() => Dispose(false);
    
    private void Dispose(bool disposing)
    {
        if (HandleZOwnedBytes == nint.Zero) return;

        ZenohC.z_bytes_drop(HandleZOwnedBytes);
        Marshal.FreeHGlobal(HandleZOwnedBytes);
        HandleZOwnedBytes = nint.Zero;
    }

    public bool IsEmpty()
    {
        var pLoanedBytes = ZenohC.z_bytes_loan(HandleZOwnedBytes);
        return ZenohC.z_bytes_is_empty(pLoanedBytes);
    }

    public nuint Length()
    {
        var pLoanedBytes = ZenohC.z_bytes_loan(HandleZOwnedBytes);
        return ZenohC.z_bytes_len(pLoanedBytes);
    }

}

public sealed class ZSlice : IDisposable
{
    // z_owned_slice_t*
    internal nint HandleZOwnedSlice { get; private set; }

    public ZSlice()
    {
        var pOwnedSlice = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedSlice>());
        ZenohC.z_slice_empty(pOwnedSlice);
        HandleZOwnedSlice = pOwnedSlice;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~ZSlice() => Dispose(false);

    private void Dispose(bool disposing)
    {
        if (HandleZOwnedSlice == nint.Zero) return;

        ZenohC.z_slice_drop(HandleZOwnedSlice);
        Marshal.FreeHGlobal(HandleZOwnedSlice);
        HandleZOwnedSlice = nint.Zero;
    }

    public bool IsEmpty()
    {
        var pLoanedSlice = ZenohC.z_slice_loan(HandleZOwnedSlice);
        return ZenohC.z_slice_is_empty(pLoanedSlice);
    }

    public nuint Length()
    {
        var pLoanedSlice = ZenohC.z_slice_loan(HandleZOwnedSlice);
        return ZenohC.z_slice_len(pLoanedSlice);
    }

}
