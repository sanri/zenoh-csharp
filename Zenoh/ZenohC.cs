#pragma warning disable CS8500

using System.Runtime.InteropServices;

namespace Zenoh;

using ZResult = System.SByte;

// zenoh_commons.h
// z_congestion_control_t
public enum CongestionControl
{
    Block = 0,
    Drop = 1
}


// z_encoding_prefix_t
public enum EncodingPrefix
{
    Empty = 0,
    AppOctetStream = 1,
    AppCustom = 2,
    TextPlain = 3,
    AppProperties = 4,
    AppJson = 5,
    AppSql = 6,
    AppInteger = 7,
    AppFloat = 8,
    AppXml = 9,
    AppXhtmlXml = 10,
    AppXWwwFormUrlencoded = 11,
    TextJson = 12,
    TextHtml = 13,
    TextXml = 14,
    TextCss = 15,
    TextCsv = 16,
    TextJavascript = 17,
    ImageJpeg = 18,
    ImagePng = 19,
    ImageGif = 20
}

// zenoh_commons.h
// z_sample_kind_t
public enum SampleKind
{
    Put = 0,
    Delete = 1
}

// zenoh_commons.h
// z_priority_t
public enum Priority
{
    RealTime = 1,
    InteractiveHigh = 2,
    InteractiveLow = 3,
    DataHigh = 4,
    Data = 5,
    DataLow = 6,
    Background = 7
}

// zenoh_commons.h
// z_consolidation_mode_t
public enum ConsolidationMode
{
    Auto = -1,
    None = 0,
    Monotonic = 1,
    Latest = 2
}

// z_query_consolidation_t
public enum QueryConsolidation
{
    Auto = -1,
    None = 0,
    Monotonic = 1,
    Latest = 2
}

// z_reliability_t
public enum Reliability
{
    BestEffort,
    Reliable
}

// zenoh_commons.h
// z_query_target_t 
public enum QueryTarget
{
    BestMatching = 0,
    All = 1,
    AllComplete = 2
}

// zenoh_commons.h
// z_what_t
public enum What
{
    Router = 1,
    Peer = 2,
    Client = 4,
    RouterPeer = 1 | 2,
    RouterClient = 1 | 4,
    PeerClient = 2 | 4,
    RouterPeerClient = 1 | 2 | 4,
}

// zenoh_commons.h
// z_whatami_t
public enum Whatami
{
    Router = 1,
    Peer = 2,
    Client = 4
}

// zenoh_commons.h
// zc_locality_t
public enum ZcLocality
{
    Any = 0,
    Local = 1,
    Remote = 2,
}

// zenoh_commons.h
// zc_log_severity_t
public enum ZcLogSeverity
{
    Trace = 0,
    Debug = 1,
    Info = 2,
    Warn = 3,
    Error = 4,
}

// zenoh_opaque.h
// z_owned_bytes_t
[StructLayout(LayoutKind.Sequential, Pack = 8)]
internal unsafe struct ZOwnedBytes
{
    private fixed byte data[32];
} 

// zenoh_opaque.h
// z_loaned_bytes_t
[StructLayout(LayoutKind.Sequential, Pack = 8)]
internal unsafe struct ZLoanedBytes
{
    private fixed byte data[32];
} 

// zenoh_commons.h
// z_moved_bytes_t
[StructLayout(LayoutKind.Sequential)]
internal struct ZMovedBytes
{
    private ZOwnedBytes ownedBytes;
} 

// zenoh_opaque.h
// z_owned_slice_t
[StructLayout(LayoutKind.Sequential, Pack = 8)]
internal unsafe struct ZOwnedSlice
{
    private fixed byte data[32];
}

// zenoh_opaque.h
// z_loaned_slice_t
[StructLayout(LayoutKind.Sequential, Pack = 8)]
internal unsafe struct ZLoanedSlice
{
    private fixed byte data[32];
}

// zenoh_opaque.h
// z_view_slice_t
[StructLayout(LayoutKind.Sequential, Pack = 8)]
internal unsafe struct ZViewSlice
{
    private fixed byte data[32];
}

// zenoh_commons.h
// z_moved_slice_t
[StructLayout(LayoutKind.Sequential)]
internal struct ZMovedSlice
{
    private ZOwnedSlice ownedSlice;
} 

// zenoh_opaque.h
// z_owned_string_t
[StructLayout(LayoutKind.Sequential, Pack = 8)]
internal unsafe struct ZOwnedString
{
    private fixed byte data[32];
}

// zenoh_commons.h
// z_bytes_slice_iterator_t
[StructLayout(LayoutKind.Sequential, Pack = 8)]
internal unsafe struct ZBytesSliceIterator
{
    private fixed byte data[24];
}

// zenoh_opaque.h
// z_loaned_string_t
[StructLayout(LayoutKind.Sequential, Pack = 8)]
internal unsafe struct ZLoanedString
{
    private fixed byte data[32];
}

// zenoh_opaque.h
// z_view_string_t
[StructLayout(LayoutKind.Sequential, Pack = 8)]
internal unsafe struct ZViewString
{
    private fixed byte data[32];
}

// zenoh_commons.h
// z_moved_string_t
[StructLayout(LayoutKind.Sequential)]
internal struct ZMovedString
{
    private ZOwnedString ownedString;
} 

// zenoh_opaque.h
// z_bytes_reader_t
[StructLayout(LayoutKind.Sequential, Pack = 8)]
internal unsafe struct ZBytesReader
{
    private fixed byte data[24];
}

// zenoh_opaque.h
// z_owned_bytes_writer_t
[StructLayout(LayoutKind.Sequential, Pack = 8)]
internal unsafe struct ZOwnedBytesWriter
{
    private fixed byte data[56];
}

// zenoh_opaque.h
// z_loaned_bytes_writer_t
[StructLayout(LayoutKind.Sequential, Pack = 8)]
internal unsafe struct ZLoanedBytesWriter
{
    private fixed byte data[56];
}

// zenoh_commons.h
// z_moved_bytes_writer_t
[StructLayout(LayoutKind.Sequential)]
internal struct ZMovedBytesWriter
{
    internal ZOwnedBytesWriter ownedBytesWriter;
} 

// zenoh_opaque.h
// z_loaned_session_t
[StructLayout(LayoutKind.Sequential, Pack = 8)]
internal unsafe struct ZLoanedSession
{
    private fixed byte data[8];
}

// zenoh_opaque.h
// z_owned_hello_t
[StructLayout(LayoutKind.Sequential, Pack = 8)]
internal unsafe struct ZOwnedHello
{
    private fixed byte data[48];
}

// zenoh_opaque.h
// z_loaned_hello_t
[StructLayout(LayoutKind.Sequential, Pack = 8)]
internal unsafe struct ZLoanedHello
{
    private fixed byte data[48];
}

// zenoh_commons.h
// z_moved_hello_t
[StructLayout(LayoutKind.Sequential)]
internal struct ZMovedHello
{
    internal ZOwnedHello ownedHello;
}


// zenoh_commons.h
// z_owned_closure_hello_t
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct ZOwnedClosureHello
{
     internal void* context;
     internal ZOwnedClosureHelloCall call;
     internal ZOwnedClosureHelloDrop? drop;
}

internal unsafe delegate void ZOwnedClosureHelloCall(ZLoanedHello* hello, void* context); 
internal unsafe delegate void ZOwnedClosureHelloDrop(void* context); 

// zenoh_opaque.h
// z_loaned_closure_hello_t
[StructLayout(LayoutKind.Sequential, Pack = 8)]
internal unsafe struct ZLoanedClosureHello
{
    private fixed byte data[3];
}

// zenoh_commons.h
// z_moved_closure_hello_t
[StructLayout(LayoutKind.Sequential)]
internal struct ZMovedClosureHello
{
    internal ZOwnedClosureHello _ownedClosureHello;
}

