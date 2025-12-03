using System;
using System.Runtime.InteropServices;

namespace Zenoh
{
#if (UNSTABLE_API && SHARED_MEMORY)

    // z_owned_shm_provider_t
    /// <summary>
    ///     An owned SHM Provider.
    /// </summary>
    public sealed class ShmProvider
    {
        private IntPtr Handle { get; set; }

        private ShmProvider()
        {
            throw new InvalidOperationException();
        }

        private ShmProvider(ShmProvider other)
        {
            throw new InvalidOperationException();
        }

        /// <summary>
        ///     Creates a new SHM Provider ith default backend.
        /// </summary>
        /// <param name="size">SHM size</param>
        /// <exception cref="InvalidOperationException">Creation failed</exception>
        public ShmProvider(UIntPtr size)
        {
            var pOwnedShmProvider = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedShmProvider>());
            var r = ZenohC.z_shm_provider_default_new(pOwnedShmProvider, size);

            if (r == Result.Ok)
            {
                Handle = pOwnedShmProvider;
            }
            else
            {
                Marshal.FreeHGlobal(pOwnedShmProvider);
                throw new InvalidOperationException(r.ToString());
            }
        }

        ~ShmProvider()
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
            if (Handle == IntPtr.Zero) return;

            ZenohC.z_shm_provider_drop(Handle);
            Marshal.FreeHGlobal(Handle);

