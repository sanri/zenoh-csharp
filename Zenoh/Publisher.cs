using System;
using System.Runtime.InteropServices;

namespace Zenoh
{
    public sealed class PutOptions : IDisposable
    {
        private Encoding? _encoding;
        private CongestionControl _congestionControl;
        private Priority _priority;
        private bool _isExpress;
        private Timestamp? _timestamp;
        private ZBytes? _attachment;

        public PutOptions()
        {
            var pPutOptions = Marshal.AllocHGlobal(Marshal.SizeOf<ZPutOptions>());
            ZenohC.z_put_options_default(pPutOptions);
            var options = Marshal.PtrToStructure<ZPutOptions>(pPutOptions);
            Marshal.FreeHGlobal(pPutOptions);

            _encoding = null;
            _timestamp = null;
            _attachment = null;
            _congestionControl = options.congestion_control;
            _priority = options.priority;
            _isExpress = options.is_express;
        }

        public PutOptions(EncodingId encodingId) : this()
        {
            _encoding = new Encoding(encodingId);
        }

        public PutOptions(PutOptions other)
        {
            _encoding = other._encoding is null ? null : new Encoding(other._encoding);
            _congestionControl = other._congestionControl;
            _priority = other._priority;
            _isExpress = other._isExpress;
            _timestamp = other._timestamp is null ? null : new Timestamp(other._timestamp);
            _attachment = other._attachment is null ? null : new ZBytes(other._attachment);
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
        }

        public void SetEncoding(Encoding? encoding)
        {
            _encoding = encoding is null ? null : new Encoding(encoding);
        }

        public Encoding? GetEncoding()
        {
            return _encoding is null ? null : new Encoding(_encoding);
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

        public void SetAttachment(ZBytes? attachment)
        {
            _attachment = attachment is null ? null : new ZBytes(attachment);
        }

        public ZBytes? GetAttachment()
        {
            return _attachment is null ? null : new ZBytes(_attachment);
        }

        internal IntPtr AllocUnmanagedMemory()
        {
            var options = new ZPutOptions()
            {
                encoding = IntPtr.Zero,
                timestamp = IntPtr.Zero,
                attachment = IntPtr.Zero,
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

            options.congestion_control = _congestionControl;
            options.priority = _priority;
            options.is_express = _isExpress;

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

            Marshal.FreeHGlobal(handle);
        }
    }

    public sealed class PublisherOptions : IDisposable
    {
        private Encoding? _encoding;
        private CongestionControl _congestionControl;
        private Priority _priority;
        private bool _isExpress;

        public PublisherOptions()
        {
            var pPublisherOptions = Marshal.AllocHGlobal(Marshal.SizeOf<ZPublisherOptions>());
            ZenohC.z_publisher_options_default(pPublisherOptions);
            var options = Marshal.PtrToStructure<ZPublisherOptions>(pPublisherOptions);
            Marshal.FreeHGlobal(pPublisherOptions);
            _encoding = null;
            _congestionControl = options.congestion_control;
            _priority = options.priority;
            _isExpress = options.is_express;
        }

        public PublisherOptions(PublisherOptions other)
        {
            _encoding = other._encoding is null ? null : new Encoding(other._encoding);
            _congestionControl = other._congestionControl;
            _priority = other._priority;
            _isExpress = other._isExpress;
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

        public void SetEncoding(Encoding? encoding)
        {
            _encoding = encoding is null ? null : new Encoding(encoding);
        }

        public Encoding? GetEncoding()
        {
            return _encoding is null ? null : new Encoding(_encoding);
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

            options.congestion_control = _congestionControl;
            options.priority = _priority;
            options.is_express = _isExpress;

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
        private Encoding? _encoding;
        private Timestamp? _timestamp;
        private ZBytes? _attachment;

        public PublisherPutOptions()
        {
            _encoding = null;
            _timestamp = null;
            _attachment = null;
        }

        public PublisherPutOptions(PublisherPutOptions other)
        {
            _encoding = other.GetEncoding();
            _timestamp = other.GetTimestamp();
            _attachment = other.GetAttachment();
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
        }

        public void SetEncoding(Encoding? encoding)
        {
            _encoding?.Dispose();
            _encoding = encoding is null ? null : new Encoding(encoding);
        }

        public Encoding? GetEncoding()
        {
            return _encoding is null ? null : new Encoding(_encoding);
        }

        public void SetTimestamp(Timestamp? timestamp)
        {
            _timestamp?.Dispose();
            _timestamp = timestamp is null ? null : new Timestamp(timestamp);
        }

        public Timestamp? GetTimestamp()
        {
            return _timestamp is null ? null : new Timestamp(_timestamp);
        }

        public void SetAttachment(ZBytes? attachment)
        {
            _attachment?.Dispose();
            _attachment = attachment is null ? null : new ZBytes(attachment);
        }

        public ZBytes? GetAttachment()
        {
            return _attachment is null ? null : new ZBytes(_attachment);
        }

        internal IntPtr AllocUnmanagedMemory()
        {
            var options = new ZPublisherPutOptions
            {
                encoding = IntPtr.Zero,
                timestamp = IntPtr.Zero,
                attachment = IntPtr.Zero,
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
    }
}