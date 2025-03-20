using System;
using System.Runtime.InteropServices;

namespace Zenoh;

public sealed class GetOptions : IDisposable
{
    private QueryTarget _target;
    private ZQueryConsolidation _consolidation;
    private CongestionControl _congestionControl;
    private Priority _priority;
    private ulong _timeoutMs;
    private Encoding? _encoding;
    private ZBytes? _payload;
    private ZBytes? _attachment;

    public GetOptions()
    {
        var pGetOptions = Marshal.AllocHGlobal(Marshal.SizeOf<ZGetOptions>());
        ZenohC.z_get_options_default(pGetOptions);
        var options = Marshal.PtrToStructure<ZGetOptions>(pGetOptions);
        Marshal.FreeHGlobal(pGetOptions);

        _target = options.target;
        _consolidation = options.consolidation;
        _congestionControl = options.congestion_control;
        _priority = options.priority;
        _timeoutMs = options.timeout_ms;
        _encoding = null;
        _payload = null;
        _attachment = null;
    }

    public GetOptions(GetOptions other)
    {
        _target = other._target;
        _consolidation = other._consolidation;
        _congestionControl = other._congestionControl;
        _priority = other._priority;
        _timeoutMs = other._timeoutMs;
        _encoding = other._encoding is null ? null : new Encoding(other._encoding);
        _payload = other._payload is null ? null : new ZBytes(other._payload);
        _attachment = other._attachment is null ? null : new ZBytes(other._attachment);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~GetOptions() => Dispose(false);

    private void Dispose(bool disposing)
    {
        if (_encoding is not null)
        {
            if (disposing)
            {
                _encoding.Dispose();
            }

            _encoding = null;
        }

        if (_payload is not null)
        {
            if (disposing)
            {
                _payload.Dispose();
            }

            _payload = null;
        }

        if (_attachment is not null)
        {
            if (disposing)
            {
                _attachment.Dispose();
            }

            _attachment = null;
        }
    }

    public void SetQueryTarget(QueryTarget target)
    {
        _target = target;
    }

    public QueryTarget GetQueryTarget()
    {
        return _target;
    }

    public void SetQueryConsolidation(ConsolidationMode mode)
    {
        _consolidation.mode = mode;
    }

    public ConsolidationMode GetQueryConsolidation()
    {
        return _consolidation.mode;
    }

    public void SetCongestionControl(CongestionControl congestionControl)
    {
        _congestionControl = congestionControl;
    }

    public CongestionControl GetCongestionControl()
    {
        return _congestionControl;
    }

    public void SetPriority(Priority priority)
    {
        _priority = priority;
    }

    public Priority GetPriority()
    {
        return _priority;
    }

    /// <summary>
    /// The timeout for the query in milliseconds.
    /// 0 means default query timeout from zenoh configuration.
    /// </summary>
    /// <param name="time"></param>
    public void SetTimeout(ulong time)
    {
        _timeoutMs = time;
    }

    /// <summary>
    /// The timeout for the query in milliseconds.
    /// 0 means default query timeout from zenoh configuration.
    /// </summary>
    /// <returns></returns>
    public ulong GetTimeout()
    {
        return _timeoutMs;
    }

    public void SetEncoding(Encoding? encoding)
    {
        _encoding = encoding is null ? null : new Encoding(encoding);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns>
    /// Return Encoding is loaned.
    /// </returns>
    public Encoding? GetEncoding()
    {
        return _encoding is null ? null : Encoding.CreateLoaned(_encoding.LoanedPointer());
    }

    public void SetPayload(ZBytes? payload)
    {
        _payload = payload is null ? null : new ZBytes(payload);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns>
    /// Return ZBytes is loaned.
    /// </returns>
    public ZBytes? GetPayload()
    {
        return _payload is null ? null : ZBytes.CreateLoaned(_payload.LoanedPointer());
    }

    public void SetAttachment(ZBytes? attachment)
    {
        _attachment = attachment is null ? null : new ZBytes(attachment);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns>
    /// Return ZBytes is loaned.
    /// </returns>
    public ZBytes? GetAttachment()
    {
        return _attachment is null ? null : ZBytes.CreateLoaned(_attachment.LoanedPointer());
    }

    internal nint AllocUnmanagedMemory()
    {
        var options = new ZGetOptions
        {
            encoding = nint.Zero,
            payload = nint.Zero,
            attachment = nint.Zero,
            target = _target,
            consolidation = _consolidation,
            congestion_control = _congestionControl,
            priority = _priority,
            timeout_ms = _timeoutMs,
        };

        if (_encoding is not null)
        {
            options.encoding = _encoding.AllocUnmanagedMemory();
        }

        if (_payload is not null)
        {
            options.payload = _payload.AllocUnmanagedMemory();
        }

        if (_attachment is not null)
        {
            options.attachment = _attachment.AllocUnmanagedMemory();
        }

        var pOptions = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(ZGetOptions)));
        Marshal.StructureToPtr(options, pOptions, false);

        return pOptions;
    }

    internal static void FreeUnmanagedMemory(nint handle)
    {
        var options = Marshal.PtrToStructure<ZGetOptions>(handle);

        if (options.encoding != nint.Zero)
        {
            Encoding.FreeUnmanagedMem(options.encoding);
        }

        if (options.payload != nint.Zero)
        {
            ZBytes.FreeUnmanagedMem(options.payload);
        }

        if (options.attachment != nint.Zero)
        {
            ZBytes.FreeUnmanagedMem(options.attachment);
        }

        Marshal.FreeHGlobal(handle);
    }
}

// z_delete_options_t
public sealed class DeleteOptions : IDisposable
{
    private CongestionControl _congestionControl;
    private Priority _priority;
    private bool _isExpress;
    private Timestamp? _timestamp;

    public DeleteOptions()
    {
        var pOptions = Marshal.AllocHGlobal(Marshal.SizeOf<ZDeleteOptions>());
        ZenohC.z_delete_options_default(pOptions);
        var options = Marshal.PtrToStructure<ZDeleteOptions>(pOptions);
        Marshal.FreeHGlobal(pOptions);
        
        _congestionControl = options.congestion_control;
        _priority = options.priority;
        _isExpress = options.is_express;
        _timestamp = null;
    }

    public DeleteOptions(DeleteOptions other)
    {
        _congestionControl = other._congestionControl;
        _priority = other._priority;
        _isExpress = other._isExpress;
        _timestamp = other._timestamp is null ? null : new Timestamp(other._timestamp);
    }
    
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    
    ~DeleteOptions()=>Dispose(false);

    private void Dispose(bool disposing)
    {
        if (_timestamp is not null)
        {
            if (disposing)
            {
                _timestamp.Dispose();
            }
            
            _timestamp = null;
        }
    }
    
    public void SetCongestionControl(CongestionControl congestionControl)
    {
        _congestionControl = congestionControl;
    }

    public CongestionControl GetCongestionControl()
    {
        return _congestionControl;
    }

    public void SetPriority(Priority priority)
    {
        _priority = priority;
    }

    public Priority GetPriority()
    {
        return _priority;
    }

    public void SetIsExpress(bool isExpress)
    {
        _isExpress = isExpress;
    }

    public bool GetIsExpress()
    {
        return _isExpress;
    }
    
    public void SetTimestamp(Timestamp? timestamp)
    {
        _timestamp = timestamp is null ? null : new Timestamp(timestamp);
    }

    public Timestamp? GetTimestamp()
    {
        return _timestamp is null ? null : new Timestamp(_timestamp);
    }

    internal nint AllocUnmanagedMemory()
    {
        var options = new ZDeleteOptions
        {
            congestion_control = _congestionControl,
            priority = _priority,
            is_express = _isExpress,
            timestamp = nint.Zero,
        };

        if (_timestamp is not null)
        {
            options.timestamp = _timestamp.AllocUnmanagedMem();
        }
        
        var pOptions = Marshal.AllocHGlobal(Marshal.SizeOf<ZDeleteOptions>());
        Marshal.StructureToPtr(options, pOptions, false);
        return pOptions;
    }

    internal static void FreeUnmanagedMemory(nint handle)
    {
        var options = Marshal.PtrToStructure<ZDeleteOptions>(handle);

        if (options.timestamp != nint.Zero)
        {
            Timestamp.FreeUnmanagedMem(options.timestamp);
        }
        
        Marshal.FreeHGlobal(handle);
    }
}

//  z_owned_reply_err_t
public sealed class ReplyErr : Loanable
{
    private ReplyErr()
    {
        throw new InvalidOperationException();
    }

    public ReplyErr(ReplyErr other)
    {
        other.CheckDisposed();

        var pOwnedReplyErr = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedReplyErr>());
        var pLoanedReplyErr = other.LoanedPointer();
        ZenohC.z_reply_err_clone(pOwnedReplyErr, pLoanedReplyErr);
        Handle = pOwnedReplyErr;
        Owned = true;
    }

    private ReplyErr(nint handle, bool owned)
    {
        Owned = owned;
        Handle = handle;
    }

    internal static ReplyErr CreateLoaned(nint handle)
    {
        return new ReplyErr(handle, false);
    }

    internal static ReplyErr CreateOwned()
    {
        var pOwnedReplyErr = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedReplyErr>());
        ZenohC.z_internal_reply_err_null(pOwnedReplyErr);
        return new ReplyErr(pOwnedReplyErr, true);
    }