            Handle = IntPtr.Zero;
        }

        internal IntPtr LoanedPointer()
        {
            return ZenohC.z_shm_provider_loan(Handle);
        }

        internal void CheckDisposed()
        {
            if (Handle == IntPtr.Zero)
                throw new ObjectDisposedException(nameof(ShmProvider));
        }

        /// <summary>
        /// Make allocation without any additional actions.
        /// </summary>
        /// <param name="size"></param>
        /// <param name="shm"></param>
        /// <returns>BufLayoutAllocStatus.Ok is alloc succeed.</returns>
        public BufLayoutAllocStatus Alloc(UIntPtr size, out ShmMut? shm)
        {
            CheckDisposed();
            var pLoanProvider = LoanedPointer();
            var pBufLayoutAllocResult = Marshal.AllocHGlobal(Marshal.SizeOf<ZBufLayoutAllocResult>());

            ZenohC.z_shm_provider_alloc(pBufLayoutAllocResult, pLoanProvider, size);
            var blar = Marshal.PtrToStructure<ZBufLayoutAllocResult>(pBufLayoutAllocResult);
            Marshal.FreeHGlobal(pBufLayoutAllocResult);

            if (blar.status != BufLayoutAllocStatus.Ok)
            {
                shm = null;
            }
            else
            {
                var pShmMut = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedShmMut>());
                Marshal.StructureToPtr(blar.buf, pShmMut, false);
                shm = new ShmMut(pShmMut);
            }

            return blar.status;
        }

        /// <summary>
        /// Make aligned allocation without any additional actions.
        /// </summary>
        /// <param name="size"></param>
        /// <param name="alignment"></param>
        /// <param name="shm"></param>
        /// <returns></returns>
        public BufLayoutAllocStatus AllocAligned(UIntPtr size, AllocAlignment alignment, out ShmMut? shm)
        {
            CheckDisposed();
            var pLoanProvider = LoanedPointer();
            var pBufLayoutAllocResult = Marshal.AllocHGlobal(Marshal.SizeOf<ZBufLayoutAllocResult>());

            ZenohC.z_shm_provider_alloc_aligned(pBufLayoutAllocResult, pLoanProvider, size, alignment);
            var blar = Marshal.PtrToStructure<ZBufLayoutAllocResult>(pBufLayoutAllocResult);
            Marshal.FreeHGlobal(pBufLayoutAllocResult);

            if (blar.status != BufLayoutAllocStatus.Ok)
            {
                shm = null;
            }
            else
            {
                var pShmMut = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedShmMut>());
                Marshal.StructureToPtr(blar.buf, pShmMut, false);
                shm = new ShmMut(pShmMut);
            }

            return blar.status;
        }

        /// <summary>
        /// Make allocation performing garbage collection if needed.
        /// </summary>
        /// <param name="size"></param>
        /// <param name="shm"></param>
        /// <returns></returns>
        public BufLayoutAllocStatus AllocGc(UIntPtr size, out ShmMut? shm)
        {
            CheckDisposed();
            var pLoanProvider = LoanedPointer();
            var pBufLayoutAllocResult = Marshal.AllocHGlobal(Marshal.SizeOf<ZBufLayoutAllocResult>());

            ZenohC.z_shm_provider_alloc_gc(pBufLayoutAllocResult, pLoanProvider, size);
            var blar = Marshal.PtrToStructure<ZBufLayoutAllocResult>(pBufLayoutAllocResult);
            Marshal.FreeHGlobal(pBufLayoutAllocResult);

            if (blar.status != BufLayoutAllocStatus.Ok)
            {
                shm = null;
            }
            else
            {
                var pShmMut = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedShmMut>());
                Marshal.StructureToPtr(blar.buf, pShmMut, false);
                shm = new ShmMut(pShmMut);
            }

            return blar.status;
        }

        /// <summary>
        /// Make aligned allocation performing garbage collection if needed.
        /// </summary>
        /// <param name="size"></param>
        /// <param name="alignment"></param>
        /// <param name="shm"></param>
        /// <returns></returns>
        public BufLayoutAllocStatus AllocGcAligned(UIntPtr size, AllocAlignment alignment, out ShmMut? shm)
        {
            CheckDisposed();
            var pLoanProvider = LoanedPointer();
            var pBufLayoutAllocResult = Marshal.AllocHGlobal(Marshal.SizeOf<ZBufLayoutAllocResult>());

            ZenohC.z_shm_provider_alloc_gc_aligned(pBufLayoutAllocResult, pLoanProvider, size, alignment);
            var blar = Marshal.PtrToStructure<ZBufLayoutAllocResult>(pBufLayoutAllocResult);
            Marshal.FreeHGlobal(pBufLayoutAllocResult);

            if (blar.status != BufLayoutAllocStatus.Ok)
            {
                shm = null;
            }
            else
            {
                var pShmMut = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedShmMut>());
                Marshal.StructureToPtr(blar.buf, pShmMut, false);
                shm = new ShmMut(pShmMut);
            }

            return blar.status;
        }

        /// <summary>
        /// Make allocation performing garbage collection and/or defragmentation if needed.
        /// </summary>
        /// <param name="size"></param>
        /// <param name="shm"></param>
        /// <returns></returns>
        public BufLayoutAllocStatus AllocGcDefrag(UIntPtr size, out ShmMut? shm)
        {
            CheckDisposed();
            var pLoanProvider = LoanedPointer();
            var pBufLayoutAllocResult = Marshal.AllocHGlobal(Marshal.SizeOf<ZBufLayoutAllocResult>());

            ZenohC.z_shm_provider_alloc_gc_defrag(pBufLayoutAllocResult, pLoanProvider, size);
            var blar = Marshal.PtrToStructure<ZBufLayoutAllocResult>(pBufLayoutAllocResult);
            Marshal.FreeHGlobal(pBufLayoutAllocResult);

            if (blar.status != BufLayoutAllocStatus.Ok)
            {
                shm = null;
            }
            else
            {
                var pShmMut = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedShmMut>());
                Marshal.StructureToPtr(blar.buf, pShmMut, false);
                shm = new ShmMut(pShmMut);
            }

            return blar.status;
        }

        /// <summary>
        /// Make aligned allocation performing garbage collection and/or defragmentation if needed.
        /// </summary>
        /// <param name="size"></param>
        /// <param name="alignment"></param>
        /// <param name="shm"></param>
        /// <returns></returns>
        public BufLayoutAllocStatus AllocGcDefragAligned(UIntPtr size, AllocAlignment alignment, out ShmMut? shm)
        {
            CheckDisposed();
            var pLoanProvider = LoanedPointer();
            var pBufLayoutAllocResult = Marshal.AllocHGlobal(Marshal.SizeOf<ZBufLayoutAllocResult>());

            ZenohC.z_shm_provider_alloc_gc_defrag_aligned(pBufLayoutAllocResult, pLoanProvider, size, alignment);
            var blar = Marshal.PtrToStructure<ZBufLayoutAllocResult>(pBufLayoutAllocResult);
            Marshal.FreeHGlobal(pBufLayoutAllocResult);

            if (blar.status != BufLayoutAllocStatus.Ok)
            {
                shm = null;
            }
            else
            {
                var pShmMut = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedShmMut>());
                Marshal.StructureToPtr(blar.buf, pShmMut, false);
                shm = new ShmMut(pShmMut);
            }

            return blar.status;
        }

        /// <summary>
        /// Make allocation performing garbage collection and/or defragmentation and/or blocking if needed.
        /// </summary>
        /// <param name="size"></param>
        /// <param name="shm"></param>
        /// <returns>BufLayoutAllocStatus.Ok is alloc succeed.</returns>
        public BufLayoutAllocStatus AllocGcDefragBlocking(UIntPtr size, out ShmMut? shm)
        {
            CheckDisposed();
            var pLoanProvider = LoanedPointer();
            var pBufLayoutAllocResult = Marshal.AllocHGlobal(Marshal.SizeOf<ZBufLayoutAllocResult>());

            ZenohC.z_shm_provider_alloc_gc_defrag_blocking(pBufLayoutAllocResult, pLoanProvider, size);
            var blar = Marshal.PtrToStructure<ZBufLayoutAllocResult>(pBufLayoutAllocResult);
            Marshal.FreeHGlobal(pBufLayoutAllocResult);

            if (blar.status != BufLayoutAllocStatus.Ok)
            {
                shm = null;
            }
            else
            {
                var pShmMut = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedShmMut>());
                Marshal.StructureToPtr(blar.buf, pShmMut, false);
                shm = new ShmMut(pShmMut);
            }

            return blar.status;
        }

        /// <summary>
        /// Make aligned allocation performing garbage collection and/or defragmentation and/or blocking if needed.
        /// </summary>
        /// <param name="size"></param>
        /// <param name="alignment"></param>
        /// <param name="shm"></param>
        /// <returns>BufLayoutAllocStatus.Ok is alloc succeed.</returns>
        public BufLayoutAllocStatus AllocGcDefragBlockingAligned(UIntPtr size, AllocAlignment alignment,
            out ShmMut? shm)
        {
            CheckDisposed();
            var pLoanProvider = LoanedPointer();
            var pBufLayoutAllocResult = Marshal.AllocHGlobal(Marshal.SizeOf<ZBufLayoutAllocResult>());

            ZenohC.z_shm_provider_alloc_gc_defrag_blocking_aligned(pBufLayoutAllocResult, pLoanProvider, size,
                alignment);
            var blar = Marshal.PtrToStructure<ZBufLayoutAllocResult>(pBufLayoutAllocResult);
            Marshal.FreeHGlobal(pBufLayoutAllocResult);

            if (blar.status != BufLayoutAllocStatus.Ok)
            {
                shm = null;
            }
            else
            {
                var pShmMut = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedShmMut>());
                Marshal.StructureToPtr(blar.buf, pShmMut, false);
                shm = new ShmMut(pShmMut);
            }

            return blar.status;
        }

        /// <summary>
        /// Make allocation performing garbage collection and/or defragmentation and/or forced deallocation if needed.
        /// </summary>
        /// <param name="size"></param>
        /// <param name="shm"></param>
        /// <returns></returns>
        public BufLayoutAllocStatus AllocGcDefragDealloc(UIntPtr size, out ShmMut? shm)
        {
            CheckDisposed();
            var pLoanProvider = LoanedPointer();
            var pBufLayoutAllocResult = Marshal.AllocHGlobal(Marshal.SizeOf<ZBufLayoutAllocResult>());

            ZenohC.z_shm_provider_alloc_gc_defrag_dealloc(pBufLayoutAllocResult, pLoanProvider, size);
            var blar = Marshal.PtrToStructure<ZBufLayoutAllocResult>(pBufLayoutAllocResult);
            Marshal.FreeHGlobal(pBufLayoutAllocResult);

            if (blar.status != BufLayoutAllocStatus.Ok)
            {
                shm = null;
            }
            else
            {
                var pShmMut = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedShmMut>());
                Marshal.StructureToPtr(blar.buf, pShmMut, false);
                shm = new ShmMut(pShmMut);
            }

            return blar.status;
        }

        /// <summary>
        /// Make aligned allocation performing garbage collection and/or defragmentation and/or forced deallocation if needed.
        /// </summary>
        /// <param name="size"></param>
        /// <param name="alignment"></param>
        /// <param name="shm"></param>
        /// <returns></returns>
        public BufLayoutAllocStatus AllocGcDefragDeallocAligned(UIntPtr size, AllocAlignment alignment, out ShmMut? shm)
        {
            CheckDisposed();
            var pLoanProvider = LoanedPointer();
            var pBufLayoutAllocResult = Marshal.AllocHGlobal(Marshal.SizeOf<ZBufLayoutAllocResult>());

            ZenohC.z_shm_provider_alloc_gc_defrag_dealloc_aligned(pBufLayoutAllocResult, pLoanProvider, size,
                alignment);
            var blar = Marshal.PtrToStructure<ZBufLayoutAllocResult>(pBufLayoutAllocResult);
            Marshal.FreeHGlobal(pBufLayoutAllocResult);

            if (blar.status != BufLayoutAllocStatus.Ok)
            {
                shm = null;
            }
            else
            {
                var pShmMut = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedShmMut>());
                Marshal.StructureToPtr(blar.buf, pShmMut, false);
                shm = new ShmMut(pShmMut);
            }

            return blar.status;
        }


        // z_shm_provider_alloc_layout

        /// <summary>
        /// Creates a new Alloc Layout for SHM Provider.
        /// </summary>
        /// <param name="size"></param>
        /// <param name="precomputedLayout"></param>
        /// <returns></returns>
        public Result AllocLayout(UIntPtr size, out PrecomputedLayout? precomputedLayout)
        {
            CheckDisposed();
            var pLoanProvider = LoanedPointer();
            var pOwnedPrecomputedLayout = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedPrecomputedLayout>());

            var r = ZenohC.z_shm_provider_alloc_layout(pOwnedPrecomputedLayout, pLoanProvider, size);
            if (r != Result.Ok)
            {
                Marshal.FreeHGlobal(pOwnedPrecomputedLayout);
                precomputedLayout = null;
            }
            else
            {
                precomputedLayout = new PrecomputedLayout(pOwnedPrecomputedLayout);
            }

            return r;
        }

        /// <summary>
        /// Creates a new Alloc Layout for SHM Provider specifying the exact alignment.
        /// </summary>
        /// <param name="size"></param>
        /// <param name="alignment"></param>
        /// <param name="precomputedLayout"></param>
        /// <returns></returns>
        public Result AllocLayoutAligned(UIntPtr size, AllocAlignment alignment,
            out PrecomputedLayout? precomputedLayout)
        {
            CheckDisposed();
            var pLoanProvider = LoanedPointer();
            var pOwnedPrecomputedLayout = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedPrecomputedLayout>());

            var r = ZenohC.z_shm_provider_alloc_layout_aligned(pOwnedPrecomputedLayout, pLoanProvider, size, alignment);
            if (r != Result.Ok)
            {
                Marshal.FreeHGlobal(pOwnedPrecomputedLayout);
                precomputedLayout = null;
            }
            else
            {
                precomputedLayout = new PrecomputedLayout(pOwnedPrecomputedLayout);
            }

            return r;
        }
    }

    // z_owned_precomputed_layout_t
    /// <summary>
    /// An owned ShmProvider's PrecomputedLayout.
    /// </summary>
    public sealed class PrecomputedLayout
    {
        private IntPtr Handle { get; set; }

        private PrecomputedLayout()
        {
            throw new InvalidOperationException();
        }

        private PrecomputedLayout(PrecomputedLayout other)
        {
            throw new InvalidOperationException();
        }

        internal PrecomputedLayout(IntPtr handle)
        {
            Handle = handle;
        }

        ~PrecomputedLayout()
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
            if (Handle == IntPtr.Zero) return;

            ZenohC.z_precomputed_layout_drop(Handle);
            Marshal.FreeHGlobal(Handle);

            Handle = IntPtr.Zero;
        }

        internal IntPtr LoanedPointer()
        {
            return ZenohC.z_precomputed_layout_loan(Handle);
        }

        internal void CheckDisposed()
        {
            if (Handle == IntPtr.Zero)
                throw new ObjectDisposedException(nameof(PrecomputedLayout));
        }

        /// <summary>
        /// Make allocation without any additional actions.
        /// </summary>
        /// <param name="shm"></param>
        /// <returns></returns>
        public BufLayoutAllocStatus Alloc(out ShmMut? shm)
        {
            var pPrecomputedLayout = LoanedPointer();
            var pBufLayoutAllocResult = Marshal.AllocHGlobal(Marshal.SizeOf<ZBufLayoutAllocResult>());

            ZenohC.z_precomputed_layout_alloc(pBufLayoutAllocResult, pPrecomputedLayout);
            var blar = Marshal.PtrToStructure<ZBufLayoutAllocResult>(pBufLayoutAllocResult);
            Marshal.FreeHGlobal(pBufLayoutAllocResult);

            if (blar.status != BufLayoutAllocStatus.Ok)
            {
                shm = null;
            }
            else
            {
                var pShmMut = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedShmMut>());
                Marshal.StructureToPtr(blar.buf, pShmMut, false);
                shm = new ShmMut(pShmMut);
            }

            return blar.status;
        }

        /// <summary>
        /// Make allocation performing garbage collection if needed.
        /// </summary>
        /// <param name="shm"></param>
        /// <returns></returns>
        public BufLayoutAllocStatus AllocGc(out ShmMut? shm)
        {
            var pPrecomputedLayout = LoanedPointer();
            var pBufLayoutAllocResult = Marshal.AllocHGlobal(Marshal.SizeOf<ZBufLayoutAllocResult>());

            ZenohC.z_precomputed_layout_alloc_gc(pBufLayoutAllocResult, pPrecomputedLayout);
            var blar = Marshal.PtrToStructure<ZBufLayoutAllocResult>(pBufLayoutAllocResult);
            Marshal.FreeHGlobal(pBufLayoutAllocResult);

            if (blar.status != BufLayoutAllocStatus.Ok)
            {
                shm = null;
            }
            else
            {
                var pShmMut = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedShmMut>());
                Marshal.StructureToPtr(blar.buf, pShmMut, false);
                shm = new ShmMut(pShmMut);
            }

            return blar.status;
        }

        /// <summary>
        /// Make allocation performing garbage collection and/or defragmentation if needed.
        /// </summary>
        /// <param name="shm"></param>
        /// <returns></returns>
        public BufLayoutAllocStatus AllocGcDefrag(out ShmMut? shm)
        {
            var pPrecomputedLayout = LoanedPointer();
            var pBufLayoutAllocResult = Marshal.AllocHGlobal(Marshal.SizeOf<ZBufLayoutAllocResult>());

            ZenohC.z_precomputed_layout_alloc_gc_defrag(pBufLayoutAllocResult, pPrecomputedLayout);
            var blar = Marshal.PtrToStructure<ZBufLayoutAllocResult>(pBufLayoutAllocResult);
            Marshal.FreeHGlobal(pBufLayoutAllocResult);

            if (blar.status != BufLayoutAllocStatus.Ok)
            {
                shm = null;
            }
            else
            {
                var pShmMut = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedShmMut>());
                Marshal.StructureToPtr(blar.buf, pShmMut, false);
                shm = new ShmMut(pShmMut);
            }

            return blar.status;
        }

        /// <summary>
        /// Make allocation performing garbage collection and/or defragmentation and/or blocking if needed.
        /// </summary>
        /// <param name="shm"></param>
        /// <returns></returns>
        public BufLayoutAllocStatus AllocGcDefragBlocking(out ShmMut? shm)
        {
            var pPrecomputedLayout = LoanedPointer();
            var pBufLayoutAllocResult = Marshal.AllocHGlobal(Marshal.SizeOf<ZBufLayoutAllocResult>());

            ZenohC.z_precomputed_layout_alloc_gc_defrag_blocking(pBufLayoutAllocResult, pPrecomputedLayout);
            var blar = Marshal.PtrToStructure<ZBufLayoutAllocResult>(pBufLayoutAllocResult);
            Marshal.FreeHGlobal(pBufLayoutAllocResult);

            if (blar.status != BufLayoutAllocStatus.Ok)
            {
                shm = null;
            }
            else
            {
                var pShmMut = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedShmMut>());
                Marshal.StructureToPtr(blar.buf, pShmMut, false);
                shm = new ShmMut(pShmMut);
            }

            return blar.status;
        }

        /// <summary>
        /// Make allocation performing garbage collection and/or defragmentation and/or forced deallocation if needed.
        /// </summary>
        /// <param name="shm"></param>
        /// <returns></returns>
        public BufLayoutAllocStatus AllocGcDefragDealloc(out ShmMut? shm)
        {
            var pPrecomputedLayout = LoanedPointer();
            var pBufLayoutAllocResult = Marshal.AllocHGlobal(Marshal.SizeOf<ZBufLayoutAllocResult>());

            ZenohC.z_precomputed_layout_alloc_gc_defrag_dealloc(pBufLayoutAllocResult, pPrecomputedLayout);
            var blar = Marshal.PtrToStructure<ZBufLayoutAllocResult>(pBufLayoutAllocResult);
            Marshal.FreeHGlobal(pBufLayoutAllocResult);

            if (blar.status != BufLayoutAllocStatus.Ok)
            {
                shm = null;
            }
            else
            {
                var pShmMut = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedShmMut>());
                Marshal.StructureToPtr(blar.buf, pShmMut, false);
                shm = new ShmMut(pShmMut);
            }

            return blar.status;
        }
    }

    // z_owned_shm_t
    /// <summary>
    ///     An owned readonly SHM.
    /// </summary>
    public sealed class Shm
    {
        /// 'true' in case owned, 'false' in case loaned
        public bool Owned { get; }

        internal IntPtr Handle { get; set; }

        private Shm()
        {
            throw new InvalidOperationException();
        }

        /// <summary>
        ///     Converts borrowed ZShm slice to owned ZShm slice by performing a shallow SHM reference copy.
        /// </summary>
        /// <param name="other"></param>
        public Shm(Shm other)
        {
            other.CheckDisposed();

            var pOwnedShm = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedShm>());
            var pLoanedShm = other.LoanedPointer();
            ZenohC.z_shm_clone(pOwnedShm, pLoanedShm);

            Handle = pOwnedShm;
            Owned = true;
        }

        private Shm(IntPtr handle, bool owned)
        {
            Owned = owned;
            Handle = handle;
        }

        ~Shm()
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
            if (Handle == IntPtr.Zero) return;

            if (Owned)
            {
                ZenohC.z_shm_drop(Handle);
                Marshal.FreeHGlobal(Handle);
            }

            Handle = IntPtr.Zero;
        }

        internal IntPtr LoanedPointer()
        {
            return Owned ? ZenohC.z_shm_loan(Handle) : Handle;
        }

        internal void CheckDisposed()
        {
            if (Handle == IntPtr.Zero) throw new ObjectDisposedException(nameof(Shm));
        }
    }

    // z_owned_shm_mut_t
    /// <summary>
    ///     An owned readable and writable SHM.
    /// </summary>
    public sealed class ShmMut
    {
        private IntPtr Handle { get; set; }

        private ShmMut()
        {
            throw new InvalidOperationException();
        }

        private ShmMut(ShmMut other)
        {
            throw new InvalidOperationException();
        }

        internal ShmMut(IntPtr handle)
        {
            Handle = handle;
        }

        ~ShmMut()
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
            if (Handle == IntPtr.Zero) return;

            ZenohC.z_shm_mut_drop(Handle);
            Marshal.FreeHGlobal(Handle);

            Handle = IntPtr.Zero;
        }

        internal IntPtr LoanedPointer()
        {
            return ZenohC.z_shm_mut_loan(Handle);
        }

        internal IntPtr LoanedMutPointer()
        {
            return ZenohC.z_shm_mut_loan_mut(Handle);
        }

        internal void CheckDisposed()
        {
            if (Handle == IntPtr.Zero) throw new ObjectDisposedException(nameof(ShmMut));
        }

        /// <summary>
        /// return the length of the ZShmMut slice. 
        /// </summary>
        public UIntPtr Len()
        {
            CheckDisposed();
            var p = LoanedPointer();
            return ZenohC.z_shm_mut_len(p);
        }

        /// <summary>
        /// return the mutable pointer to the underlying data.
        /// </summary>
        public IntPtr DataPointer()
        {
            CheckDisposed();
            var p = LoanedMutPointer();
            return ZenohC.z_shm_mut_data_mut(p);
        }

        /// <summary>
        /// Write data to shared memory.
        /// </summary>
        /// <param name="data"></param>
        /// <exception cref="InvalidOperationException">The data length is greater than the memory size.</exception>
        public void WriteData(byte[] data)
        {
            CheckDisposed();
            var pLoanedShmMut = LoanedPointer();

            var bufLen = ZenohC.z_shm_mut_len(pLoanedShmMut);
            if (data.Length > (int)bufLen) throw new InvalidOperationException();

            var pMem = ZenohC.z_shm_mut_data_mut(pLoanedShmMut);
            Marshal.Copy(data, 0, pMem, data.Length);
        }

        /// <summary>
        /// Convert to ZBytes. 
        /// Do not use the "ShmMut" after calling this function.
        /// Dispose() is called inside this function.
        /// </summary>
        public ZBytes ToZBytes()
        {
            CheckDisposed();
            var payload = new ZBytes();
            ZenohC.z_bytes_from_shm_mut(payload.Handle, Handle);
            Dispose();
            return payload;
        }
    }
#endif
}