// zenoh_opaque.h
// z_owned_query_t
[StructLayout(LayoutKind.Sequential, Pack = 8)]
internal unsafe struct ZOwnedQuery
{
    private fixed byte data[136];
}

// zenoh_opaque.h
// z_loaned_query_t
[StructLayout(LayoutKind.Sequential, Pack = 8)]
internal unsafe struct ZLoanedQuery
{
    private fixed byte data[136];
}

// zenoh_commons.h
// z_owned_closure_query_t
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct ZOwnedClosureQuery
{
     internal void* context;
     internal ZOwnedClosureQueryCall call;
     internal ZOwnedClosureQueryDrop? drop;
}

internal unsafe delegate void ZOwnedClosureQueryCall(ZLoanedQuery* query, void* context); 
internal unsafe delegate void ZOwnedClosureQueryDrop(void* context); 

// zenoh_opaque.h
// z_loaned_closure_query_t
[StructLayout(LayoutKind.Sequential, Pack = 8)]
internal unsafe struct ZLoanedClosureQuery
{
    private fixed ulong data[3];
}

// zenoh_commons.h
// z_moved_closure_query_t
[StructLayout(LayoutKind.Sequential)]
internal struct ZMovedClosureQuery
{
    internal ZOwnedClosureQuery _ownedClosureQuery;
}

// zenoh_opaque.h
// z_owned_reply_t
[StructLayout(LayoutKind.Sequential, Pack = 8)]
internal unsafe struct ZOwnedReply
{
    private fixed byte data[184];
}

// zenoh_opaque.h
// z_loaned_reply_t
[StructLayout(LayoutKind.Sequential, Pack = 8)]
internal unsafe struct ZLoanedReply
{
    private fixed byte data[184];
}

// zenoh_commons.h
// z_owned_closure_reply_t
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct ZOwnedClosureReply
{
     internal void* context;
     internal ZOwnedClosureReplyCall call;
     internal ZOwnedClosureReplyDrop? drop;
}

internal unsafe delegate void ZOwnedClosureReplyCall(ZLoanedReply* reply, void* context); 
internal unsafe delegate void ZOwnedClosureReplyDrop(void* context); 

// zenoh_opaque.h
// z_loaned_closure_reply_t
[StructLayout(LayoutKind.Sequential, Pack = 8)]
internal unsafe struct ZLoanedClosureReply
{
    private fixed ulong data[3];
}

// zenoh_commons.h
// z_moved_closure_reply_t
[StructLayout(LayoutKind.Sequential)]
internal struct ZMovedClosureReply
{
    internal ZOwnedClosureReply _ownedClosureReply;
}

// zenoh_opaque.h
// z_owned_sample_t
[StructLayout(LayoutKind.Sequential, Pack = 8)]
internal unsafe struct ZOwnedSample
{
    private fixed byte data[184];
}

// zenoh_opaque.h
// z_loaned_sample_t
[StructLayout(LayoutKind.Sequential, Pack = 8)]
internal unsafe struct ZLoanedSample
{
    private fixed byte data[184];
}

// zenoh_commons.h
// z_owned_closure_sample_t
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct ZOwnedClosureSample
{
     internal void* context;
     internal ZOwnedClosureSampleCall call;
     internal ZOwnedClosureSampleDrop? drop;
}

internal unsafe delegate void ZOwnedClosureSampleCall(ZLoanedSample* reply, void* context); 
internal unsafe delegate void ZOwnedClosureSampleDrop(void* context); 

// zenoh_opaque.h
// z_loaned_closure_sample_t
[StructLayout(LayoutKind.Sequential, Pack = 8)]
internal unsafe struct ZLoanedClosureSample
{
    private fixed ulong data[3];
}

// zenoh_commons.h
// z_owned_moved_sample_t
[StructLayout(LayoutKind.Sequential)]
internal struct ZMovedClosureSample
{
    internal ZOwnedClosureSample _ownedClosureSample;
}

// zenoh_opaque.h
// z_id_t
[StructLayout(LayoutKind.Sequential, Pack = 1)]
internal unsafe struct ZId
{
    private fixed byte id[16];
}

// zenoh_commons.h
// z_owned_closure_zid_t
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct ZOwnedClosureZid
{
     internal void* context;
     internal ZOwnedClosureZidCall call;
     internal ZOwnedClosureZidDrop? drop;
}

internal unsafe delegate void ZOwnedClosureZidCall(ZId* zId, void* context); 
internal unsafe delegate void ZOwnedClosureZidDrop(void* context); 

// zenoh_opaque.h
// z_loaned_closure_zid_t
[StructLayout(LayoutKind.Sequential, Pack = 8)]
internal unsafe struct ZLoanedClosureZid
{
    private fixed ulong data[3];
}

// zenoh_commons.h
// z_moved_closure_zid_t
[StructLayout(LayoutKind.Sequential)]
internal struct ZMovedClosureZid
{
    internal ZOwnedClosureZid _ownedClosureZid;
}

// zenoh_opaque.h
// z_owned_condvar_t
[StructLayout(LayoutKind.Sequential, Pack = 8)]
internal unsafe struct ZOwnedCondvar
{
    private fixed byte data[24];
}

// zenoh_opaque.h
// z_loaned_condvar_t
[StructLayout(LayoutKind.Sequential, Pack = 8)]
internal unsafe struct ZLoanedCondvar
{
    private fixed byte data[16];
}

// zenoh_commons.h
// z_moved_condvar_t
[StructLayout(LayoutKind.Sequential)]
internal struct ZMovedCondvar
{
    internal ZOwnedCondvar _ownedCondvar;
}


// zenoh_opaque.h
// z_loaned_mutex_t
[StructLayout(LayoutKind.Sequential, Pack = 8)]
internal unsafe struct ZLoanedMutex
{
    private fixed byte data[32];
}

// zenoh_opaque.h
// z_timestamp_t
[StructLayout(LayoutKind.Sequential, Pack = 8)]
internal unsafe struct ZTimestamp
{
    private fixed byte data[24];
}

// zenoh_opaque.h
// z_owned_config_t
[StructLayout(LayoutKind.Sequential, Pack = 8)]
internal unsafe struct ZOwnedConfig
{
    private fixed byte data[1800];
}

// zenoh_opaque.h
// z_loaned_config_t
[StructLayout(LayoutKind.Sequential, Pack = 8)]
internal unsafe struct ZLoanedConfig
{
    private fixed byte data[1800];
}

// zenoh_commons.h
// z_moved_config_t
[StructLayout(LayoutKind.Sequential)]
internal struct ZMovedConfig
{
    internal ZOwnedConfig _ownedConfig;
}

// zenoh_opaque.h
// z_owned_keyexpr_t
[StructLayout(LayoutKind.Sequential, Pack = 8)]
internal unsafe struct ZOwnedKeyexpr
{
    private fixed byte data[32];
}

// zenoh_opaque.h
// z_loaned_keyexpr_t
[StructLayout(LayoutKind.Sequential, Pack = 8)]
internal unsafe struct ZLoanedKeyexpr
{
    private fixed byte data[32];
}

// zenoh_opaque.h
// z_view_keyexpr_t
[StructLayout(LayoutKind.Sequential, Pack = 8)]
internal unsafe struct ZViewKeyexpr
{
    private fixed byte data[32];
}

// zenoh_commons.h
// z_moved_keyexpr_t
[StructLayout(LayoutKind.Sequential)]
internal struct ZMovedKeyexpr
{
    internal ZOwnedKeyexpr ownedKeyexpr;
}

// zenoh_opaque.h
// z_owned_encoding_t
[StructLayout(LayoutKind.Sequential, Pack = 8)]
internal unsafe struct ZOwnedEncoding
{
    private fixed byte data[40];
}

// zenoh_opaque.h
// z_loaned_encoding_t
[StructLayout(LayoutKind.Sequential, Pack = 8)]
internal unsafe struct ZLoanedEncoding
{
    private fixed byte data[40];
}

