using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Zenoh
{
    public abstract class Loanable : IDisposable
    {
        /// 'true' in case owned, 'false' in case loaned 
        public bool Owned { get; private protected set; }

        internal IntPtr Handle { get; private protected set; }

        public abstract void Dispose();

        public abstract void ToOwned();

        internal abstract IntPtr LoanedPointer();

        internal void CheckDisposed()
        {
            if (Handle == IntPtr.Zero)
            {
                throw new InvalidOperationException("Object is disposed");
            }
        }
    }

    // z_owned_string_t
    public sealed class ZString : Loanable
    {
        public ZString()
        {
            var pOwnedString = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedString>());
            ZenohC.z_string_empty(pOwnedString);
            Handle = pOwnedString;
            Owned = true;
        }

        public ZString(ZString other)
        {
            other.CheckDisposed();

            var pOwnedString = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedString>());
            var pLoanedString = other.LoanedPointer();
            ZenohC.z_string_clone(pOwnedString, pLoanedString);
            Handle = pOwnedString;
            Owned = true;
        }

        public ZString(string str)
        {
            var pOwnedString = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedString>());
            var utf8Bytes = System.Text.Encoding.UTF8.GetBytes(str);
            Result r;
            unsafe
            {
                fixed (void* pStr = utf8Bytes)
                {
                    r = ZenohC.z_string_copy_from_substr(pOwnedString, (IntPtr)pStr, (UIntPtr)utf8Bytes.Length);
                }
            }

            if (r != Result.Ok) ZenohC.z_string_empty(pOwnedString);
            Handle = pOwnedString;
            Owned = true;
        }

        private ZString(IntPtr handle, bool owned)
        {
            Handle = handle;
            Owned = owned;
        }

        public override void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~ZString() => Dispose(false);

        private void Dispose(bool disposing)
        {
            if (Handle == IntPtr.Zero) return;

            if (Owned)
            {
                ZenohC.z_string_drop(Handle);
                Marshal.FreeHGlobal(Handle);
            }

            Handle = IntPtr.Zero;
        }

        public override void ToOwned()
        {
            if (Owned) return;

            var pOwnedString = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedString>());
            var pLoanedString = LoanedPointer();
            ZenohC.z_string_clone(pOwnedString, pLoanedString);
            Handle = pOwnedString;
            Owned = true;
        }

        internal override IntPtr LoanedPointer()
        {
            return Owned ? ZenohC.z_string_loan(Handle) : Handle;
        }

        internal static ZString CreateLoaned(IntPtr handle)
        {
            return new ZString(handle, false);
        }

        internal static ZString CreateOwned()
        {
            return new ZString();
        }

        public bool IsEmpty()
        {
            CheckDisposed();

            var pLoanedString = ZenohC.z_string_loan(Handle);
            return ZenohC.z_string_is_empty(pLoanedString);
        }

        public UIntPtr Length()
        {
            CheckDisposed();

            var pLoanedString = ZenohC.z_string_loan(Handle);
            return ZenohC.z_string_len(pLoanedString);
        }

        public override string ToString()
        {
            CheckDisposed();

            var pLoanedString = LoanedPointer();
            var pS = ZenohC.z_string_data(pLoanedString);
            var sLen = ZenohC.z_string_len(pLoanedString);
            string s;
            try
            {
                unsafe
                {
                    s = System.Text.Encoding.UTF8.GetString((byte*)pS, (int)sLen);
                }
            }
            catch
            {
                s = "";
            }

            return s;
        }
    }

    internal sealed class ViewString : IDisposable
    {
        internal IntPtr Handle { get; private set; }

        public ViewString()
        {
            var pViewString = Marshal.AllocHGlobal(Marshal.SizeOf<ZViewString>());
            ZenohC.z_view_string_empty(pViewString);
            Handle = pViewString;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~ViewString() => Dispose(false);

        private void Dispose(bool disposing)
        {
            if (Handle == IntPtr.Zero) return;

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

        public override string ToString()
        {
            CheckDisposed();

            var pLoanedString = ZenohC.z_view_string_loan(Handle);
            var pS = ZenohC.z_string_data(pLoanedString);
            var sLen = ZenohC.z_string_len(pLoanedString);
            string s;
            try
            {
                unsafe
                {
                    s = System.Text.Encoding.UTF8.GetString((byte*)pS, (int)sLen);
                }
            }
            catch
            {
                s = "";
            }

            return s;
        }
    }

    // z_owned_bytes_t
    public sealed class ZBytes : Loanable
    {
        public ZBytes()
        {
            var pOwnedBytes = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedBytes>());
            ZenohC.z_bytes_empty(pOwnedBytes);
            Handle = pOwnedBytes;
            Owned = true;
        }

        public ZBytes(ZBytes other)
        {
            other.CheckDisposed();

            var pOwnedBytes = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedBytes>());
            var pLoanedBytes = other.LoanedPointer();
            ZenohC.z_bytes_clone(pOwnedBytes, pLoanedBytes);
            Handle = pOwnedBytes;
            Owned = true;
        }

        private ZBytes(IntPtr handle, bool owned)
        {
            Handle = handle;
            Owned = owned;
        }

        public override void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~ZBytes() => Dispose(false);

        private void Dispose(bool disposing)
        {
            if (Handle == IntPtr.Zero) return;

            if (Owned)
            {
                ZenohC.z_bytes_drop(Handle);
                Marshal.FreeHGlobal(Handle);
            }

            Handle = IntPtr.Zero;
        }

        // 'handle'  z_loaned_bytes_t*
        internal static ZBytes CreateLoaned(IntPtr handle)
        {
            return new ZBytes(handle, false);
        }

        public override void ToOwned()
        {
            if (Owned) return;

            var pOwnedBytes = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedBytes>());
            var pLoanedBytes = LoanedPointer();
            ZenohC.z_bytes_clone(pOwnedBytes, pLoanedBytes);
            Handle = pOwnedBytes;
            Owned = true;
        }

        internal override IntPtr LoanedPointer()
        {
            return Owned ? ZenohC.z_bytes_loan(Handle) : Handle;
        }

        public bool IsEmpty()
        {
            CheckDisposed();

            var pLoanedBytes = ZenohC.z_bytes_loan(Handle);
            return ZenohC.z_bytes_is_empty(pLoanedBytes);
        }

        public UIntPtr Length()
        {
            CheckDisposed();

            var pLoanedBytes = LoanedPointer();
            return ZenohC.z_bytes_len(pLoanedBytes);
        }

        internal IntPtr AllocUnmanagedMemory()
        {
            var pOwnedBytes = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedBytes>());
            var pLoanedBytes = LoanedPointer();
            ZenohC.z_bytes_clone(pOwnedBytes, pLoanedBytes);
            return pOwnedBytes;
        }

        internal static void FreeUnmanagedMem(IntPtr handle)
        {
            ZenohC.z_bytes_drop(handle);
            Marshal.FreeHGlobal(handle);
        }

        /// <summary>
        /// Convert str to a utf-8 string and copy.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static ZBytes FromString(string str)
        {
            var utf8Bytes = System.Text.Encoding.UTF8.GetBytes(str);
            var zBytes = new ZBytes();
            unsafe
            {
                fixed (void* pStr = utf8Bytes)
                {
                    ZenohC.z_bytes_copy_from_buf(zBytes.Handle, (IntPtr)pStr, (UIntPtr)utf8Bytes.Length);
                }
            }

            return zBytes;
        }

        public static ZBytes FromBytes(byte[] bytes)
        {
            var zBytes = new ZBytes();
            unsafe
            {
                fixed (void* bytePtr = bytes)
                {
                    ZenohC.z_bytes_copy_from_buf(zBytes.Handle, (IntPtr)bytePtr, (UIntPtr)bytes.Length);
                }
            }

            return zBytes;
        }

        /// <summary>
        /// ZByte is generated by copying data from byte arrays in unmanaged memory.
        /// </summary>
        /// <param name="dataPtr">A pointer to a bytes array.</param>
        /// <param name="dataLength">Bytes array length.</param>
        /// <returns></returns>
        public static ZBytes FromStaticBytes(IntPtr dataPtr, UIntPtr dataLength)
        {
            var zBytes = new ZBytes();
            ZenohC.z_bytes_copy_from_buf(zBytes.Handle, dataPtr, dataLength);

            return zBytes;
        }

        public ZString? ToZString()
        {
            CheckDisposed();

            var zString = new ZString();
            var pLoanedBytes = LoanedPointer();
            var r = ZenohC.z_bytes_to_string(pLoanedBytes, zString.Handle);
            if (r == Result.Ok) return zString;

            zString.Dispose();
            return null;
        }
    }

    // z_owned_slice_t
    public sealed class ZSlice : Loanable
    {
        public ZSlice()
        {
            var pOwnedSlice = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedSlice>());
            ZenohC.z_slice_empty(pOwnedSlice);
            Handle = pOwnedSlice;
            Owned = true;
        }

        public ZSlice(ZSlice other)
        {
            other.CheckDisposed();

            var pOwnedSlice = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedSlice>());
            var pLoanedSlice = other.LoanedPointer();
            ZenohC.z_slice_clone(pOwnedSlice, pLoanedSlice);
            Handle = pOwnedSlice;
            Owned = true;
        }

        public override void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~ZSlice() => Dispose(false);

        private void Dispose(bool disposing)
        {
            if (Handle == IntPtr.Zero) return;

            if (Owned)
            {
                ZenohC.z_slice_drop(Handle);
                Marshal.FreeHGlobal(Handle);
            }

            Handle = IntPtr.Zero;
        }

        internal override IntPtr LoanedPointer()
        {
            return Owned ? ZenohC.z_slice_loan(Handle) : Handle;
        }

        public override void ToOwned()
        {
            if (Owned) return;

            var pOwnedSlice = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedSlice>());
            var pLoanedSlice = LoanedPointer();
            ZenohC.z_slice_clone(pOwnedSlice, pLoanedSlice);
            Handle = pOwnedSlice;
            Owned = true;
        }

        public bool IsEmpty()
        {
            if (Handle == IntPtr.Zero)
            {
                throw new InvalidOperationException("Object has been destroyed");
            }

            var pLoanedSlice = ZenohC.z_slice_loan(Handle);
            return ZenohC.z_slice_is_empty(pLoanedSlice);
        }

        public UIntPtr Length()
        {
            if (Handle == IntPtr.Zero)
            {
                throw new InvalidOperationException("Object has been destroyed");
            }

            var pLoanedSlice = ZenohC.z_slice_loan(Handle);
            return ZenohC.z_slice_len(pLoanedSlice);
        }
    }

    public sealed class Timestamp : IDisposable
    {
        // z_timestamp_t*
        internal IntPtr Handle { get; private set; }

        private Timestamp()
        {
            throw new InvalidOperationException();
        }

        private Timestamp(IntPtr handle)
        {
            Handle = handle;
        }

        public Timestamp(Timestamp other)
        {
            other.CheckDisposed();

            var timestamp = Marshal.PtrToStructure<ZTimestamp>(other.Handle);
            var pTimestamp = Marshal.AllocHGlobal(Marshal.SizeOf<ZTimestamp>());
            Marshal.StructureToPtr(timestamp, pTimestamp, false);
            Handle = pTimestamp;
        }

        // 'handle' z_timestamp_t*
        internal static Timestamp CloneFromPointer(IntPtr handle)
        {
            var timestamp = Marshal.PtrToStructure<ZTimestamp>(handle);
            var pTimestamp = Marshal.AllocHGlobal(Marshal.SizeOf<ZTimestamp>());
            Marshal.StructureToPtr(timestamp, pTimestamp, false);
            return new Timestamp(pTimestamp);
        }

        // 'handle' z_loaned_session_t*
        internal static Timestamp? NewFromSession(IntPtr handle)
        {
            var pTimestamp = Marshal.AllocHGlobal(Marshal.SizeOf<ZTimestamp>());
            var r = ZenohC.z_timestamp_new(pTimestamp, handle);
            Timestamp? o;
            if (r == Result.Ok)
            {
                o = new Timestamp(pTimestamp);
            }
            else
            {
                Marshal.FreeHGlobal(pTimestamp);
                o = null;
            }

            return o;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~Timestamp() => Dispose(false);

        private void Dispose(bool disposing)
        {
            if (Handle == IntPtr.Zero) return;

            Marshal.FreeHGlobal(Handle);
            Handle = IntPtr.Zero;
        }

        public void CheckDisposed()
        {
            if (Handle == IntPtr.Zero)
            {
                throw new InvalidOperationException("Object has been destroyed");
            }
        }

        public Id GetId()
        {
            CheckDisposed();

            var zid = ZenohC.z_timestamp_id(Handle);
            return new Id(zid);
        }

        public ulong Ntp64Time()
        {
            CheckDisposed();

            return ZenohC.z_timestamp_ntp64_time(Handle);
        }

        internal IntPtr AllocUnmanagedMem()
        {
            var zTimestamp = Marshal.PtrToStructure<ZTimestamp>(Handle);
            var pTimestamp = Marshal.AllocHGlobal(Marshal.SizeOf<ZTimestamp>());
            Marshal.StructureToPtr(zTimestamp, pTimestamp, false);
            return pTimestamp;
        }

        internal static void FreeUnmanagedMem(IntPtr handle)
        {
            Marshal.FreeHGlobal(handle);
        }
    }

    public sealed class Id
    {
        public delegate void Cb(Id id);

        private byte[] _data;

        private Id()
        {
            throw new InvalidOperationException();
        }

        public Id(Id other)
        {
            _data = new byte[16];
            Array.Copy(other._data, _data, 16);
        }

        internal Id(ZId zid)
        {
            _data = zid.GetId();
        }

        public string ToHexStr()
        {
            var sb = new StringBuilder();
            foreach (var b in _data)
            {
                sb.Append(b.ToString("x"));
            }

            return sb.ToString();
        }

        public byte[] GetValue()
        {
            var b = new byte[_data.Length];
            Array.Copy(_data, b, _data.Length);
            return b;
        }

        internal static void CallbackClosureIdCall(IntPtr id, IntPtr context)
        {
            var gcHandle = GCHandle.FromIntPtr(context);
            if (!(gcHandle.Target is Cb callback)) return;

            var zid = Marshal.PtrToStructure<ZId>(id);
            callback(new Id(zid));
        }

        internal static void CallbackClosureIdDrop(IntPtr context)
        {
            var gcHandle = GCHandle.FromIntPtr(context);
            gcHandle.Free();
        }
    }

    // z_owned_keyexpr_t
    public sealed class Keyexpr : Loanable
    {
        internal Keyexpr()
        {
            var pOwnedKeyexpr = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedKeyexpr>());
            ZenohC.z_internal_keyexpr_null(pOwnedKeyexpr);
            Handle = pOwnedKeyexpr;
            Owned = true;
        }

        private Keyexpr(IntPtr handle, bool owned)
        {
            Handle = handle;
            Owned = owned;
        }

        public Keyexpr(Keyexpr other)
        {
            other.CheckDisposed();

            var pOwnedKeyexpr = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedKeyexpr>());
            var pOtherKeyexpr = other.LoanedPointer();
            ZenohC.z_keyexpr_clone(pOwnedKeyexpr, pOtherKeyexpr);
            Handle = pOwnedKeyexpr;
            Owned = true;
        }

        // 'handle' z_loaned_keyexpr*
        internal static Keyexpr CreateLoaned(IntPtr handle)
        {
            return new Keyexpr(handle, false);
        }

        public static Keyexpr? FromString(string keyexpr)
        {
            var pOwnedKeyexpr = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedKeyexpr>());

            var utf8Bytes = System.Text.Encoding.UTF8.GetBytes(keyexpr);
            var length = (UIntPtr)utf8Bytes.Length;
            Result r;
            unsafe
            {
                fixed (byte* pStr = utf8Bytes)
                {
                    r = ZenohC.z_keyexpr_from_substr_autocanonize(pOwnedKeyexpr, (IntPtr)pStr, (IntPtr)(&length));
                }
            }

            if (r == Result.Ok) return new Keyexpr(pOwnedKeyexpr, true);

            Marshal.FreeHGlobal(pOwnedKeyexpr);
            return null;
        }

        /// Constructs key expression by performing path-joining (automatically inserting '/' in-between) of `left` with `right`.
        public static Keyexpr? Join(Keyexpr left, Keyexpr right)
        {
            var pOwnedKeyexpr = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedKeyexpr>());
            var pLeft = left.LoanedPointer();
            var pRight = right.LoanedPointer();
            var r = ZenohC.z_keyexpr_join(pOwnedKeyexpr, pLeft, pRight);
            if (r == Result.Ok) return new Keyexpr(pOwnedKeyexpr, true);

            Marshal.FreeHGlobal(pOwnedKeyexpr);
            return null;
        }

        public override void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~Keyexpr() => Dispose(false);

        private void Dispose(bool disposing)
        {
            if (Handle == IntPtr.Zero) return;

            if (Owned)
            {
                ZenohC.z_keyexpr_drop(Handle);
                Marshal.FreeHGlobal(Handle);
            }

            Handle = IntPtr.Zero;
        }

        public override void ToOwned()
        {
            if (Owned) return;

            var pOwnedKeyexpr = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedKeyexpr>());
            var pLoanedKeyexpr = LoanedPointer();
            ZenohC.z_keyexpr_clone(pOwnedKeyexpr, pLoanedKeyexpr);
            Handle = pOwnedKeyexpr;
            Owned = true;
        }

        internal override IntPtr LoanedPointer()
        {
            return Owned ? ZenohC.z_keyexpr_loan(Handle) : Handle;
        }

        /// <summary>
        /// Returns Result.Ok if the passed string is a valid (and canon) key expression.
        /// </summary>
        /// <param name="keyexpr"></param>
        /// <returns></returns>
        public static Result IsCanon(string keyexpr)
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(keyexpr);
            var len = (UIntPtr)bytes.Length;
            Result r;
            unsafe
            {
                fixed (byte* ptr = bytes)
                {
                    r = ZenohC.z_keyexpr_is_canon((IntPtr)ptr, len);
                }
            }

            return r;
        }

        public bool Equals(Keyexpr other)
        {
            var pThis = LoanedPointer();
            var pOther = other.LoanedPointer();
            return ZenohC.z_keyexpr_equals(pThis, pOther);
        }

        /// Return true if 'this' includes 'other',
        /// i.e. the set defined by `this` contains every key belonging to the set defined by `other`.
        public bool Includes(Keyexpr other)
        {
            var pThis = LoanedPointer();
            var pOther = other.LoanedPointer();
            return ZenohC.z_keyexpr_includes(pThis, pOther);
        }

        /// Returns true if the keyexprs intersect,
        /// i.e. there exists at least one key which is contained in both of the sets defined by `this` and `other`.
        public bool Intersects(Keyexpr other)
        {
            var pThis = LoanedPointer();
            var pOther = other.LoanedPointer();
            return ZenohC.z_keyexpr_intersects(pThis, pOther);
        }

        public override string ToString()
        {
            var viewString = new ViewString();
            var pLoanedKeyexpr = LoanedPointer();
            ZenohC.z_keyexpr_as_view_string(pLoanedKeyexpr, viewString.Handle);
            var s = viewString.ToString();
            viewString.Dispose();
            return s;
        }
    }

    // z_owned_sample_t
    public sealed class Sample : Loanable
    {
        private Sample()
        {
            throw new InvalidOperationException();
        }

        internal Sample(Sample other)
        {
            other.CheckDisposed();

            var pOwnedSample = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedSample>());
            var pLoanedSample = other.LoanedPointer();
            ZenohC.z_sample_clone(pOwnedSample, pLoanedSample);
            Owned = true;
            Handle = pOwnedSample;
        }

        private Sample(IntPtr handle, bool owned)
        {
            Owned = owned;
            Handle = handle;
        }

        internal static Sample CreateLoaned(IntPtr handle)
        {
            return new Sample(handle, false);
        }

        internal static Sample CreateOwned()
        {
            var pOwnedSample = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedSample>());
            ZenohC.z_internal_sample_null(pOwnedSample);
            return new Sample(pOwnedSample, true);
        }

        ~Sample()
        {
            Dispose(false);
        }

        public override void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (Handle == IntPtr.Zero) return;

            if (Owned)
            {
                ZenohC.z_sample_drop(Handle);
                Marshal.FreeHGlobal(Handle);
            }

            Handle = IntPtr.Zero;
        }

        public override void ToOwned()
        {
            if (Owned) return;

            var pOwnedSample = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedSample>());
            var pLoanedSample = LoanedPointer();
            ZenohC.z_sample_clone(pOwnedSample, pLoanedSample);
            Owned = true;
            Handle = pOwnedSample;
        }

        internal override IntPtr LoanedPointer()
        {
            return Owned ? ZenohC.z_sample_loan(Handle) : Handle;
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

            var pLoanedSample = LoanedPointer();
            var pLoanedBytes = ZenohC.z_sample_attachment(pLoanedSample);
            return pLoanedBytes == IntPtr.Zero ? null : ZBytes.CreateLoaned(pLoanedBytes);
        }

        /// <summary>
        /// Returns sample qos congestion control value.
        /// </summary>
        /// <returns></returns>
        public CongestionControl GetCongestionControl()
        {
            CheckDisposed();

            var pLoanedSample = LoanedPointer();
            return ZenohC.z_sample_congestion_control(pLoanedSample);
        }

        /// <summary>
        /// Returns the encoding associated with the sample data.
        /// </summary>
        /// <returns></returns>
        public Encoding GetEncoding()
        {
            CheckDisposed();

            var pLoanedSample = LoanedPointer();
            var pLoanedEncoding = ZenohC.z_sample_encoding(pLoanedSample);
            return Encoding.CreateLoaned(pLoanedEncoding);
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

            var pLoanedSample = LoanedPointer();
            return ZenohC.z_sample_express(pLoanedSample);
        }

        /// <summary>
        /// Returns the key expression of the sample.
        /// </summary>
        /// <returns>
        /// The return Keyexpr is loaned. 
        /// </returns>
        public Keyexpr GetKeyexpr()
        {
            CheckDisposed();

            var pLoanedSample = LoanedPointer();
            var pLoanedKeyexpr = ZenohC.z_sample_keyexpr(pLoanedSample);
            return Keyexpr.CreateLoaned(pLoanedKeyexpr);
        }

        /// <summary>
        /// Returns the sample kind.
        /// </summary>
        /// <returns></returns>
        public SampleKind GetKind()
        {
            CheckDisposed();

            var pLoanedSample = LoanedPointer();
            return ZenohC.z_sample_kind(pLoanedSample);
        }

        /// <summary>
        /// Returns the sample payload data.
        /// </summary>
        /// <returns>
        /// The return ZBytes is loaned. 
        /// </returns>
        public ZBytes GetPayload()
        {
            CheckDisposed();

            var pLoanedSample = LoanedPointer();
            var pLoanedBytes = ZenohC.z_sample_payload(pLoanedSample);
            return ZBytes.CreateLoaned(pLoanedBytes);
        }

        /// <summary>
        /// Returns sample qos priority value.
        /// </summary>
        /// <returns></returns>
        public Priority GetPriority()
        {
            CheckDisposed();

            var pLoanedSample = LoanedPointer();
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

            var pLoanedSample = LoanedPointer();
            var pTimestamp = ZenohC.z_sample_timestamp(pLoanedSample);
            return pTimestamp == IntPtr.Zero ? null : Timestamp.CloneFromPointer(pTimestamp);
        }

