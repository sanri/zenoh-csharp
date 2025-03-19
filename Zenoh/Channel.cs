using System;
using System.Runtime.InteropServices;

namespace Zenoh;

/// <summary>
/// Buffer channel type. There are ring or fifo buffer channel inside.
/// </summary>
public enum ChannelType
{
    /// <summary>
    /// Ring buffer channel,
    /// a synchronous ring channel with a limited size that allows users to keep the last N data.
    /// RingChannel implements FIFO semantics with a dropping strategy when full.
    /// The oldest elements will be dropped when newer arrive.
    /// </summary>
    Ring,

    /// <summary>
    /// Fifo buffer channel,
    /// that pushing on a full FifoChannel that is full will block until a slot is available.
    /// E.g., a slow subscriber could block the underlying Zenoh thread because it is not emptying the FifoChannel fast enough.
    /// In this case, you may want to look into RingChannel that will drop samples when full.
    /// </summary>
    Fifo
}

public abstract class Channel : IDisposable
{
    internal nint Handle { get; private protected set; }
    public ChannelType ChannelType { get; private protected set; }

    public abstract void Dispose();

    internal void CheckDisposed()
    {
        if (Handle == nint.Zero)
        {
            throw new InvalidOperationException("Channel is disposed");
        }
    }
}

public abstract class ChannelSample : Channel
{
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
    /// <para>"Result.Ok" in case of success.</para>
    /// <para>"Result.ChannelDisconnected" if channel was dropped (the sample will be set to "null").</para>
    /// </returns>
    public abstract Result Recv(out Sample? sample);

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
    /// <para>"Result.Ok" in case of success.</para>
    /// <para>"Result.ChannelDisconnected" if channel was dropped (the sample will be set to "null").</para>
    /// <para>"Result.ChannelNodata" if the channel is still alive, but its buffer is empty (the sample will be set to "null").</para>
    /// </returns>
    public abstract Result TryRecv(out Sample? sample);
}

// z_owned_ring_handler_sample_t
public sealed class ChannelSampleRing : ChannelSample
{
    internal ChannelSampleRing()
    {
        ChannelType = ChannelType.Ring;
        var pChannel = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedRingHandlerSample>());
        ZenohC.z_internal_ring_handler_sample_null(pChannel);
        Handle = pChannel;
    }

    private ChannelSampleRing(ChannelSampleRing other)
    {
        throw new InvalidOperationException();
    }

    public override void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~ChannelSampleRing() => Dispose(false);

    private void Dispose(bool disposing)
    {
        if (Handle == nint.Zero) return;

        ZenohC.z_ring_handler_sample_drop(Handle);
        Marshal.FreeHGlobal(Handle);
        Handle = nint.Zero;
    }

    public override Result Recv(out Sample? sample)
    {
        CheckDisposed();

        sample = Sample.CreateOwned();
        var pLoanedRingHandlerSample = ZenohC.z_ring_handler_sample_loan(Handle);

        var r = ZenohC.z_ring_handler_sample_recv(pLoanedRingHandlerSample, sample.Handle);

        if (r == Result.Ok) return Result.Ok;

        sample.Dispose();
        sample = null;
        return r;
    }

    public override Result TryRecv(out Sample? sample)
    {
        CheckDisposed();

        sample = Sample.CreateOwned();
        var pLoanedRingHandlerSample = ZenohC.z_ring_handler_sample_loan(Handle);

        var r = ZenohC.z_ring_handler_sample_try_recv(pLoanedRingHandlerSample, sample.Handle);

        if (r == Result.Ok) return Result.Ok;

        sample.Dispose();
        sample = null;
        return r;
    }
}

// z_owned_fifo_handler_sample_t
public sealed class ChannelSampleFifo : ChannelSample
{
    internal ChannelSampleFifo()
    {
        ChannelType = ChannelType.Fifo;
        var pChannel = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedFifoHandlerSample>());
        ZenohC.z_internal_fifo_handler_sample_null(pChannel);
        Handle = pChannel;
    }

    private ChannelSampleFifo(ChannelSampleFifo other)
    {
        throw new InvalidOperationException();
    }

    public override void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~ChannelSampleFifo() => Dispose(false);

    private void Dispose(bool disposing)
    {
        if (Handle == nint.Zero) return;

        ZenohC.z_fifo_handler_sample_drop(Handle);
        Marshal.FreeHGlobal(Handle);
        Handle = nint.Zero;
    }

    public override Result Recv(out Sample? sample)
    {
        CheckDisposed();

        sample = Sample.CreateOwned();
        var pLoanedFifoHandlerSample = ZenohC.z_fifo_handler_sample_loan(Handle);

        var r = ZenohC.z_fifo_handler_sample_recv(pLoanedFifoHandlerSample, sample.Handle);

        if (r == Result.Ok) return Result.Ok;

        sample.Dispose();
        sample = null;
        return r;
    }

    public override Result TryRecv(out Sample? sample)
    {
        CheckDisposed();

        sample = Sample.CreateOwned();
        var pLoanedFifoHandlerSample = ZenohC.z_fifo_handler_sample_loan(Handle);

        var r = ZenohC.z_fifo_handler_sample_try_recv(pLoanedFifoHandlerSample, sample.Handle);

        if (r == Result.Ok) return Result.Ok;

        sample.Dispose();
        sample = null;
        return r;
    }
}