    public override void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~ReplyErr() => Dispose(false);

    private void Dispose(bool disposing)
    {
        if (Handle == nint.Zero) return;

        if (Owned)
        {
            ZenohC.z_reply_err_drop(Handle);
            Marshal.FreeHGlobal(Handle);
        }

        Handle = nint.Zero;
    }

    internal override nint LoanedPointer()
    {
        return Owned ? ZenohC.z_reply_err_loan(Handle) : Handle;
    }

    public override void ToOwned()
    {
        var pOwnedReplyErr = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedReplyErr>());
        var pLoanedReplyErr = LoanedPointer();
        ZenohC.z_reply_err_clone(pOwnedReplyErr, pLoanedReplyErr);
        Handle = pOwnedReplyErr;
        Owned = true;
    }

    /// <summary>
    /// Returns reply error payload.
    /// </summary>
    /// <returns>
    /// Return ZBytes is loaned.
    /// </returns>
    public ZBytes GetPayload()
    {
        var pLoanedReplyErr = LoanedPointer();
        var pLoanedBytes = ZenohC.z_reply_err_payload(pLoanedReplyErr);
        return ZBytes.CreateLoaned(pLoanedBytes);
    }

    /// <summary>
    /// Returns reply error encoding.
    /// </summary>
    /// <returns>
    /// Return Encoding is loaned.
    /// </returns>
    public Encoding GetEncoding()
    {
        var pLoanedReplyErr = LoanedPointer();
        var pLoanedEncoding = ZenohC.z_reply_err_encoding(pLoanedReplyErr);
        return Encoding.CreateLoaned(pLoanedEncoding);
    }
}

