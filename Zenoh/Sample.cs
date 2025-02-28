using System;
using System.Runtime.InteropServices;

namespace Zenoh;

public sealed class Sample : IDisposable
{
    // 'true' in case z_owned_sample_t, 'false' in case z_loaned_sample_t 
    internal bool Owned { get; }

    // z_loaned_sample_t*  or  z_owned_sample_t*
    internal nint Handle { get; private set; }

    private Sample()
    {
        throw new InvalidOperationException();
    }

    internal Sample(Sample other)
    {
        var pOwnedSample = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedSample>());
        var pLoanedSample = other.Owned ? ZenohC.z_sample_loan(other.Handle) : other.Handle;
        ZenohC.z_sample_clone(pOwnedSample, pLoanedSample);
        Owned = true;
        Handle = pOwnedSample;
    }

    private Sample(nint handle, bool owned)
    {
        Owned = owned;
        Handle = handle;
    }

    internal static Sample CreateLoanedSample(nint handle)
    {
        return new Sample(handle, false);
    }

    internal static Sample CreateOwnedSample()
    {
        var pOwnedSample = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedSample>());
        ZenohC.z_internal_sample_null(pOwnedSample);
        return new Sample(pOwnedSample, true);
    }

    ~Sample()
    {
        Dispose(false);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (Handle == nint.Zero) return;

        if (Owned)
        {
            ZenohC.z_sample_drop(Handle);
            Marshal.FreeHGlobal(Handle);
        }

        Handle = nint.Zero;
    }

    public void CheckDisposed()
    {
        if (Handle == nint.Zero)
        {
            throw new InvalidOperationException("Object is disposed");
        }
    }
    
    /// <summary>
    /// Returns sample attachment.
    /// </summary>
    /// <returns>
    /// Returns 'null', if sample does not contain any attachment.
    /// </returns>
    public ZBytes? GetAttachment()
    {
        CheckDisposed();

        var pLoanedSample = Owned ? ZenohC.z_sample_loan(Handle) : Handle;
        var pLoanedBytes = ZenohC.z_sample_attachment(pLoanedSample);
        return pLoanedBytes == nint.Zero ? null : ZBytes.CloneFromLoaned(pLoanedBytes);
    }

    /// <summary>
    /// Returns sample qos congestion control value.
    /// </summary>
    /// <returns></returns>
    public CongestionControl GetCongestionControl()
    {
        CheckDisposed();

        var pLoanedSample = Owned ? ZenohC.z_sample_loan(Handle) : Handle;
        return ZenohC.z_sample_congestion_control(pLoanedSample);
    }

    /// <summary>
    /// Returns the encoding associated with the sample data.
    /// </summary>
    /// <returns></returns>
    public Encoding GetEncoding()
    {
        CheckDisposed();

        var pLoanedSample = Owned ? ZenohC.z_sample_loan(Handle) : Handle;
        var pLoanedEncoding = ZenohC.z_sample_encoding(pLoanedSample);
        return Encoding.CloneFromLoaned(pLoanedEncoding);
    }

    /// <summary>
    /// <para>Gets the express flag value.</para>
    /// <para>
    /// If true, the message is not batched during transmission, in order to reduce latency.
    /// </para>
    /// </summary>
    public bool GetExpress()
    {
        CheckDisposed();

        var pLoanedSample = Owned ? ZenohC.z_sample_loan(Handle) : Handle;
        return ZenohC.z_sample_express(pLoanedSample);
    }

    /// <summary>
    /// Returns the key expression of the sample.
    /// </summary>
    public Keyexpr GetKeyexpr()
    {
        CheckDisposed();

        var pLoanedSample = Owned ? ZenohC.z_sample_loan(Handle) : Handle;
        var pLoanedKeyexpr = ZenohC.z_sample_keyexpr(pLoanedSample);
        return Keyexpr.CloneFromLoaned(pLoanedKeyexpr);
    }

    /// <summary>
    /// Returns the sample kind.
    /// </summary>
    /// <returns></returns>
    public SampleKind GetKind()
    {
        CheckDisposed();

        var pLoanedSample = Owned ? ZenohC.z_sample_loan(Handle) : Handle;
        return ZenohC.z_sample_kind(pLoanedSample);
    }

    /// <summary>
    /// Returns the sample payload data.
    /// </summary>
    /// <returns></returns>
    public ZBytes GetPayload()
    {
        CheckDisposed();

        var pLoanedSample = Owned ? ZenohC.z_sample_loan(Handle) : Handle;
        var pLoanedBytes = ZenohC.z_sample_payload(pLoanedSample);
        return ZBytes.CloneFromLoaned(pLoanedBytes);
    }

    /// <summary>
    /// Returns sample qos priority value.
    /// </summary>
    /// <returns></returns>
    public Priority GetPriority()
    {
        CheckDisposed();

        var pLoanedSample = Owned ? ZenohC.z_sample_loan(Handle) : Handle;
        return ZenohC.z_sample_priority(pLoanedSample);
    }

    /// <summary>
    /// Returns the sample timestamp.
    /// </summary>
    /// <returns>
    /// Will return 'null', if sample is not associated with a timestamp.
    /// </returns>
    public Timestamp? GetTimestamp()
    {
        CheckDisposed();

        var pLoanedSample = Owned ? ZenohC.z_sample_loan(Handle) : Handle;
        var pTimestamp = ZenohC.z_sample_timestamp(pLoanedSample);
        return pTimestamp == nint.Zero ? null : Timestamp.CloneFromPointer(pTimestamp);
    }
}