// zenoh_commons.h
// z_moved_encoding_t
[StructLayout(LayoutKind.Sequential)]
internal struct ZMovedEncoding
{
    internal ZOwnedEncoding _ownedEncoding;
}

// zenoh_opaque.h
// z_owned_publisher_t
[StructLayout(LayoutKind.Sequential, Pack = 8)]
internal unsafe struct ZOwnedPublisher
{
    private fixed byte data[96];
}

// zenoh_opaque.h
// z_loaned_publisher_t
[StructLayout(LayoutKind.Sequential, Pack = 8)]
internal unsafe struct ZLoanedPublisher
{
    private fixed byte data[96];
}

// zenoh_opaque.h
// z_owned_queryable_t
[StructLayout(LayoutKind.Sequential, Pack = 8)]
internal unsafe struct ZOwnedQueryable
{
    private fixed byte data[16];
}

// zenoh_opaque.h
// z_loaned_queryable_t
[StructLayout(LayoutKind.Sequential, Pack = 8)]
internal unsafe struct ZLoanedQueryable
{
    private fixed byte data[16];
}

// zenoh_opaque.h
// z_owned_subscriber_t
[StructLayout(LayoutKind.Sequential, Pack = 8)]
internal unsafe struct ZOwnedSubscriber
{
    private fixed byte data[48];
}

// zenoh_opaque.h
// z_loaned_subscriber_t
[StructLayout(LayoutKind.Sequential, Pack = 8)]
internal unsafe struct ZLoanedSubscriber
{
    private fixed byte data[48];
}

// zenoh_opaque.h
// z_owned_fifo_handler_query_t
[StructLayout(LayoutKind.Sequential, Pack = 8)]
internal unsafe struct ZOwnedFifoHandlerQuery
{
    private fixed byte data[8];
}

// zenoh_opaque.h
// z_loaned_fifo_handler_query_t
[StructLayout(LayoutKind.Sequential, Pack = 8)]
internal unsafe struct ZLoanedFifoHandlerQuery
{
    private fixed byte data[8];
}

// zenoh_commons.h
// z_moved_fifo_handler_query_t
[StructLayout(LayoutKind.Sequential)]
internal struct ZMovedFifoHandlerQuery
{
    internal ZOwnedFifoHandlerQuery _ownedFifoHandlerQuery;
}

// zenoh_opaque.h
// z_owned_fifo_handler_reply_t
[StructLayout(LayoutKind.Sequential, Pack = 8)]
internal unsafe struct ZOwnedFifoHandlerReply
{
    private fixed byte data[8];
}

// zenoh_opaque.h
// z_loaned_fifo_handler_reply_t
[StructLayout(LayoutKind.Sequential, Pack = 8)]
internal unsafe struct ZLoanedFifoHandlerReply
{
    private fixed byte data[8];
}

// zenoh_commons.h
// z_moved_fifo_handler_reply_t
[StructLayout(LayoutKind.Sequential)]
internal struct ZMovedFifoHandlerReply
{
    internal ZOwnedFifoHandlerReply _ownedFifoHandlerReply;
}

// zenoh_opaque.h
// z_owned_fifo_handler_sample_t
[StructLayout(LayoutKind.Sequential, Pack = 8)]
internal unsafe struct ZOwnedFifoHandlerSample
{
    private fixed byte data[8];
}

// zenoh_opaque.h
// z_loaned_fifo_handler_sample_t
[StructLayout(LayoutKind.Sequential, Pack = 8)]
internal unsafe struct ZLoanedFifoHandlerSample
{
    private fixed byte data[8];
}

// zenoh_commons.h
// z_moved_fifo_handler_sample_t
[StructLayout(LayoutKind.Sequential)]
internal struct ZMovedFifoHandlerSample
{
    internal ZOwnedFifoHandlerSample _ownedFifoHandlerSample;
}

// zenoh_opaque.h
// z_owned_reply_err_t
[StructLayout(LayoutKind.Sequential, Pack = 8)]
internal unsafe struct ZOwnedReplyErr
{
    private fixed byte data[72];
}

// zenoh_opaque.h
// z_loaned_reply_err_t
[StructLayout(LayoutKind.Sequential, Pack = 8)]
internal unsafe struct ZLoanedReplyErr
{
    private fixed byte data[72];
}



// zenoh_opaque.h
// z_owned_string_array_t
[StructLayout(LayoutKind.Sequential, Pack = 8)]
internal unsafe struct ZOwnedStringArray
{
    private fixed byte data[24];
}

// zenoh_opaque.h
// z_loaned_string_array_t
[StructLayout(LayoutKind.Sequential, Pack = 8)]
internal unsafe struct ZLoanedStringArray
{
    private fixed byte data[24];
}

// zenoh_opaque.h
// z_owned_liveliness_token_t
[StructLayout(LayoutKind.Sequential, Pack = 8)]
internal unsafe struct ZOwnedLivelinessToken
{
    private fixed byte data[16];
}

// zenoh_opaque.h
// z_loaned_liveliness_token_t
[StructLayout(LayoutKind.Sequential, Pack = 8)]
internal unsafe struct ZLoanedLivelinessToken
{
    private fixed byte data[16];
}

// zenoh_commons.h
// z_liveliness_subscriber_options_t
[StructLayout(LayoutKind.Sequential)]
internal struct ZLivelinessSubscriberOptions
{
    internal byte history;
}

// zenoh_opaque.h
// z_owned_mutex_t
[StructLayout(LayoutKind.Sequential, Pack = 8)]
internal unsafe struct ZOwnedMutex
{
    private fixed byte data[32];
}

// zenoh_opaque.h
// z_owned_ring_handler_query_t
[StructLayout(LayoutKind.Sequential, Pack = 8)]
internal unsafe struct ZOwnedRingHandlerQuery
{
    private fixed byte data[8];
}

// zenoh_opaque.h
// z_loaned_ring_handler_query_t
[StructLayout(LayoutKind.Sequential, Pack = 8)]
internal unsafe struct ZLoanedRingHandlerQuery
{
    private fixed byte data[8];
}

// zenoh_opaque.h
// z_owned_ring_handler_reply_t
[StructLayout(LayoutKind.Sequential, Pack = 8)]
internal unsafe struct ZOwnedRingHandlerReply
{
    private fixed byte data[8];
}

// zenoh_opaque.h
// z_loaned_ring_handler_reply_t
[StructLayout(LayoutKind.Sequential, Pack = 8)]
internal unsafe struct ZLoanedRingHandlerReply
{
    private fixed byte data[8];
}

// zenoh_opaque.h
// z_owned_ring_handler_sample_t
[StructLayout(LayoutKind.Sequential, Pack = 8)]
internal unsafe struct ZOwnedRingHandlerSample
{
    private fixed byte data[8];
}

// zenoh_opaque.h
// z_loaned_ring_handler_sample_t
[StructLayout(LayoutKind.Sequential, Pack = 8)]
internal unsafe struct ZLoanedRingHandlerSample
{
    private fixed byte data[8];
}

// zenoh_opaque.h
// z_owned_session_t
[StructLayout(LayoutKind.Sequential, Pack = 8)]
internal unsafe struct ZOwnedSession
{
    private fixed byte data[8];
}

// zenoh_opaque.h
// z_owned_task_t
[StructLayout(LayoutKind.Sequential, Pack = 8)]
internal unsafe struct ZOwnedTask
{
    private fixed byte data[24];
}


// z_bytes_t
// --------------------------------
//  typedef struct z_bytes_t {
//      size_t len;
//      const uint8_t *start;
//  } z_bytes_t;
// --------------------------------
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct ZBytes
{
    internal nuint len;
    internal byte* start;
}