public abstract class ChannelReply : Channel
{
    /// <summary>
    /// <para>
    /// Returns reply from the buffer channel.
    /// </para>
    /// <para>
    /// If there are no more pending replies will block until next sample is received,
    /// or until the channel is dropped (normally when there are no more replies to receive).
    /// </para>
    /// </summary>
    /// <param name="reply"></param>
    /// <returns>
    /// <para>"Result.Ok" in case of success.</para>
    /// <para>"Result.ChannelDisconnected" if channel was dropped (the reply will be set to "null").</para>
    /// </returns>
    public abstract Result Recv(out Reply? reply);

    /// <summary>
    /// <para>
    /// Returns reply from the buffer channel.
    /// </para>
    /// <para>
    /// If there are no more pending replies will return immediately (with reply set to null).
    /// </para>
    /// </summary>
    /// <param name="reply"></param>
    /// <returns>
    /// <para>"Result.Ok" in case of success.</para>
    /// <para>"Result.ChannelDisconnected" if channel was dropped (the reply will be set to "null").</para>
    /// <para>"Result.ChannelNodata" if the channel is still alive, but its buffer is empty (the reply will be set to "null").</para>
    /// </returns>
    public abstract Result TryRecv(out Reply? reply);
}

// z_owned_ring_handler_reply_t
public sealed class ChannelReplyRing : ChannelReply
{
    internal ChannelReplyRing()
    {
        ChannelType = ChannelType.Ring;
        var pOwnedRingHandlerReply = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedRingHandlerReply>());
        ZenohC.z_internal_ring_handler_reply_null(pOwnedRingHandlerReply);
        Handle = pOwnedRingHandlerReply;
    }

    private ChannelReplyRing(ChannelQueryRing other)
    {
        throw new InvalidOperationException();
    }

    public override void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~ChannelReplyRing() => Dispose(false);

    private void Dispose(bool disposing)
    {
        if (Handle == nint.Zero) return;

        ZenohC.z_ring_handler_reply_drop(Handle);
        Marshal.FreeHGlobal(Handle);
        Handle = nint.Zero;
    }

    public override Result Recv(out Reply? reply)
    {
        CheckDisposed();

        reply = new Reply();
        var pLoanedRingHandlerReply = ZenohC.z_ring_handler_reply_loan(Handle);
        var r = ZenohC.z_ring_handler_reply_recv(pLoanedRingHandlerReply, reply.Handle);

        if (r == Result.Ok) return Result.Ok;

        reply.Dispose();
        reply = null;
        return r;
    }

    public override Result TryRecv(out Reply? reply)
    {
        CheckDisposed();

        reply = new Reply();
        var pLoanedRingHandlerReply = ZenohC.z_ring_handler_reply_loan(Handle);
        var r = ZenohC.z_ring_handler_reply_try_recv(pLoanedRingHandlerReply, reply.Handle);

        if (r == Result.Ok) return Result.Ok;

        reply.Dispose();
        reply = null;
        return r;
    }
}

// z_owned_fifo_handler_reply_t
public sealed class ChannelReplyFifo : ChannelReply
{
    internal ChannelReplyFifo()
    {
        ChannelType = ChannelType.Fifo;
        var pOwnedFifoHandlerReply = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedFifoHandlerReply>());
        ZenohC.z_internal_fifo_handler_reply_null(pOwnedFifoHandlerReply);
        Handle = pOwnedFifoHandlerReply;
    }

    private ChannelReplyFifo(ChannelQueryFifo other)
    {
        throw new InvalidOperationException();
    }

    public override void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~ChannelReplyFifo() => Dispose(false);

    private void Dispose(bool disposing)
    {
        if (Handle == nint.Zero) return;

        ZenohC.z_fifo_handler_reply_drop(Handle);
        Marshal.FreeHGlobal(Handle);
        Handle = nint.Zero;
    }

    public override Result Recv(out Reply? reply)
    {
        CheckDisposed();

        reply = new Reply();
        var pLoanedFifoHandlerReply = ZenohC.z_fifo_handler_reply_loan(Handle);
        var r = ZenohC.z_fifo_handler_reply_recv(pLoanedFifoHandlerReply, reply.Handle);

        if (r == Result.Ok) return Result.Ok;

        reply.Dispose();
        reply = null;
        return r;
    }

    public override Result TryRecv(out Reply? reply)
    {
        CheckDisposed();

        reply = new Reply();
        var pLoanedFifoHandlerReply = ZenohC.z_fifo_handler_reply_loan(Handle);
        var r = ZenohC.z_fifo_handler_reply_try_recv(pLoanedFifoHandlerReply, reply.Handle);

        if (r == Result.Ok) return Result.Ok;

        reply.Dispose();
        reply = null;
        return r;
    }
}

