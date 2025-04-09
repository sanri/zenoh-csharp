using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Zenoh;

namespace ZBytes
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var data = new byte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };

            var zBytes1 = Zenoh.ZBytes.FromBytes(data);
            Trace.Assert((int)zBytes1.Length() == data.Length);

            var zBytes1Data = zBytes1.ToByteArray();
            Trace.Assert(zBytes1Data.SequenceEqual(data));

            var zBytes2 = Zenoh.ZBytes.FromBytes(data);
            var zBytes2ReadStream = zBytes2.CreateReadStream();
            Trace.Assert(zBytes2ReadStream.Length == data.Length);

            var d0 = zBytes2ReadStream.ReadByte();
            Trace.Assert(d0 == 0);

            zBytes2ReadStream.Seek(8, SeekOrigin.Begin);
            var d1 = zBytes2ReadStream.ReadByte();
            Trace.Assert(d1 == 8);

            zBytes2ReadStream.Seek(-2, SeekOrigin.Current);
            var d2 = zBytes2ReadStream.ReadByte();
            Trace.Assert(d2 == 7);

            zBytes2ReadStream.Seek(-2, SeekOrigin.End);
            var d3 = zBytes2ReadStream.ReadByte();
            Trace.Assert(d3 == 14);
            
            zBytes2ReadStream.Seek(16, SeekOrigin.Begin);
            var d4 = zBytes2ReadStream.ReadByte();
            Trace.Assert(d4 == -1);

            var zBytes1ReadStream = zBytes1.CreateReadStream();
            var zBytes3WriteStream = new ZBytesWriteStream();
            zBytes1ReadStream.CopyTo(zBytes3WriteStream);
            var zBytes3 = zBytes3WriteStream.Finish();
            var vBytes3Data = zBytes3.ToByteArray();
            Trace.Assert(vBytes3Data.SequenceEqual(data));

            var bufferPtr = Marshal.AllocHGlobal(data.Length);
            zBytes1.CopyToMemory(bufferPtr, (ulong)data.Length);
            var zBytes4 = Zenoh.ZBytes.FromStaticBytes(bufferPtr, (ulong)data.Length);
            Marshal.FreeHGlobal(bufferPtr);
            var zBytes4Data = zBytes4.ToByteArray();
            Trace.Assert(zBytes4Data.SequenceEqual(data));
        }
    }
}