// z_owned_str_t 
// --------------------------------
//  typedef struct z_owned_str_t {
//      char *_cstr;
//  } z_owned_str_t;
// --------------------------------
[StructLayout(LayoutKind.Sequential)]
internal struct ZOwnedStr
{
    internal nint cstr;
}

// z_owned_str_array_t
// --------------------------------
//  typedef struct z_owned_str_array_t {
//      char **val;
//      size_t len;
//  } z_owned_str_array_t;
// --------------------------------
[StructLayout(LayoutKind.Sequential)]
internal struct ZOwnedStrArray
{
    internal nint val;
    internal nuint len;
}

// z_str_array_t 
// --------------------------------
//  typedef struct z_str_array_t {
//      size_t len;
//      const char *const *val;
//  } z_str_array_t;
// --------------------------------
[StructLayout(LayoutKind.Sequential)]
internal struct ZStrArray
{
    internal nuint len;
    internal nint val;
}


// z_encoding_t
// --------------------------------
//  typedef struct z_encoding_t {
//      enum z_encoding_prefix_t prefix;
//      struct z_bytes_t suffix;
//  } z_encoding_t;
// --------------------------------
[StructLayout(LayoutKind.Sequential)]
internal struct ZEncoding
{
    internal EncodingPrefix prefix;
    internal ZBytes suffix;
}

// public string PrefixToString()
// {
//     return prefix.ToString();
// }
//
// public static ZEncoding New(EncodingPrefix prefix)
// {
//     return FnZEncoding(prefix, IntPtr.Zero);
// }
//
// [DllImport(ZenohC.DllName, EntryPoint = "z_encoding", CallingConvention = CallingConvention.Cdecl)]
// internal static extern ZEncoding FnZEncoding(EncodingPrefix prefix, IntPtr suffix);


// z_keyexpr_t  
// --------------------------------
//  typedef struct z_keyexpr_t {
//      uint64_t _0[4];
//  } z_keyexpr_t;
// --------------------------------
[StructLayout(LayoutKind.Sequential, Pack = 8)]
internal unsafe struct ZKeyexpr
{
    // [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    private fixed ulong _[4];
}


// z_sample_t
// --------------------------------
//  typedef struct z_sample_t {
//      struct z_keyexpr_t keyexpr;
//      struct z_bytes_t payload;
//      struct z_encoding_t encoding;
//      const void *_zc_buf;
//      enum z_sample_kind_t kind;
//      struct z_timestamp_t timestamp;
//  } z_sample_t;
// --------------------------------
[StructLayout(LayoutKind.Sequential)]
internal struct ZSample
{
    internal ZKeyexpr keyexpr;
    internal ZBytes payload;
    internal ZEncoding encoding;
    private nint _zc_buf;
    internal SampleKind kind;
    internal ZTimestamp timestamp;
}

// z_config_t
// --------------------------------
//  typedef struct z_config_t {
//      const struct z_owned_config_t *_0;
//  } z_config_t;
// --------------------------------
[StructLayout(LayoutKind.Sequential)]
internal struct ZConfig
{
    private nint _;
}



// z_publisher_t 
// --------------------------------
//  typedef struct z_publisher_t {
//      const struct z_owned_publisher_t *_0;
//  } z_publisher_t;
// --------------------------------
[StructLayout(LayoutKind.Sequential)]
internal struct ZPublisher
{
    private nint _;
}

// zenoh_commons.h
// z_publisher_options_t 
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct ZPublisherOptions
{
    internal ZMovedEncoding *encoding;
    internal CongestionControl congestion_control;
    internal Priority priority;
    internal byte is_express;
}

// z_publisher_delete_options_t 
// --------------------------------
//  typedef struct z_publisher_delete_options_t {
//      uint8_t __dummy;
//  } z_publisher_delete_options_t;
// --------------------------------
[StructLayout(LayoutKind.Sequential)]
internal struct ZPublisherDeleteOptions
{
    internal byte dummy;
}

// z_publisher_put_options_t 
// --------------------------------
//  typedef struct z_publisher_put_options_t {
//      struct z_encoding_t encoding;
//  } z_publisher_put_options_t;
// --------------------------------
[StructLayout(LayoutKind.Sequential)]
internal struct ZPublisherPutOptions
{
    internal ZEncoding encoding;
}

// z_pull_subscriber_options_t 
// --------------------------------
//  typedef struct z_pull_subscriber_options_t {
//      enum z_reliability_t reliability;
//  } z_pull_subscriber_options_t;
// --------------------------------
[StructLayout(LayoutKind.Sequential)]
internal struct ZPullSubscriberOptions
{
    internal Reliability reliability;
}

// z_owned_pull_subscriber_t 
// --------------------------------
//  typedef struct ALIGN(8) z_owned_pull_subscriber_t {
//      uint64_t _0[1];
//  } z_owned_pull_subscriber_t;
// --------------------------------
[StructLayout(LayoutKind.Sequential, Pack = 8)]
internal struct ZOwnedPullSubscriber
{
    private ulong _;
}

// z_pull_subscriber_t 
// --------------------------------
//  typedef struct z_pull_subscriber_t {
//      const struct z_owned_pull_subscriber_t *_0;
//  } z_pull_subscriber_t;
// --------------------------------
[StructLayout(LayoutKind.Sequential)]
internal struct ZPullSubscriber
{
    internal nint _;
}

// zenoh_commons.h
// z_queryable_options_t 
[StructLayout(LayoutKind.Sequential)]
internal struct ZQueryableOptions
{
    internal byte complete;
}

// zenoh_commons.h
// z_subscriber_options_t 
[StructLayout(LayoutKind.Sequential)]
internal struct ZSubscriberOptions
{
    internal byte option;
}

#if PLATFORM_ARM64
// --------------------------------
//  typedef struct ALIGN(16) z_owned_reply_t {
//      uint64_t _0[24];
//  } z_owned_reply_t;
// --------------------------------
[StructLayout(LayoutKind.Sequential, Pack = 16)]
internal unsafe struct ZOwnedReply{
    private fixed ulong _[24];
}
#elif PLATFORM_x64
// --------------------------------
//  typedef struct ALIGN(8) z_owned_reply_t {
//      uint64_t _0[22];
//  } z_owned_reply_t;
// --------------------------------
// [StructLayout(LayoutKind.Sequential, Pack = 8)]
// internal unsafe struct ZOwnedReply
// {
//     private fixed ulong _[22];
// }
#else
#error  PLATFORM_ARM64 or PLATFORM_x64
#endif

// zenoh_commons.h
// z_delete_options_t 
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct ZDeleteOptions
{
    internal CongestionControl congestion_control;
    internal Priority priority;
    internal byte is_express;
    internal ZTimestamp *timestamp;
}

// zenoh_commons.h
// z_query_consolidation_t 
[StructLayout(LayoutKind.Sequential)]
internal struct ZQueryConsolidation
{
    internal ConsolidationMode mode;
}

// zenoh_commons.h
// z_get_options_t 
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct ZGetOptions
{
    internal QueryTarget target;
    internal ZQueryConsolidation consolidation;
    internal ZMovedBytes *payload;
    internal ZMovedEncoding *encoding;
    internal CongestionControl congestion_control;
    internal byte is_express;
    internal ZMovedBytes* attachment;
    internal ulong timeout_ms;
}

// z_value_t 
// --------------------------------
//  typedef struct z_value_t {
//      struct z_bytes_t payload;
//      struct z_encoding_t encoding;
//  } z_value_t;
// --------------------------------
[StructLayout(LayoutKind.Sequential)]
internal struct ZValue
{
    internal ZBytes payload;
    internal ZEncoding encoding;
}

// zc_owned_payload_t 
// --------------------------------
//  typedef struct zc_owned_payload_t {
//      struct z_bytes_t payload;
//      uintptr_t _owner[4];
//  } zc_owned_payload_t;
// --------------------------------
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct ZcOwnedPayload
{
    private fixed ulong _[4];
}

