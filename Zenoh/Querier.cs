using System;
using System.Runtime.InteropServices;

namespace Zenoh
{
    // z_get_options_t
    public sealed class GetOptions : IDisposable
    {
        public QueryTarget Target { get; set; }

        public ConsolidationMode Consolidation { get; set; }

        public CongestionControl CongestionControl { get; set; }

        public Priority Priority { get; set; }

        ///  The timeout for the query in milliseconds. 0 means default query timeout from zenoh configuration.
        public ulong Timeout { get; set; }

        public Encoding? Encoding
        {
            get => _encoding is null ? null : new Encoding(_encoding);
            set => _encoding = value is null ? null : new Encoding(value);
        }

        private Encoding? _encoding;

        public ZBytes? Payload
        {
            get => _payload is null ? null : new ZBytes(_payload);
            set => _payload = value is null ? null : new ZBytes(value);
        }

        private ZBytes? _payload;


        public ZBytes? Attachment
        {
            get => _attachment is null ? null : new ZBytes(_attachment);
            set => _attachment = value is null ? null : new ZBytes(value);
        }

        private ZBytes? _attachment;

#if UNSTABLE_API
        public Locality AllowedDestination { get; set; }

        public ReplyKeyexpr AcceptReplies { get; set; }

        public SourceInfo? SourceInfo
        {
            get => _sourceInfo is null ? null : new SourceInfo(_sourceInfo);
            set => _sourceInfo = value is null ? null : new SourceInfo(value);
        }

        private SourceInfo? _sourceInfo;
#endif

        public GetOptions()
        {
            var pGetOptions = Marshal.AllocHGlobal(Marshal.SizeOf<ZGetOptions>());
            ZenohC.z_get_options_default(pGetOptions);
            var options = Marshal.PtrToStructure<ZGetOptions>(pGetOptions);
            Marshal.FreeHGlobal(pGetOptions);

            Target = options.target;
            Consolidation = options.consolidation.mode;
            CongestionControl = options.congestion_control;
            Priority = options.priority;
            Timeout = options.timeout_ms;
            _encoding = null;
            _payload = null;
            _attachment = null;
#if UNSTABLE_API
            AllowedDestination = options.allowed_destination;
            AcceptReplies = options.accept_replies;
            _sourceInfo = null;
#endif
        }

        public GetOptions(GetOptions other)
        {
            Target = other.Target;
            Consolidation = other.Consolidation;
            CongestionControl = other.CongestionControl;
            Priority = other.Priority;
            Timeout = other.Timeout;
            _encoding = other.Encoding;
            _payload = other.Payload;
            _attachment = other.Attachment;
#if UNSTABLE_API
            AllowedDestination = other.AllowedDestination;
            AcceptReplies = other.AcceptReplies;
            _sourceInfo = other.SourceInfo;
#endif
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~GetOptions() => Dispose(false);

        private void Dispose(bool disposing)
        {
            if (_encoding is null == false)
            {
                if (disposing)
                {
                    _encoding.Dispose();
                }

                _encoding = null;
            }

            if (_payload is null == false)
            {
                if (disposing)
                {
                    _payload.Dispose();
                }

                _payload = null;
            }

            if (_attachment is null == false)
            {
                if (disposing)
                {
                    _attachment.Dispose();
                }

                _attachment = null;
            }
#if UNSTABLE_API
            if (_sourceInfo is null == false)
            {
                if (disposing)
                {
                    _sourceInfo.Dispose();
                }

                _sourceInfo = null;
            }
#endif
        }

        internal IntPtr AllocUnmanagedMemory()
        {
            var options = new ZGetOptions
            {
                encoding = IntPtr.Zero,
                payload = IntPtr.Zero,
                attachment = IntPtr.Zero,
                target = Target,
                consolidation = new ZQueryConsolidation { mode = Consolidation },
                congestion_control = CongestionControl,
                priority = Priority,
                timeout_ms = Timeout,
#if UNSTABLE_API
                source_info = IntPtr.Zero,
                allowed_destination = AllowedDestination,
                accept_replies = AcceptReplies,
#endif
            };

            if (_encoding is null == false)
            {
                options.encoding = _encoding.AllocUnmanagedMemory();
            }

            if (_payload is null == false)
            {
                options.payload = _payload.AllocUnmanagedMemory();
            }

            if (_attachment is null == false)
            {
                options.attachment = _attachment.AllocUnmanagedMemory();
            }

#if UNSTABLE_API
            if (_sourceInfo is null == false)
            {
                options.source_info = _sourceInfo.AllocUnmanagedMemory();
            }
#endif

            var pOptions = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(ZGetOptions)));
            Marshal.StructureToPtr(options, pOptions, false);

