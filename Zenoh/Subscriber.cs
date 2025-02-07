#pragma warning disable CS8500

using System;
using System.Runtime.InteropServices;

namespace Zenoh;

public struct SubscriberHandle
{
    internal int handle;
}

public delegate void SubscriberCallback(Sample sample);

public class Subscriber : IDisposable
{
    internal readonly unsafe ZOwnedClosureSample* closureSample;
    internal unsafe ZOwnedSubscriber* ownedSubscriber;
    internal readonly string keyexpr;
    internal readonly ZSubscriberOptions options;
    private readonly ZOwnedClosureSample _ownedClosureSample;
    private GCHandle _userCallbackGcHandle;
    private bool _disposed;

    public Subscriber(string key, SubscriberCallback userCallback)
        : this(key, userCallback, ZReliability.Reliable)
    {
    }

    public Subscriber(string key, SubscriberCallback userCallback, ZReliability zReliability)
    {
        unsafe
        {
            keyexpr = key;
            _disposed = false;
            options.reliability = zReliability;
            _userCallbackGcHandle = GCHandle.Alloc(userCallback, GCHandleType.Normal);
            _ownedClosureSample = new ZOwnedClosureSample
            {
                context = (void*)GCHandle.ToIntPtr(_userCallbackGcHandle),
                call = Call,
                drop = null,
            };

            nint p = Marshal.AllocHGlobal(Marshal.SizeOf(_ownedClosureSample));
            Marshal.StructureToPtr(_ownedClosureSample, p, false);
            closureSample = (ZOwnedClosureSample*)p;
            ownedSubscriber = null;
        }
    }

    public void Dispose() => Dispose(true);

    private void Dispose(bool disposing)
    {
        if (_disposed) return;

        unsafe
        {
            Marshal.FreeHGlobal((nint)closureSample);
            Marshal.FreeHGlobal((nint)ownedSubscriber);
        }

        _userCallbackGcHandle.Free();
        _disposed = true;
    }

    private static unsafe void Call(ZSample* zSample, void* context)
    {
        var gch = GCHandle.FromIntPtr((nint)context);
        var callback = (SubscriberCallback?)gch.Target;
        var sample = new Sample(zSample);
        if (callback != null)
        {
            callback(sample);
        }
    }
}