// zc_owned_shmbuf_t 
// --------------------------------
//  typedef struct zc_owned_shmbuf_t {
//      uintptr_t _0[9];
//  } zc_owned_shmbuf_t;
// --------------------------------
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct ZcOwnedShmbuf
{
    private fixed ulong _[9];
}

// zc_owned_shm_manager_t 
// --------------------------------
//  typedef struct zc_owned_shm_manager_t {
//      uintptr_t _0;
//  } zc_owned_shm_manager_t;
// --------------------------------
[StructLayout(LayoutKind.Sequential)]
internal struct ZcOwnedShmManager
{
    private nint _;
}

// z_put_options_t 
// --------------------------------
//  typedef struct z_put_options_t {
//      struct z_encoding_t encoding;
//      enum z_congestion_control_t congestion_control;
//      enum z_priority_t priority;
//  } z_put_options_t;
// --------------------------------
[StructLayout(LayoutKind.Sequential)]
internal struct ZPutOptions
{
    internal ZEncoding encoding;
    internal CongestionControl congestionControl;
    internal Priority priority;
}

// z_query_reply_options_t 
// --------------------------------
//  typedef struct z_query_reply_options_t {
//      struct z_encoding_t encoding;
//  } z_query_reply_options_t;
// --------------------------------
[StructLayout(LayoutKind.Sequential)]
internal struct ZQueryReplyOptions
{
    internal ZEncoding encoding;
}

// z_query_t 
// --------------------------------
// typedef struct z_query_t {
//   void *_0;
// } z_query_t;
// --------------------------------
[StructLayout(LayoutKind.Sequential)]
internal struct ZQuery
{
    private nint _;
}


// z_session_t 
// --------------------------------
// typedef struct z_session_t {
//     uintptr_t _0;
// } z_session_t;
// --------------------------------
[StructLayout(LayoutKind.Sequential)]
internal struct ZSession
{
    private nint _;
}


// --------------------------------
// typedef struct z_owned_closure_zid_t {
//     void *context;
//     void (*call)(const struct z_id_t*, void*);
//     void (*drop)(void*);
// } z_owned_closure_zid_t;
// --------------------------------
internal unsafe delegate void ZOwnedClosureZIdCall(ZId* zId, void* context);

internal unsafe delegate void ZOwnedClosureZIdDrop(void* context);

[StructLayout(LayoutKind.Sequential)]
internal unsafe struct ZOwnedClosureZId
{
    internal void* context;
    internal ZOwnedClosureZIdCall call;
    internal ZOwnedClosureZIdDrop? drop;
}


// z_owned_reply_channel_closure_t 
// --------------------------------
// typedef struct z_owned_reply_channel_closure_t {
//   void *context;
//   bool (*call)(struct z_owned_reply_t*, void*);
//   void (*drop)(void*);
// } z_owned_reply_channel_closure_t;
// --------------------------------
internal unsafe delegate void ZOwnedReplayChannelClosureCall(ZOwnedReply* zOwnedReply, void* context);

internal unsafe delegate void ZOwnedReplyChannelClosureDrop(void* context);

[StructLayout(LayoutKind.Sequential)]
internal unsafe struct ZOwnedReplyChannelClosure
{
    internal void* context;
    internal ZOwnedReplayChannelClosureCall call;
    internal ZOwnedReplyChannelClosureDrop drop;
}

// z_owned_reply_channel_t 
// --------------------------------
// typedef struct z_owned_reply_channel_t {
//     struct z_owned_closure_reply_t send;
//     struct z_owned_reply_channel_closure_t recv;
// } z_owned_reply_channel_t;
// --------------------------------
[StructLayout(LayoutKind.Sequential)]
internal struct ZOwnedReplyChannel
{
    internal ZOwnedClosureReply send;
    internal ZOwnedReplyChannelClosure recv;
}

[StructLayout(LayoutKind.Sequential)]
public struct ConsolidationStrategy // z_consolidation_strategy_t
{
    public ConsolidationMode firstRouters;
    public ConsolidationMode lastRouters;
    public ConsolidationMode reception;
}

internal static unsafe class ZenohC
{
    internal const string DllName = "zenohc";
    internal static uint zRouter = 1;
    internal static uint zPeer = 2;
    internal static uint zClient = 4;
    internal static string zConfigModeKey = "mode";
    internal static string zConfigConnectKey = "connect/endpoints";
    internal static string zConfigListenKey = "listen/endpoints";
    internal static string zConfigUserKey = "transport/auth/usrpwd/user";
    internal static string zConfigPasswordKey = "transport/auth/usrpwd/password";
    internal static string zConfigMulticastScoutingKey = "scouting/multicast/enabled";
    internal static string zConfigMulticastInterfaceKey = "scouting/multicast/interface";
    internal static string zConfigMulticastIpv4AddressKey = "scouting/multicast/address";
    internal static string zConfigScoutingTimeoutKey = "scouting/timeout";
    internal static string zConfigScoutingDelayKey = "scouting/delay";
    internal static string zConfigAddTimestampKey = "add_timestamp";

    internal static string ZOwnedStrToString(ZOwnedStr* zs)
    {
        if (z_str_check(zs) != 1) return "";

        return Marshal.PtrToStringUTF8(zs->cstr) ?? "";
    }

    internal static string ZKeyexprToString(ZKeyexpr keyexpr)
    {
        var str = z_keyexpr_to_string(keyexpr);
        var o = ZOwnedStrToString(&str);
        z_str_drop(&str);
        return o;
    }

    [DllImport(DllName, EntryPoint = "z_bytes_check", CallingConvention = CallingConvention.Cdecl)]
    internal static extern sbyte z_bytes_check(ZBytes* b);

    [DllImport(DllName, EntryPoint = "z_str_array_check", CallingConvention = CallingConvention.Cdecl)]
    internal static extern sbyte z_str_array_check(ZOwnedStrArray* strs);

    [DllImport(DllName, EntryPoint = "z_str_array_drop", CallingConvention = CallingConvention.Cdecl)]
    internal static extern void z_str_array_drop(ZOwnedStrArray* strs);

    [DllImport(DllName, EntryPoint = "z_str_array_loan", CallingConvention = CallingConvention.Cdecl)]
    internal static extern ZStrArray z_str_array_loan(ZOwnedStrArray* strs);

    [DllImport(DllName, EntryPoint = "z_str_check", CallingConvention = CallingConvention.Cdecl)]
    internal static extern sbyte z_str_check(ZOwnedStr* s);

    [DllImport(DllName, EntryPoint = "z_str_drop", CallingConvention = CallingConvention.Cdecl)]
    internal static extern void z_str_drop(ZOwnedStr* s);

    [DllImport(DllName, EntryPoint = "z_str_loan", CallingConvention = CallingConvention.Cdecl)]
    internal static extern byte* z_str_loan(ZOwnedStr* s);

    [DllImport(DllName, EntryPoint = "z_str_null", CallingConvention = CallingConvention.Cdecl)]
    internal static extern ZOwnedStr z_str_null();

    [DllImport(DllName, EntryPoint = "z_config_check", CallingConvention = CallingConvention.Cdecl)]
    internal static extern sbyte z_config_check(ZOwnedConfig* config);

    [DllImport(DllName, EntryPoint = "z_config_drop", CallingConvention = CallingConvention.Cdecl)]
    internal static extern void z_config_drop(ZOwnedConfig* config);

    [DllImport(DllName, EntryPoint = "z_config_loan", CallingConvention = CallingConvention.Cdecl)]
    internal static extern ZConfig z_config_loan(ZOwnedConfig* config);

    [DllImport(DllName, EntryPoint = "z_config_default", CallingConvention = CallingConvention.Cdecl)]
    internal static extern ZOwnedConfig z_config_default();

    [DllImport(DllName, EntryPoint = "z_config_new", CallingConvention = CallingConvention.Cdecl)]
    internal static extern ZOwnedConfig z_config_new();