            return pOptions;
        }

        internal static void FreeUnmanagedMemory(IntPtr handle)
        {
            var options = Marshal.PtrToStructure<ZGetOptions>(handle);

            if (options.encoding != IntPtr.Zero)
            {
                Encoding.FreeUnmanagedMem(options.encoding);
            }

            if (options.payload != IntPtr.Zero)
            {
                ZBytes.FreeUnmanagedMem(options.payload);
            }

            if (options.attachment != IntPtr.Zero)
            {
                ZBytes.FreeUnmanagedMem(options.attachment);
            }

#if UNSTABLE_API
            if (options.source_info != IntPtr.Zero)
            {
                SourceInfo.FreeUnmanagedMemory(options.source_info);
            }
#endif

            Marshal.FreeHGlobal(handle);
        }
    }

    // z_delete_options_t
    public sealed class DeleteOptions : IDisposable
    {
        private CongestionControl _congestionControl;
        private Priority _priority;
        private bool _isExpress;
        private Timestamp? _timestamp;

        public DeleteOptions()
        {
            var pOptions = Marshal.AllocHGlobal(Marshal.SizeOf<ZDeleteOptions>());
            ZenohC.z_delete_options_default(pOptions);
            var options = Marshal.PtrToStructure<ZDeleteOptions>(pOptions);
            Marshal.FreeHGlobal(pOptions);

            _congestionControl = options.congestion_control;
            _priority = options.priority;
            _isExpress = options.is_express;
            _timestamp = null;
        }

        public DeleteOptions(DeleteOptions other)
        {
            _congestionControl = other._congestionControl;
            _priority = other._priority;
            _isExpress = other._isExpress;
            _timestamp = other._timestamp is null ? null : new Timestamp(other._timestamp);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~DeleteOptions() => Dispose(false);

        private void Dispose(bool disposing)
        {
            if (_timestamp is null == false)
            {
                if (disposing)
                {
                    _timestamp.Dispose();
                }

                _timestamp = null;
            }
        }

        public void SetCongestionControl(CongestionControl congestionControl)
        {
            _congestionControl = congestionControl;
        }

        public CongestionControl GetCongestionControl()
        {
            return _congestionControl;
        }

        public void SetPriority(Priority priority)
        {
            _priority = priority;
        }

        public Priority GetPriority()
        {
            return _priority;
        }

        public void SetIsExpress(bool isExpress)
        {
            _isExpress = isExpress;
        }

        public bool GetIsExpress()
        {
            return _isExpress;
        }

        public void SetTimestamp(Timestamp? timestamp)
        {
            _timestamp = timestamp is null ? null : new Timestamp(timestamp);
        }

        public Timestamp? GetTimestamp()
        {
            return _timestamp is null ? null : new Timestamp(_timestamp);
        }

        internal IntPtr AllocUnmanagedMemory()
        {
            var options = new ZDeleteOptions
            {
                congestion_control = _congestionControl,
                priority = _priority,
                is_express = _isExpress,
                timestamp = IntPtr.Zero,
            };

            if (_timestamp is null == false)
            {
                options.timestamp = _timestamp.AllocUnmanagedMem();
            }

            var pOptions = Marshal.AllocHGlobal(Marshal.SizeOf<ZDeleteOptions>());
            Marshal.StructureToPtr(options, pOptions, false);
            return pOptions;
        }

        internal static void FreeUnmanagedMemory(IntPtr handle)
        {
            var options = Marshal.PtrToStructure<ZDeleteOptions>(handle);

            if (options.timestamp != IntPtr.Zero)
            {
                Timestamp.FreeUnmanagedMem(options.timestamp);
            }

            Marshal.FreeHGlobal(handle);
        }
    }

    //  z_owned_reply_err_t
    public sealed class ReplyErr : Loanable
    {
        private ReplyErr()
        {
            throw new InvalidOperationException();
        }

        public ReplyErr(ReplyErr other)
        {
            other.CheckDisposed();

            var pOwnedReplyErr = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedReplyErr>());
            var pLoanedReplyErr = other.LoanedPointer();
            ZenohC.z_reply_err_clone(pOwnedReplyErr, pLoanedReplyErr);
            Handle = pOwnedReplyErr;
            Owned = true;
        }

        private ReplyErr(IntPtr handle, bool owned)
        {
            Owned = owned;
            Handle = handle;
        }

        internal static ReplyErr CreateLoaned(IntPtr handle)
        {
            return new ReplyErr(handle, false);
        }

        internal static ReplyErr CreateOwned()
        {
            var pOwnedReplyErr = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedReplyErr>());
            ZenohC.z_internal_reply_err_null(pOwnedReplyErr);
            return new ReplyErr(pOwnedReplyErr, true);
        }

        public override void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~ReplyErr() => Dispose(false);

        private void Dispose(bool disposing)
        {
            if (Handle == IntPtr.Zero) return;

            if (Owned)
            {
                ZenohC.z_reply_err_drop(Handle);
                Marshal.FreeHGlobal(Handle);
            }

            Handle = IntPtr.Zero;
        }

        internal override IntPtr LoanedPointer()
        {
            return Owned ? ZenohC.z_reply_err_loan(Handle) : Handle;
        }

        public override void ToOwned()
        {
            var pOwnedReplyErr = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedReplyErr>());
            var pLoanedReplyErr = LoanedPointer();
            ZenohC.z_reply_err_clone(pOwnedReplyErr, pLoanedReplyErr);
            Handle = pOwnedReplyErr;
            Owned = true;
        }

        /// <summary>
        /// Returns reply error payload.
        /// </summary>
        /// <returns>
        /// Return ZBytes is loaned.
        /// </returns>
        public ZBytes GetPayload()
        {
            var pLoanedReplyErr = LoanedPointer();
            var pLoanedBytes = ZenohC.z_reply_err_payload(pLoanedReplyErr);
            return ZBytes.CreateLoaned(pLoanedBytes);
        }

        /// <summary>
        /// Returns reply error encoding.
        /// </summary>
        /// <returns>
        /// Return Encoding is loaned.
        /// </returns>
        public Encoding GetEncoding()
        {
            var pLoanedReplyErr = LoanedPointer();
            var pLoanedEncoding = ZenohC.z_reply_err_encoding(pLoanedReplyErr);
            return Encoding.CreateLoaned(pLoanedEncoding);
        }
    }

    // z_owned_reply_t
    public sealed class Reply : IDisposable
    {
        // z_owned_reply_t*
        internal IntPtr Handle { get; private set; }

        internal Reply()
        {
            var pOwnedReply = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedReply>());
            ZenohC.z_internal_reply_null(pOwnedReply);
            Handle = pOwnedReply;
        }

        public Reply(Reply other)
        {
            other.CheckDisposed();

            var pOwnedReply = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedReply>());
            var pLoanedReply = ZenohC.z_reply_loan(other.Handle);
            ZenohC.z_reply_clone(pOwnedReply, pLoanedReply);
            Handle = pOwnedReply;
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~Reply() => Dispose(false);

        private void Dispose(bool disposing)
        {
            if (Handle == IntPtr.Zero) return;

            ZenohC.z_reply_drop(Handle);
            Marshal.FreeHGlobal(Handle);
            Handle = IntPtr.Zero;
        }

        internal void CheckDisposed()
        {
            if (Handle == IntPtr.Zero)
            {
                throw new InvalidOperationException("Object is disposed");
            }
        }

        /// <summary>
        /// Returns 'true' if reply contains a valid response, 'false' otherwise.
        /// </summary>
        public bool IsOk()
        {
            CheckDisposed();

            var pLoanedReply = ZenohC.z_reply_loan(Handle);
            return ZenohC.z_reply_is_ok(pLoanedReply);
        }

        /// <summary>
        /// Yields the contents of the reply by asserting it indicates a failure.
        /// </summary>
        /// <returns>
        /// Return ReplyErr is loaned.
        /// return null if reply does not contain a error  (i. e. if `IsOk()` returns `true`).
        /// </returns>
        public ReplyErr? AsErr()
        {
            CheckDisposed();

            var pLoanedReply = ZenohC.z_reply_loan(Handle);
            var pLoanedReplyErr = ZenohC.z_reply_err(pLoanedReply);

            return pLoanedReplyErr == IntPtr.Zero ? null : ReplyErr.CreateLoaned(pLoanedReplyErr);
        }

        /// <summary>
        /// Yields the contents of the reply by asserting it indicates a success.
        /// </summary>
        /// <returns>
        /// Return Sample is loaned,
        /// return null if reply does not contain a sample (i. e. if `IsOk()` returns `false`).
        /// </returns>
        public Sample? AsOk()
        {
            CheckDisposed();

            var pLoanedReply = ZenohC.z_reply_loan(Handle);
            var pLoanedSample = ZenohC.z_reply_ok(pLoanedReply);

            return pLoanedSample == IntPtr.Zero ? null : Sample.CreateLoaned(pLoanedSample);
        }
    }


