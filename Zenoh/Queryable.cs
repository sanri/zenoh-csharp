using System;
using System.Runtime.InteropServices;

namespace Zenoh;

public sealed class QueryReplyOptions : IDisposable
{
    private CongestionControl _congestionControl;
    private Priority _priority;
    private bool _isExpress;
    private Encoding? _encoding;
    private Timestamp? _timestamp;
    private ZBytes? _attachment;

    public QueryReplyOptions()
    {
        var pQueryReplyOptions = Marshal.AllocHGlobal(Marshal.SizeOf<ZQueryReplyOptions>());
        ZenohC.z_query_reply_options_default(pQueryReplyOptions);
        var options = Marshal.PtrToStructure<ZQueryReplyOptions>(pQueryReplyOptions);
        Marshal.FreeHGlobal(pQueryReplyOptions);

        _congestionControl = options.congestion_control;
        _priority = options.priority;
        _isExpress = options.is_express;
        _encoding = null;
        _timestamp = null;
        _attachment = null;
    }

    public QueryReplyOptions(QueryReplyOptions other)
    {
        _congestionControl = other._congestionControl;
        _priority = other._priority;
        _isExpress = other._isExpress;
        _encoding = other._encoding is null ? null : new Encoding(other._encoding);
        _timestamp = other._timestamp is null ? null : new Timestamp(other._timestamp);
        _attachment = other._attachment is null ? null : new ZBytes(other._attachment);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~QueryReplyOptions() => Dispose(false);

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

        if (_timestamp is not null)
        {
            if (disposing)
            {
                _timestamp.Dispose();
            }

            _timestamp = null;
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

    public void SetTimestamp(Timestamp? timestamp)
    {
        _timestamp = timestamp is null ? null : new Timestamp(timestamp);
    }

    public Timestamp? GetTimestamp()
    {
        return _timestamp is null ? null : new Timestamp(_timestamp);
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
        var options = new ZQueryReplyOptions
        {
            encoding = nint.Zero,
            timestamp = nint.Zero,
            attachment = nint.Zero,
            congestion_control = _congestionControl,
            priority = _priority,
            is_express = _isExpress,
        };

        if (_encoding is not null)
        {
            options.encoding = _encoding.AllocUnmanagedMemory();
        }

        if (_timestamp is not null)
        {
            options.timestamp = _timestamp.AllocUnmanagedMem();
        }

        if (_attachment is not null)
        {
            options.attachment = _attachment.AllocUnmanagedMemory();
        }

        var pOptions = Marshal.AllocHGlobal(Marshal.SizeOf<ZQueryReplyOptions>());
        Marshal.StructureToPtr(options, pOptions, false);

        return pOptions;
    }

    internal static void FreeUnmanagedMemory(nint handle)
    {
        var options = Marshal.PtrToStructure<ZQueryReplyOptions>(handle);

        if (options.encoding != nint.Zero)
        {
            Encoding.FreeUnmanagedMem(options.encoding);
        }

        if (options.timestamp != nint.Zero)
        {
            Timestamp.FreeUnmanagedMem(options.timestamp);
        }

        if (options.attachment != nint.Zero)
        {
            ZBytes.FreeUnmanagedMem(options.attachment);
        }

        Marshal.FreeHGlobal(handle);
    }
}

public sealed class QueryReplyErrOptions : IDisposable
{
    public void Dispose()
    {
        throw new NotImplementedException();
    }
}

// z_owned_query_t
public sealed class Query : Loanable
{
    internal Query()
    {
        var pOwnedQuery = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedQuery>());
        ZenohC.z_internal_query_null(pOwnedQuery);
        Handle = pOwnedQuery;
        Owned = true;
    }

    public Query(Query other)
    {
        other.CheckDisposed();

        var pOwnedQuery = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedQuery>());
        var pLoanedQuery = other.LoanedPointer();
        ZenohC.z_query_clone(pOwnedQuery, pLoanedQuery);
        Handle = pOwnedQuery;
        Owned = true;
    }

    private Query(nint handle, bool owned)
    {
        Handle = handle;
        Owned = owned;
    }

    internal static Query CreateLoaned(nint handle)
    {
        return new Query(handle, false);
    }

    public override void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~Query() => Dispose(false);

    private void Dispose(bool disposing)
    {
        if (Handle == nint.Zero) return;

        if (Owned)
        {
            ZenohC.z_query_drop(Handle);
            Marshal.FreeHGlobal(Handle);
        }

        Handle = nint.Zero;
    }