// z_owned_reply_t
public sealed class Reply : IDisposable
{
    // z_owned_reply_t*
    internal nint Handle { get; private set; }

    internal Reply()
    {
        var pOwnedReply = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedReply>());
        ZenohC.z_internal_reply_null(pOwnedReply);
        Handle = pOwnedReply;
    }

    public Reply(Reply other)
    {
        other.CheckDisposed();

        var pOwnedReply = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedReply>());
        var pLoanedReply = ZenohC.z_reply_loan(other.Handle);
        ZenohC.z_reply_clone(pOwnedReply, pLoanedReply);
        Handle = pOwnedReply;
    }


    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~Reply() => Dispose(false);

    private void Dispose(bool disposing)
    {
        if (Handle == nint.Zero) return;

        ZenohC.z_reply_drop(Handle);
        Marshal.FreeHGlobal(Handle);
        Handle = nint.Zero;
    }

    internal void CheckDisposed()
    {
        if (Handle == nint.Zero)
        {
            throw new InvalidOperationException("Object is disposed");
        }
    }

    /// <summary>
    /// Returns 'true' if reply contains a valid response, 'false' otherwise.
    /// </summary>
    public bool IsOk()
    {
        CheckDisposed();

        var pLoanedReply = ZenohC.z_reply_loan(Handle);
        return ZenohC.z_reply_is_ok(pLoanedReply);
    }

    /// <summary>
    /// Yields the contents of the reply by asserting it indicates a failure.
    /// </summary>
    /// <returns>
    /// Return ReplyErr is loaned.
    /// return null if reply does not contain a error  (i. e. if `IsOk()` returns `true`).
    /// </returns>
    public ReplyErr? AsErr()
    {
        CheckDisposed();
        
        var pLoanedReply = ZenohC.z_reply_loan(Handle);
        var pLoanedReplyErr = ZenohC.z_reply_err(pLoanedReply);
        
        return pLoanedReplyErr == nint.Zero ? null : ReplyErr.CreateLoaned(pLoanedReplyErr);
    }

    /// <summary>
    /// Yields the contents of the reply by asserting it indicates a success.
    /// </summary>
    /// <returns>
    /// Return Sample is loaned,
    /// return null if reply does not contain a sample (i. e. if `IsOk()` returns `false`).
    /// </returns>
    public Sample? AsOk()
    {
        CheckDisposed();
        
        var pLoanedReply = ZenohC.z_reply_loan(Handle);
        var pLoanedSample = ZenohC.z_reply_ok(pLoanedReply);
        
        return pLoanedSample == nint.Zero ? null: Sample.CreateLoaned(pLoanedSample);
    }
}