    [DllImport(DllName, EntryPoint = "z_config_null", CallingConvention = CallingConvention.Cdecl)]
    internal static extern ZOwnedConfig z_config_null();

    [DllImport(DllName, EntryPoint = "z_config_client", CallingConvention = CallingConvention.Cdecl)]
    internal static extern ZOwnedConfig z_config_client(string[] peers, nuint nPeers);

    [DllImport(DllName, EntryPoint = "z_config_peer", CallingConvention = CallingConvention.Cdecl)]
    internal static extern ZOwnedConfig z_config_peer();

    [DllImport(DllName, EntryPoint = "zc_config_from_file", CallingConvention = CallingConvention.Cdecl)]
    internal static extern ZOwnedConfig zc_config_from_file([MarshalAs(UnmanagedType.LPStr)] string path);

    [DllImport(DllName, EntryPoint = "zc_config_from_str", CallingConvention = CallingConvention.Cdecl)]
    internal static extern ZOwnedConfig zc_config_from_str([MarshalAs(UnmanagedType.LPStr)] string s);

    [DllImport(DllName, EntryPoint = "zc_config_get", CallingConvention = CallingConvention.Cdecl)]
    internal static extern ZOwnedStr zc_config_get(ZConfig config, [MarshalAs(UnmanagedType.LPStr)] string key);

    [DllImport(DllName, EntryPoint = "zc_config_insert_json", CallingConvention = CallingConvention.Cdecl)]
    internal static extern sbyte zc_config_insert_json(
        ZConfig config, [MarshalAs(UnmanagedType.LPStr)] string key, [MarshalAs(UnmanagedType.LPStr)] string value);

    [DllImport(DllName, EntryPoint = "zc_config_to_string", CallingConvention = CallingConvention.Cdecl)]
    internal static extern ZOwnedStr zc_config_to_string(ZConfig config);

    [DllImport(DllName, EntryPoint = "z_encoding", CallingConvention = CallingConvention.Cdecl)]
    internal static extern ZEncoding z_encoding(EncodingPrefix prefix, byte* suffix);

    [DllImport(DllName, EntryPoint = "z_encoding_default", CallingConvention = CallingConvention.Cdecl)]
    internal static extern ZEncoding z_encoding_default();

    [DllImport(DllName, EntryPoint = "z_encoding_check", CallingConvention = CallingConvention.Cdecl)]
    internal static extern sbyte z_encoding_check(ZOwnedEncoding* encoding);

    [DllImport(DllName, EntryPoint = "z_encoding_drop", CallingConvention = CallingConvention.Cdecl)]
    internal static extern void z_encoding_drop(ZOwnedEncoding* encoding);

    [DllImport(DllName, EntryPoint = "z_encoding_loan", CallingConvention = CallingConvention.Cdecl)]
    internal static extern ZEncoding z_encoding_loan(ZOwnedEncoding* encoding);

    [DllImport(DllName, EntryPoint = "z_encoding_null", CallingConvention = CallingConvention.Cdecl)]
    internal static extern ZOwnedEncoding z_encoding_null();

    [DllImport(DllName, EntryPoint = "z_keyexpr_check", CallingConvention = CallingConvention.Cdecl)]
    internal static extern sbyte z_keyexpr_check(ZOwnedKeyexpr* keyexpr);

    [DllImport(DllName, EntryPoint = "z_keyexpr", CallingConvention = CallingConvention.Cdecl)]
    internal static extern ZKeyexpr z_keyexpr(byte* name);
    // internal static extern ZKeyexpr z_keyexpr([MarshalAs(UnmanagedType.LPStr)] string name);

    [DllImport(DllName, EntryPoint = "z_keyexpr_as_bytes", CallingConvention = CallingConvention.Cdecl)]
    internal static extern ZBytes z_keyexpr_as_bytes(ZKeyexpr keyexpr);

    [DllImport(DllName, EntryPoint = "z_keyexpr_canonize", CallingConvention = CallingConvention.Cdecl)]
    internal static extern sbyte z_keyexpr_canonize(byte* start, nuint* len);

    [DllImport(DllName, EntryPoint = "z_keyexpr_canonize_null_terminated", CallingConvention = CallingConvention.Cdecl)]
    internal static extern sbyte z_keyexpr_canonize_null_terminated(byte* start);

    [DllImport(DllName, EntryPoint = "z_keyexpr_drop", CallingConvention = CallingConvention.Cdecl)]
    internal static extern void z_keyexpr_drop(ZOwnedKeyexpr* keyexpr);

    [DllImport(DllName, EntryPoint = "z_keyexpr_concat", CallingConvention = CallingConvention.Cdecl)]
    internal static extern ZOwnedKeyexpr z_keyexpr_concat(ZKeyexpr left, byte* rightStart, nuint rightLen);

    [DllImport(DllName, EntryPoint = "z_keyexpr_equals", CallingConvention = CallingConvention.Cdecl)]
    internal static extern sbyte z_keyexpr_equals(ZKeyexpr left, ZKeyexpr right);

    [DllImport(DllName, EntryPoint = "z_keyexpr_includes", CallingConvention = CallingConvention.Cdecl)]
    internal static extern sbyte z_keyexpr_includes(ZKeyexpr left, ZKeyexpr right);

    [DllImport(DllName, EntryPoint = "z_keyexpr_intersects", CallingConvention = CallingConvention.Cdecl)]
    internal static extern sbyte z_keyexpr_intersects(ZKeyexpr left, ZKeyexpr right);

    [DllImport(DllName, EntryPoint = "z_keyexpr_is_canon", CallingConvention = CallingConvention.Cdecl)]
    internal static extern sbyte z_keyexpr_is_canon(byte* start, nuint len);

    [DllImport(DllName, EntryPoint = "z_keyexpr_is_initialized", CallingConvention = CallingConvention.Cdecl)]
    internal static extern sbyte z_keyexpr_is_initialized(ZKeyexpr* keyexpr);

    [DllImport(DllName, EntryPoint = "z_keyexpr_join", CallingConvention = CallingConvention.Cdecl)]
    internal static extern ZOwnedKeyexpr z_keyexpr_join(ZKeyexpr left, ZKeyexpr right);

    [DllImport(DllName, EntryPoint = "z_keyexpr_loan", CallingConvention = CallingConvention.Cdecl)]
    internal static extern ZKeyexpr z_keyexpr_loan(ZOwnedKeyexpr* keyexpr);

    [DllImport(DllName, EntryPoint = "z_keyexpr_new", CallingConvention = CallingConvention.Cdecl)]
    internal static extern ZOwnedKeyexpr z_keyexpr_new(byte* name);

    [DllImport(DllName, EntryPoint = "z_keyexpr_unchecked", CallingConvention = CallingConvention.Cdecl)]
    internal static extern ZKeyexpr z_keyexpr_unchecked(byte* name);

    [DllImport(DllName, EntryPoint = "z_keyexpr_null", CallingConvention = CallingConvention.Cdecl)]
    internal static extern ZOwnedKeyexpr z_keyexpr_null();

    [DllImport(DllName, EntryPoint = "z_keyexpr_to_string", CallingConvention = CallingConvention.Cdecl)]
    internal static extern ZOwnedStr z_keyexpr_to_string(ZKeyexpr keyexpr);

    [DllImport(DllName, EntryPoint = "z_declare_keyexpr", CallingConvention = CallingConvention.Cdecl)]
    internal static extern ZOwnedKeyexpr z_declare_keyexpr(ZSession session, ZKeyexpr keyexpr);

    [DllImport(DllName, EntryPoint = "z_undeclare_keyexpr", CallingConvention = CallingConvention.Cdecl)]
    internal static extern sbyte z_undeclare_keyexpr(ZSession session, ZOwnedKeyexpr* keyexpr);

