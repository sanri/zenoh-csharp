using System;
using System.Runtime.InteropServices;

namespace Zenoh;

public sealed class OpenOptions : IDisposable
{
    // z_open_options*
    internal nint HandleZOpenOptions { get; private set; }

    public OpenOptions()
    {
        var pOpenOptions = Marshal.AllocHGlobal(Marshal.SizeOf<ZOpenOptions>());
        ZenohC.z_open_options_default(pOpenOptions);
        HandleZOpenOptions = pOpenOptions;
    }

    public OpenOptions(OpenOptions other)
    {
        var pTarget = Marshal.AllocHGlobal(Marshal.SizeOf<ZOpenOptions>());
        var openOptions = Marshal.PtrToStructure<ZOpenOptions>(other.HandleZOpenOptions);
        Marshal.StructureToPtr(openOptions, pTarget, false);
        HandleZOpenOptions = pTarget;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~OpenOptions() => Dispose(false);

    private void Dispose(bool disposing)
    {
        if (HandleZOpenOptions == nint.Zero) return;

        Marshal.FreeHGlobal(HandleZOpenOptions);
        HandleZOpenOptions = nint.Zero;
    }
}

public sealed class Session : IDisposable
{
    // z_owned_session*
    internal nint HandleZOwnedSession { get; private set; }

    private Session()
    {
        throw new InvalidOperationException();
    }

    private Session(Session session)
    {
        throw new InvalidOperationException();
    }

    private Session(nint handle)
    {
        HandleZOwnedSession = handle;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~Session() => Dispose(false);

    private void Dispose(bool disposing)
    {
        if (HandleZOwnedSession == nint.Zero) return;

        ZenohC.z_session_drop(HandleZOwnedSession);

        Marshal.FreeHGlobal(HandleZOwnedSession);
        HandleZOwnedSession = nint.Zero;
    }

    internal void CheckDisposed()
    {
        if (HandleZOwnedSession == nint.Zero)
        {
            throw new ObjectDisposedException("Object has been destroyed");
        }
    }

    /// <summary>
    /// <para>
    /// Constructs and opens a new Zenoh session.
    /// </para>
    /// <para>
    /// Do not use the "config" after calling this function.
    /// config.Dispose() is called inside this function.
    /// </para>
    /// </summary>
    /// <param name="config">Zenoh session config</param>
    /// <param name="openOptions"></param>
    /// <param name="session"></param>
    /// <returns>
    /// ZResult.Ok in case of success
    /// </returns>
    public static ZResult Open(Config config, OpenOptions openOptions, out Session? session)
    {
        config.CheckDisposed();

        session = null;
        var pOwnedSession = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedSession>());
        ZenohC.z_internal_session_null(pOwnedSession);
        var configCopy = new Config(config);

        var r = ZenohC.z_open(pOwnedSession, configCopy.HandleZOwnedConfig, openOptions.HandleZOpenOptions);
        configCopy.Dispose();

        if (r == ZResult.Ok)
        {
            session = new Session(pOwnedSession);
        }
        else
        {
            Marshal.FreeHGlobal(pOwnedSession);
        }

        return r;
    }

    /// <summary>
    /// Close the session and free memory. This is equivalent to calling the "Dispose()". 
    /// </summary>
    public void Close()
    {
        Dispose();
    }

    /// <summary>
    /// Create uhlc timestamp from session id.
    /// </summary>
    /// <returns></returns>
    public Timestamp? NewTimestamp()
    {
        CheckDisposed();

        var pLoanedSession = ZenohC.z_session_loan(HandleZOwnedSession);
        return Timestamp.NewFromSession(pLoanedSession);
    }

    /// <summary>
    /// Constructs and declares a publisher for the given key expression.
    /// </summary>
    /// <param name="keyexpr">The key expression to publish</param>
    /// <param name="options">Additional options for the publisher.</param>
    /// <param name="publisher"></param>
    /// <returns></returns>
    public ZResult DeclarePublisher(Keyexpr keyexpr, PublisherOptions options, out Publisher? publisher)
    {
        CheckDisposed();
        
        publisher = null;

        var pLoanedSession = ZenohC.z_session_loan(HandleZOwnedSession);
        var pOwnedPublisher = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedPublisher>());
        var pLoanedKeyexpr = ZenohC.z_keyexpr_loan(keyexpr.HandleZOwnedKeyexpr);
        var pPublisherOptions = options.AllocUnmanagedMem();

        Publisher? o;
        var r = ZenohC.z_declare_publisher(pLoanedSession, pOwnedPublisher, pLoanedKeyexpr, pPublisherOptions);
        if (r == ZResult.Ok)
        {
            publisher = new Publisher(pOwnedPublisher);
        }
        else
        {
            ZenohC.z_publisher_drop(pOwnedPublisher);
            Marshal.FreeHGlobal(pOwnedPublisher);
            publisher = null;
        }

