using System;
using System.Runtime.InteropServices;

namespace Zenoh;

/// <summary>
/// Callback type subscriber.
/// </summary>
public class SubscriberCallback : IDisposable
{
    // z_owned_subscriber_t*
    internal nint HandleOwnedSubscriber { get; private set; }
    private readonly Cb _cb;

    public delegate void Cb(Sample sample);


    private SubscriberCallback()
    {
        throw new InvalidOperationException();
    }

    internal SubscriberCallback(Cb cb)
    {
        var pOwnedSubscriber = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedSubscriber>());
        ZenohC.z_internal_subscriber_null(pOwnedSubscriber);
        HandleOwnedSubscriber = pOwnedSubscriber;
        _cb = cb;
    }

    private SubscriberCallback(SubscriberCallback other)
    {
        throw new InvalidOperationException();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~SubscriberCallback() => Dispose(false);

    private void Dispose(bool disposing)
    {
        if (HandleOwnedSubscriber == nint.Zero) return;

        ZenohC.z_subscriber_drop(HandleOwnedSubscriber);
        Marshal.FreeHGlobal(HandleOwnedSubscriber);
        HandleOwnedSubscriber = nint.Zero;
    }

    internal void CheckDisposed()
    {
        if (HandleOwnedSubscriber == nint.Zero)
        {
            throw new InvalidOperationException("Object has been destroyed");
        }
    }

    /// <summary>
    /// Returns the key expression of the subscriber.
    /// </summary>
    /// <returns></returns>
    public Keyexpr GetKeyexpr()
    {
        CheckDisposed();

        var pLoanedSubscriber = ZenohC.z_subscriber_loan(HandleOwnedSubscriber);
        var pLoanedKeyexpr = ZenohC.z_subscriber_keyexpr(pLoanedSubscriber);
        return Keyexpr.CloneFromLoaned(pLoanedKeyexpr);
    }


    /// <summary>
    /// Undeclare the subscriber and free memory. This is equivalent to calling the "Dispose()".
    /// </summary>
    public void Undeclare()
    {
        Dispose();
    }

    internal static void CallbackClosureSampleCall(nint sample, nint context)
    {
        var gcHandle = GCHandle.FromIntPtr(context);
        if (gcHandle.Target is not SubscriberCallback subscriber) return;

        var loanedSample = Sample.CreateLoanedSample(sample);
        subscriber._cb(loanedSample);
    }

    internal static void CallbackClosureSampleDrop(nint context)
    {
        var gcHandle = GCHandle.FromIntPtr(context);
        gcHandle.Free();
    }
}

/// <summary>
/// <para>
/// Buffer channel type subscriber. There are ring or fifo buffer channel inside.
/// </para>
/// <para>
/// Ring buffer channel,
/// a synchronous ring channel with a limited size that allows users to keep the last N data.
/// RingChannel implements FIFO semantics with a dropping strategy when full.
/// The oldest elements will be dropped when newer arrive.
/// </para>
/// <para>
/// Fifo buffer channel,
/// that pushing on a full FifoChannel that is full will block until a slot is available.
/// E.g., a slow subscriber could block the underlying Zenoh thread because is not emptying the FifoChannel fast enough.
/// In this case, you may want to look into RingChannel that will drop samples when full.
/// </para>
/// </summary>
public class SubscriberBuffer : IDisposable
{
    // z_owned_subscriber_t*
    internal nint HandleOwnedSubscriber { get; private set; }

    // z_owned_ring_handler_sample_t*  or  z_owned_fifo_handler_sample_t*
    internal nint HandleChannel { get; private set; }

    public readonly ChannelType BufferChannelType;

    internal SubscriberBuffer(ChannelType ct, uint size)
    {
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~SubscriberBuffer() => Dispose(false);

    private void Dispose(bool disposing)
    {
        // todo
    }
}