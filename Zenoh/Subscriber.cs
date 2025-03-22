using System;
using System.Runtime.InteropServices;

namespace Zenoh
{
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