public abstract class ChannelQuery : Channel
{
    /// <summary>
    /// <para>
    /// Returns query from the buffer channel.
    /// </para>
    /// <para>
    /// If there are no more pending replies will block until next sample is received,
    /// or until the channel is dropped (normally when there are no more replies to receive).
    /// </para>
    /// </summary>
    /// <param name="query"></param>
    /// <returns>
    /// <para>"Result.Ok" in case of success.</para>
    /// <para>"Result.ChannelDisconnected" if channel was dropped (the reply will be set to "null").</para>
    /// </returns>
    public abstract Result Recv(out Query? query);

    /// <summary>
    /// <para>
    /// Returns query from the buffer channel.
    /// </para>
    /// <para>
    /// If there are no more pending replies will return immediately (with reply set to null).
    /// </para>
    /// </summary>
    /// <param name="query"></param>
    /// <returns>
    /// <para>"Result.Ok" in case of success.</para>
    /// <para>"Result.ChannelDisconnected" if channel was dropped (the reply will be set to "null").</para>
    /// <para>"Result.ChannelNodata" if the channel is still alive, but its buffer is empty (the reply will be set to "null").</para>
    /// </returns>
    public abstract Result TryRecv(out Query? query);
}

// z_owned_ring_handler_query_t
public sealed class ChannelQueryRing : ChannelQuery
{
    internal ChannelQueryRing()
    {
        ChannelType = ChannelType.Ring;
        var pOwnedRingHandlerQuery = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedRingHandlerQuery>());
        ZenohC.z_internal_ring_handler_query_null(pOwnedRingHandlerQuery);
        Handle = pOwnedRingHandlerQuery;
    }

    private ChannelQueryRing(ChannelQueryRing other)
    {
        throw new InvalidOperationException();
    }

    public override void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~ChannelQueryRing() => Dispose(false);

    private void Dispose(bool disposing)
    {
        if (Handle == nint.Zero) return;

        ZenohC.z_ring_handler_query_drop(Handle);
        Marshal.FreeHGlobal(Handle);
        Handle = nint.Zero;
    }

    public override Result Recv(out Query? query)
    {
        CheckDisposed();

        query = new Query();
        var pLoanedRingHandlerQuery = ZenohC.z_ring_handler_query_loan(Handle);
        var r = ZenohC.z_ring_handler_query_recv(pLoanedRingHandlerQuery, query.Handle);

        if (r == Result.Ok) return Result.Ok;

        query.Dispose();
        query = null;
        return r;
    }

    public override Result TryRecv(out Query? query)
    {
        CheckDisposed();

        query = new Query();
        var pLoanedRingHandlerQuery = ZenohC.z_ring_handler_query_loan(Handle);
        var r = ZenohC.z_ring_handler_query_try_recv(pLoanedRingHandlerQuery, query.Handle);

        if (r == Result.Ok) return Result.Ok;
        query.Dispose();
        query = null;
        return r;
    }
}

// z_owned_fifo_handler_query_t
public sealed class ChannelQueryFifo : ChannelQuery
{
    internal ChannelQueryFifo()
    {
        ChannelType = ChannelType.Fifo;
        var pOwnedFifoHandlerQuery = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedFifoHandlerQuery>());
        ZenohC.z_internal_fifo_handler_query_null(pOwnedFifoHandlerQuery);
        Handle = pOwnedFifoHandlerQuery;
    }

    private ChannelQueryFifo(ChannelQueryFifo other)
    {
        throw new InvalidOperationException();
    }

    public override void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~ChannelQueryFifo() => Dispose(false);

    private void Dispose(bool disposing)
    {
        if (Handle == nint.Zero) return;

        ZenohC.z_fifo_handler_query_drop(Handle);
        Marshal.FreeHGlobal(Handle);
        Handle = nint.Zero;
    }

    public override Result Recv(out Query? query)
    {
        CheckDisposed();

        query = new Query();
        var pLoanedFifoHandlerQuery = ZenohC.z_fifo_handler_query_loan(Handle);
        var r = ZenohC.z_fifo_handler_query_recv(pLoanedFifoHandlerQuery, query.Handle);

        if (r == Result.Ok) return Result.Ok;

        query.Dispose();
        query = null;
        return r;
    }

    public override Result TryRecv(out Query? query)
    {
        query = new Query();
        var pLoanedFifoHandlerQuery = ZenohC.z_fifo_handler_query_loan(Handle);
        var r = ZenohC.z_fifo_handler_query_try_recv(pLoanedFifoHandlerQuery, query.Handle);

        if (r == Result.Ok) return Result.Ok;

        query.Dispose();
        query = null;
        return r;
    }
}