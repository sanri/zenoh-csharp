using System;
using System.Runtime.InteropServices;

namespace Zenoh;

public sealed class Session : IDisposable
{
    // z_owned_session*
    internal nint Handle { get; private set; }

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
        Handle = handle;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~Session() => Dispose(false);

    private void Dispose(bool disposing)
    {
        if (Handle == nint.Zero) return;

        ZenohC.z_session_drop(Handle);

        Marshal.FreeHGlobal(Handle);
        Handle = nint.Zero;
    }

    internal void CheckDisposed()
    {
        if (Handle == nint.Zero)
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
    public static Result Open(Config config, out Session? session)
    {
        config.CheckDisposed();

        var pOwnedSession = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedSession>());
        ZenohC.z_internal_session_null(pOwnedSession);
        var configCopy = new Config(config);

        var r = ZenohC.z_open(pOwnedSession, configCopy.Handle, nint.Zero);
        configCopy.Dispose();

        if (r == Result.Ok)
        {
            session = new Session(pOwnedSession);
        }
        else
        {
            Marshal.FreeHGlobal(pOwnedSession);
            session = null;
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

        var pLoanedSession = ZenohC.z_session_loan(Handle);
        return Timestamp.NewFromSession(pLoanedSession);
    }

    /// <summary>
    /// Constructs and declares a publisher for the given key expression.
    /// </summary>
    /// <param name="keyexpr">The key expression to publish</param>
    /// <param name="options">Additional options for the publisher.</param>
    /// <param name="publisher"></param>
    /// <returns></returns>
    public Result DeclarePublisher(Keyexpr keyexpr, PublisherOptions options, out Publisher? publisher)
    {
        CheckDisposed();

        var pLoanedSession = ZenohC.z_session_loan(Handle);
        var pLoanedKeyexpr = keyexpr.LoanedPointer();
        var pPublisherOptions = options.AllocUnmanagedMemory();
        publisher = new Publisher();

        var r = ZenohC.z_declare_publisher(pLoanedSession, publisher.Handle, pLoanedKeyexpr, pPublisherOptions);

        PublisherOptions.FreeUnmanagedMemory(pPublisherOptions);

        if (r == Result.Ok) return Result.Ok;

        publisher.Dispose();
        publisher = null;
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
    public Result DeclareSubscriber(Keyexpr keyexpr, Subscriber.Cb callback,
        out Subscriber? subscriber)
    {
        CheckDisposed();
        keyexpr.CheckDisposed();

        var gcHandle = GCHandle.Alloc(callback);

        var pOwnedClosureSample = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedClosureSample>());
        ZenohC.z_closure_sample(
            pOwnedClosureSample,
            Subscriber.CallbackClosureSampleCall,
            Subscriber.CallbackClosureSampleDrop,
            GCHandle.ToIntPtr(gcHandle)
        );

        var pLoanedSession = ZenohC.z_session_loan(Handle);
        var pLoanedKeyexpr = keyexpr.LoanedPointer();
        subscriber = new Subscriber();

        var r = ZenohC.z_declare_subscriber(
            pLoanedSession,
            subscriber.Handle,
            pLoanedKeyexpr,
            pOwnedClosureSample,
            nint.Zero
        );

        Marshal.FreeHGlobal(pOwnedClosureSample);

        if (r == Result.Ok) return Result.Ok;

        subscriber.Dispose();
        gcHandle.Free();
        subscriber = null;

        return r;
    }

    /// <summary>
    /// Constructs and declares a subscriber for a given key expression. 
    /// </summary>
    /// <param name="keyexpr">The key expression to subscribe.</param>
    /// <param name="channelType">The buffer channel type selected.</param>
    /// <param name="channelSize">The buffer channel capacity.</param>
    /// <param name="handler"></param>
    /// <returns></returns>
    public Result DeclareSubscriber(Keyexpr keyexpr, ChannelType channelType, uint channelSize,
        out (Subscriber, ChannelSample)? handler)
    {
        CheckDisposed();
        keyexpr.CheckDisposed();

        var pOwnedClosureSample = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedClosureSample>());

        ChannelSample channel;
        switch (channelType)
        {
            case ChannelType.Ring:
                channel = new ChannelSampleRing();
                ZenohC.z_ring_channel_sample_new(pOwnedClosureSample, channel.Handle, channelSize);
                break;
            case ChannelType.Fifo:
                channel = new ChannelSampleFifo();
                ZenohC.z_fifo_channel_sample_new(pOwnedClosureSample, channel.Handle, channelSize);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(channelType), channelType, null);
        }

        Subscriber subscriber = new Subscriber();
        var pLoanedSession = ZenohC.z_session_loan(Handle);
        var pLoanedKeyexpr = keyexpr.LoanedPointer();

        var r = ZenohC.z_declare_subscriber(
            pLoanedSession,
            subscriber.Handle,
            pLoanedKeyexpr,
            pOwnedClosureSample,
            nint.Zero
        );

        Marshal.FreeHGlobal(pOwnedClosureSample);

        if (r == Result.Ok)
        {
            handler = (subscriber, channel);
            return Result.Ok;
        }

