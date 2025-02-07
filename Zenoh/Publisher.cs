using System;
using System.Runtime.InteropServices;

namespace Zenoh;

public struct PublisherHandle
{
    internal int handle;
}

public class Publisher : IDisposable
{
    internal unsafe ZOwnedPublisher* ownedPublisher;
    internal readonly ZPublisherOptions options;
    internal readonly string keyexpr;
    private bool _disposed;

    public Publisher(string key) : this(key, ZCongestionControl.Block, ZPriority.RealTime)
    {
    }

    public Publisher(string key, ZCongestionControl control, ZPriority zPriority)
    {
        unsafe
        {
            keyexpr = key;
            _disposed = false;
            ownedPublisher = null;
            options.ZCongestionControl = control;
            options.ZPriority = zPriority;
        }
    }

    public void Dispose() => Dispose(true);

    private void Dispose(bool disposing)
    {
        if (_disposed) return;
        unsafe
        {
            Marshal.FreeHGlobal((nint)ownedPublisher);
        }

        _disposed = true;
    }
}