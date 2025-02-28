using System;
using System.Runtime.InteropServices;

namespace Zenoh;

public enum ChannelType
{
    Ring,
    Fifo
}

public abstract class Channel : IDisposable
{
    internal nint Handle{get; private protected set;}
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

public abstract class ChannelSample: Channel
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
    /// <para>"ZResult.Ok" in case of success.</para>
    /// <para>"ZResult.ChannelDisconnected" if channel was dropped (the sample will be set to "null").</para>
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
    /// <para>"ZResult.Ok" in case of success.</para>
    /// <para>"ZResult.ChannelDisconnected" if channel was dropped (the sample will be set to "null").</para>
    /// <para>"ZResult.ChannelNodata" if the channel is still alive, but its buffer is empty (the sample will be set to "null").</para>
    /// </returns>
    public abstract Result TryRecv(out Sample? sample);

}

public sealed class ChannelSampleRing : ChannelSample
{
    // z_owned_ring_handler_sample_t
    
    internal ChannelSampleRing()
    {
        ChannelType = ChannelType.Ring;
        var pChannel = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedRingHandlerSample>());
        ZenohC.z_internal_ring_handler_sample_null(pChannel);
        Handle = pChannel;
    }

    public override void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    
    ~ChannelSampleRing()=>Dispose(false);

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
        
        sample = Sample.CreateOwnedSample();
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
        
        sample = Sample.CreateOwnedSample();
        var pLoanedRingHandlerSample = ZenohC.z_ring_handler_sample_loan(Handle);
        
        var r = ZenohC.z_ring_handler_sample_try_recv(pLoanedRingHandlerSample, sample.Handle);
        
        if (r == Result.Ok) return Result.Ok;
        
        sample.Dispose();
        sample = null;
        return r;
    }
}

public sealed class ChannelSampleFifo : ChannelSample
{
    // z_owned_fifo_handler_sample_t
    
    private ChannelSampleFifo()
    {
        ChannelType = ChannelType.Fifo;
        var pChannel = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedFifoHandlerSample>());
        ZenohC.z_internal_fifo_handler_sample_null(pChannel);
        Handle = pChannel;
    }
    
    public override void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    
    ~ChannelSampleFifo()=>Dispose(false);

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
        
        sample = Sample.CreateOwnedSample();
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
        
        sample = Sample.CreateOwnedSample();
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
}

