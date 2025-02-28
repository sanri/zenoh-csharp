using System;
using System.Runtime.InteropServices;

namespace Zenoh;

/// <summary>
/// Callback type subscriber.
/// </summary>
public sealed class SubscriberCallback : IDisposable
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
    /// Undeclare the subscriber and free memory. This is equivalent to calling the "Dispose()".
    /// </summary>
    public void Undeclare()
    {
        Dispose();
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
/// E.g., a slow subscriber could block the underlying Zenoh thread because it is not emptying the FifoChannel fast enough.
/// In this case, you may want to look into RingChannel that will drop samples when full.
/// </para>
/// </summary>
public sealed class SubscriberBuffer : IDisposable
{
    // z_owned_subscriber_t*
    internal nint HandleOwnedSubscriber { get; private set; }

    // z_owned_ring_handler_sample_t*  or  z_owned_fifo_handler_sample_t*
    internal nint HandleChannel { get; private set; }

    public readonly ChannelType BufferChannelType;

    private SubscriberBuffer()
    {
        throw new InvalidOperationException();
    }

    private SubscriberBuffer(SubscriberBuffer other)
    {
        throw new InvalidOperationException();
    }

    internal SubscriberBuffer(ChannelType ct)
    {
        var pOwnedSubscriber = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedSubscriber>());
        ZenohC.z_internal_subscriber_null(pOwnedSubscriber);
        nint pChannel;
        switch (ct)
        {
            case ChannelType.Ring:
                pChannel = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedRingHandlerSample>());
                ZenohC.z_internal_ring_handler_sample_null(pChannel);
                break;
            case ChannelType.Fifo:
                pChannel = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedFifoHandlerSample>());
                ZenohC.z_internal_fifo_handler_sample_null(pChannel);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(ct), ct, null);
        }

        BufferChannelType = ct;
        HandleOwnedSubscriber = pOwnedSubscriber;
        HandleChannel = pChannel;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~SubscriberBuffer() => Dispose(false);

    private void Dispose(bool disposing)
    {
        if (HandleOwnedSubscriber == nint.Zero) return;

        ZenohC.z_subscriber_drop(HandleOwnedSubscriber);
        Marshal.FreeHGlobal(HandleOwnedSubscriber);
        HandleOwnedSubscriber = nint.Zero;

        if (HandleChannel == nint.Zero) return;

        switch (BufferChannelType)
        {
            case ChannelType.Ring:
                ZenohC.z_ring_handler_sample_drop(HandleChannel);
                break;
            case ChannelType.Fifo:
                ZenohC.z_fifo_handler_sample_drop(HandleChannel);
                break;
            default:
                return;
        }

        Marshal.FreeHGlobal(HandleChannel);
        HandleChannel = nint.Zero;
    }

    internal void CheckDisposed()
    {
        if (HandleOwnedSubscriber == nint.Zero)
        {
            throw new InvalidOperationException("Object has been destroyed");
        }

        if (HandleChannel == nint.Zero)
        {
            throw new InvalidOperationException("Object has been destroyed");
        }
    }

    /// <summary>
    /// Undeclare the subscriber and free memory. This is equivalent to calling the "Dispose()".
    /// </summary>
    public void Undeclare()
    {
        Dispose();
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
    /// <para>
    /// Returns sample from the buffer channel.
    /// </para>
    /// <para>
    /// If there are no more pending replies will block until next sample is received,
    /// or until the channel is dropped (normally when there are no more samples to receive).
    /// </para>
    /// </summary>
    /// <param name="sample"></param>
    /// <returns>
    /// <para>"ZResult.Ok" in case of success.</para>
    /// <para>"ZResult.ChannelDisconnected" if channel was dropped (the sample will be set to "null").</para>
    /// </returns>
    public Result Recv(out Sample? sample)
    {
        CheckDisposed();

        Result r;
        sample = Sample.CreateOwnedSample();

        switch (BufferChannelType)
        {
            case ChannelType.Ring:
                var pLoanedRingHandlerSample = ZenohC.z_ring_handler_sample_loan(HandleChannel);
                r = ZenohC.z_ring_handler_sample_recv(pLoanedRingHandlerSample, sample.Handle);
                if (r == Result.Ok) return r;
                break;
            case ChannelType.Fifo:
                var pLoanedFifoHandlerSample = ZenohC.z_fifo_handler_sample_loan(HandleChannel);
                r = ZenohC.z_fifo_handler_sample_recv(pLoanedFifoHandlerSample, sample.Handle);
                if (r == Result.Ok) return r;
                break;
            default:
                r = Result.ErrorGeneric;
                break;
        }

        sample.Dispose();
        sample = null;
        return r;
    }

    /// <summary>
    /// <para>
    /// Returns sample from the buffer channel.
    /// </para>
    /// <para>
    /// If there are no more pending replies will return immediately (with sample set to null).
    /// </para>
    /// </summary>
    /// <param name="sample"></param>
    /// <returns>
    /// <para>"ZResult.Ok" in case of success.</para>
    /// <para>"ZResult.ChannelDisconnected" if channel was dropped (the sample will be set to "null").</para>
    /// <para>"ZResult.ChannelNodata" if the channel is still alive, but its buffer is empty (the sample will be set to "null").</para>
    /// </returns>
    public Result TryRecv(out Sample? sample)
    {
        CheckDisposed();

        Result r;
        sample = Sample.CreateOwnedSample();

        switch (BufferChannelType)
        {
            case ChannelType.Ring:
                var pLoanedRingHandlerSample = ZenohC.z_ring_handler_sample_loan(HandleChannel);
                r = ZenohC.z_ring_handler_sample_try_recv(pLoanedRingHandlerSample, sample.Handle);
                if (r == Result.Ok) return r;
                break;
            case ChannelType.Fifo:
                var pLoanedFifoHandlerSample = ZenohC.z_fifo_handler_sample_loan(HandleChannel);
                r = ZenohC.z_fifo_handler_sample_try_recv(pLoanedFifoHandlerSample, sample.Handle);
                if (r == Result.Ok) return r;
                break;
            default:
                r = Result.ErrorGeneric;
                break;
        }

        sample.Dispose();
        sample = null;
        return r;
    }
}