    public override void ToOwned()
    {
        var pOwnedQuery = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedQuery>());
        var pLoanedQuery = LoanedPointer();
        ZenohC.z_query_clone(pOwnedQuery, pLoanedQuery);
        Handle = pOwnedQuery;
        Owned = true;
    }

    internal override nint LoanedPointer()
    {
        return Owned ? ZenohC.z_query_loan(Handle) : Handle;
    }

    /// <summary>
    /// Gets query attachment.
    /// </summary>
    /// <returns></returns>
    public ZBytes? GetAttachment()
    {
        CheckDisposed();

        var pLoanedQuery = LoanedPointer();
        var pLoanedBytes = ZenohC.z_query_attachment(pLoanedQuery);

        return pLoanedBytes == nint.Zero ? null : ZBytes.CreateLoaned(pLoanedBytes);
    }

    /// <summary>
    /// Gets query payload encoding.
    /// </summary>
    /// <returns></returns>
    public Encoding? GetEncoding()
    {
        CheckDisposed();

        var pLoanedQuery = LoanedPointer();
        var pLoanedEncoding = ZenohC.z_query_encoding(pLoanedQuery);

        return pLoanedEncoding == nint.Zero ? null : Encoding.CreateLoaned(pLoanedEncoding);
    }

    /// <summary>
    /// Gets query payload.
    /// </summary>
    /// <returns>
    /// Return ZBytes is loaned.
    /// </returns>
    public ZBytes? GetPayload()
    {
        CheckDisposed();

        var pLoanedQuery = LoanedPointer();
        var pLoanedBytes = ZenohC.z_query_payload(pLoanedQuery);

        return pLoanedBytes == nint.Zero ? null : ZBytes.CreateLoaned(pLoanedBytes);
    }

    /// <summary>
    /// Gets query key expression.
    /// </summary>
    /// <returns></returns>
    public Keyexpr GetKeyexpr()
    {
        CheckDisposed();

        var pLoanedQuery = LoanedPointer();
        var pLoanedKeyexpr = ZenohC.z_query_keyexpr(pLoanedQuery);

        return Keyexpr.CreateLoaned(pLoanedKeyexpr);
    }

    /// <summary>
    /// <para>
    /// Gets query value selector.
    /// </para>
    /// </summary>
    /// <returns></returns>
    public string? GetParameters()
    {
        CheckDisposed();

        var pLoanedQuery = LoanedPointer();
        var viewString = new ViewString();
        ZenohC.z_query_parameters(pLoanedQuery, viewString.Handle);
        var r = viewString.ToString();
        viewString.Dispose();
        return r;
    }

    /// <summary>
    /// <para>
    /// Sends a reply to a query.
    /// </para>
    /// <para>
    /// This function must be called inside of a Queryable callback passing the query received as parameters of the callback function.
    /// This function can be called multiple times to send multiple replies to a query.
    /// The reply will be considered complete when the Queryable callback returns.
    /// </para>
    /// <para>
    /// Do not use the "payload" after calling this function.
    /// payload.Dispose() is called inside this function.
    /// </para>
    /// </summary>
    /// <param name="keyexpr">The key of this reply</param>
    /// <param name="payload">The payload of this reply.</param>
    /// <param name="options">The options of this reply</param>
    /// <returns>
    /// Result.Ok in case of success.
    /// </returns>
    public Result Reply(Keyexpr keyexpr, ZBytes payload, QueryReplyOptions options)
    {
        CheckDisposed();
        keyexpr.CheckDisposed();
        payload.CheckDisposed();
        payload.ToOwned();

        var pLoanedQuery = LoanedPointer();
        var pLoanedKeyexpr = keyexpr.LoanedPointer();
        var pMovedBytes = payload.Handle;
        var pQueryReplyOptions = options.AllocUnmanagedMemory();

        var r = ZenohC.z_query_reply(pLoanedQuery, pLoanedKeyexpr, pMovedBytes, pQueryReplyOptions);

        QueryReplyOptions.FreeUnmanagedMemory(pQueryReplyOptions);
        payload.Dispose();

        return r;
    }

    public Result ReplyErr(ZBytes payload, QueryReplyErrOptions options)
    {
        // todo
        return Result.ErrorGeneric;
    }
}


public sealed class QueryableOptions
{
    public bool Complete;

    public QueryableOptions()
    {
        var pOptions = Marshal.AllocHGlobal(Marshal.SizeOf<ZQueryableOptions>());
        ZenohC.z_queryable_options_default(pOptions);
        var options = Marshal.PtrToStructure<ZQueryableOptions>(pOptions);
        Marshal.FreeHGlobal(pOptions);
        Complete = options.complete;
    }

    public QueryableOptions(QueryableOptions other)
    {
        Complete = other.Complete;
    }

    internal nint AllocUnmanagedMem()
    {
        var options = new ZQueryableOptions
        {
            complete = Complete
        };
        var pOptions = Marshal.AllocHGlobal(Marshal.SizeOf<ZQueryableOptions>());
        Marshal.StructureToPtr(options, pOptions, false);
        return pOptions;
    }

    internal static void FreeUnmanagedMem(nint handle)
    {
        Marshal.FreeHGlobal(handle);
    }
    
}

// z_owned_queryable_t
public sealed class Queryable : IDisposable
{
    internal nint Handle { get;private set; }
    
    public delegate void Cb(Query query);

    internal Queryable()
    {
        var pOwnedQueryable = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedQueryable>());
        ZenohC.z_internal_queryable_null(pOwnedQueryable);
        Handle = pOwnedQueryable;
    }

    private Queryable(Queryable other)
    {
        throw new InvalidOperationException();
    }
    
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~Queryable() => Dispose(false);

    private void Dispose(bool disposing)
    {
        if (Handle == nint.Zero) return;
        
        ZenohC.z_queryable_drop(Handle);
        Marshal.FreeHGlobal(Handle);
        Handle = nint.Zero;
    }
    
    internal void CheckDisposed()
    {
        if (Handle == nint.Zero)
        {
            throw new InvalidOperationException("Object has been destroyed");
        }
    }
    
    /// <summary>
    /// Undeclare the queryable and free memory. This is equivalent to calling the "Dispose()".
    /// </summary>
    public void Undeclare()
    {
        Dispose();
    }

    // void (*call)(struct z_loaned_query_t *query, void *context)
    internal static void CallbackClosureQueryCall(nint query, nint context)
    {
        var gcHandle = GCHandle.FromIntPtr(context);
        if (gcHandle.Target is not Cb callback) return;
        
        var loanedQuery = Query.CreateLoaned(query);
        callback(loanedQuery);
    }

    // void (*drop)(void *context)
    internal static void CallbackClosureQueryDrop(nint context)
    {
        var gcHandle = GCHandle.FromIntPtr(context);
        gcHandle.Free();
    }
}