        PublisherOptions.FreeUnmanagedMem(pPublisherOptions);

        return r;
    }

    /// <summary>
    /// Constructs and declares a callback type subscriber for a given key expression. 
    /// </summary>
    /// <param name="keyexpr">
    /// The key expression to subscribe
    /// </param>
    /// <param name="callback">
    /// The callback function that will be called each time a data matching the subscribed expression is received.
    /// </param>
    /// <param name="subscriber"></param>
    /// <returns></returns>
    public ZResult DeclareSubscriberCallback(Keyexpr keyexpr, SubscriberCallback.Cb callback,
        out SubscriberCallback? subscriber)
    {
        CheckDisposed();
        keyexpr.CheckDisposed();
        
        subscriber = null;

        subscriber = new SubscriberCallback(callback);
        var gcHandle = GCHandle.Alloc(subscriber);

        var pOwnedClosureSample = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedClosureSample>());
        ZenohC.z_closure_sample(
            pOwnedClosureSample,
            SubscriberCallback.CallbackClosureSampleCall,
            SubscriberCallback.CallbackClosureSampleDrop,
            GCHandle.ToIntPtr(gcHandle)
        );

        var pLoanedSession = ZenohC.z_session_loan(HandleZOwnedSession);
        var pLoanedKeyexpr = ZenohC.z_keyexpr_loan(keyexpr.HandleZOwnedKeyexpr);

        var r = ZenohC.z_declare_subscriber(
            pLoanedSession,
            subscriber.HandleOwnedSubscriber,
            pLoanedKeyexpr,
            pOwnedClosureSample,
            nint.Zero
        );

        Marshal.FreeHGlobal(pOwnedClosureSample);

        if (r == ZResult.Ok) return ZResult.Ok;

        subscriber.Dispose();
        gcHandle.Free();
        subscriber = null;

        return r;
    }

    /// <summary>
    /// Constructs and declares a callback type subscriber for a given key expression. 
    /// </summary>
    /// <param name="keyexpr">The key expression to subscribe.</param>
    /// <param name="channelType">The buffer channel type selected.</param>
    /// <param name="channelSize">The buffer channel capacity.</param>
    /// <param name="subscriber"></param>
    /// <returns></returns>
    public ZResult DeclareSubscriberBuffer(Keyexpr keyexpr, ChannelType channelType, uint channelSize,
        out SubscriberBuffer? subscriber)
    {
        CheckDisposed();
        keyexpr.CheckDisposed();
        
        subscriber = null;

        subscriber = new SubscriberBuffer(channelType);
        var pOwnedClosureSample = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedClosureSample>());

        switch (channelType)
        {
            case ChannelType.Ring:
                ZenohC.z_ring_channel_sample_new(pOwnedClosureSample, subscriber.HandleChannel, channelSize);
                break;
            case ChannelType.Fifo:
                ZenohC.z_fifo_channel_sample_new(pOwnedClosureSample, subscriber.HandleChannel, channelSize);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(channelType), channelType, null);
        }

        var pLoanedSession = ZenohC.z_session_loan(HandleZOwnedSession);
        var pLoanedKeyexpr = ZenohC.z_keyexpr_loan(keyexpr.HandleZOwnedKeyexpr);

        var r = ZenohC.z_declare_subscriber(
            pLoanedSession,
            subscriber.HandleOwnedSubscriber,
            pLoanedKeyexpr,
            pOwnedClosureSample,
            nint.Zero
        );

        Marshal.FreeHGlobal(pOwnedClosureSample);

        if (r == ZResult.Ok) return ZResult.Ok;

        subscriber.Dispose();
        return r;
    }

    /// <summary>
    /// <para>
    /// Publishes data on specified key expression.
    /// </para>
    /// <para>
    /// Do not use the "payload" after calling this function.
    /// payload.Dispose() is called inside this function.
    /// </para>
    /// </summary>
    /// <param name="keyexpr">The key expression to publish to.</param>
    /// <param name="payload">The value to put.</param>
    /// <param name="options">The put options.</param>
    /// <returns></returns>
    public ZResult Put(Keyexpr keyexpr, ZBytes payload, PutOptions options)
    {
        CheckDisposed();
        keyexpr.CheckDisposed();
        
        var pLoanedSession = ZenohC.z_session_loan(HandleZOwnedSession);
        var pLoanedKeyexpr = ZenohC.z_keyexpr_loan(keyexpr.HandleZOwnedKeyexpr);
        var pMovedBytes = payload.HandleZOwnedBytes;
        var pPutOptions = options.AllocUnmanagedMem();
        
        var r = ZenohC.z_put(pLoanedSession, pLoanedKeyexpr, pMovedBytes, pPutOptions);
        
        PutOptions.FreeUnmanagedMem(pPutOptions);
        payload.Dispose();

        return r;
    }
}