        subscriber.Dispose();
        channel.Dispose();
        handler = null;
        return r;
    }


    /// <summary>
    /// Constructs a callback type Queryable for the given key expression.
    /// </summary>
    /// <param name="keyexpr"></param>
    /// <param name="options"></param>
    /// <param name="callback"></param>
    /// <param name="queryable"></param>
    /// <returns></returns>
    public Result DeclareQueryable(Keyexpr keyexpr, QueryableOptions options, Queryable.Cb callback,
        out Queryable? queryable)
    {
        CheckDisposed();
        keyexpr.CheckDisposed();

        var gcHandle = GCHandle.Alloc(callback);
        var pOwnedClosureQuery = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedClosureQuery>());
        ZenohC.z_closure_query(pOwnedClosureQuery, Queryable.CallbackClosureQueryCall,
            Queryable.CallbackClosureQueryDrop,
            GCHandle.ToIntPtr(gcHandle));

        var pLoanedSession = ZenohC.z_session_loan(Handle);
        var pLoanedKeyexpr = keyexpr.LoanedPointer();
        var pOptions = options.AllocUnmanagedMem();
        queryable = new Queryable();

        var r = ZenohC.z_declare_queryable(pLoanedSession, queryable.Handle, pLoanedKeyexpr, pOwnedClosureQuery,
            pOptions);

        Marshal.FreeHGlobal(pOwnedClosureQuery);
        QueryableOptions.FreeUnmanagedMem(pOptions);

        if (r == Result.Ok) return Result.Ok;

        queryable.Dispose();
        queryable = null;
        return r;
    }

    /// <summary>
    /// Constructs a Queryable for the given key expression.
    /// </summary>
    /// <param name="keyexpr"></param>
    /// <param name="options"></param>
    /// <param name="channelType"></param>
    /// <param name="channelSize"></param>
    /// <param name="handler"></param>
    /// <returns></returns>
    public Result DeclareQueryable(Keyexpr keyexpr, QueryableOptions options, ChannelType channelType, uint channelSize,
        out (Queryable, ChannelQuery)? handler)
    {
        CheckDisposed();
        keyexpr.CheckDisposed();

        var pOwnedClosureQuery = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedClosureQuery>());

        ChannelQuery channel;
        switch (channelType)
        {
            case ChannelType.Ring:
                channel = new ChannelQueryRing();
                ZenohC.z_ring_channel_query_new(pOwnedClosureQuery, channel.Handle, channelSize);
                break;
            case ChannelType.Fifo:
                channel = new ChannelQueryFifo();
                ZenohC.z_fifo_channel_query_new(pOwnedClosureQuery, channel.Handle, channelSize);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(channelType), channelType, null);
        }

        var pOptions = options.AllocUnmanagedMem();
        var pLoanedSession = ZenohC.z_session_loan(Handle);
        var pLoanedKeyexpr = keyexpr.LoanedPointer();
        var queryable = new Queryable();

        var r = ZenohC.z_declare_queryable(pLoanedSession, queryable.Handle, pLoanedKeyexpr, pOwnedClosureQuery,
            pOptions);
        QueryableOptions.FreeUnmanagedMem(pOptions);
        Marshal.FreeHGlobal(pOwnedClosureQuery);

        if (r == Result.Ok)
        {
            handler = (queryable, channel);
            return Result.Ok;
        }

        queryable.Dispose();
        channel.Dispose();
        handler = null;
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
    public Result Put(Keyexpr keyexpr, ZBytes payload, PutOptions options)
    {
        CheckDisposed();
        keyexpr.CheckDisposed();
        payload.CheckDisposed();
        payload.ToOwned();

        var pLoanedSession = ZenohC.z_session_loan(Handle);
        var pLoanedKeyexpr = keyexpr.LoanedPointer();
        var pPutOptions = options.AllocUnmanagedMemory();

        var r = ZenohC.z_put(pLoanedSession, pLoanedKeyexpr, payload.Handle, pPutOptions);

        PutOptions.FreeUnmanagedMemory(pPutOptions);
        payload.Dispose();
        return r;
    }


    /// <summary>
    /// Query data from the matching queryable in the system.
    /// </summary>
    /// <param name="keyexpr">The key expression to subscribe.</param>
    /// <param name="options"></param>
    /// <param name="parameters"></param>
    /// <param name="channelType">The buffer channel type selected.</param>
    /// <param name="channelSize">The buffer channel capacity.</param>
    /// <param name="channel"></param>
    /// <returns></returns>
    public Result Get(Keyexpr keyexpr, GetOptions options, string? parameters, ChannelType channelType,
        uint channelSize,
        out ChannelReply? channel)
    {
        CheckDisposed();
        keyexpr.CheckDisposed();

        var pOwnedClosureReply = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedClosureReply>());
        switch (channelType)
        {
            case ChannelType.Ring:
                channel = new ChannelReplyRing();
                ZenohC.z_ring_channel_reply_new(pOwnedClosureReply, channel.Handle, channelSize);
                break;
            case ChannelType.Fifo:
                channel = new ChannelReplyFifo();
                ZenohC.z_fifo_channel_reply_new(pOwnedClosureReply, channel.Handle, channelSize);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(channelType), channelType, null);
        }

        var pLoanedSession = ZenohC.z_session_loan(Handle);
        var pLoanedKeyexpr = keyexpr.LoanedPointer();
        var pParameters = Marshal.StringToHGlobalAnsi(parameters);
        var pGetOptions = options.AllocUnmanagedMemory();

        var r = ZenohC.z_get(pLoanedSession, pLoanedKeyexpr, pParameters, pOwnedClosureReply, pGetOptions);

        ZenohC.z_closure_reply_drop(pOwnedClosureReply);
        Marshal.FreeHGlobal(pOwnedClosureReply);
        Marshal.FreeHGlobal(pParameters);
        GetOptions.FreeUnmanagedMemory(pGetOptions);

        if (r == Result.Ok) return r;

        channel.Dispose();
        channel = null;
        return r;
    }
}