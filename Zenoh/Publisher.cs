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


public sealed class Publisher : IDisposable
{
    // z_owned_publisher_t*
    internal nint HandleZOwnedPublisher { get; private set; }
    
    private Publisher(){}
    

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~Publisher() => Dispose(false);

    private void Dispose(bool disposing)
    {
        // todo
    }
}