#if UNSTABLE_API
        /// <summary>
        /// Returns the reliability setting the sample was delivered with.
        /// </summary>
        /// <returns></returns>
        public Reliability GetReliability()
        {
            CheckDisposed();

            var pLoanedSample = LoanedPointer();
            return ZenohC.z_sample_reliability(pLoanedSample);
        }
#endif

#if UNSTABLE_API
        /// <summary>
        /// Returns the sample source_info.
        /// </summary>
        /// <returns></returns>
        public SourceInfo GetSourceInfo()
        {
            CheckDisposed();

            var pLoanedSample = LoanedPointer();
            var pLoanedSourceInfo = ZenohC.z_sample_source_info(pLoanedSample);
            return new SourceInfo(pLoanedSourceInfo, false);
        }
#endif
    }

#if UNSTABLE_API
    public sealed class EntityGlobalId : IDisposable
    {
        // z_entity_global_id_t*
        internal IntPtr Handle { get; private set; }

        private EntityGlobalId()
        {
            throw new InvalidOperationException();
        }

        internal EntityGlobalId(ZEntityGlobalId id)
        {
            var pEntityGlobalId = Marshal.AllocHGlobal(Marshal.SizeOf<ZEntityGlobalId>());
            Marshal.StructureToPtr(id, pEntityGlobalId, false);
            Handle = pEntityGlobalId;
        }

        /// <summary>
        /// Returns the entity id of the entity global id.
        /// </summary>
        /// <returns></returns>
        public uint GetEntityId()
        {
            CheckDisposed();

            return ZenohC.z_entity_global_id_eid(Handle);
        }

        /// <summary>
        /// Returns the zenoh id of entity global id.
        /// </summary>
        /// <returns></returns>
        public Id GetZenohId()
        {
            CheckDisposed();

            var zid = ZenohC.z_entity_global_id_zid(Handle);
            return new Id(zid);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~EntityGlobalId() => Dispose(false);

        private void Dispose(bool disposing)
        {
            if (Handle == IntPtr.Zero) return;

            Marshal.FreeHGlobal(Handle);
            Handle = IntPtr.Zero;
        }

        public void CheckDisposed()
        {
            if (Handle == IntPtr.Zero)
            {
                throw new InvalidOperationException("Object has been destroyed");
            }
        }
    }
#endif

#if UNSTABLE_API
    // z_owned_source_info_t
    public sealed class SourceInfo : Loanable
    {
        private SourceInfo()
        {
            throw new InvalidOperationException();
        }

        public SourceInfo(SourceInfo other)
        {
            other.CheckDisposed();

            var pOther = other.LoanedPointer();
            var sn = ZenohC.z_source_info_sn(pOther);
            var eid = ZenohC.z_source_info_id(pOther);
            var pOwnedSourceInfo = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedSourceInfo>());
            unsafe
            {
                var pEid = (IntPtr)(&eid);
                var r = ZenohC.z_source_info_new(pOwnedSourceInfo, pEid, sn);
            }

            Handle = pOwnedSourceInfo;
            Owned = true;
        }

        internal SourceInfo(IntPtr handle, bool owned)
        {
            Handle = handle;
            Owned = owned;
        }

        /// <summary>
        /// Creates source info. 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="sn"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public static Result Create(EntityGlobalId id, uint sn, out SourceInfo? info)
        {
            id.CheckDisposed();

            var pOwnedSourceInfo = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedSourceInfo>());
            var r = ZenohC.z_source_info_new(pOwnedSourceInfo, id.Handle, sn);

            if (r == Result.Ok)
            {
                info = new SourceInfo(pOwnedSourceInfo, true);
            }
            else
            {
                info = null;
                Marshal.FreeHGlobal(pOwnedSourceInfo);
            }

            return r;
        }

        public override void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~SourceInfo() => Dispose(false);

        private void Dispose(bool disposing)
        {
            if (Handle == IntPtr.Zero) return;

            if (Owned)
            {
                ZenohC.z_source_info_drop(Handle);
                Marshal.FreeHGlobal(Handle);
            }

            Handle = IntPtr.Zero;
        }

        public override void ToOwned()
        {
            if (Owned) return;

            var sn = ZenohC.z_source_info_sn(Handle);
            var eid = ZenohC.z_source_info_id(Handle);
            var pOwnedSourceInfo = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedSourceInfo>());
            unsafe
            {
                var pEid = (IntPtr)(&eid);
                var r = ZenohC.z_source_info_new(pOwnedSourceInfo, pEid, sn);
            }

            Handle = pOwnedSourceInfo;
            Owned = true;
        }

        internal override IntPtr LoanedPointer()
        {
            return Owned ? ZenohC.z_source_info_loan(Handle) : Handle;
        }

        /// <summary>
        /// Returns the source_sn of the source info. 
        /// </summary>
        /// <returns></returns>
        public uint GetSn()
        {
            CheckDisposed();

            var pLoanedSourceInfo = LoanedPointer();
            return ZenohC.z_source_info_sn(pLoanedSourceInfo);
        }

        /// <summary>
        /// Returns the entity_global_id of the source info.
        /// </summary>
        /// <returns></returns>
        public EntityGlobalId GetEntityGlobalId()
        {
            CheckDisposed();

            var pLoanedSourceInfo = LoanedPointer();
            var entityGlobalId = ZenohC.z_source_info_id(pLoanedSourceInfo);
            return new EntityGlobalId(entityGlobalId);
        }

        internal IntPtr AllocUnmanagedMemory()
        {
            var pLoanedSourceInfo = LoanedPointer();
            var eid = ZenohC.z_source_info_id(pLoanedSourceInfo);
            var sn = ZenohC.z_source_info_sn(pLoanedSourceInfo);
            var pOwnedSourceInfo = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedSourceInfo>());
            unsafe
            {
                var pEid = (IntPtr)(&eid);
                var r = ZenohC.z_source_info_new(pOwnedSourceInfo, pEid, sn);
            }

            return pOwnedSourceInfo;
        }

        internal static void FreeUnmanagedMemory(IntPtr handle)
        {
            ZenohC.z_source_info_drop(handle);
            Marshal.FreeHGlobal(handle);
        }
    }
#endif
}