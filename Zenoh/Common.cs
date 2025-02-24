using System;
using System.Runtime.InteropServices;
using System.Text;

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
        other.CheckDisposed();

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

    internal void CheckDisposed()
    {
        if (HandleZOwnedString == nint.Zero)
        {
            throw new InvalidOperationException("Object has been destroyed");
        }
    }

    public bool IsEmpty()
    {
        CheckDisposed();

        var pLoanedString = ZenohC.z_string_loan(HandleZOwnedString);
        return ZenohC.z_string_is_empty(pLoanedString);
    }

    public nuint Length()
    {
        CheckDisposed();

        var pLoanedString = ZenohC.z_string_loan(HandleZOwnedString);
        return ZenohC.z_string_len(pLoanedString);
    }

    public override string? ToString()
    {
        CheckDisposed();

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
    
    internal void CheckDisposed()
    {
        if (HandleViewString == nint.Zero)
        {
            throw new InvalidOperationException("Object has been destroyed");
        }
    }

    public override string? ToString()
    {
        CheckDisposed();

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

    public ZBytes(ZBytes other)
    {
        other.CheckDisposed();
        
        var pOwnedBytes = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedBytes>());
        ZenohC.z_bytes_clone(pOwnedBytes, other.HandleZOwnedBytes);   
        HandleZOwnedBytes = pOwnedBytes;
    }

    private ZBytes(nint handle)
    {
        HandleZOwnedBytes = handle;
    }

    // 'handle'  z_loaned_bytes_t*
    internal static ZBytes CloneFromLoaned(nint handle)
    {
        var pOwnedBytes = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedBytes>());
        ZenohC.z_bytes_clone(pOwnedBytes, handle);
        return new ZBytes(pOwnedBytes);
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

    internal void CheckDisposed()
    {
        if (HandleZOwnedBytes == nint.Zero)
        {
            throw new InvalidOperationException("Object has been destroyed");
        }
    }

    public bool IsEmpty()
    {
        CheckDisposed();
        
        var pLoanedBytes = ZenohC.z_bytes_loan(HandleZOwnedBytes);
        return ZenohC.z_bytes_is_empty(pLoanedBytes);
    }

    public nuint Length()
    {
        CheckDisposed();
        
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
        if (HandleZOwnedSlice == nint.Zero)
        {
            throw new InvalidOperationException("Object has been destroyed");
        }
        
        var pLoanedSlice = ZenohC.z_slice_loan(HandleZOwnedSlice);
        return ZenohC.z_slice_is_empty(pLoanedSlice);
    }

    public nuint Length()
    {
        if (HandleZOwnedSlice == nint.Zero)
        {
            throw new InvalidOperationException("Object has been destroyed");
        }
        
        var pLoanedSlice = ZenohC.z_slice_loan(HandleZOwnedSlice);
        return ZenohC.z_slice_len(pLoanedSlice);
    }

}

public sealed class Timestamp : IDisposable
{
    // z_timestamp_t*
    internal nint HandleTimestamp { get; private set; }

    private Timestamp()
    {
        throw new InvalidOperationException();
    }

    private Timestamp(nint handle)
    {
        HandleTimestamp = handle;
    }

    public Timestamp(Timestamp other)
    {
        other.CheckDisposed();

        var timestamp = Marshal.PtrToStructure<ZTimestamp>(other.HandleTimestamp);
        var pTimestamp = Marshal.AllocHGlobal(Marshal.SizeOf<ZTimestamp>());
        Marshal.StructureToPtr(timestamp, pTimestamp, false);
        HandleTimestamp = pTimestamp;
    }

    // 'handle' z_timestamp_t*
    internal static Timestamp CloneFromPointer(nint handle)
    {
        var timestamp = Marshal.PtrToStructure<ZTimestamp>(handle);
        var pTimestamp = Marshal.AllocHGlobal(Marshal.SizeOf<ZTimestamp>());
        Marshal.StructureToPtr(timestamp, pTimestamp, false);
        return new Timestamp(pTimestamp);
    }

    // 'handle' z_loaned_session_t*
    internal static Timestamp? NewFromSession(nint handle)
    {
        var pTimestamp = Marshal.AllocHGlobal(Marshal.SizeOf<ZTimestamp>());
        var r = ZenohC.z_timestamp_new(pTimestamp,handle);
        Timestamp? o;
        if (r == ZResult.Ok)
        {
            o = new Timestamp(pTimestamp);
        }
        else
        {
            Marshal.FreeHGlobal(pTimestamp);
            o = null;
        }

        return o;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~Timestamp() => Dispose(false);

    private void Dispose(bool disposing)
    {
        if (HandleTimestamp == nint.Zero) return;

        Marshal.FreeHGlobal(HandleTimestamp);
        HandleTimestamp = nint.Zero;
    }

    public void CheckDisposed()
    {
        if (HandleTimestamp == nint.Zero)
        {
            throw new InvalidOperationException("Object has been destroyed");
        }
    }

    public Id GetId()
    {
        CheckDisposed();

        var zid = ZenohC.z_timestamp_id(HandleTimestamp);
        return new Id(zid);
    }

    public ulong Ntp64Time()
    {
        CheckDisposed();

        return ZenohC.z_timestamp_ntp64_time(HandleTimestamp);
    }
}

public sealed class Id
{
    private byte[] _data;

    private Id()
    {
        throw new InvalidOperationException();
    }

    internal Id(ZId zid)
    {
        _data = zid.GetId();
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        foreach (var b in _data)
        {
            sb.Append(b.ToString("x"));
        }

        return sb.ToString();
    }
}