    [DllImport(DllName, EntryPoint = "z_timestamp_check", CallingConvention = CallingConvention.Cdecl)]
    internal static extern sbyte z_timestamp_check(ZTimestamp ts);

    [DllImport(DllName, EntryPoint = "z_subscriber_check", CallingConvention = CallingConvention.Cdecl)]
    internal static extern sbyte z_subscriber_check(ZOwnedSubscriber* sub);

    [DllImport(DllName, EntryPoint = "z_subscriber_null", CallingConvention = CallingConvention.Cdecl)]
    internal static extern ZOwnedSubscriber z_subscriber_null();

    [DllImport(DllName, EntryPoint = "z_subscriber_options_default", CallingConvention = CallingConvention.Cdecl)]
    internal static extern ZSubscriberOptions z_subscriber_options_default();

    [DllImport(DllName, EntryPoint = "z_subscriber_pull", CallingConvention = CallingConvention.Cdecl)]
    internal static extern sbyte z_subscriber_pull(ZPullSubscriber* sub);

    [DllImport(DllName, EntryPoint = "z_declare_subscriber", CallingConvention = CallingConvention.Cdecl)]
    internal static extern ZOwnedSubscriber z_declare_subscriber(
        ZSession session, ZKeyexpr keyexpr, ZOwnedClosureSample* callback, ZSubscriberOptions* options);

    [DllImport(DllName, EntryPoint = "z_undeclare_subscriber", CallingConvention = CallingConvention.Cdecl)]
    internal static extern sbyte z_undeclare_subscriber(ZOwnedSubscriber* sub);

    [DllImport(DllName, EntryPoint = "z_pull_subscriber_check", CallingConvention = CallingConvention.Cdecl)]
    internal static extern sbyte z_pull_subscriber_check(ZOwnedPullSubscriber* sub);

    [DllImport(DllName, EntryPoint = "z_pull_subscriber_loan", CallingConvention = CallingConvention.Cdecl)]
    internal static extern ZPullSubscriber z_pull_subscriber_loan(ZOwnedPullSubscriber* sub);

    [DllImport(DllName, EntryPoint = "z_pull_subscriber_null", CallingConvention = CallingConvention.Cdecl)]
    internal static extern ZOwnedPullSubscriber z_pull_subscriber_null();

    [DllImport(DllName, EntryPoint = "z_pull_subscriber_options_default", CallingConvention = CallingConvention.Cdecl)]
    internal static extern ZPullSubscriberOptions z_pull_subscriber_options_default();

    [DllImport(DllName, EntryPoint = "z_declare_pull_subscriber", CallingConvention = CallingConvention.Cdecl)]
    internal static extern ZOwnedPullSubscriber z_declare_pull_subscriber(
        ZSession session, ZKeyexpr keyexpr, ZOwnedClosureSample* callback, ZPullSubscriberOptions* options);

    [DllImport(DllName, EntryPoint = "z_undeclare_pull_subscriber", CallingConvention = CallingConvention.Cdecl)]
    internal static extern sbyte z_undeclare_pull_subscriber(ZOwnedPullSubscriber* sub);

    [DllImport(DllName, EntryPoint = "z_declare_queryable", CallingConvention = CallingConvention.Cdecl)]
    internal static extern ZOwnedQueryable z_declare_queryable(
        ZSession session, ZKeyexpr keyexpr, ZOwnedClosureQuery* callback, ZQueryableOptions* options);

    [DllImport(DllName, EntryPoint = "z_undeclare_queryable", CallingConvention = CallingConvention.Cdecl)]
    internal static extern sbyte z_undeclare_queryable(ZOwnedQueryable* queryable);

    [DllImport(DllName, EntryPoint = "z_delete", CallingConvention = CallingConvention.Cdecl)]
    internal static extern sbyte z_delete(ZSession session, ZKeyexpr keyexpr, ZDeleteOptions* options);

    [DllImport(DllName, EntryPoint = "z_delete_options_default", CallingConvention = CallingConvention.Cdecl)]
    internal static extern ZDeleteOptions z_delete_options_default();

    [DllImport(DllName, EntryPoint = "z_get", CallingConvention = CallingConvention.Cdecl)]
    internal static extern sbyte z_get(
        ZSession session, ZKeyexpr keyexpr, byte* parameters, ZOwnedClosureReply* callback, ZGetOptions* options);

    [DllImport(DllName, EntryPoint = "z_get_options_default", CallingConvention = CallingConvention.Cdecl)]
    internal static extern ZGetOptions z_get_options_default();

    [DllImport(DllName, EntryPoint = "z_info_peers_zid", CallingConvention = CallingConvention.Cdecl)]
    internal static extern sbyte z_info_peers_zid(ZSession session, ZOwnedClosureZId* callback);

    [DllImport(DllName, EntryPoint = "z_info_routers_zid", CallingConvention = CallingConvention.Cdecl)]
    internal static extern sbyte z_info_routers_zid(ZSession session, ZOwnedClosureZId* callback);

    [DllImport(DllName, EntryPoint = "z_info_zid", CallingConvention = CallingConvention.Cdecl)]
    internal static extern ZId z_info_zid(ZSession session);

    [DllImport(DllName, EntryPoint = "z_declare_publisher", CallingConvention = CallingConvention.Cdecl)]
    internal static extern ZOwnedPublisher z_declare_publisher(
        ZSession session, ZKeyexpr keyexpr, ZPublisherOptions* options);

    [DllImport(DllName, EntryPoint = "z_undeclare_publisher", CallingConvention = CallingConvention.Cdecl)]
    internal static extern sbyte z_undeclare_publisher(ZOwnedPublisher* publisher);

    [DllImport(DllName, EntryPoint = "z_publisher_options_default", CallingConvention = CallingConvention.Cdecl)]
    internal static extern ZPublisherOptions z_publisher_options_default();

    [DllImport(DllName, EntryPoint = "z_publisher_check", CallingConvention = CallingConvention.Cdecl)]
    internal static extern sbyte z_publisher_check(ZOwnedPublisher* pbl);

    [DllImport(DllName, EntryPoint = "z_publisher_delete", CallingConvention = CallingConvention.Cdecl)]
    internal static extern sbyte z_publisher_delete(ZPublisher publisher, ZPublisherDeleteOptions* options);

    [DllImport(DllName, EntryPoint = "z_publisher_delete_options_default", CallingConvention = CallingConvention.Cdecl)]
    internal static extern ZPublisherDeleteOptions z_publisher_delete_options_default();

    [DllImport(DllName, EntryPoint = "z_publisher_loan", CallingConvention = CallingConvention.Cdecl)]
    internal static extern ZPublisher z_publisher_loan(ZOwnedPublisher* pbl);

    [DllImport(DllName, EntryPoint = "z_publisher_null", CallingConvention = CallingConvention.Cdecl)]
    internal static extern ZOwnedPublisher z_publisher_null();

    [DllImport(DllName, EntryPoint = "z_publisher_put", CallingConvention = CallingConvention.Cdecl)]
    internal static extern sbyte z_publisher_put(
        ZPublisher publisher, byte* payload, nuint len, ZPublisherPutOptions* options);

    [DllImport(DllName, EntryPoint = "zc_publisher_put_owned", CallingConvention = CallingConvention.Cdecl)]
    internal static extern sbyte zc_publisher_put_owned(
        ZPublisher publisher, ZcOwnedPayload* payload, ZPublisherPutOptions* options);

    [DllImport(DllName, EntryPoint = "z_publisher_put_options_default", CallingConvention = CallingConvention.Cdecl)]
    internal static extern ZPublisherOptions z_publisher_put_options_default();

    [DllImport(DllName, EntryPoint = "z_put", CallingConvention = CallingConvention.Cdecl)]
    internal static extern sbyte z_put(ZSession session, ZKeyexpr keyexpr, byte* payload, nuint len, ZPutOptions* opts);

