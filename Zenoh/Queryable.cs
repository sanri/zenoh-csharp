#pragma warning disable CS8500

using System;
using System.Runtime.InteropServices;
using System.Text;


namespace Zenoh;

public struct QueryableHandle
{
    internal int handle;
}

public delegate void QueryableCallback(Query query);

public class Queryable : IDisposable
{
    internal string keyexpr;
    internal unsafe ZOwnedQueryable* zOwnedQueryable;
    internal unsafe ZOwnedClosureQuery* closureQuery;
    internal unsafe ZQueryableOptions* options;
    private readonly ZOwnedClosureQuery _ownedClosureQuery;
    private GCHandle _userCallbackGcHandle;
    private bool _disposed;

    public Queryable(string keyexpr, QueryableCallback userCallback, bool complete = false)
    {
        unsafe
        {
            this.keyexpr = keyexpr;
            _disposed = false;
            zOwnedQueryable = null;
            _userCallbackGcHandle = GCHandle.Alloc(userCallback);

            _ownedClosureQuery = new ZOwnedClosureQuery
            {
                context = (void*)GCHandle.ToIntPtr(_userCallbackGcHandle),
                call = Call,
                drop = null,
            };
            nint p = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedClosureQuery>());
            Marshal.StructureToPtr(_ownedClosureQuery, p, false);
            closureQuery = (ZOwnedClosureQuery*)p;

            nint pOptions = Marshal.AllocHGlobal(Marshal.SizeOf<ZQueryableOptions>());
            Marshal.WriteByte(pOptions, complete ? (byte)1 : (byte)0);
            options = (ZQueryableOptions*)pOptions;
        }
    }

    public void Dispose() => Dispose(true);

    private void Dispose(bool disposing)
    {
        if (_disposed) return;

        unsafe
        {
            Marshal.FreeHGlobal((nint)closureQuery);
            Marshal.FreeHGlobal((nint)options);
            Marshal.FreeHGlobal((nint)zOwnedQueryable);
        }

        _userCallbackGcHandle.Free();
        _disposed = true;
    }

    private static unsafe void Call(ZQuery* zQuery, void* context)
    {
        var gch = GCHandle.FromIntPtr((nint)context);
        var callback = (QueryableCallback?)gch.Target;
        var query = new Query(zQuery);
        if (callback != null)
        {
            callback(query);
        }
    }
}

public class Query
{
    private unsafe ZQuery* _query;

    internal unsafe Query(ZQuery* query)
    {
        _query = query;
    }

    public string GetKeyexpr()
    {
        unsafe
        {
            ZKeyexpr keyexpr = ZenohC.z_query_keyexpr(_query);
            string keyStr = ZenohC.ZKeyexprToString(keyexpr);
            return keyStr;
        }
    }

    public Value GetValue()
    {
        unsafe
        {
            ZValue zValue = ZenohC.z_query_value(_query);
            return new Value(zValue);
        }
    }

    public bool Reply(string key, byte[] payload, ZEncodingPrefix zEncodingPrefix, byte[]? encodingSuffix)
    {
        unsafe
        {
            sbyte r;
            fixed (byte* pv = payload)
            {
                nint pKey = Marshal.StringToHGlobalAnsi(key);
                ZKeyexpr keyexpr = ZenohC.z_keyexpr((byte*)pKey);
                if (encodingSuffix is null)
                {
                    ZQueryReplyOptions options = new ZQueryReplyOptions
                    {
                        encoding = ZenohC.z_encoding(zEncodingPrefix, null),
                    };
                    nuint len = (nuint)payload.Length;
                    r = ZenohC.z_query_reply(_query, keyexpr, pv, len, &options);
                }
                else
                {
                    fixed (byte* suffix = encodingSuffix)
                    {
                        ZQueryReplyOptions options = new ZQueryReplyOptions
                        {
                            encoding = ZenohC.z_encoding(zEncodingPrefix, suffix),
                        };
                        nuint len = (nuint)payload.Length;
                        r = ZenohC.z_query_reply(_query, keyexpr, pv, len, &options);
                    }
                }

                Marshal.FreeHGlobal(pKey);
            }

            return r == 0;
        }
    }

    public bool ReplyStr(string key, string value)
    {
        byte[] payload = Encoding.UTF8.GetBytes(value);
        return Reply(key, payload, ZEncodingPrefix.TextPlain, null);
    }

    public bool ReplyJson(string key, string value)
    {
        byte[] payload = Encoding.UTF8.GetBytes(value);
        return Reply(key, payload, ZEncodingPrefix.AppJson, null);
    }

    public bool ReplyInt(string key, long value)
    {
        string s = value.ToString("G");
        byte[] payload = Encoding.UTF8.GetBytes(s);
        return Reply(key, payload, ZEncodingPrefix.AppInteger, null);
    }

    public bool ReplyFloat(string key, double value)
    {
        string s = value.ToString("G");
        byte[] payload = Encoding.UTF8.GetBytes(s);
        return Reply(key, payload, ZEncodingPrefix.AppFloat, null);
    }
}