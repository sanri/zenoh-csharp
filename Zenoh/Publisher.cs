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

    public PublisherPutOptions(PublisherPutOptions other)
    {
        other.CheckDisposed();

        var pPublisherPutOptions = Marshal.AllocHGlobal(Marshal.SizeOf<ZPublisherPutOptions>());
        ZPublisherPutOptions zPublisherPutOptions;

        var encoding = other.GetEncoding();
        zPublisherPutOptions.encoding = encoding?.HandleZOwnedEncoding ?? nint.Zero;

        var timestamp = other.GetTimestamp();
        zPublisherPutOptions.timestamp = timestamp?.HandleTimestamp ?? nint.Zero;

        var attachment = other.GetAttachment();
        zPublisherPutOptions.attachment = attachment?.HandleZOwnedBytes ?? nint.Zero;

        Marshal.StructureToPtr(zPublisherPutOptions, pPublisherPutOptions, false);

        HandlePublisherPutOptions = pPublisherPutOptions;
        _encoding = encoding;
        _timestamp = timestamp;
        _attachment = attachment;
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
            throw new InvalidOperationException("Object has been destroyed");
        }
    }

    public void SetEncoding(Encoding? encoding)
    {
        CheckDisposed();

        var zPublisherPutOptions = Marshal.PtrToStructure<ZPublisherPutOptions>(HandlePublisherPutOptions);

        if (encoding is null)
        {
            zPublisherPutOptions.encoding = nint.Zero;
        }
        else
        {
            encoding.CheckDisposed();
            zPublisherPutOptions.encoding = encoding.HandleZOwnedEncoding;
        }

        Marshal.StructureToPtr(zPublisherPutOptions, HandlePublisherPutOptions, false);
        _encoding?.Dispose();
        _encoding = encoding;
    }

    public Encoding? GetEncoding()
    {
        return _encoding is null ? null : new Encoding(_encoding);
    }

    public void SetTimestamp(Timestamp? timestamp)
    {
        CheckDisposed();

        var zPublisherPutOptions = Marshal.PtrToStructure<ZPublisherPutOptions>(HandlePublisherPutOptions);

        if (timestamp is null)
        {
            zPublisherPutOptions.timestamp = nint.Zero;
        }
        else
        {
            timestamp.CheckDisposed();
            zPublisherPutOptions.timestamp = timestamp.HandleTimestamp;
        }

        Marshal.StructureToPtr(zPublisherPutOptions, HandlePublisherPutOptions, false);
        _timestamp?.Dispose();
        _timestamp = timestamp;

    }

    public Timestamp? GetTimestamp()
    {
        return _timestamp is null ? null : new Timestamp(_timestamp);
    }

    public void SetAttachment(ZBytes? attachment)
    {
        CheckDisposed();

        var zPublisherPutOptions = Marshal.PtrToStructure<ZPublisherPutOptions>(HandlePublisherPutOptions);

        if (attachment is null)
        {
            zPublisherPutOptions.attachment = nint.Zero;
        }
        else
        {
            attachment.CheckDisposed();
            zPublisherPutOptions.attachment = attachment.HandleZOwnedBytes;
        }

        Marshal.StructureToPtr(zPublisherPutOptions, HandlePublisherPutOptions, false);
        _attachment?.Dispose();
        _attachment = attachment;
    }

    public ZBytes? GetAttachment()
    {
        return _attachment is null ? null : new ZBytes(_attachment);
    }

}

public sealed class Publisher : IDisposable
{
    // z_owned_publisher_t*
    internal nint HandleZOwnedPublisher { get; private set; }

    private Publisher()
    {
        throw new InvalidCastException();
    }

    internal Publisher(nint handle)
    {
        HandleZOwnedPublisher = handle;
    }

    private Publisher(Publisher other)
    {
        throw new InvalidCastException();
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

    internal void CheckDisposed()
    {
        if (HandleZOwnedPublisher == nint.Zero)
        {
            throw new InvalidOperationException("Object has been destroyed");
        }
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
        CheckDisposed();

        var pLoanedPublisher = ZenohC.z_publisher_loan(HandleZOwnedPublisher);
        var pLoanedKeyexpr = ZenohC.z_publisher_keyexpr(pLoanedPublisher);
        return Keyexpr.CloneFromLoaned(pLoanedKeyexpr);
    }

    /// <summary>
    /// <para>
    /// Sends a "PUT" message onto the publisher's key expression, transfering the payload ownership.
    /// </para>
    /// <para>
    /// Do not use the "payload" and "options" after calling this function.
    /// payload.Dispose() and options.Dispose() is called inside this function.
    /// </para>
    /// </summary>
    /// <param name="payload">The data to publish. Will be consumed.</param>
    /// <param name="options">The publisher put options. Will be consumed.</param>
    /// <returns>ZResult.Ok in case of success.</returns>
    public ZResult Put(ZBytes payload, PublisherPutOptions options)
    {
        CheckDisposed();
        payload.CheckDisposed();
        options.CheckDisposed();

        var pLoanedPublisher = ZenohC.z_publisher_loan(HandleZOwnedPublisher);
        var r = ZenohC.z_publisher_put(pLoanedPublisher, payload.HandleZOwnedBytes, options.HandlePublisherPutOptions);
        payload.Dispose();
        options.Dispose();
        return r;
    }

}