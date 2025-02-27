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

    public Encoding? GetEncoding()
    {
        return _encoding is null ? null : new Encoding(_encoding);
    }

    public void SetPayload(ZBytes? payload)
    {
        _payload = payload is null ? null : new ZBytes(payload);
    }

    public ZBytes? GetPayload()
    {
        return _payload is null ? null : new ZBytes(_payload);
    }
    
    public void SetAttachment(ZBytes? attachment)
    {
        _attachment = attachment is null ? null : new ZBytes(attachment);
    }

    public ZBytes? GetAttachment()
    {
        return _attachment is null ? null : new ZBytes(_attachment);
    }

    internal nint AllocUnmanagedMemory()
    {
        var options = new ZGetOptions
        {
            encoding =nint.Zero,
            payload = nint.Zero,
            attachment = nint.Zero,
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
        
        options.target = _target;
        options.consolidation = _consolidation;
        options.congestion_control = _congestionControl;
        options.priority = _priority;
        options.timeout_ms = _timeoutMs;
        
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

