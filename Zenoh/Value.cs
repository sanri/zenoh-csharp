using System;
using System.Runtime.InteropServices;

namespace Zenoh;

// 这是一个引用类型, 不释放引用的内存.
public class Value
{
    internal ZValue zValue;

    internal Value(ZValue value)
    {
        zValue = value;
    }

    public ZEncodingPrefix GetEncodingPrefix()
    {
        return zValue.encoding.prefix;
    }

    public byte[] GetEncodingSuffix()
    {
        unsafe
        {
            nuint len = zValue.encoding.suffix.len;
            nint start = (nint)zValue.encoding.suffix.start;
            byte[] data = new byte[len];
            Marshal.Copy(start, data, 0, (int)len);
            return data;
        }
    }

    public byte[] GetPayload()
    {
        unsafe
        {
            fixed (ZBytes* pBytes = &zValue.payload)
            {
                if (ZenohC.z_bytes_check(pBytes) != 1)
                    return Array.Empty<byte>();
            }

            var len = zValue.payload.len;
            byte[] data = new byte[len];
            Marshal.Copy((nint)zValue.payload.start, data, 0, (int)len);
            return data;
        }
    }

    public string? GetString()
    {
        unsafe
        {
            fixed (ZBytes* pBytes = &zValue.payload)
            {
                if (ZenohC.z_bytes_check(pBytes) != 1)
                    return null;
            }

            return Marshal.PtrToStringUTF8((nint)zValue.payload.start, (int)zValue.payload.len);
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