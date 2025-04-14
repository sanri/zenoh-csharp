using System;
using System.Runtime.InteropServices;


namespace Zenoh
{
    public sealed class LivelinessToken : IDisposable
    {
        // z_owned_liveliness_token_t*
        internal IntPtr Handle { get; private set; }

        internal LivelinessToken()
        {
            var pOwnedLivelinessToken = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedLivelinessToken>());
            ZenohC.z_internal_liveliness_token_null(pOwnedLivelinessToken);
            Handle = pOwnedLivelinessToken;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~LivelinessToken() => Dispose(false);

        private void Dispose(bool disposing)
        {
            if (Handle == IntPtr.Zero) return;

            ZenohC.z_liveliness_token_drop(Handle);
            Marshal.FreeHGlobal(Handle);
            Handle = IntPtr.Zero;
        }

        /// <summary>
        /// Destroys a liveliness token, notifying subscribers of its destruction.
        /// </summary>
        public void Undeclare()
        {
            if (Handle == IntPtr.Zero) return;

            var r = ZenohC.z_liveliness_undeclare_token(Handle);
        }
    }

    public sealed class LivelinessGetOptions
    {
        /// The timeout for the liveliness query in milliseconds.
        public ulong Timeout { get; set; }

        public LivelinessGetOptions()
        {
            var pOptions = Marshal.AllocHGlobal(Marshal.SizeOf<ZLivelinessGetOptions>());
            var options = Marshal.PtrToStructure<ZLivelinessGetOptions>(pOptions);
            Marshal.FreeHGlobal(pOptions);
            Timeout = options.timeout_ms;
        }

        public LivelinessGetOptions(LivelinessGetOptions other)
        {
            Timeout = other.Timeout;
        }

        internal IntPtr AllocUnmanagedMemory()
        {
            var options = new ZLivelinessGetOptions { timeout_ms = Timeout };
            var pOptions = Marshal.AllocHGlobal(Marshal.SizeOf<ZLivelinessGetOptions>());
            Marshal.StructureToPtr(options, pOptions, false);
            return pOptions;
        }

        internal static void FreeUnmanagedMemory(IntPtr handle)
        {
            Marshal.FreeHGlobal(handle);
        }
    }

    public sealed class LivelinessSubscriberOptions
    {
        public bool History { get; set; }

        internal IntPtr AllocUnmanagedMemory()
        {
            var options = new ZLivelinessSubscriberOptions { history = History };
            var pOptions = Marshal.AllocHGlobal(Marshal.SizeOf<ZLivelinessSubscriberOptions>());
            Marshal.StructureToPtr(options, pOptions, false);
            return pOptions;
        }

        internal static void FreeUnmanagedMemory(IntPtr handle)
        {
            Marshal.FreeHGlobal(handle);
        }
    }

    public sealed class LivelinessSubscriber : IDisposable
    {
        // z_owned_subscriber_t*
        internal IntPtr Handle { get; private set; }
        internal static ZenohC.Cb2 CbCall = _Call;
        internal static ZenohC.Cb1 CbDrop = _Drop;

        public delegate void Cb(Sample sample);

        internal LivelinessSubscriber()
        {
            var pOwnedSubscriber = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedSubscriber>());
            ZenohC.z_internal_subscriber_null(pOwnedSubscriber);
            Handle = pOwnedSubscriber;
        }

        private LivelinessSubscriber(LivelinessSubscriber other)
        {
            throw new InvalidOperationException();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~LivelinessSubscriber() => Dispose(false);

        private void Dispose(bool disposing)
        {
            if (Handle == IntPtr.Zero) return;

            ZenohC.z_subscriber_drop(Handle);
            Marshal.FreeHGlobal(Handle);
            Handle = IntPtr.Zero;
        }

        internal void CheckDisposed()
        {
            if (Handle == IntPtr.Zero)
            {
                throw new ObjectDisposedException(nameof(LivelinessSubscriber));
            }
        }

        /// <summary>
        /// Undeclare the liveliness subscriber and free memory. This is equivalent to calling the "Dispose()".
        /// </summary>
        public void Undeclare()
        {
            Dispose();
        }

        /// <summary>
        /// Returns the key expression of the subscriber.
        /// </summary>
        /// <returns>
        /// The return Keyexpr is loaned.
        /// </returns>
        public Keyexpr GetKeyexpr()
        {
            CheckDisposed();

            var pLoanedSubscriber = ZenohC.z_subscriber_loan(Handle);
            var pLoanedKeyexpr = ZenohC.z_subscriber_keyexpr(pLoanedSubscriber);
            return Keyexpr.CreateLoaned(pLoanedKeyexpr);
        }

#if UNSTABLE_API
        /// <summary>
        /// Returns the ID of the subscriber.
        /// </summary>
        /// <returns></returns>
        public EntityGlobalId GetId()
        {
            CheckDisposed();

            var pLoanedSubscriber = ZenohC.z_subscriber_loan(Handle);
            var zEntityGlobalId = ZenohC.z_subscriber_id(pLoanedSubscriber);
            return new EntityGlobalId(zEntityGlobalId);
        }
#endif

        private static void _Call(IntPtr sample, IntPtr context)
        {
            var gcHandle = GCHandle.FromIntPtr(context);
            if (!(gcHandle.Target is Cb callback)) return;

            var loanedSample = Sample.CreateLoaned(sample);
            callback(loanedSample);
        }

        private static void _Drop(IntPtr context)
        {
            var gcHandle = GCHandle.FromIntPtr(context);
            gcHandle.Free();
        }
    }
}