#if UNSTABLE_API
    public sealed class QuerierOptions
    {
        /// The Queryables that should be target of the querier queries.
        public QueryTarget Target { get; set; }

        /// The replies consolidation strategy to apply on replies to the querier queries.
        public ConsolidationMode Consolidation { get; set; }

        // The congestion control to apply when routing the querier queries.
        public CongestionControl CongestionControl { get; set; }

        public bool IsExpress { get; set; }

        public Locality AllowedDestination { get; set; }

        public ReplyKeyexpr AcceptReplies { get; set; }

        public Priority Priority { get; set; }

        /// The timeout for the querier queries in milliseconds.
        /// 0 means default query timeout from zenoh configuration.
        public ulong Timeout { get; set; }

        internal IntPtr AllocUnmanagedMemory()
        {
            var options = new ZQuerierOptions
            {
                target = Target,
                consolidation = new ZQueryConsolidation { mode = Consolidation },
                congestion_control = CongestionControl,
                is_express = IsExpress,
                allowed_destination = AllowedDestination,
                accept_replies = AcceptReplies,
                priority = Priority,
                timeout_ms = Timeout,
            };

            var pOptions = Marshal.AllocHGlobal(Marshal.SizeOf<ZQuerierOptions>());
            Marshal.StructureToPtr(options, pOptions, false);

            return pOptions;
        }

        internal static void FreeUnmanagedMemory(IntPtr handle)
        {
            Marshal.FreeHGlobal(handle);
        }
    }
