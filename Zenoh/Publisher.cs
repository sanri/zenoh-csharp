using System;
using System.Runtime.InteropServices;

namespace Zenoh
{
    public sealed class PutOptions : IDisposable
    {
        public CongestionControl CongestionControl { get; set; }
        public Priority Priority { get; set; }
        public bool IsExpress { get; set; }

        public Encoding? Encoding
        {
            get => _encoding is null ? null : new Encoding(_encoding);
            set => _encoding = value is null ? null : new Encoding(value);
        }

        private Encoding? _encoding;

        public Timestamp? Timestamp
        {
            get => _timestamp is null ? null : new Timestamp(_timestamp);
            set => _timestamp = value is null ? null : new Timestamp(value);
        }

        private Timestamp? _timestamp;

        public ZBytes? Attachment
        {
            get => _attachment is null ? null : new ZBytes(_attachment);
            set => _attachment = value is null ? null : new ZBytes(value);
        }

        private ZBytes? _attachment;

#if UNSTABLE_API
        public Reliability Reliability { get; set; }
        public Locality AllowedDestination { get; set; }

        public SourceInfo? SourceInfo
        {
            get => _sourceInfo is null ? null : new SourceInfo(_sourceInfo);
            set => _sourceInfo = value is null ? null : new SourceInfo(value);
        }

        private SourceInfo? _sourceInfo;
#endif

        public PutOptions()
        {
            var pPutOptions = Marshal.AllocHGlobal(Marshal.SizeOf<ZPutOptions>());
            ZenohC.z_put_options_default(pPutOptions);
            var options = Marshal.PtrToStructure<ZPutOptions>(pPutOptions);
            Marshal.FreeHGlobal(pPutOptions);

            CongestionControl = options.congestion_control;
            Priority = options.priority;
            IsExpress = options.is_express;

            _encoding = null;
            _timestamp = null;
            _attachment = null;

#if UNSTABLE_API
            Reliability = options.reliability;
            AllowedDestination = options.allowed_destination;

            _sourceInfo = null;
#endif
        }

        public PutOptions(EncodingId encodingId) : this()
        {
            _encoding = new Encoding(encodingId);
        }

