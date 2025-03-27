using System;
using System.Runtime.InteropServices;

namespace Zenoh
{
    public enum EncodingId : ushort
    {
        /// Just some bytes.
        ZenohBytes = 0,

        /// A UTF-8 string.
        ZenohString = 1,

        /// Zenoh serialized data.
        ZenohSerialized = 2,

        /// An application-specific stream of bytes.
        ApplicationOctetStream = 3,

        /// A textual file.
        TextPlain = 4,

        /// JSON data intended to be consumed by an application.
        ApplicationJson = 5,

        /// JSON data intended to be human-readable.
        TextJson = 6,

        /// A Common Data Representation (CDR)-encoded data.
        ApplicationCdr = 7,

        /// A Concise Binary Object Representation (CBOR)-encoded data.
        ApplicationCbor = 8,

        /// YAML data intended to be consumed by an application.
        ApplicationYaml = 9,

        /// YAML data intended to be human-readable.
        TextYaml = 10,

        /// JSON5 encoded data that are human-readable.
        TextJson5 = 11,

        /// A Python object serialized using pickle. <see href="https://docs.python.org/3/library/pickle.html"/>
        ApplicationPythonSerializedObject = 12,

        /// An application-specific protobuf-encoded data.
        ApplicationProtobuf = 13,

        // A Java serialized object.
        ApplicationJavaSerializedObject = 14,

        /// An openmetrics data, common used by Prometheus. <see href="https://prometheus.io/"/>
        ApplicationOpenmetricsText = 15,

        /// A Portable Network Graphics (PNG) image.
        ImagePng = 16,

        /// A Joint Photographic Experts Group (JPEG) image.
        ImageJpeg = 17,

        /// A Graphics Interchange Format (GIF) image.
        ImageGif = 18,

        /// A BitMap (BMP) image.
        ImageBmp = 19,

        /// A Web Portable (WebP) image.
        ImageWebP = 20,

        /// An XML file intended to be consumed by an application.
        ApplicationXml = 21,

        /// An encoded a list of tuples, each consisting of a name and a value.
        ApplicationXWwwFormUrlencoded = 22,

        /// An HTML file.
        TextHtml = 23,

        /// An XML file that is human-readable.
        TextXml = 24,

        /// A CSS file.
        TextCss = 25,

        /// A JavaScript file.
        TextJavascript = 26,

        /// A MarkDown file.
        TextMarkdown = 27,

        /// A CSV file.
        TextCsv = 28,

        /// An application-specific SQL query.
        ApplicationSql = 29,

        /// Constrained Application Protocol (CoAP) data intended for CoAP-to-HTTP and HTTP-to-CoAP proxies.
        ApplicationCoapPayload = 30,

        /// Defines a JSON document structure for expressing a sequence of operations to apply to a JSON document.
        ApplicationJsonPatchJson = 31,

        /// A JSON text sequence consists of any number of JSON texts, all encoded in UTF-8.
        ApplicationJsonSeq = 32,

        /// A JSONPath defines a string syntax for selecting and extracting JSON values from within a given JSON value.
        ApplicationJsonpath = 33,

        /// A JSON Web Token (JWT).
        ApplicationJwt = 34,

        /// An application-specific MPEG-4 encoded data, either audio or video.
        ApplicationMp4 = 35,

        /// A SOAP 1.2 message serialized as XML 1.0.
        ApplicationSoapXml = 36,

        /// A YANG-encoded data commonly used by the Network Configuration Protocol (NETCONF).
        ApplicationYang = 37,

        /// A MPEG-4 Advanced Audio Coding (AAC) media.
        AudioAac = 38,

        /// A Free Lossless Audio Codec (FLAC) media.
        AudioFlac = 39,

        /// An audio codec defined in MPEG-1, MPEG-2, MPEG-4, or registered at the MP4 registration authority.
        AudioMp4 = 40,

        /// An Ogg-encapsulated audio stream.
        AudioOgg = 41,

        /// A Vorbis-encoded audio stream.
        AudioVorbis = 42,

        /// A h261-encoded video stream.
        VideoH261 = 43,

        /// A h263-encoded video stream.
        VideoH263 = 44,

        /// A h264-encoded video stream.
        VideoH264 = 45,

        /// A h265-encoded video stream.
        VideoH265 = 46,

        /// A h266-encoded video stream.
        VideoH266 = 47,

        /// A video codec defined in MPEG-1, MPEG-2, MPEG-4, or registered at the MP4 registration authority.
        VideoMp4 = 48,

        /// An Ogg-encapsulated video stream.
        VideoOgg = 49,

        /// An uncompressed, studio-quality video stream.
        VideoRaw = 50,

        /// A VP8-encoded video stream.
        VideoVp8 = 51,

        /// A VP9-encoded video stream.
        VideoVp9 = 52,
    }