    [DllImport(DllName, EntryPoint = "zc_put_owned", CallingConvention = CallingConvention.Cdecl)]
    internal static extern sbyte zc_put_owned(
        ZSession session, ZKeyexpr keyexpr, ZcOwnedPayload* payload, ZPutOptions* opts);

    [DllImport(DllName, EntryPoint = "z_put_options_default", CallingConvention = CallingConvention.Cdecl)]
    internal static extern ZPutOptions z_put_options_default();

    [DllImport(DllName, EntryPoint = "z_query_consolidation_auto", CallingConvention = CallingConvention.Cdecl)]
    internal static extern ZQueryConsolidation z_query_consolidation_auto();

    [DllImport(DllName, EntryPoint = "z_query_consolidation_default", CallingConvention = CallingConvention.Cdecl)]
    internal static extern ZQueryConsolidation z_query_consolidation_default();

    [DllImport(DllName, EntryPoint = "z_query_consolidation_latest", CallingConvention = CallingConvention.Cdecl)]
    internal static extern ZQueryConsolidation z_query_consolidation_latest();

    [DllImport(DllName, EntryPoint = "z_query_consolidation_monotonic", CallingConvention = CallingConvention.Cdecl)]
    internal static extern ZQueryConsolidation z_query_consolidation_monotonic();

    [DllImport(DllName, EntryPoint = "z_query_consolidation_none", CallingConvention = CallingConvention.Cdecl)]
    internal static extern ZQueryConsolidation z_query_consolidation_none();

    [DllImport(DllName, EntryPoint = "z_query_keyexpr", CallingConvention = CallingConvention.Cdecl)]
    internal static extern ZKeyexpr z_query_keyexpr(ZQuery* query);

    [DllImport(DllName, EntryPoint = "z_query_parameters", CallingConvention = CallingConvention.Cdecl)]
    internal static extern ZBytes z_query_parameters(ZQuery* query);

    [DllImport(DllName, EntryPoint = "z_query_reply", CallingConvention = CallingConvention.Cdecl)]
    internal static extern sbyte z_query_reply(
        ZQuery* query, ZKeyexpr key, byte* payload, nuint len, ZQueryReplyOptions* options);

    [DllImport(DllName, EntryPoint = "z_query_parameters", CallingConvention = CallingConvention.Cdecl)]
    internal static extern ZQueryReplyOptions z_query_reply_options_default();

    [DllImport(DllName, EntryPoint = "z_query_target_default", CallingConvention = CallingConvention.Cdecl)]
    internal static extern QueryTarget z_query_target_default();

    [DllImport(DllName, EntryPoint = "z_query_value", CallingConvention = CallingConvention.Cdecl)]
    internal static extern ZValue z_query_value(ZQuery* query);

    [DllImport(DllName, EntryPoint = "z_queryable_check", CallingConvention = CallingConvention.Cdecl)]
    internal static extern sbyte z_queryable_check(ZOwnedQueryable* queryable);

    [DllImport(DllName, EntryPoint = "z_queryable_null", CallingConvention = CallingConvention.Cdecl)]
    internal static extern ZOwnedQueryable z_queryable_null();

    [DllImport(DllName, EntryPoint = "z_queryable_options_default", CallingConvention = CallingConvention.Cdecl)]
    internal static extern ZQueryableOptions z_queryable_options_default();

    [DllImport(DllName, EntryPoint = "z_reply_check", CallingConvention = CallingConvention.Cdecl)]
    internal static extern sbyte z_reply_check(ZOwnedReply* replyData);

    [DllImport(DllName, EntryPoint = "z_reply_is_ok", CallingConvention = CallingConvention.Cdecl)]
    internal static extern sbyte z_reply_is_ok(ZOwnedReply* reply);

    [DllImport(DllName, EntryPoint = "z_reply_drop", CallingConvention = CallingConvention.Cdecl)]
    internal static extern void z_reply_drop(ZOwnedReply* replyData);

    [DllImport(DllName, EntryPoint = "z_reply_err", CallingConvention = CallingConvention.Cdecl)]
    internal static extern ZValue z_reply_err(ZOwnedReply* replyData);

    [DllImport(DllName, EntryPoint = "z_reply_null", CallingConvention = CallingConvention.Cdecl)]
    internal static extern ZOwnedReply z_reply_null();

    [DllImport(DllName, EntryPoint = "z_reply_ok", CallingConvention = CallingConvention.Cdecl)]
    internal static extern ZSample z_reply_ok(ZOwnedReply* reply);

    [DllImport(DllName, EntryPoint = "z_open", CallingConvention = CallingConvention.Cdecl)]
    internal static extern ZOwnedSession z_open(ZOwnedConfig* config);

    [DllImport(DllName, EntryPoint = "z_close", CallingConvention = CallingConvention.Cdecl)]
    internal static extern sbyte z_close(ZOwnedSession* session);

    [DllImport(DllName, EntryPoint = "z_session_check", CallingConvention = CallingConvention.Cdecl)]
    internal static extern sbyte z_session_check(ZOwnedSession* session);

    [DllImport(DllName, EntryPoint = "z_session_loan", CallingConvention = CallingConvention.Cdecl)]
    internal static extern ZSession z_session_loan(ZOwnedSession* session);

    [DllImport(DllName, EntryPoint = "z_session_null", CallingConvention = CallingConvention.Cdecl)]
    internal static extern ZOwnedSession z_session_null();

    [DllImport(DllName, EntryPoint = "zc_session_rcinc", CallingConvention = CallingConvention.Cdecl)]
    internal static extern ZOwnedSession zc_session_rcinc(ZSession session);

    [DllImport(DllName, EntryPoint = "zc_init_logger", CallingConvention = CallingConvention.Cdecl)]
    internal static extern void zc_init_logger();

    [DllImport(DllName, EntryPoint = "zc_payload_check", CallingConvention = CallingConvention.Cdecl)]
    internal static extern sbyte zc_payload_check(ZcOwnedPayload* payload);

    [DllImport(DllName, EntryPoint = "zc_payload_drop", CallingConvention = CallingConvention.Cdecl)]
    internal static extern void zc_payload_drop(ZcOwnedPayload* payload);

    [DllImport(DllName, EntryPoint = "zc_payload_null", CallingConvention = CallingConvention.Cdecl)]
    internal static extern ZcOwnedPayload zc_payload_null();

    [DllImport(DllName, EntryPoint = "zc_payload_rcinc", CallingConvention = CallingConvention.Cdecl)]
    internal static extern ZcOwnedPayload zc_payload_rcinc(ZcOwnedPayload* payload);

    [DllImport(DllName, EntryPoint = "zc_sample_payload_rcinc", CallingConvention = CallingConvention.Cdecl)]
    internal static extern ZcOwnedPayload zc_sample_payload_rcinc(ZSample* sample);

    [DllImport(DllName, EntryPoint = "z_closure_sample_drop", CallingConvention = CallingConvention.Cdecl)]
    internal static extern void z_closure_sample_drop(ZOwnedClosureSample* closure);

    [DllImport(DllName, EntryPoint = "z_closure_reply_call", CallingConvention = CallingConvention.Cdecl)]
    internal static extern void z_closure_reply_call(ZOwnedClosureReply* closure, ZOwnedReply* sample);

    [DllImport(DllName, EntryPoint = "zc_reply_fifo_new", CallingConvention = CallingConvention.Cdecl)]
    internal static extern ZOwnedReplyChannel zc_reply_fifo_new(nuint bound);

    [DllImport(DllName, EntryPoint = "z_reply_channel_drop", CallingConvention = CallingConvention.Cdecl)]
    internal static extern void z_reply_channel_drop(ZOwnedReplyChannel* channel);

    [DllImport(DllName, EntryPoint = "z_reply_channel_closure_call", CallingConvention = CallingConvention.Cdecl)]
    internal static extern sbyte z_reply_channel_closure_call(ZOwnedReplyChannelClosure* closure, ZOwnedReply* reply);
}