        public PutOptions(PutOptions other)
        {
            Encoding = other.Encoding;
            CongestionControl = other.CongestionControl;
            Priority = other.Priority;
            IsExpress = other.IsExpress;
            Timestamp = other.Timestamp;
            Attachment = other.Attachment;
#if UNSTABLE_API
            Reliability = other.Reliability;
            AllowedDestination = other.AllowedDestination;
            SourceInfo = other.SourceInfo;
#endif
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~PutOptions() => Dispose(false);

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

            if (_timestamp is null == false)
            {
                if (disposing)
                {
                    _timestamp.Dispose();
                }

                _timestamp = null;
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
            var options = new ZPutOptions
            {
                encoding = IntPtr.Zero,
                timestamp = IntPtr.Zero,
                attachment = IntPtr.Zero,
#if UNSTABLE_API
                source_info = IntPtr.Zero,
#endif
            };

            if (_encoding is null == false)
            {
                options.encoding = _encoding.AllocUnmanagedMemory();
            }

            if (_timestamp is null == false)
            {
                options.timestamp = _timestamp.AllocUnmanagedMem();
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

            options.congestion_control = CongestionControl;
            options.priority = Priority;
            options.is_express = IsExpress;

#if UNSTABLE_API
            options.reliability = Reliability;
            options.allowed_destination = AllowedDestination;
#endif

            var pOptions = Marshal.AllocHGlobal(Marshal.SizeOf<ZPutOptions>());
            Marshal.StructureToPtr(options, pOptions, false);

            return pOptions;
        }

        internal static void FreeUnmanagedMemory(IntPtr handle)
        {
            var options = Marshal.PtrToStructure<ZPutOptions>(handle);

            if (options.encoding != IntPtr.Zero)
            {
                Encoding.FreeUnmanagedMem(options.encoding);
            }

            if (options.timestamp != IntPtr.Zero)
            {
                Timestamp.FreeUnmanagedMem(options.timestamp);
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

    public sealed class PublisherOptions : IDisposable
    {
        public Encoding? Encoding
        {
            get => _encoding is null ? null : new Encoding(_encoding);
            set => _encoding = value is null ? null : new Encoding(value);
        }

        private Encoding? _encoding;

        public CongestionControl CongestionControl { get; set; }
        public Priority Priority { get; set; }
        public bool IsExpress { get; set; }

#if UNSTABLE_API
        public Reliability Reliability { get; set; }
        public Locality AllowedDestination { get; set; }
#endif

        public PublisherOptions()
        {
            var pPublisherOptions = Marshal.AllocHGlobal(Marshal.SizeOf<ZPublisherOptions>());
            ZenohC.z_publisher_options_default(pPublisherOptions);
            var options = Marshal.PtrToStructure<ZPublisherOptions>(pPublisherOptions);
            Marshal.FreeHGlobal(pPublisherOptions);
            _encoding = null;
            CongestionControl = options.congestion_control;
            Priority = options.priority;
            IsExpress = options.is_express;

#if UNSTABLE_API
            Reliability = options.reliability;
            AllowedDestination = options.allowed_destination;
#endif
        }

        public PublisherOptions(PublisherOptions other)
        {
            _encoding = other.Encoding;
            CongestionControl = other.CongestionControl;
            Priority = other.Priority;
            IsExpress = other.IsExpress;

#if UNSTABLE_API
            Reliability = other.Reliability;
            AllowedDestination = other.AllowedDestination;
#endif
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~PublisherOptions() => Dispose(false);

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
        }

        internal IntPtr AllocUnmanagedMemory()
        {
            var options = new ZPublisherOptions
            {
                encoding = IntPtr.Zero
            };

            if (_encoding is null == false)
            {
                options.encoding = _encoding.AllocUnmanagedMemory();
            }

            options.congestion_control = CongestionControl;
            options.priority = Priority;
            options.is_express = IsExpress;

#if UNSTABLE_API
            options.reliability = Reliability;
            options.allowed_destination = AllowedDestination;
#endif

            var pOptions = Marshal.AllocHGlobal(Marshal.SizeOf<ZPublisherOptions>());
            Marshal.StructureToPtr(options, pOptions, false);

            return pOptions;
        }

        internal static void FreeUnmanagedMemory(IntPtr handle)
        {
            var options = Marshal.PtrToStructure<ZPublisherOptions>(handle);

            if (options.encoding != IntPtr.Zero)
            {
                Encoding.FreeUnmanagedMem(options.encoding);
            }

            Marshal.FreeHGlobal(handle);
        }
    }

    public sealed class PublisherPutOptions : IDisposable
    {
        public Encoding? Encoding
        {
            get => _encoding is null ? null : new Encoding(_encoding);
            set => _encoding = value is null ? null : new Encoding(value);
        }

        private Encoding? _encoding;

        public Timestamp? Timestamp
        {
            get => _timestamp is null ? null : new Timestamp(_timestamp);
            set => _timestamp = value is null ? null : new Timestamp(value);
        }

        private Timestamp? _timestamp;

        public ZBytes? Attachment
        {
            get => _attachment is null ? null : new ZBytes(_attachment);
            set => _attachment = value is null ? null : new ZBytes(value);
        }

        private ZBytes? _attachment;

#if UNSTABLE_API
        public SourceInfo? SourceInfo
        {
            get => _sourceInfo is null ? null : new SourceInfo(_sourceInfo);
            set => _sourceInfo = value is null ? null : new SourceInfo(value);
        }

        private SourceInfo? _sourceInfo;
#endif

        public PublisherPutOptions()
        {
            _encoding = null;
            _timestamp = null;
            _attachment = null;
#if UNSTABLE_API
            _sourceInfo = null;
#endif
        }

        public PublisherPutOptions(PublisherPutOptions other)
        {
            _encoding = other.Encoding;
            _timestamp = other.Timestamp;
            _attachment = other.Attachment;
#if UNSTABLE_API
            _sourceInfo = other.SourceInfo;
#endif
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~PublisherPutOptions() => Dispose(false);

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

            if (_timestamp is null == false)
            {
                if (disposing)
                {
                    _timestamp.Dispose();
                }

                _timestamp = null;
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
            var options = new ZPublisherPutOptions
            {
                encoding = IntPtr.Zero,
                timestamp = IntPtr.Zero,
                attachment = IntPtr.Zero,
#if UNSTABLE_API
                source_info = IntPtr.Zero,
#endif
            };

            if (_encoding is null == false)
            {
                options.encoding = _encoding.AllocUnmanagedMemory();
            }

            if (_timestamp is null == false)
            {
                options.timestamp = _timestamp.AllocUnmanagedMem();
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

            var pPublisherPutOptions = Marshal.AllocHGlobal(Marshal.SizeOf<ZPublisherPutOptions>());
            Marshal.StructureToPtr(options, pPublisherPutOptions, false);

            return pPublisherPutOptions;
        }

        internal static void FreeUnmanagedMemory(IntPtr handle)
        {
            var options = Marshal.PtrToStructure<ZPublisherPutOptions>(handle);

            if (options.encoding != IntPtr.Zero)
            {
                Encoding.FreeUnmanagedMem(options.encoding);
            }

            if (options.timestamp != IntPtr.Zero)
            {
                Timestamp.FreeUnmanagedMem(options.timestamp);
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

    public sealed class PublisherDeleteOptions : IDisposable
    {
        public Timestamp? Timestamp
        {
            get => _timestamp is null ? null : new Timestamp(_timestamp);
            set => _timestamp = value is null ? null : new Timestamp(value);
        }

        private Timestamp? _timestamp;

        public PublisherDeleteOptions()
        {
            _timestamp = null;
        }

        public PublisherDeleteOptions(PublisherDeleteOptions other)
        {
            Timestamp = other.Timestamp;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~PublisherDeleteOptions() => Dispose(false);

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

        internal IntPtr AllocUnmanagedMemory()
        {
            var options = new ZPublisherDeleteOptions
            {
                timestamp = IntPtr.Zero,
            };

            if (_timestamp is null == false)
            {
                options.timestamp = _timestamp.AllocUnmanagedMem();
            }

            var pPublisherDeleteOptions = Marshal.AllocHGlobal(Marshal.SizeOf<ZPublisherDeleteOptions>());
            Marshal.StructureToPtr(options, pPublisherDeleteOptions, false);

            return pPublisherDeleteOptions;
        }

        internal static void FreeUnmanagedMemory(IntPtr handle)
        {
            var options = Marshal.PtrToStructure<ZPublisherDeleteOptions>(handle);

            if (options.timestamp != IntPtr.Zero)
            {
                Timestamp.FreeUnmanagedMem(options.timestamp);
            }

            Marshal.FreeHGlobal(handle);
        }
    }

    public sealed class Publisher : IDisposable
    {
        // z_owned_publisher_t*
        internal IntPtr Handle { get; private set; }

        internal Publisher()
        {
            var pOwnedPublisher = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedPublisher>());
            ZenohC.z_internal_publisher_null(pOwnedPublisher);
            Handle = pOwnedPublisher;
        }

        private Publisher(Publisher other)
        {
            throw new InvalidOperationException();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~Publisher() => Dispose(false);

        private void Dispose(bool disposing)
        {
            if (Handle == IntPtr.Zero) return;

            ZenohC.z_publisher_drop(Handle);
            Marshal.FreeHGlobal(Handle);
            Handle = IntPtr.Zero;
        }

        internal void CheckDisposed()
        {
            if (Handle == IntPtr.Zero)
            {
                throw new InvalidOperationException("Object has been destroyed");
            }
        }

        /// <summary>
        /// Undeclare the publisher and free memory. This is equivalent to calling the "Dispose()".
        /// </summary>
        public void Undeclare()
        {
            Dispose();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>
        /// The return Keyexpr is loaned.
        /// </returns>
        public Keyexpr GetKeyexpr()
        {
            CheckDisposed();

            var pLoanedPublisher = ZenohC.z_publisher_loan(Handle);
            var pLoanedKeyexpr = ZenohC.z_publisher_keyexpr(pLoanedPublisher);
            return Keyexpr.CreateLoaned(pLoanedKeyexpr);
        }

        /// <summary>
        /// <para>
        /// Sends a "PUT" message onto the publisher's key expression, transferring the payload ownership.
        /// </para>
        /// <para>
        /// Do not use the "payload" after calling this function.
        /// payload.Dispose() is called inside this function.
        /// </para>
        /// </summary>
        /// <param name="payload">The data to publish. Will be consumed.</param>
        /// <param name="options">The publisher put options.</param>
        /// <returns>ZResult.Ok in case of success.</returns>
        public Result Put(ZBytes payload, PublisherPutOptions options)
        {
            CheckDisposed();
            payload.CheckDisposed();
            payload.ToOwned();

            var pPublisherPutOptions = options.AllocUnmanagedMemory();
            var pLoanedPublisher = ZenohC.z_publisher_loan(Handle);

            var r = ZenohC.z_publisher_put(pLoanedPublisher, payload.Handle, pPublisherPutOptions);

            PublisherPutOptions.FreeUnmanagedMemory(pPublisherPutOptions);
            payload.Dispose();
            return r;
        }

        /// <summary>
        /// Sends a `DELETE` message onto the publisher's key expression. 
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public Result Delete(PublisherDeleteOptions options)
        {
            CheckDisposed();

            var pLoanedPublisher = ZenohC.z_publisher_loan(Handle);
            var pOptions = options.AllocUnmanagedMemory();
            var r = ZenohC.z_publisher_delete(pLoanedPublisher, pOptions);
            PublisherDeleteOptions.FreeUnmanagedMemory(pOptions);
            return r;
        }

#if UNSTABLE_API
        /// <summary>
        /// Returns the ID of the publisher.
        /// </summary>
        /// <returns></returns>
        public EntityGlobalId GetId()
        {
            CheckDisposed();

            var pLoanedPublisher = ZenohC.z_publisher_loan(Handle);
            var zEntityGlobalId = ZenohC.z_publisher_id(pLoanedPublisher);
            return new EntityGlobalId(zEntityGlobalId);
        }
#endif
    }
}