    // z_owned_encoding_t*
    public sealed class Encoding : Loanable
    {
        public Encoding()
        {
            var pZOwnedEncoding = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedEncoding>());
            var pZLoanedEncoding = ZenohC.z_encoding_loan_default();
            ZenohC.z_encoding_clone(pZOwnedEncoding, pZLoanedEncoding);
            Handle = pZOwnedEncoding;
            Owned = true;
        }

        private Encoding(IntPtr handle, bool owned)
        {
            Handle = handle;
            Owned = owned;
        }

        public Encoding(Encoding other)
        {
            other.CheckDisposed();

            var pZOwnedEncoding = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedEncoding>());
            var pZLoanedEncoding = other.LoanedPointer();
            ZenohC.z_encoding_clone(pZOwnedEncoding, pZLoanedEncoding);
            Handle = pZOwnedEncoding;
            Owned = true;
        }


        public Encoding(EncodingId id)
        {
            var pZOwnedEncoding = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedEncoding>());

            var pEncoding = id switch
            {
                EncodingId.ZenohBytes => ZenohC.z_encoding_zenoh_bytes(),
                EncodingId.ZenohString => ZenohC.z_encoding_zenoh_string(),
                EncodingId.ZenohSerialized => ZenohC.z_encoding_zenoh_serialized(),
                EncodingId.ApplicationOctetStream => ZenohC.z_encoding_application_octet_stream(),
                EncodingId.TextPlain => ZenohC.z_encoding_text_plain(),
                EncodingId.ApplicationJson => ZenohC.z_encoding_application_json(),
                EncodingId.TextJson => ZenohC.z_encoding_text_json(),
                EncodingId.ApplicationCdr => ZenohC.z_encoding_application_cdr(),
                EncodingId.ApplicationCbor => ZenohC.z_encoding_application_cbor(),
                EncodingId.ApplicationYaml => ZenohC.z_encoding_application_yaml(),
                EncodingId.TextYaml => ZenohC.z_encoding_text_yaml(),
                EncodingId.TextJson5 => ZenohC.z_encoding_text_json5(),
                EncodingId.ApplicationPythonSerializedObject =>
                    ZenohC.z_encoding_application_python_serialized_object(),
                EncodingId.ApplicationProtobuf => ZenohC.z_encoding_application_protobuf(),
                EncodingId.ApplicationJavaSerializedObject => ZenohC.z_encoding_application_java_serialized_object(),
                EncodingId.ApplicationOpenmetricsText => ZenohC.z_encoding_application_openmetrics_text(),
                EncodingId.ImagePng => ZenohC.z_encoding_image_png(),
                EncodingId.ImageJpeg => ZenohC.z_encoding_image_jpeg(),
                EncodingId.ImageGif => ZenohC.z_encoding_image_gif(),
                EncodingId.ImageBmp => ZenohC.z_encoding_image_bmp(),
                EncodingId.ImageWebP => ZenohC.z_encoding_image_webp(),
                EncodingId.ApplicationXml => ZenohC.z_encoding_application_xml(),
                EncodingId.ApplicationXWwwFormUrlencoded => ZenohC.z_encoding_application_x_www_form_urlencoded(),
                EncodingId.TextHtml => ZenohC.z_encoding_text_html(),
                EncodingId.TextXml => ZenohC.z_encoding_text_xml(),
                EncodingId.TextCss => ZenohC.z_encoding_text_css(),
                EncodingId.TextJavascript => ZenohC.z_encoding_text_javascript(),
                EncodingId.TextMarkdown => ZenohC.z_encoding_text_markdown(),
                EncodingId.TextCsv => ZenohC.z_encoding_text_csv(),
                EncodingId.ApplicationSql => ZenohC.z_encoding_application_sql(),
                EncodingId.ApplicationCoapPayload => ZenohC.z_encoding_application_coap_payload(),
                EncodingId.ApplicationJsonPatchJson => ZenohC.z_encoding_application_json_patch_json(),
                EncodingId.ApplicationJsonSeq => ZenohC.z_encoding_application_json_seq(),
                EncodingId.ApplicationJsonpath => ZenohC.z_encoding_application_jsonpath(),
                EncodingId.ApplicationJwt => ZenohC.z_encoding_application_jwt(),
                EncodingId.ApplicationMp4 => ZenohC.z_encoding_application_mp4(),
                EncodingId.ApplicationSoapXml => ZenohC.z_encoding_application_soap_xml(),
                EncodingId.ApplicationYang => ZenohC.z_encoding_application_yang(),
                EncodingId.AudioAac => ZenohC.z_encoding_audio_aac(),
                EncodingId.AudioFlac => ZenohC.z_encoding_audio_flac(),
                EncodingId.AudioMp4 => ZenohC.z_encoding_audio_mp4(),
                EncodingId.AudioOgg => ZenohC.z_encoding_audio_ogg(),
                EncodingId.AudioVorbis => ZenohC.z_encoding_audio_vorbis(),
                EncodingId.VideoH261 => ZenohC.z_encoding_video_h261(),
                EncodingId.VideoH263 => ZenohC.z_encoding_video_h263(),
                EncodingId.VideoH264 => ZenohC.z_encoding_video_h264(),
                EncodingId.VideoH265 => ZenohC.z_encoding_video_h265(),
                EncodingId.VideoH266 => ZenohC.z_encoding_video_h266(),
                EncodingId.VideoMp4 => ZenohC.z_encoding_video_mp4(),
                EncodingId.VideoOgg => ZenohC.z_encoding_video_ogg(),
                EncodingId.VideoRaw => ZenohC.z_encoding_video_raw(),
                EncodingId.VideoVp8 => ZenohC.z_encoding_video_vp8(),
                EncodingId.VideoVp9 => ZenohC.z_encoding_video_vp9(),
                _ => ZenohC.z_encoding_loan_default()
            };

            ZenohC.z_encoding_clone(pZOwnedEncoding, pEncoding);
            Handle = pZOwnedEncoding;
            Owned = true;
        }

        public override void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~Encoding() => Dispose(false);

        private void Dispose(bool disposing)
        {
            if (Handle == IntPtr.Zero) return;

            if (Owned)
            {
                ZenohC.z_encoding_drop(Handle);
                Marshal.FreeHGlobal(Handle);
            }

            Handle = IntPtr.Zero;
        }

        // 'handle' z_loaned_encoding_t*
        internal static Encoding CreateLoaned(IntPtr handle)
        {
            return new Encoding(handle, false);
        }

        internal static Encoding CreateOwned()
        {
            return new Encoding();
        }

        public override void ToOwned()
        {
            if (Owned) return;

            var pZOwnedEncoding = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedEncoding>());
            var pZLoanedEncoding = LoanedPointer();
            ZenohC.z_encoding_clone(pZOwnedEncoding, pZLoanedEncoding);
            Handle = pZOwnedEncoding;
            Owned = true;
        }

        internal override IntPtr LoanedPointer()
        {
            return Owned ? ZenohC.z_encoding_loan(Handle) : Handle;
        }

        /// <summary>
        /// Checking encoding for equality.
        /// </summary>
        /// <param name="other"></param>
        /// <returns>true if `this` equals to `other`, false otherwise.</returns>
        public bool Equals(Encoding other)
        {
            CheckDisposed();
            other.CheckDisposed();

            var pThis = LoanedPointer();
            var pOther = other.LoanedPointer();
            return ZenohC.z_encoding_equals(pThis, pOther);
        }

        public ZString ToZString()
        {
            CheckDisposed();

            var pZLoanedEncoding = LoanedPointer();
            var zString = new ZString();
            ZenohC.z_encoding_to_string(pZLoanedEncoding, zString.Handle);
            return zString;
        }

        public override string ToString()
        {
            CheckDisposed();

            var zString = ToZString();
            return zString.ToString();
        }

        /// <summary>
        /// <para>
        /// Set a schema to this encoding.
        /// </para>
        /// <para>
        /// Zenoh does not define what a schema is and its semantichs is left to the implementer.
        /// </para>
        /// <para>
        /// E.g. a common schema for `text/plain` encoding is `utf-8`.
        /// </para>
        /// </summary>
        /// <param name="schema"></param>
        public Result SetSchema(byte[] schema)
        {
            CheckDisposed();

            if (!Owned) ToOwned();

            var pLoanedEncoding = ZenohC.z_encoding_loan_mut(Handle);

            Result r;
            unsafe
            {
                fixed (void* pStr = schema)
                {
                    r = ZenohC.z_encoding_set_schema_from_substr(pLoanedEncoding, (IntPtr)pStr, (UIntPtr)schema.Length);
                }
            }

            return r;
        }

        public byte[] GetSchema()
        {
            CheckDisposed();

            var pLoanedEncoding = LoanedPointer();
            var encodingData = ZenohC.zc_internal_encoding_get_data(pLoanedEncoding);
            var bytes = Array.Empty<byte>();
            try
            {
                Marshal.Copy(encodingData.schema_ptr, bytes, 0, (int)encodingData.schema_len);
            }
            catch
            {
                // ignored
            }

            return bytes;
        }

        public EncodingId GetEncodingId()
        {
            CheckDisposed();

            var pLoanedEncoding = ZenohC.z_encoding_loan(Handle);
            var encodingData = ZenohC.zc_internal_encoding_get_data(pLoanedEncoding);
            return (EncodingId)encodingData.id;
        }

        internal IntPtr AllocUnmanagedMemory()
        {
            var pZOwnedEncoding = Marshal.AllocHGlobal(Marshal.SizeOf<ZOwnedEncoding>());
            var pZLoanedEncoding = LoanedPointer();
            ZenohC.z_encoding_clone(pZOwnedEncoding, pZLoanedEncoding);
            return pZOwnedEncoding;
        }

        internal static void FreeUnmanagedMem(IntPtr handle)
        {
            ZenohC.z_encoding_drop(handle);
            Marshal.FreeHGlobal(handle);
        }
    }
}