#endif

#if UNSTABLE_API
    public sealed class QuerierGetOptions : IDisposable
    {
        public ZBytes? Payload
        {
            get => _payload is null ? null : new ZBytes(_payload);
            set => _payload = value is null ? null : new ZBytes(value);
        }

        private ZBytes? _payload;

        public Encoding? Encoding
        {
            get => _encoding is null ? null : new Encoding(_encoding);
            set => _encoding = value is null ? null : new Encoding(value);
        }

        private Encoding? _encoding;

        public SourceInfo? SourceInfo
        {
            get => _sourceInfo is null ? null : new SourceInfo(_sourceInfo);
            set => _sourceInfo = value is null ? null : new SourceInfo(value);
        }

        private SourceInfo? _sourceInfo;

        public ZBytes? Attachment
        {
            get => _attachment is null ? null : new ZBytes(_attachment);
            set => _attachment = value is null ? null : new ZBytes(value);
        }

        private ZBytes? _attachment;

        public QuerierGetOptions()
        {
            _payload = null;
            _encoding = null;
            _sourceInfo = null;
            _attachment = null;
        }

        public QuerierGetOptions(QuerierGetOptions other)
        {
            Payload = other.Payload;
            Encoding = other.Encoding;
            SourceInfo = other.SourceInfo;
            Attachment = other.Attachment;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~QuerierGetOptions() => Dispose(false);

        private void Dispose(bool disposing)
        {
            if (_payload is null == false)
            {
                if (disposing)
                {
                    _payload.Dispose();
                }

                _payload = null;
            }

            if (_encoding is null == false)
            {
                if (disposing)
                {
                    _encoding.Dispose();
                }

                _encoding = null;
            }

            if (_attachment is null == false)
            {
                if (disposing)
                {
                    _attachment.Dispose();
                }

                _attachment = null;
            }

            if (_sourceInfo is null == false)
            {
                if (disposing)
                {
                    _sourceInfo.Dispose();
                }

                _sourceInfo = null;
            }
        }

        internal IntPtr AllocUnmanagedMemory()
        {
            var options = new ZQuerierGetOptions
            {
                payload = IntPtr.Zero,
                encoding = IntPtr.Zero,
                source_info = IntPtr.Zero,
                attachment = IntPtr.Zero
            };

            if (_payload is null == false)
            {
                options.payload = _payload.AllocUnmanagedMemory();
            }

            if (_encoding is null == false)
            {
                options.encoding = _encoding.AllocUnmanagedMemory();
            }

            if (_sourceInfo is null == false)
            {
                options.source_info = _sourceInfo.AllocUnmanagedMemory();
            }

            if (_attachment is null == false)
            {
                options.attachment = _attachment.AllocUnmanagedMemory();
            }

            var pOptions = Marshal.AllocHGlobal(Marshal.SizeOf<ZQuerierGetOptions>());
            Marshal.StructureToPtr(options, pOptions, false);

            return pOptions;
        }

        internal static void FreeUnmanagedMemory(IntPtr handle)
        {
            var options = Marshal.PtrToStructure<ZQuerierGetOptions>(handle);

            if (options.payload != IntPtr.Zero)
            {
                ZBytes.FreeUnmanagedMem(options.payload);
            }

            if (options.encoding != IntPtr.Zero)
            {
                Encoding.FreeUnmanagedMem(options.encoding);
            }

            if (options.source_info != IntPtr.Zero)
            {
                SourceInfo.FreeUnmanagedMemory(options.source_info);
            }

            if (options.attachment != IntPtr.Zero)
            {
                ZBytes.FreeUnmanagedMem(options.attachment);
            }

            Marshal.FreeHGlobal(handle);
        }
    }
#endif

#if UNSTABLE_API
    public sealed class Querier : IDisposable
    {
        // z_owned_querier_t*
        internal IntPtr Handle { get; private set; }

        internal Querier()
        {
            var pOwnedQuerier = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedQuerier>());
            ZenohC.z_internal_querier_null(pOwnedQuerier);
            Handle = pOwnedQuerier;
        }

        private Querier(Querier other)
        {
            throw new InvalidOperationException();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~Querier() => Dispose(false);

        private void Dispose(bool disposing)
        {
            if (Handle == IntPtr.Zero) return;

            ZenohC.z_querier_drop(Handle);
            Marshal.FreeHGlobal(Handle);
            Handle = IntPtr.Zero;
        }

        internal void CheckDisposed()
        {
            if (Handle == IntPtr.Zero)
            {
                throw new ObjectDisposedException("Object has been destroyed");
            }
        }

        /// <summary>
        /// Undeclare the querier and free memory. This is equivalent to calling the "Dispose()"
        /// </summary>
        public void Undeclare()
        {
            Dispose();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <param name="parameters"></param>
        /// <param name="channelType"></param>
        /// <param name="channelSize"></param>
        /// <param name="channel"></param>
        /// <returns></returns>
        public Result Get(QuerierGetOptions options, string? parameters, ChannelType channelType, uint channelSize,
            out ChannelReply? channel
        )
        {
            CheckDisposed();

            var pOwnedClosureReply = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedClosureReply>());
            switch (channelType)
            {
                case ChannelType.Ring:
                    channel = new ChannelReplyRing();
                    ZenohC.z_ring_channel_reply_new(pOwnedClosureReply, channel.Handle, (UIntPtr)channelSize);
                    break;
                case ChannelType.Fifo:
                    channel = new ChannelReplyFifo();
                    ZenohC.z_fifo_channel_reply_new(pOwnedClosureReply, channel.Handle, (UIntPtr)channelSize);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(channelType), channelType, null);
            }

            var pLoanedQuerier = ZenohC.z_querier_loan(Handle);
            var pOptions = options.AllocUnmanagedMemory();
            var pParameters = IntPtr.Zero;
            if (!string.IsNullOrEmpty(parameters))
            {
                var utf8BytesParameters = System.Text.Encoding.UTF8.GetBytes(parameters + "\0");
                pParameters = Marshal.AllocHGlobal(utf8BytesParameters.Length);
                Marshal.Copy(utf8BytesParameters, 0, pParameters, utf8BytesParameters.Length);
            }

            var r = ZenohC.z_querier_get(pLoanedQuerier, pParameters, pOwnedClosureReply, pOptions);

            ZenohC.z_closure_reply_drop(pOwnedClosureReply);
            Marshal.FreeHGlobal(pOwnedClosureReply);
            Marshal.FreeHGlobal(pParameters);
            QuerierGetOptions.FreeUnmanagedMemory(pOptions);

            if (r == Result.Ok) return Result.Ok;

            channel.Dispose();
            channel = null;
            return r;
        }
    }
#endif
}