using System;
using System.Runtime.InteropServices;

namespace Zenoh;

// 这是一个引用类型, 不释放引用的内存.
public class Sample
{
    private readonly unsafe ZSample* _native;

    internal unsafe Sample(ZSample* sample)
    {
        _native = sample;
    }

    public string GetKeyexpr()
    {
        unsafe
        {
            return ZenohC.ZKeyexprToString(_native->keyexpr);
        }
    }

    public ZEncodingPrefix GetEncodingPrefix()
    {
        unsafe
        {
            return _native->encoding.prefix;
        }
    }

    public byte[] GetEncodingSuffix()
    {
        unsafe
        {
            nuint len = _native->encoding.suffix.len;
            nint start = (nint)_native->encoding.suffix.start;
            byte[] data = new byte[len];
            Marshal.Copy(start, data, 0, (int)len);
            return data;
        }
    }

    public ZSampleKind GetSampleKind()
    {
        unsafe
        {
            return _native->kind;
        }
    }

    public byte[] GetPayload()
    {
        unsafe
        {
            if (ZenohC.z_bytes_check(&_native->payload) != 1)
                return Array.Empty<byte>();
            var len = _native->payload.len;
            byte[] data = new byte[len];
            Marshal.Copy((nint)_native->payload.start, data, 0, (int)len);
            return data;
        }
    }

    public string? GetString()
    {
        unsafe
        {
            if (ZenohC.z_bytes_check(&_native->payload) != 1)
                return null;
            return Marshal.PtrToStringUTF8((nint)_native->payload.start, (int)_native->payload.len);
        }
    }

    public long? GetInteger()
    {
        var str = GetString();
        if (!Int64.TryParse(str, out long n))
        {
            return null;
        }

        return n;
    }

    public double? GetDouble()
    {
        var str = GetString();
        if (!Double.TryParse(str, out double n))
        {
            return null;
        }

        return n;
    }
}