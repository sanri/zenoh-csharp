using System;
using System.Runtime.InteropServices;

namespace Zenoh
{
    public sealed class SubscriberOptions
    {
#if UNSTABLE_API
        /// Restricts the matching publications that will be received by this Subscriber to
        /// the ones that have the compatible AllowedDestination
        public Locality AllowedOrigin { get; set; }
#else
        /// Dummy field to avoid having fieldless struct.
        public byte Dummy{ get; set; }
#endif

        public SubscriberOptions()
        {
            var pSubscriberOptions = Marshal.AllocHGlobal(Marshal.SizeOf<ZSubscriberOptions>());
            ZenohC.z_subscriber_options_default(pSubscriberOptions);
            var options = Marshal.PtrToStructure<ZSubscriberOptions>(pSubscriberOptions);
            Marshal.FreeHGlobal(pSubscriberOptions);

#if UNSTABLE_API
            AllowedOrigin = options.allowed_origin;
#else
            Dummy = options.dummy;
#endif
        }


        internal IntPtr AllocUnmanagedMemory()
        {
            var options = new ZSubscriberOptions
            {
#if UNSTABLE_API
                allowed_origin = AllowedOrigin,
#else
                dummy = Dummy,
#endif
            };

            var pSubscriberOptions = Marshal.AllocHGlobal(Marshal.SizeOf<ZSubscriberOptions>());
            Marshal.StructureToPtr(options, pSubscriberOptions, false);

            return pSubscriberOptions;
        }

        internal static void FreeUnmanagedMemory(IntPtr handle)
        {
            Marshal.FreeHGlobal(handle);
        }
    }

    public sealed class Subscriber : IDisposable
    {
        // z_owned_subscriber_t*
        internal IntPtr Handle { get; private set; }

        public delegate void Cb(Sample sample);

        internal Subscriber()
        {
            var pOwnedSubscriber = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedSubscriber>());
            ZenohC.z_internal_subscriber_null(pOwnedSubscriber);
            Handle = pOwnedSubscriber;
        }

        private Subscriber(Subscriber other)
        {
            throw new InvalidOperationException();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~Subscriber() => Dispose(false);

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

        internal static void CallbackClosureSampleCall(IntPtr sample, IntPtr context)
        {
            var gcHandle = GCHandle.FromIntPtr(context);
            if (!(gcHandle.Target is Cb callback)) return;

            var loanedSample = Sample.CreateLoaned(sample);
            callback(loanedSample);
        }

        internal static void CallbackClosureSampleDrop(IntPtr context)
        {
            var gcHandle = GCHandle.FromIntPtr(context);
            gcHandle.Free();
        }
    }
}