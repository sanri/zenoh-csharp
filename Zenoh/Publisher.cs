using System;
using System.Runtime.InteropServices;

namespace Zenoh;


public sealed class PublisherOptions : IDisposable
{
    private Encoding? _encoding;
    private ZCongestionControl _congestionControl;
    private ZPriority _priority;
    private bool _isDisposed;

    public PublisherOptions()
    {
        var pPublisherOptions = Marshal.AllocHGlobal(Marshal.SizeOf<ZPublisherOptions>());
        ZenohC.z_publisher_options_default(pPublisherOptions);
        var options = Marshal.PtrToStructure<ZPublisherOptions>(pPublisherOptions);
        Marshal.FreeHGlobal(pPublisherOptions);
        _encoding = null;
        _congestionControl = options.ZCongestionControl;
        _priority = options.ZPriority;
        _isDisposed = options.is_express;
    }

    public PublisherOptions(PublisherOptions other)
    {
        _encoding = other._encoding is null ? null : new Encoding(other._encoding);
        _congestionControl = other._congestionControl;
        _priority = other._priority;
        _isDisposed = other._isDisposed;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~PublisherOptions() => Dispose(false);

    private void Dispose(bool disposing)
    {
        if (_encoding == null) return;

        if (disposing) _encoding.Dispose();

        _encoding = null;
    }

    public void SetEncoding(Encoding? encoding)
    {
        _encoding = encoding is null ? null : new Encoding(encoding);
    }

    public Encoding? GetEncoding()
    {
        return _encoding is null ? null : new Encoding(_encoding);
    }

    public void SetCongestionControl(ZCongestionControl congestionControl)
    {
        _congestionControl = congestionControl;
    }

    public ZCongestionControl GetCongestionControl()
    {
        return _congestionControl;
    }

    public void SetPriority(ZPriority priority)
    {
        _priority = priority;
    }

    public ZPriority GetPriority()
    {
        return _priority;
    }

    public void SetIsDisposed(bool isDisposed)
    {
        _isDisposed = isDisposed;
    }

    public bool GetIsDisposed()
    {
        return _isDisposed;
    }

    internal nint AllocUnmanagedMem()
    {
        var options = new ZPublisherOptions();
        if (_encoding is null)
        {
            options.encoding = nint.Zero;
        }
        else
        {
            var pZOwnedEncoding = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedEncoding>());
            var pZLoanedEncoding = ZenohC.z_encoding_loan(_encoding.HandleZOwnedEncoding);
            ZenohC.z_encoding_clone(pZOwnedEncoding, pZLoanedEncoding);
            options.encoding = pZOwnedEncoding;
        }

        options.ZCongestionControl = _congestionControl;
        options.ZPriority = _priority;
        options.is_express = _isDisposed;

        var pOptions = Marshal.AllocHGlobal(Marshal.SizeOf<ZPublisherOptions>());
        Marshal.StructureToPtr(options, pOptions, false);

        return pOptions;
    }

    internal static void FreeUnmanagedMem(nint handle)
    {
        var options = Marshal.PtrToStructure<ZPublisherOptions>(handle);
        if (options.encoding != nint.Zero)
        {
            ZenohC.z_encoding_drop(options.encoding);
            Marshal.FreeHGlobal(options.encoding);
        }

        Marshal.FreeHGlobal(handle);
    }

}

public sealed class PublisherPutOptions : IDisposable
{
    // z_publisher_put_options_t*
    internal nint HandlePublisherPutOptions { get; private set; }
    private Encoding? _encoding;
    private Timestamp? _timestamp;
    private ZBytes? _attachment;

    public PublisherPutOptions()
    {
        var pPublisherPutOptions = Marshal.AllocHGlobal(Marshal.SizeOf<ZPublisherPutOptions>());
        ZenohC.z_publisher_options_default(pPublisherPutOptions);
        HandlePublisherPutOptions = pPublisherPutOptions;
        _encoding = null;
        _timestamp = null;
        _attachment = null;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~PublisherPutOptions() => Dispose(false);

    private void Dispose(bool disposing)
    {
        if (HandlePublisherPutOptions == nint.Zero) return;

        if (disposing)
        {
            _encoding?.Dispose();
            _encoding = null;
            _timestamp?.Dispose();
            _timestamp = null;
            _attachment?.Dispose();
            _attachment = null;
        }
        
        Marshal.FreeHGlobal(HandlePublisherPutOptions);
        HandlePublisherPutOptions = nint.Zero;
    }

    internal void CheckDisposed()
    {
        if (HandlePublisherPutOptions == nint.Zero)
        {
            throw new ArgumentException("Object has been destroyed");
        }
    }

    public void SetEncoding(Encoding? encoding)
    {
        CheckDisposed();
        
        // todo
        
        // if (encoding is null)
        // {
        //     _encoding = null;
        //     return;
        // }
        //
        // var zPublisherPutOptions = Marshal.PtrToStructure<ZPublisherPutOptions>(HandlePublisherPutOptions);
    }
}

public sealed class Publisher : IDisposable
{
    // z_owned_publisher_t*
    internal nint HandleZOwnedPublisher { get; private set; }

    private Publisher()
    {
    }

    internal Publisher(nint handle)
    {
        HandleZOwnedPublisher = handle;
    }

    private Publisher(Publisher other)
    {
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~Publisher() => Dispose(false);

    private void Dispose(bool disposing)
    {
        if (HandleZOwnedPublisher == nint.Zero) return;

        ZenohC.z_publisher_drop(HandleZOwnedPublisher);
        Marshal.FreeHGlobal(HandleZOwnedPublisher);
        HandleZOwnedPublisher = IntPtr.Zero;
    }

    /// <summary>
    /// Undeclare the publisher and free memory. This is equivalent to calling the "Dispose()".
    /// </summary>
    public void Undeclare()
    {
        Dispose();
    }

    public Keyexpr GetKeyexpr()
    {
        if (HandleZOwnedPublisher == nint.Zero)
        {
            throw new ArgumentException("Object has been destroyed");
        }

        var pLoanedPublisher = ZenohC.z_publisher_loan(HandleZOwnedPublisher);
        var pLoanedKeyexpr = ZenohC.z_publisher_keyexpr(pLoanedPublisher);
        return Keyexpr.FromLoanedKeyexpr(pLoanedKeyexpr);
    }

    public ZResult Put()
    {
        if (HandleZOwnedPublisher == nint.Zero)
        {
            throw new ArgumentException("Object has been destroyed");
        }

        // todo
    }

}