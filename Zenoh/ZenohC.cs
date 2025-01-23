#pragma warning disable CS8500

// Marshal.GetFunctionPointerForDelegate 
// 将委托转换为可从非托管代码调用的函数指针
//
// Marshal.GetDelegateForFunctionPointer
// 将非托管函数指针转换为委托

using System.Runtime.InteropServices;

namespace Zenoh;

using ZResult = System.SByte;

// zenoh_commons.h
// z_congestion_control_t
public enum CongestionControl : uint
{
    Block = 0,
    Drop = 1
}

// z_encoding_prefix_t
public enum EncodingPrefix : uint
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
public enum SampleKind : uint
{
    Put = 0,
    Delete = 1
}

// zenoh_commons.h
// z_priority_t
public enum Priority : uint
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
public enum ConsolidationMode : int
{
    Auto = -1,
    None = 0,
    Monotonic = 1,
    Latest = 2
}

// z_query_consolidation_t
public enum QueryConsolidation : int
{
    Auto = -1,
    None = 0,
    Monotonic = 1,
    Latest = 2
}

// z_reliability_t
public enum Reliability : uint
{
    BestEffort,
    Reliable
}

// zenoh_commons.h
// z_query_target_t 
public enum QueryTarget : uint
{
    BestMatching = 0,
    All = 1,
    AllComplete = 2
}

// zenoh_commons.h
// z_what_t
public enum What : uint
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
public enum Whatami : uint
{
    Router = 1,
    Peer = 2,
    Client = 4
}

// zenoh_commons.h
// zc_locality_t
public enum ZcLocality : uint
{
    Any = 0,
    Local = 1,
    Remote = 2,
}

// zenoh_commons.h
// zc_log_severity_t
public enum ZcLogSeverity : uint
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
    internal delegate* unmanaged[Cdecl] <ZLoanedHello*, void*, void> call;
    internal delegate* unmanaged[Cdecl]<void*, void> drop;
}

// internal unsafe delegate void ZOwnedClosureHelloCall(ZLoanedHello* hello, void* context);
// internal unsafe delegate void ZOwnedClosureHelloDrop(void* context);

// zenoh_opaque.h
// z_loaned_closure_hello_t
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct ZLoanedClosureHello
{
    private fixed nuint data[3];
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
// z_moved_query_t
[StructLayout(LayoutKind.Sequential)]
internal struct ZMovedQuery
{
    internal ZOwnedQuery ownedQuery;
}

// zenoh_commons.h
// z_owned_closure_query_t
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct ZOwnedClosureQuery
{
    internal void* context;
    internal delegate* unmanaged[Cdecl]<ZLoanedQuery*, void*, void> call;
    internal delegate* unmanaged[Cdecl]<void*, void> drop;
}

// internal unsafe delegate void ZOwnedClosureQueryCall(ZLoanedQuery* query, void* context);
// internal unsafe delegate void ZOwnedClosureQueryDrop(void* context);

// zenoh_opaque.h
// z_loaned_closure_query_t
[StructLayout(LayoutKind.Sequential, Pack = 8)]
internal unsafe struct ZLoanedClosureQuery
{
    private fixed nuint data[3];
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
// z_moved_reply_t
[StructLayout(LayoutKind.Sequential)]
internal struct ZMovedReply
{
    internal ZOwnedReply ownedReply;
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

// zenoh_commons.h
// z_moved_reply_err_t
[StructLayout(LayoutKind.Sequential)]
internal struct ZMovedReplyErr
{
    internal ZOwnedReplyErr ownedReplyErr;
}

// zenoh_commons.h
// z_owned_closure_reply_t
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct ZOwnedClosureReply
{
    internal void* context;
    internal delegate* unmanaged[Cdecl]<ZLoanedReply*, void*> call;
    internal delegate* unmanaged[Cdecl]<void*, void> drop;
}

// internal unsafe delegate void ZOwnedClosureReplyCall(ZLoanedReply* reply, void* context);
// internal unsafe delegate void ZOwnedClosureReplyDrop(void* context);

// zenoh_opaque.h
// z_loaned_closure_reply_t
[StructLayout(LayoutKind.Sequential, Pack = 8)]
internal unsafe struct ZLoanedClosureReply
{
    private fixed nuint data[3];
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
// z_moved_sample_t
[StructLayout(LayoutKind.Sequential)]
internal struct ZMovedSample
{
    internal ZOwnedSample ownedSample;
}

// zenoh_commons.h
// z_owned_closure_sample_t
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct ZOwnedClosureSample
{
    internal void* context;
    internal delegate* unmanaged[Cdecl]<ZLoanedSample*, void*> call;
    internal delegate* unmanaged[Cdecl]<void*, void> drop;
}

// internal unsafe delegate void ZOwnedClosureSampleCall(ZLoanedSample* reply, void* context);
// internal unsafe delegate void ZOwnedClosureSampleDrop(void* context);

// zenoh_opaque.h
// z_loaned_closure_sample_t
[StructLayout(LayoutKind.Sequential, Pack = 8)]
internal unsafe struct ZLoanedClosureSample
{
    private fixed nuint data[3];
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
    internal delegate* unmanaged[Cdecl]<ZId*, void*, void> call;
    internal delegate* unmanaged[Cdecl]<void*, void> drop;
}

// internal unsafe delegate void ZOwnedClosureZidCall(ZId* zId, void* context);
// internal unsafe delegate void ZOwnedClosureZidDrop(void* context);

// zenoh_opaque.h
// z_loaned_closure_zid_t
[StructLayout(LayoutKind.Sequential, Pack = 8)]
internal unsafe struct ZLoanedClosureZid
{
    private fixed nuint data[3];
}

// zenoh_commons.h
// z_moved_closure_zid_t
[StructLayout(LayoutKind.Sequential)]
internal struct ZMovedClosureZid
{
    internal ZOwnedClosureZid owned_closure_zid;
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
    internal ZOwnedCondvar owned_condvar;
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

// zenoh_commons.h
// z_time_t
[StructLayout(LayoutKind.Sequential)]
internal struct ZTime
{
    internal ulong t;
}

// zenoh_commons.h
// z_clock_t
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct ZClock
{
    internal ulong t;
    internal void* t_base;
}

// zenoh_commons.h
// z_close_options_t
[StructLayout(LayoutKind.Sequential)]
internal struct ZCloseOptions
{
    internal byte dummy;
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
    internal ZOwnedEncoding ownedEncoding;
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

// zenoh_commons.h
// z_moved_publisher_t
[StructLayout(LayoutKind.Sequential)]
internal struct ZMovedPublisher
{
    internal ZOwnedPublisher ownedPublisher;
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

// zenoh_commons.h
// z_moved_queryable_t
[StructLayout(LayoutKind.Sequential)]
internal struct ZMovedQueryable
{
    internal ZOwnedQueryable ownedQueryable;
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

// zenoh_commons.h
// z_moved_subscriber_t
[StructLayout(LayoutKind.Sequential)]
internal struct ZMovedSubscriber
{
    internal ZOwnedSubscriber ownedSubscriber;
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

// zenoh_commons.h
// z_moved_string_array_t
[StructLayout(LayoutKind.Sequential)]
internal struct ZMovedStringArray
{
    internal ZOwnedStringArray ownedStringArray;
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
// z_moved_liveliness_token_t
[StructLayout(LayoutKind.Sequential)]
internal struct ZMovedLivelinessToken
{
    internal ZOwnedLivelinessToken ownedLivelinessToken;
}

// zenoh_commons.h
// z_liveliness_subscriber_options_t
[StructLayout(LayoutKind.Sequential)]
internal struct ZLivelinessSubscriberOptions
{
    internal byte history;
}

// zenoh_commons.h
// z_liveliness_token_options_t
[StructLayout(LayoutKind.Sequential)]
internal struct ZLivelinessTokenOptions
{
    internal byte dummy;
}

// zenoh_commons.h
// z_liveliness_get_options_t
[StructLayout(LayoutKind.Sequential)]
internal struct ZLivelinessGetOptions
{
    internal uint timeout_ms;
}

// zenoh_opaque.h
// z_owned_mutex_t
[StructLayout(LayoutKind.Sequential, Pack = 8)]
internal unsafe struct ZOwnedMutex
{
    private fixed byte data[32];
}

// zenoh_commons.h
// z_moved_mutex_t
[StructLayout(LayoutKind.Sequential)]
internal struct ZMovedMutex
{
    internal ZOwnedMutex ownedMutex;
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

// zenoh_commons.h
// z_moved_ring_handler_query_t
[StructLayout(LayoutKind.Sequential)]
internal struct ZMovedRingHandlerQuery
{
    internal ZOwnedRingHandlerQuery ownedRingHandlerQuery;
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

// zenoh_commons.h
// z_moved_ring_handler_reply_t
[StructLayout(LayoutKind.Sequential)]
internal struct ZMovedRingHandlerReply
{
    internal ZOwnedRingHandlerReply ownedRingHandlerReply;
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

// zenoh_commons.h
// z_moved_ring_handler_sample_t
[StructLayout(LayoutKind.Sequential)]
internal struct ZMovedRingHandlerSample
{
    internal ZOwnedRingHandlerSample ownedRingHandlerSample;
}

// zenoh_opaque.h
// z_owned_session_t
[StructLayout(LayoutKind.Sequential, Pack = 8)]
internal unsafe struct ZOwnedSession
{
    private fixed byte data[8];
}

// zenoh_commons.h
// z_moved_session_t
[StructLayout(LayoutKind.Sequential)]
internal struct ZMovedSession
{
    internal ZOwnedSession ownedSession;
}

// zenoh_opaque.h
// z_owned_task_t
[StructLayout(LayoutKind.Sequential, Pack = 8)]
internal unsafe struct ZOwnedTask
{
    private fixed byte data[24];
}

// zenoh_commons.h
// z_moved_task_t
[StructLayout(LayoutKind.Sequential)]
internal struct ZMovedTask
{
    internal ZOwnedTask ownedTask;
}

// zenoh_commons.h
// z_task_attr_t
[StructLayout(LayoutKind.Sequential)]
internal struct ZTaskAttr
{
    internal nuint attr;
}

// zenoh_commons.h
// z_open_options_t
[StructLayout(LayoutKind.Sequential)]
internal struct ZOpenOptions
{
    internal byte dummy;
}

// zenoh_commons.h
// z_scout_options_t
[StructLayout(LayoutKind.Sequential)]
internal struct ZScoutOptions
{
    internal ulong timeout_ms;
    internal What What;
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

// zenoh_commons.h
// z_publisher_options_t 
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct ZPublisherOptions
{
    internal ZMovedEncoding* encoding;
    internal CongestionControl congestion_control;
    internal Priority priority;
    [MarshalAs(UnmanagedType.U1)] internal bool is_express;
}

// zenoh_commons.h
// z_publisher_delete_options_t
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct ZPublisherDeleteOptions
{
    internal ZTimestamp* timestamp;
}

// zenoh_commons.h
// z_publisher_put_options_t 
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct ZPublisherPutOptions
{
    internal ZMovedEncoding* encoding;
    internal ZTimestamp* timestamp;
    internal ZMovedBytes* attachment;
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
    [MarshalAs(UnmanagedType.U1)] internal bool is_express;
    internal ZTimestamp* timestamp;
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
    internal ZMovedBytes* payload;
    internal ZMovedEncoding* encoding;
    internal CongestionControl congestion_control;
    [MarshalAs(UnmanagedType.U1)] internal bool is_express;
    internal ZMovedBytes* attachment;
    internal ulong timeout_ms;
}

// zenoh_commons.h
// z_put_options_t 
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct ZPutOptions
{
    internal ZMovedEncoding* encoding;
    internal CongestionControl congestionControl;
    internal Priority priority;
    [MarshalAs(UnmanagedType.U1)] internal bool is_express;
    internal ZTimestamp* timestamp;
    internal ZMovedBytes* attachment;
}

// zenoh_commons.h
// z_query_reply_options_t 
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct ZQueryReplyOptions
{
    internal ZMovedEncoding* encoding;
    internal CongestionControl congestionControl;
    internal Priority priority;
    [MarshalAs(UnmanagedType.U1)] internal bool is_express;
    internal ZTimestamp* timestamp;
    internal ZMovedBytes* attachment;
}

// zenoh_commons.h
// z_query_reply_del_options_t 
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct ZQueryReplyDelOptions
{
    internal CongestionControl congestionControl;
    internal Priority priority;
    [MarshalAs(UnmanagedType.U1)] internal bool is_express;
    internal ZTimestamp* timestamp;
    internal ZMovedBytes* attachment;
}

// zenoh_commons.h
// z_query_reply_err_options_t 
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct ZQueryReplyErrOptions
{
    internal ZMovedEncoding* encoding;
}

// zenoh_opaque.h
// zc_loaned_closure_log_t
[StructLayout(LayoutKind.Sequential, Pack = 8)]
internal unsafe struct ZcLoanedClosureLog
{
    private fixed nuint data[3];
}

// zenoh_opaque.h
// ze_deserializer_t
[StructLayout(LayoutKind.Sequential, Pack = 8)]
internal unsafe struct ZeDeserializer
{
    private fixed byte data[24];
}

// zenoh_opaque.h
// ze_owned_serializer_t
[StructLayout(LayoutKind.Sequential, Pack = 8)]
internal unsafe struct ZeOwnedSerializer
{
    private fixed byte data[56];
}

// zenoh_opaque.h
// ze_loaned_serializer_t
[StructLayout(LayoutKind.Sequential, Pack = 8)]
internal unsafe struct ZeLoanedSerializer
{
    private fixed byte data[56];
}

// zenoh_commons.h
// ze_moved_serializer_t
[StructLayout(LayoutKind.Sequential)]
internal struct ZeMovedSerializer
{
    internal ZeOwnedSerializer ownedSerializer;
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
    internal static string zConfigAddTimestampKey = "timestamping/enabled";

    // internal static string ZOwnedStrToString(ZOwnedStr* zs)
    // {
    //     if (z_str_check(zs) != 1) return "";
    //
    //     return Marshal.PtrToStringUTF8(zs->cstr) ?? "";
    // }
    //
    // internal static string ZKeyexprToString(ZKeyexpr keyexpr)
    // {
    //     var str = z_keyexpr_to_string(keyexpr);
    //     var o = ZOwnedStrToString(&str);
    //     z_str_drop(&str);
    //     return o;
    // }

    [DllImport(DllName, EntryPoint = "z_bytes_clone", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_bytes_clone(ZOwnedBytes* dst, ZLoanedBytes* src);

    [DllImport(DllName, EntryPoint = "z_bytes_copy_from_buf", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_bytes_copy_from_buf(ZOwnedBytes* dst, byte* src, nuint len);

    [DllImport(DllName, EntryPoint = "z_bytes_copy_from_slice", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_bytes_copy_from_slice(ZOwnedBytes* dst, ZLoanedSlice* src);

    [DllImport(DllName, EntryPoint = "z_bytes_copy_from_str", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_bytes_copy_from_str(ZOwnedBytes* dst, byte* src);

    [DllImport(DllName, EntryPoint = "z_bytes_copy_from_string", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_bytes_copy_from_string(ZOwnedBytes* dst, ZLoanedString* src);

    [DllImport(DllName, EntryPoint = "z_bytes_drop", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    internal static extern void z_bytes_drop(ZMovedBytes* t);

    [DllImport(DllName, EntryPoint = "z_bytes_empty", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_bytes_empty(ZOwnedBytes* t);

    [DllImport(DllName, EntryPoint = "z_bytes_from_buf", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_bytes_from_buf(ZOwnedBytes* dst, byte* src, nuint len, void* deleter,
        void* context);

    [DllImport(DllName, EntryPoint = "z_bytes_from_slice", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_bytes_from_slice(ZOwnedBytes* dst, ZMovedSlice* src);

    [DllImport(DllName, EntryPoint = "z_bytes_from_static_buf", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_bytes_from_static_buf(ZOwnedBytes* dst, byte* src, nuint len);

    [DllImport(DllName, EntryPoint = "z_bytes_from_static_str", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_bytes_from_static_str(ZOwnedBytes* dst, byte* src);

    [DllImport(DllName, EntryPoint = "z_bytes_from_str", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_bytes_from_str(ZOwnedBytes* dst, byte* src, void* deleter, void* context);

    [DllImport(DllName, EntryPoint = "z_bytes_from_string", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_bytes_from_string(ZOwnedBytes* dst, ZMovedString* src);

    [DllImport(DllName, EntryPoint = "z_bytes_get_reader", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZBytesReader z_bytes_get_reader(ZLoanedBytes* data);

    [DllImport(DllName, EntryPoint = "z_bytes_get_slice_iterator", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZBytesSliceIterator z_bytes_get_slice_iterator(ZLoanedBytes* data);

    [DllImport(DllName, EntryPoint = "z_bytes_is_empty", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool z_bytes_is_empty(ZLoanedBytes* data);

    [DllImport(DllName, EntryPoint = "z_bytes_len", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    internal static extern nuint z_bytes_len(ZLoanedBytes* data);

    [DllImport(DllName, EntryPoint = "z_bytes_loan", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    internal static extern ZLoanedBytes* z_bytes_loan(ZOwnedBytes* data);

    [DllImport(DllName, EntryPoint = "z_bytes_loan_mut", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedBytes* z_bytes_loan_mut(ZOwnedBytes* data);

    [DllImport(DllName, EntryPoint = "z_bytes_reader_read", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern nuint z_bytes_reader_read(ZBytesReader* reader, byte* dst, nuint len);

    [DllImport(DllName, EntryPoint = "z_bytes_reader_remaining", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern nuint z_bytes_reader_remaining(ZBytesReader* reader);

    [DllImport(DllName, EntryPoint = "z_bytes_reader_seek", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_bytes_reader_seek(ZBytesReader* reader, long offset, int origin);

    [DllImport(DllName, EntryPoint = "z_bytes_reader_tell", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern long z_bytes_reader_tell(ZBytesReader* reader);

    [DllImport(DllName, EntryPoint = "z_bytes_slice_iterator_next", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool z_bytes_slice_iterator_next(ZBytesSliceIterator* iter, ZViewSlice* slice);

    [DllImport(DllName, EntryPoint = "z_bytes_to_slice", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_bytes_to_slice(ZLoanedBytes* bytes, ZOwnedSlice* dst);

    [DllImport(DllName, EntryPoint = "z_bytes_to_string", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_bytes_to_string(ZLoanedBytes* bytes, ZOwnedString* dst);

    [DllImport(DllName, EntryPoint = "z_bytes_writer_append", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_bytes_writer_append(ZLoanedBytesWriter* writer, ZMovedBytes* bytes);

    [DllImport(DllName, EntryPoint = "z_bytes_writer_drop", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_bytes_writer_drop(ZMovedBytesWriter* writer);

    [DllImport(DllName, EntryPoint = "z_bytes_writer_empty", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_bytes_writer_empty(ZOwnedBytesWriter* writer);

    [DllImport(DllName, EntryPoint = "z_bytes_writer_finish", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_bytes_writer_finish(ZMovedBytesWriter* writer, ZOwnedBytes* bytes);

    [DllImport(DllName, EntryPoint = "z_bytes_writer_loan", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedBytesWriter* z_bytes_writer_loan(ZOwnedBytesWriter* writer);

    [DllImport(DllName, EntryPoint = "z_bytes_writer_loan_mut", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedBytesWriter* z_bytes_writer_loan_mut(ZOwnedBytesWriter* writer);

    [DllImport(DllName, EntryPoint = "z_bytes_writer_write_all", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_bytes_writer_write_all(ZLoanedBytesWriter* writer, byte* src, nuint len);

    [DllImport(DllName, EntryPoint = "z_clock_elapsed_s", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ulong z_clock_elapsed_s(ZClock* time);

    [DllImport(DllName, EntryPoint = "z_clock_elapsed_ms", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ulong z_clock_elapsed_ms(ZClock* time);

    [DllImport(DllName, EntryPoint = "z_clock_elapsed_us", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ulong z_clock_elapsed_us(ZClock* time);

    [DllImport(DllName, EntryPoint = "z_clock_now", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    internal static extern ZClock z_clock_now();

    [DllImport(DllName, EntryPoint = "z_close", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    internal static extern ZResult z_close(ZLoanedSession* session, ZCloseOptions* options);

    [DllImport(DllName, EntryPoint = "z_close_options_default", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_close_options_default(ZCloseOptions* options);

    [DllImport(DllName, EntryPoint = "z_closure_hello", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_closure_hello(
        ZOwnedClosureHello* closure,
        delegate*<ZLoanedHello*, void*, void> call,
        delegate*<void*, void> drop,
        void* context
    );

    [DllImport(DllName, EntryPoint = "z_closure_hello_call", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_closure_hello_call(ZLoanedClosureHello* closure, ZLoanedHello* hello);

    [DllImport(DllName, EntryPoint = "z_closure_hello_drop", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_closure_hello_drop(ZMovedClosureHello* closure);

    [DllImport(DllName, EntryPoint = "z_closure_hello_loan", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedClosureHello* z_closure_hello_loan(ZOwnedClosureHello* closure);

    [DllImport(DllName, EntryPoint = "z_closure_hello_loan_mut", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedClosureHello* z_closure_hello_loan_mut(ZOwnedClosureHello* closure);

    [DllImport(DllName, EntryPoint = "z_closure_query", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_closure_query(
        ZOwnedClosureQuery* closure,
        delegate*<ZLoanedQuery*, void*, void> call,
        delegate*<void*, void> drop,
        void* context
    );

    [DllImport(DllName, EntryPoint = "z_closure_query_call", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_closure_query_call(ZLoanedClosureQuery* closure, ZLoanedQuery* query);

    [DllImport(DllName, EntryPoint = "z_closure_query_drop", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_closure_query_drop(ZMovedClosureQuery* closure);

    [DllImport(DllName, EntryPoint = "z_closure_query_loan", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedClosureQuery* z_closure_query_loan(ZOwnedClosureQuery* closure);

    [DllImport(DllName, EntryPoint = "z_closure_query_loan_mut", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedClosureQuery* z_closure_query_loan_mut(ZOwnedClosureQuery* closure);

    [DllImport(DllName, EntryPoint = "z_closure_reply", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_closure_reply(
        ZOwnedClosureReply* closure,
        delegate*<ZLoanedReply*, void*, void> call,
        delegate*<void*, void> drop,
        void* context
    );

    [DllImport(DllName, EntryPoint = "z_closure_reply_call", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_closure_reply_call(ZLoanedClosureReply* closure, ZLoanedReply* reply);

    [DllImport(DllName, EntryPoint = "z_closure_reply_drop", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_closure_reply_drop(ZMovedClosureReply* closure);

    [DllImport(DllName, EntryPoint = "z_closure_reply_loan", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedClosureReply* z_closure_reply_loan(ZOwnedClosureReply* closure);

    [DllImport(DllName, EntryPoint = "z_closure_reply_loan_mut", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedClosureReply* z_closure_reply_loan_mut(ZOwnedClosureReply* closure);

    [DllImport(DllName, EntryPoint = "z_closure_sample", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_closure_sample(
        ZOwnedClosureSample* closure,
        delegate*<ZLoanedSample*, void*, void> call,
        delegate*<void*, void> drop,
        void* context
    );

    [DllImport(DllName, EntryPoint = "z_closure_sample_call", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_closure_sample_call(ZLoanedClosureSample* closure, ZLoanedSample* sample);

    [DllImport(DllName, EntryPoint = "z_closure_sample_drop", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_closure_sample_drop(ZMovedClosureSample* closure);

    [DllImport(DllName, EntryPoint = "z_closure_sample_loan", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedClosureSample* z_closure_sample_loan(ZOwnedClosureSample* closure);

    [DllImport(DllName, EntryPoint = "z_closure_sample_loan_mut", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedClosureSample* z_closure_sample_loan_mut(ZOwnedClosureSample* closure);

    [DllImport(DllName, EntryPoint = "z_closure_zid", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_closure_zid(
        ZOwnedClosureZid* closure,
        delegate*<ZId*, void*, void> call,
        delegate*<void*, void> drop,
        void* context
    );

    [DllImport(DllName, EntryPoint = "z_closure_zid_call", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_closure_zid_call(ZLoanedClosureZid* closure, ZId* zId);

    [DllImport(DllName, EntryPoint = "z_closure_zid_drop", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_closure_zid_drop(ZMovedClosureZid* closure);

    [DllImport(DllName, EntryPoint = "z_closure_zid_loan", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedClosureZid* z_closure_zid_loan(ZOwnedClosureZid* closure);

    [DllImport(DllName, EntryPoint = "z_closure_zid_loan_mut", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedClosureZid* z_closure_zid_loan_mut(ZOwnedClosureZid* closure);

    [DllImport(DllName, EntryPoint = "z_condvar_drop", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_condvar_drop(ZMovedCondvar* condvar);

    [DllImport(DllName, EntryPoint = "z_condvar_init", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_condvar_init(ZOwnedCondvar* condvar);

    [DllImport(DllName, EntryPoint = "z_condvar_loan", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedCondvar* z_condvar_loan(ZOwnedCondvar* condvar);

    [DllImport(DllName, EntryPoint = "z_condvar_loan_mut", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedCondvar* z_condvar_loan_mut(ZOwnedCondvar* condvar);

    [DllImport(DllName, EntryPoint = "z_condvar_signal", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_condvar_signal(ZLoanedCondvar* condvar);

    [DllImport(DllName, EntryPoint = "z_condvar_wait", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_condvar_wait(ZLoanedCondvar* condvar, ZLoanedMutex* mutex);

    [DllImport(DllName, EntryPoint = "z_config_clone", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_config_clone(ZOwnedConfig* dst, ZLoanedConfig* src);

    [DllImport(DllName, EntryPoint = "z_config_default", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_config_default(ZOwnedConfig* config);

    [DllImport(DllName, EntryPoint = "z_config_drop", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_config_drop(ZMovedConfig* config);

    [DllImport(DllName, EntryPoint = "z_config_loan", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedConfig* z_config_loan(ZOwnedConfig* config);

    [DllImport(DllName, EntryPoint = "z_config_loan_mut", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedConfig* z_config_loan_mut(ZOwnedConfig* config);

    [DllImport(DllName, EntryPoint = "z_declare_background_queryable", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_declare_background_queryable(
        ZLoanedSession* session,
        ZLoanedKeyexpr* keyexpr,
        ZMovedClosureQuery* callback,
        ZQueryableOptions* options
    );

    [DllImport(DllName, EntryPoint = "z_declare_background_subscriber", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_declare_background_subscriber(
        ZLoanedSession* session,
        ZLoanedKeyexpr* keyexpr,
        ZMovedClosureSample* callback,
        ZSubscriberOptions* options
    );

    [DllImport(DllName, EntryPoint = "z_declare_keyexpr", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_declare_keyexpr(
        ZLoanedSession* session,
        ZOwnedKeyexpr* declaredKeyexpr,
        ZLoanedKeyexpr* keyexpr
    );

    [DllImport(DllName, EntryPoint = "z_declare_publisher", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_declare_publisher(
        ZLoanedSession* session,
        ZOwnedPublisher* publisher,
        ZLoanedKeyexpr* keyexpr,
        ZPublisherOptions* options
    );

    [DllImport(DllName, EntryPoint = "z_declare_queryable", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_declare_queryable(
        ZLoanedSession* session,
        ZOwnedQueryable* queryable,
        ZLoanedKeyexpr* keyexpr,
        ZMovedClosureQuery* callback,
        ZQueryableOptions* options
    );

    [DllImport(DllName, EntryPoint = "z_declare_subscriber", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_declare_subscriber(
        ZLoanedSession* session,
        ZOwnedSubscriber* subscriber,
        ZLoanedKeyexpr* keyexpr,
        ZMovedClosureSample* callback,
        ZSubscriberOptions* options
    );

    [DllImport(DllName, EntryPoint = "z_delete", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_delete(ZLoanedSession* session, ZLoanedKeyexpr* keyexpr, ZDeleteOptions* options);

    [DllImport(DllName, EntryPoint = "z_delete_options_default", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_delete_options_default(ZDeleteOptions* options);

    [DllImport(DllName, EntryPoint = "z_encoding_application_cbor", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedEncoding* z_encoding_application_cbor();

    [DllImport(DllName, EntryPoint = "z_encoding_application_cdr", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedEncoding* z_encoding_application_cdr();

    [DllImport(DllName, EntryPoint = "z_encoding_application_coap_payload", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedEncoding* z_encoding_application_coap_payload();

    [DllImport(DllName, EntryPoint = "z_encoding_application_java_serialized_object",
        CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedEncoding* z_encoding_application_java_serialized_object();

    [DllImport(DllName, EntryPoint = "z_encoding_application_json", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedEncoding* z_encoding_application_json();

    [DllImport(DllName, EntryPoint = "z_encoding_application_json_patch_json",
        CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedEncoding* z_encoding_application_json_patch_json();

    [DllImport(DllName, EntryPoint = "z_encoding_application_json_seq", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedEncoding* z_encoding_application_json_seq();

    [DllImport(DllName, EntryPoint = "z_encoding_application_jsonpath", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedEncoding* z_encoding_application_jsonpath();

    [DllImport(DllName, EntryPoint = "z_encoding_application_jwt", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedEncoding* z_encoding_application_jwt();

    [DllImport(DllName, EntryPoint = "z_encoding_application_mp4", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedEncoding* z_encoding_application_mp4();

    [DllImport(DllName, EntryPoint = "z_encoding_application_octet_stream", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedEncoding* z_encoding_application_octet_stream();

    [DllImport(DllName, EntryPoint = "z_encoding_application_openmetrics_text",
        CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedEncoding* z_encoding_application_openmetrics_text();

    [DllImport(DllName, EntryPoint = "z_encoding_application_protobuf", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedEncoding* z_encoding_application_protobuf();

    [DllImport(DllName, EntryPoint = "z_encoding_application_python_serialized_object",
        CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedEncoding* z_encoding_application_python_serialized_object();

    [DllImport(DllName, EntryPoint = "z_encoding_application_soap_xml", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedEncoding* z_encoding_application_soap_xml();

    [DllImport(DllName, EntryPoint = "z_encoding_application_sql", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedEncoding* z_encoding_application_sql();

    [DllImport(DllName, EntryPoint = "z_encoding_application_x_www_form_urlencoded",
        CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedEncoding* z_encoding_application_x_www_form_urlencoded();

    [DllImport(DllName, EntryPoint = "z_encoding_application_xml", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedEncoding* z_encoding_application_xml();

    [DllImport(DllName, EntryPoint = "z_encoding_application_yaml", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedEncoding* z_encoding_application_yaml();

    [DllImport(DllName, EntryPoint = "z_encoding_application_yang", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedEncoding* z_encoding_application_yang();

    [DllImport(DllName, EntryPoint = "z_encoding_audio_aac", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedEncoding* z_encoding_audio_aac();

    [DllImport(DllName, EntryPoint = "z_encoding_audio_flac", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedEncoding* z_encoding_audio_flac();

    [DllImport(DllName, EntryPoint = "z_encoding_audio_mp4", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedEncoding* z_encoding_audio_mp4();

    [DllImport(DllName, EntryPoint = "z_encoding_audio_ogg", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedEncoding* z_encoding_audio_ogg();

    [DllImport(DllName, EntryPoint = "z_encoding_audio_vorbis", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedEncoding* z_encoding_audio_vorbis();

    [DllImport(DllName, EntryPoint = "z_encoding_image_bmp", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedEncoding* z_encoding_image_bmp();

    [DllImport(DllName, EntryPoint = "z_encoding_image_gif", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedEncoding* z_encoding_image_gif();

    [DllImport(DllName, EntryPoint = "z_encoding_image_jpeg", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedEncoding* z_encoding_image_jpeg();

    [DllImport(DllName, EntryPoint = "z_encoding_image_png", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedEncoding* z_encoding_image_png();

    [DllImport(DllName, EntryPoint = "z_encoding_image_webp", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedEncoding* z_encoding_image_webp();

    [DllImport(DllName, EntryPoint = "z_encoding_text_css", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedEncoding* z_encoding_text_css();

    [DllImport(DllName, EntryPoint = "z_encoding_text_csv", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedEncoding* z_encoding_text_csv();

    [DllImport(DllName, EntryPoint = "z_encoding_text_html", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedEncoding* z_encoding_text_html();

    [DllImport(DllName, EntryPoint = "z_encoding_text_javascript", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedEncoding* z_encoding_text_javascript();

    [DllImport(DllName, EntryPoint = "z_encoding_text_json", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedEncoding* z_encoding_text_json();

    [DllImport(DllName, EntryPoint = "z_encoding_text_json5", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedEncoding* z_encoding_text_json5();

    [DllImport(DllName, EntryPoint = "z_encoding_text_markdown", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedEncoding* z_encoding_text_markdown();

    [DllImport(DllName, EntryPoint = "z_encoding_text_plain", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedEncoding* z_encoding_text_plain();

    [DllImport(DllName, EntryPoint = "z_encoding_text_xml", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedEncoding* z_encoding_text_xml();

    [DllImport(DllName, EntryPoint = "z_encoding_text_yaml", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedEncoding* z_encoding_text_yaml();

    [DllImport(DllName, EntryPoint = "z_encoding_video_h261", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedEncoding* z_encoding_video_h261();

    [DllImport(DllName, EntryPoint = "z_encoding_video_h263", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedEncoding* z_encoding_video_h263();

    [DllImport(DllName, EntryPoint = "z_encoding_video_h264", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedEncoding* z_encoding_video_h264();

    [DllImport(DllName, EntryPoint = "z_encoding_video_h265", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedEncoding* z_encoding_video_h265();

    [DllImport(DllName, EntryPoint = "z_encoding_video_h266", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedEncoding* z_encoding_video_h266();

    [DllImport(DllName, EntryPoint = "z_encoding_video_mp4", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedEncoding* z_encoding_video_mp4();

    [DllImport(DllName, EntryPoint = "z_encoding_video_ogg", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedEncoding* z_encoding_video_ogg();

    [DllImport(DllName, EntryPoint = "z_encoding_video_raw", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedEncoding* z_encoding_video_raw();

    [DllImport(DllName, EntryPoint = "z_encoding_video_vp8", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedEncoding* z_encoding_video_vp8();

    [DllImport(DllName, EntryPoint = "z_encoding_video_vp9", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedEncoding* z_encoding_video_vp9();

    [DllImport(DllName, EntryPoint = "z_encoding_zenoh_bytes", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedEncoding* z_encoding_zenoh_bytes();

    [DllImport(DllName, EntryPoint = "z_encoding_zenoh_serialized", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedEncoding* z_encoding_zenoh_serialized();

    [DllImport(DllName, EntryPoint = "z_encoding_zenoh_string", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedEncoding* z_encoding_zenoh_string();

    [DllImport(DllName, EntryPoint = "z_encoding_clone", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_encoding_clone(ZOwnedEncoding* dst, ZLoanedEncoding* src);

    [DllImport(DllName, EntryPoint = "z_encoding_drop", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_encoding_drop(ZMovedEncoding* encoding);

    [DllImport(DllName, EntryPoint = "z_encoding_equals", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool z_encoding_equals(ZLoanedEncoding* encoding, ZLoanedEncoding* other);

    [DllImport(DllName, EntryPoint = "z_encoding_from_str", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_encoding_from_str(ZOwnedEncoding* encoding, byte* s);

    [DllImport(DllName, EntryPoint = "z_encoding_from_substr", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_encoding_from_substr(ZOwnedEncoding* encoding, byte* s, nuint len);

    [DllImport(DllName, EntryPoint = "z_encoding_loan_default", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedEncoding* z_encoding_loan_default();

    [DllImport(DllName, EntryPoint = "z_encoding_loan", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedEncoding* z_encoding_loan(ZOwnedEncoding* encoding);

    [DllImport(DllName, EntryPoint = "z_encoding_loan_mut", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedEncoding* z_encoding_loan_mut(ZOwnedEncoding* encoding);

    [DllImport(DllName, EntryPoint = "z_encoding_set_schema_from_str", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_encoding_set_schema_from_str(ZLoanedEncoding* encoding, byte* s);

    [DllImport(DllName, EntryPoint = "z_encoding_set_schema_from_substr", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_encoding_set_schema_from_substr(ZLoanedEncoding* encoding, byte* s, nuint len);

    [DllImport(DllName, EntryPoint = "z_encoding_to_string", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_encoding_to_string(ZLoanedEncoding* encoding, ZOwnedString* outStr);

    [DllImport(DllName, EntryPoint = "z_fifo_channel_query_new", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_fifo_channel_query_new(ZOwnedClosureQuery* callback, ZOwnedFifoHandlerQuery* handler,
        nuint capacity);

    [DllImport(DllName, EntryPoint = "z_fifo_handler_query_drop", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_fifo_handler_query_drop(ZMovedFifoHandlerQuery* handler);

    [DllImport(DllName, EntryPoint = "z_fifo_handler_query_loan", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedFifoHandlerQuery* z_fifo_handler_query_loan(ZOwnedFifoHandlerQuery* handler);

    [DllImport(DllName, EntryPoint = "z_fifo_handler_query_recv", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_fifo_handler_query_recv(ZLoanedFifoHandlerQuery* handler, ZOwnedQuery* query);

    [DllImport(DllName, EntryPoint = "z_fifo_handler_query_try_recv", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_fifo_handler_query_try_recv(ZLoanedFifoHandlerQuery* handler, ZOwnedQuery* query);

    [DllImport(DllName, EntryPoint = "z_fifo_channel_reply_new", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_fifo_channel_reply_new(ZOwnedClosureReply* callback, ZOwnedFifoHandlerReply* handler,
        nuint capacity);

    [DllImport(DllName, EntryPoint = "z_fifo_handler_reply_drop", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_fifo_handler_reply_drop(ZMovedFifoHandlerReply* handler);

    [DllImport(DllName, EntryPoint = "z_fifo_handler_reply_loan", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedFifoHandlerReply* z_fifo_handler_reply_loan(ZOwnedFifoHandlerReply* handler);

    [DllImport(DllName, EntryPoint = "z_fifo_handler_reply_recv", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_fifo_handler_reply_recv(ZLoanedFifoHandlerReply* handler, ZOwnedReply* reply);

    [DllImport(DllName, EntryPoint = "z_fifo_handler_reply_try_recv", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_fifo_handler_reply_try_recv(ZLoanedFifoHandlerReply* handler, ZOwnedReply* reply);

    [DllImport(DllName, EntryPoint = "z_fifo_channel_sample_new", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_fifo_channel_sample_new(ZOwnedClosureSample* callback,
        ZOwnedFifoHandlerSample* handler, nuint capacity);

    [DllImport(DllName, EntryPoint = "z_fifo_handler_sample_drop", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_fifo_handler_sample_drop(ZMovedFifoHandlerSample* handler);

    [DllImport(DllName, EntryPoint = "z_fifo_handler_sample_loan", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedFifoHandlerSample* z_fifo_handler_sample_loan(ZOwnedFifoHandlerSample* handler);

    [DllImport(DllName, EntryPoint = "z_fifo_handler_sample_recv", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_fifo_handler_sample_recv(ZLoanedFifoHandlerSample* handler, ZOwnedSample* sample);

    [DllImport(DllName, EntryPoint = "z_fifo_handler_sample_try_recv", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_fifo_handler_sample_try_recv(ZLoanedFifoHandlerSample* handler,
        ZOwnedSample* sample);

    [DllImport(DllName, EntryPoint = "z_get", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    internal static extern ZResult z_get(
        ZLoanedSession* session,
        ZLoanedKeyexpr* keyexpr,
        byte* parameters,
        ZMovedClosureReply* callback,
        ZGetOptions* options
    );

    [DllImport(DllName, EntryPoint = "z_get_options_default", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_get_options_default(ZGetOptions* options);

    [DllImport(DllName, EntryPoint = "z_hello_clone", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_hello_clone(ZOwnedHello* dst, ZLoanedHello* src);

    [DllImport(DllName, EntryPoint = "z_hello_drop", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_hello_drop(ZMovedHello* hello);

    [DllImport(DllName, EntryPoint = "z_hello_loan", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedHello* z_hello_loan(ZOwnedHello* hello);

    [DllImport(DllName, EntryPoint = "z_hello_locators", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_hello_locators(ZLoanedHello* hello, ZOwnedStringArray* locatorsOut);

    [DllImport(DllName, EntryPoint = "z_hello_whatami", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern Whatami z_hello_whatami(ZLoanedHello* hello);

    [DllImport(DllName, EntryPoint = "z_hello_zid", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZId z_hello_zid(ZLoanedHello* hello);

    [DllImport(DllName, EntryPoint = "z_id_to_string", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_id_to_string(ZId* id, ZOwnedString* dst);

    [DllImport(DllName, EntryPoint = "z_info_peers_zid", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_info_peers_zid(ZLoanedSession* session, ZMovedClosureZid* callback);

    [DllImport(DllName, EntryPoint = "z_info_routers_zid", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_info_routers_zid(ZLoanedSession* session, ZMovedClosureZid* callback);

    [DllImport(DllName, EntryPoint = "z_info_zid", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZId z_info_zid(ZLoanedSession* session);

    [DllImport(DllName, EntryPoint = "z_internal_bytes_check", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool z_internal_bytes_check(ZOwnedBytes* b);

    [DllImport(DllName, EntryPoint = "z_internal_bytes_null", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_internal_bytes_null(ZOwnedBytes* b);

    [DllImport(DllName, EntryPoint = "z_internal_bytes_writer_check", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool z_internal_bytes_writer_check(ZOwnedBytesWriter* writer);

    [DllImport(DllName, EntryPoint = "z_internal_bytes_writer_null", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_internal_bytes_writer_null(ZOwnedBytesWriter* writer);

    [DllImport(DllName, EntryPoint = "z_internal_closure_hello_check", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool z_internal_closure_hello_check(ZOwnedClosureHello* hello);

    [DllImport(DllName, EntryPoint = "z_internal_closure_hello_null", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_internal_closure_hello_null(ZOwnedClosureHello* hello);

    [DllImport(DllName, EntryPoint = "z_internal_closure_query_check", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool z_internal_closure_query_check(ZOwnedClosureQuery* query);

    [DllImport(DllName, EntryPoint = "z_internal_closure_query_null", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_internal_closure_query_null(ZOwnedClosureQuery* query);

    [DllImport(DllName, EntryPoint = "z_internal_closure_reply_check", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool z_internal_closure_reply_check(ZOwnedClosureReply* reply);

    [DllImport(DllName, EntryPoint = "z_internal_closure_reply_null", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_internal_closure_reply_null(ZOwnedClosureReply* reply);

    [DllImport(DllName, EntryPoint = "z_internal_closure_sample_check", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool z_internal_closure_sample_check(ZOwnedClosureSample* sample);

    [DllImport(DllName, EntryPoint = "z_internal_closure_sample_null", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_internal_closure_sample_null(ZOwnedClosureSample* sample);

    [DllImport(DllName, EntryPoint = "z_internal_closure_zid_check", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool z_internal_closure_zid_check(ZOwnedClosureZid* zid);

    [DllImport(DllName, EntryPoint = "z_internal_closure_zid_null", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_internal_closure_zid_null(ZOwnedClosureZid* zid);

    [DllImport(DllName, EntryPoint = "z_internal_condvar_check", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool z_internal_condvar_check(ZOwnedCondvar* condvar);

    [DllImport(DllName, EntryPoint = "z_internal_condvar_null", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_internal_condvar_null(ZOwnedCondvar* condvar);

    [DllImport(DllName, EntryPoint = "z_internal_config_check", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool z_internal_config_check(ZOwnedConfig* config);

    [DllImport(DllName, EntryPoint = "z_internal_config_null", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_internal_config_null(ZOwnedConfig* config);

    [DllImport(DllName, EntryPoint = "z_internal_encoding_check", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool z_internal_encoding_check(ZOwnedEncoding* encoding);

    [DllImport(DllName, EntryPoint = "z_internal_encoding_null", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_internal_encoding_null(ZOwnedEncoding* encoding);

    [DllImport(DllName, EntryPoint = "z_internal_fifo_handler_query_check", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool z_internal_fifo_handler_query_check(ZOwnedFifoHandlerQuery* handler);

    [DllImport(DllName, EntryPoint = "z_internal_fifo_handler_query_null", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_internal_fifo_handler_query_null(ZOwnedFifoHandlerQuery* handler);

    [DllImport(DllName, EntryPoint = "z_internal_fifo_handler_reply_check", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool z_internal_fifo_handler_reply_check(ZOwnedFifoHandlerReply* handler);

    [DllImport(DllName, EntryPoint = "z_internal_fifo_handler_reply_null", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_internal_fifo_handler_reply_null(ZOwnedFifoHandlerReply* handler);

    [DllImport(DllName, EntryPoint = "z_internal_fifo_handler_sample_check",
        CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool z_internal_fifo_handler_sample_check(ZOwnedFifoHandlerSample* handler);

    [DllImport(DllName, EntryPoint = "z_internal_fifo_handler_sample_null", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_internal_fifo_handler_sample_null(ZOwnedFifoHandlerSample* handler);

    [DllImport(DllName, EntryPoint = "z_internal_hello_check", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool z_internal_hello_check(ZOwnedHello* hello);

    [DllImport(DllName, EntryPoint = "z_internal_hello_null", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_internal_hello_null(ZOwnedHello* hello);

    [DllImport(DllName, EntryPoint = "z_internal_keyexpr_check", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool z_internal_keyexpr_check(ZOwnedKeyexpr* keyexpr);

    [DllImport(DllName, EntryPoint = "z_internal_keyexpr_null", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_internal_keyexpr_null(ZOwnedKeyexpr* keyexpr);

    [DllImport(DllName, EntryPoint = "z_internal_liveliness_token_check", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool z_internal_liveliness_token_check(ZOwnedLivelinessToken* token);

    [DllImport(DllName, EntryPoint = "z_internal_liveliness_token_null", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_internal_liveliness_token_null(ZOwnedLivelinessToken* token);

    [DllImport(DllName, EntryPoint = "z_internal_mutex_check", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool z_internal_mutex_check(ZOwnedMutex* mutex);

    [DllImport(DllName, EntryPoint = "z_internal_mutex_null", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_internal_mutex_null(ZOwnedMutex* mutex);

    [DllImport(DllName, EntryPoint = "z_internal_publisher_check", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool z_internal_publisher_check(ZOwnedPublisher* publisher);

    [DllImport(DllName, EntryPoint = "z_internal_publisher_null", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_internal_publisher_null(ZOwnedPublisher* publisher);

    [DllImport(DllName, EntryPoint = "z_internal_query_check", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool z_internal_query_check(ZOwnedQuery* query);

    [DllImport(DllName, EntryPoint = "z_internal_query_null", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_internal_query_null(ZOwnedQuery* query);

    [DllImport(DllName, EntryPoint = "z_internal_queryable_check", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool z_internal_queryable_check(ZOwnedQueryable* queryable);

    [DllImport(DllName, EntryPoint = "z_internal_queryable_null", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_internal_queryable_null(ZOwnedQueryable* queryable);

    [DllImport(DllName, EntryPoint = "z_internal_reply_check", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool z_internal_reply_check(ZOwnedReply* reply);

    [DllImport(DllName, EntryPoint = "z_internal_reply_null", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_internal_reply_null(ZOwnedReply* reply);

    [DllImport(DllName, EntryPoint = "z_internal_reply_err_check", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool z_internal_reply_err_check(ZOwnedReplyErr* replyErr);

    [DllImport(DllName, EntryPoint = "z_internal_reply_err_null", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_internal_reply_err_null(ZOwnedReplyErr* replyErr);

    [DllImport(DllName, EntryPoint = "z_internal_ring_handler_query_check", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool z_internal_ring_handler_query_check(ZOwnedRingHandlerQuery* handler);

    [DllImport(DllName, EntryPoint = "z_internal_ring_handler_query_null", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_internal_ring_handler_query_null(ZOwnedRingHandlerQuery* handler);

    [DllImport(DllName, EntryPoint = "z_internal_ring_handler_reply_check", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool z_internal_ring_handler_reply_check(ZOwnedRingHandlerReply* handler);

    [DllImport(DllName, EntryPoint = "z_internal_ring_handler_reply_null", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_internal_ring_handler_reply_null(ZOwnedRingHandlerReply* handler);

    [DllImport(DllName, EntryPoint = "z_internal_ring_handler_sample_check",
        CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool z_internal_ring_handler_sample_check(ZOwnedRingHandlerSample* handler);

    [DllImport(DllName, EntryPoint = "z_internal_ring_handler_sample_null", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_internal_ring_handler_sample_null(ZOwnedRingHandlerSample* handler);

    [DllImport(DllName, EntryPoint = "z_internal_sample_check", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool z_internal_sample_check(ZOwnedSample* sample);

    [DllImport(DllName, EntryPoint = "z_internal_sample_null", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_internal_sample_null(ZOwnedSample* sample);

    [DllImport(DllName, EntryPoint = "z_internal_session_check", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool z_internal_session_check(ZOwnedSession* session);

    [DllImport(DllName, EntryPoint = "z_internal_session_null", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_internal_session_null(ZOwnedSession* session);

    [DllImport(DllName, EntryPoint = "z_internal_slice_check", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool z_internal_slice_check(ZOwnedSlice* slice);

    [DllImport(DllName, EntryPoint = "z_internal_slice_null", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_internal_slice_null(ZOwnedSlice* slice);

    [DllImport(DllName, EntryPoint = "z_internal_string_array_check", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool z_internal_string_array_check(ZOwnedStringArray* strings);

    [DllImport(DllName, EntryPoint = "z_internal_string_array_null", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_internal_string_array_null(ZOwnedStringArray* strings);

    [DllImport(DllName, EntryPoint = "z_internal_string_check", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool z_internal_string_check(ZOwnedString* str);

    [DllImport(DllName, EntryPoint = "z_internal_string_null", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_internal_string_null(ZOwnedString* str);

    [DllImport(DllName, EntryPoint = "z_internal_subscriber_check", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool z_internal_subscriber_check(ZOwnedSubscriber* subscriber);

    [DllImport(DllName, EntryPoint = "z_internal_subscriber_null", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_internal_subscriber_null(ZOwnedSubscriber* subscriber);

    [DllImport(DllName, EntryPoint = "z_internal_task_check", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool z_internal_task_check(ZOwnedTask* task);

    [DllImport(DllName, EntryPoint = "z_internal_task_null", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_internal_task_null(ZOwnedTask* task);

    [DllImport(DllName, EntryPoint = "z_keyexpr_as_view_string", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_keyexpr_as_view_string(ZLoanedKeyexpr* keyexpr, ZViewString* outString);

    [DllImport(DllName, EntryPoint = "z_keyexpr_canonize", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_keyexpr_canonize(byte* start, nuint* len);

    [DllImport(DllName, EntryPoint = "z_keyexpr_canonize_null_terminated", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_keyexpr_canonize_null_terminated(byte* start);

    [DllImport(DllName, EntryPoint = "z_keyexpr_clone", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_keyexpr_clone(ZOwnedKeyexpr* dst, ZLoanedKeyexpr* src);

    [DllImport(DllName, EntryPoint = "z_keyexpr_concat", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_keyexpr_concat(ZOwnedKeyexpr* keyexpr, ZLoanedKeyexpr* left, byte* rightStart,
        nuint rightLen);

    [DllImport(DllName, EntryPoint = "z_keyexpr_drop", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_keyexpr_drop(ZMovedKeyexpr* keyexpr);

    [DllImport(DllName, EntryPoint = "z_keyexpr_equals", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool z_keyexpr_equals(ZLoanedKeyexpr* left, ZLoanedKeyexpr* right);

    [DllImport(DllName, EntryPoint = "z_keyexpr_from_str", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_keyexpr_from_str(ZOwnedKeyexpr* keyexpr, byte* str);

    [DllImport(DllName, EntryPoint = "z_keyexpr_from_str_autocanonize", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_keyexpr_from_str_autocanonize(ZOwnedKeyexpr* keyexpr, byte* str);

    [DllImport(DllName, EntryPoint = "z_keyexpr_from_substr", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_keyexpr_from_substr(ZOwnedKeyexpr* keyexpr, byte* str, nuint len);

    [DllImport(DllName, EntryPoint = "z_keyexpr_from_substr_autocanonize", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_keyexpr_from_substr_autocanonize(ZOwnedKeyexpr* keyexpr, byte* str, nuint len);

    [DllImport(DllName, EntryPoint = "z_keyexpr_includes", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool z_keyexpr_includes(ZLoanedKeyexpr* left, ZLoanedKeyexpr* right);

    [DllImport(DllName, EntryPoint = "z_keyexpr_intersects", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool z_keyexpr_intersects(ZLoanedKeyexpr* left, ZLoanedKeyexpr* right);

    [DllImport(DllName, EntryPoint = "z_keyexpr_is_canon", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_keyexpr_is_canon(byte* start, nuint len);

    [DllImport(DllName, EntryPoint = "z_keyexpr_join", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_keyexpr_join(ZOwnedKeyexpr* keyexpr, ZLoanedKeyexpr* left, ZLoanedKeyexpr* right);

    [DllImport(DllName, EntryPoint = "z_keyexpr_loan", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedKeyexpr* z_keyexpr_loan(ZOwnedKeyexpr* keyexpr);

    [DllImport(DllName, EntryPoint = "z_liveliness_declare_subscriber", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_liveliness_declare_subscriber(
        ZLoanedSession* session,
        ZOwnedSubscriber* subscriber,
        ZLoanedKeyexpr* keyexpr,
        ZMovedClosureSample* callback,
        ZLivelinessSubscriberOptions* options
    );

    [DllImport(DllName, EntryPoint = "z_liveliness_declare_token", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_liveliness_declare_token(
        ZLoanedSession* session,
        ZOwnedLivelinessToken* token,
        ZLoanedKeyexpr* keyexpr,
        ZLivelinessTokenOptions* options
    );

    [DllImport(DllName, EntryPoint = "z_liveliness_get", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_liveliness_get(
        ZLoanedSession* session,
        ZLoanedKeyexpr* keyexpr,
        ZMovedClosureReply* callback,
        ZLivelinessGetOptions* options
    );

    [DllImport(DllName, EntryPoint = "z_liveliness_get_options_default", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_liveliness_get_options_default(ZLivelinessGetOptions* options);

    [DllImport(DllName, EntryPoint = "z_liveliness_subscriber_options_default",
        CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_liveliness_subscriber_options_default(ZLivelinessSubscriberOptions* options);

    [DllImport(DllName, EntryPoint = "z_liveliness_token_drop", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_liveliness_token_drop(ZMovedLivelinessToken* token);

    [DllImport(DllName, EntryPoint = "z_liveliness_token_loan", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedLivelinessToken* z_liveliness_token_loan(ZOwnedLivelinessToken* token);

    [DllImport(DllName, EntryPoint = "z_liveliness_token_options_default", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_liveliness_token_options_default(ZLivelinessTokenOptions* options);

    [DllImport(DllName, EntryPoint = "z_liveliness_undeclare_token", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_liveliness_undeclare_token(ZMovedLivelinessToken* token);

    [DllImport(DllName, EntryPoint = "z_mutex_drop", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_mutex_drop(ZMovedMutex* mutex);

    [DllImport(DllName, EntryPoint = "z_mutex_init", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_mutex_init(ZOwnedMutex* mutex);

    [DllImport(DllName, EntryPoint = "z_mutex_loan_mut", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedMutex* z_mutex_loan_mut(ZOwnedMutex* mutex);

    [DllImport(DllName, EntryPoint = "z_mutex_lock", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_mutex_lock(ZLoanedMutex* mutex);

    [DllImport(DllName, EntryPoint = "z_mutex_try_lock", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_mutex_try_lock(ZLoanedMutex* mutex);

    [DllImport(DllName, EntryPoint = "z_mutex_unlock", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_mutex_unlock(ZLoanedMutex* mutex);

    [DllImport(DllName, EntryPoint = "z_open", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_open(ZOwnedSession* session, ZMovedConfig* config, ZOpenOptions* options);

    [DllImport(DllName, EntryPoint = "z_open_options_default", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_open_options_default(ZOpenOptions* options);

    [DllImport(DllName, EntryPoint = "z_priority_default", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern Priority z_priority_default();

    [DllImport(DllName, EntryPoint = "z_publisher_delete", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_publisher_delete(ZLoanedPublisher* publisher, ZPublisherDeleteOptions* options);

    [DllImport(DllName, EntryPoint = "z_publisher_delete_options_default", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_publisher_delete_options_default(ZPublisherDeleteOptions* options);

    [DllImport(DllName, EntryPoint = "z_publisher_drop", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_publisher_drop(ZMovedPublisher* publisher);

    [DllImport(DllName, EntryPoint = "z_publisher_keyexpr", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedKeyexpr* z_publisher_keyexpr(ZLoanedPublisher* publisher);

    [DllImport(DllName, EntryPoint = "z_publisher_loan", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedPublisher* z_publisher_loan(ZOwnedPublisher* publisher);

    [DllImport(DllName, EntryPoint = "z_publisher_loan_mut", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedPublisher* z_publisher_loan_mut(ZOwnedPublisher* publisher);

    [DllImport(DllName, EntryPoint = "z_publisher_options_default", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_publisher_options_default(ZPublisherOptions* options);

    [DllImport(DllName, EntryPoint = "z_publisher_put", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_publisher_put(ZLoanedPublisher* publisher, ZMovedBytes* payload,
        ZPublisherPutOptions* options);

    [DllImport(DllName, EntryPoint = "z_publisher_put_options_default", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_publisher_put_options_default(ZPublisherPutOptions* options);

    [DllImport(DllName, EntryPoint = "z_put", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_put(
        ZLoanedSession* session,
        ZLoanedKeyexpr* keyexpr,
        ZMovedBytes* payload,
        ZPutOptions* options
    );

    [DllImport(DllName, EntryPoint = "z_put_options_default", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_put_options_default(ZPutOptions* options);

    [DllImport(DllName, EntryPoint = "z_query_attachment", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedBytes* z_query_attachment(ZLoanedQuery* query);

    [DllImport(DllName, EntryPoint = "z_query_clone", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_query_clone(ZOwnedQuery* dst, ZLoanedQuery* src);

    [DllImport(DllName, EntryPoint = "z_query_consolidation_auto", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZQueryConsolidation z_query_consolidation_auto();

    [DllImport(DllName, EntryPoint = "z_query_consolidation_default", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZQueryConsolidation z_query_consolidation_default();

    [DllImport(DllName, EntryPoint = "z_query_consolidation_latest", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZQueryConsolidation z_query_consolidation_latest();

    [DllImport(DllName, EntryPoint = "z_query_consolidation_monotonic", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZQueryConsolidation z_query_consolidation_monotonic();

    [DllImport(DllName, EntryPoint = "z_query_consolidation_none", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZQueryConsolidation z_query_consolidation_none();

    [DllImport(DllName, EntryPoint = "z_query_drop", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_query_drop(ZMovedQuery* query);

    [DllImport(DllName, EntryPoint = "z_query_encoding", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedEncoding* z_query_encoding(ZLoanedQuery* query);

    [DllImport(DllName, EntryPoint = "z_query_keyexpr", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedKeyexpr* z_query_keyexpr(ZLoanedQuery* query);

    [DllImport(DllName, EntryPoint = "z_query_loan", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedQuery* z_query_loan(ZLoanedQuery* query);

    [DllImport(DllName, EntryPoint = "z_query_parameters", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_query_parameters(ZLoanedQuery* query, ZViewString* parameters);

    [DllImport(DllName, EntryPoint = "z_query_payload", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedBytes* z_query_payload(ZLoanedQuery* query);

    [DllImport(DllName, EntryPoint = "z_query_reply", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_query_reply(
        ZLoanedQuery* query,
        ZLoanedKeyexpr* keyexpr,
        ZMovedBytes* payload,
        ZQueryReplyOptions* options
    );

    [DllImport(DllName, EntryPoint = "z_query_reply_del", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_query_reply_del(ZLoanedQuery* query, ZLoanedKeyexpr* keyexpr,
        ZQueryReplyDelOptions* options);

    [DllImport(DllName, EntryPoint = "z_query_reply_del_options_default", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_query_reply_del_options_default(ZQueryReplyDelOptions* options);

    [DllImport(DllName, EntryPoint = "z_query_reply_err", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_query_reply_err(ZLoanedQuery* query, ZMovedBytes* payload,
        ZQueryReplyErrOptions* options);

    [DllImport(DllName, EntryPoint = "z_query_reply_options_default", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_query_reply_options_default(ZQueryReplyOptions* options);

    [DllImport(DllName, EntryPoint = "z_query_target_default", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern QueryTarget z_query_target_default();

    [DllImport(DllName, EntryPoint = "z_queryable_drop", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_queryable_drop(ZMovedQueryable* queryable);

    [DllImport(DllName, EntryPoint = "z_queryable_loan", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedQueryable* z_queryable_loan(ZOwnedQueryable* queryable);

    [DllImport(DllName, EntryPoint = "z_queryable_options_default", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_queryable_options_default(ZQueryableOptions* options);

    [DllImport(DllName, EntryPoint = "z_random_fill", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_random_fill(byte* buf, nuint len);

    [DllImport(DllName, EntryPoint = "z_random_u8", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern byte z_random_u8();

    [DllImport(DllName, EntryPoint = "z_random_u16", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.U2)]
    internal static extern ushort z_random_u16();

    [DllImport(DllName, EntryPoint = "z_random_u32", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.U4)]
    internal static extern uint z_random_u32();

    [DllImport(DllName, EntryPoint = "z_random_u64", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.U8)]
    internal static extern ulong z_random_u64();

    [DllImport(DllName, EntryPoint = "z_reply_clone", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_reply_clone(ZOwnedReply* dst, ZLoanedReply* src);

    [DllImport(DllName, EntryPoint = "z_reply_drop", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_reply_drop(ZMovedReply* reply);

    [DllImport(DllName, EntryPoint = "z_reply_err", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedReplyErr* z_reply_err(ZLoanedReply* reply);

    [DllImport(DllName, EntryPoint = "z_reply_err_clone", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_reply_err_clone(ZOwnedReplyErr* dst, ZLoanedReplyErr* src);

    [DllImport(DllName, EntryPoint = "z_reply_err_drop", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_reply_err_drop(ZMovedReplyErr* replyErr);

    [DllImport(DllName, EntryPoint = "z_reply_err_encoding", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedEncoding* z_reply_err_encoding(ZLoanedReplyErr* replyErr);

    [DllImport(DllName, EntryPoint = "z_reply_err_loan", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedReplyErr* z_reply_err_loan(ZOwnedReplyErr* replyErr);

    [DllImport(DllName, EntryPoint = "z_reply_err_payload", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedBytes* z_reply_err_payload(ZLoanedReplyErr* replyErr);

    [DllImport(DllName, EntryPoint = "z_reply_is_ok", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool z_reply_is_ok(ZLoanedReply* reply);

    [DllImport(DllName, EntryPoint = "z_reply_loan", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedReply* z_reply_loan(ZOwnedReply* reply);

    [DllImport(DllName, EntryPoint = "z_reply_ok", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedSample* z_reply_ok(ZLoanedReply* reply);

    [DllImport(DllName, EntryPoint = "z_ring_channel_query_new", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_ring_channel_query_new(
        ZOwnedClosureQuery* callback,
        ZOwnedRingHandlerQuery* handler,
        nuint capacity
    );

    [DllImport(DllName, EntryPoint = "z_ring_channel_reply_new", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_ring_channel_reply_new(
        ZOwnedClosureReply* callback,
        ZOwnedRingHandlerReply* handler,
        nuint capacity
    );

    [DllImport(DllName, EntryPoint = "z_ring_channel_sample_new", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_ring_channel_sample_new(
        ZOwnedClosureSample* callback,
        ZOwnedRingHandlerSample* handler,
        nuint capacity
    );

    [DllImport(DllName, EntryPoint = "z_ring_handler_query_drop", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_ring_handler_query_drop(ZMovedRingHandlerQuery* handler);

    [DllImport(DllName, EntryPoint = "z_ring_handler_query_loan", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedRingHandlerQuery* z_ring_handler_query_loan(ZOwnedRingHandlerQuery* handler);

    [DllImport(DllName, EntryPoint = "z_ring_handler_query_recv", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_ring_handler_query_recv(ZLoanedRingHandlerQuery* handler, ZOwnedQuery* query);

    [DllImport(DllName, EntryPoint = "z_ring_handler_query_try_recv", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_ring_handler_query_try_recv(ZLoanedRingHandlerQuery* handler, ZOwnedQuery* query);

    [DllImport(DllName, EntryPoint = "z_ring_handler_reply_drop", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_ring_handler_reply_drop(ZMovedRingHandlerReply* handler);

    [DllImport(DllName, EntryPoint = "z_ring_handler_reply_loan", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedRingHandlerReply* z_ring_handler_reply_loan(ZOwnedRingHandlerReply* handler);

    [DllImport(DllName, EntryPoint = "z_ring_handler_reply_recv", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_ring_handler_reply_recv(ZLoanedRingHandlerReply* handler, ZOwnedReply* reply);

    [DllImport(DllName, EntryPoint = "z_ring_handler_reply_try_recv", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_ring_handler_reply_try_recv(ZLoanedRingHandlerReply* handler, ZOwnedReply* reply);

    [DllImport(DllName, EntryPoint = "z_ring_handler_sample_drop", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_ring_handler_sample_drop(ZMovedRingHandlerSample* handler);

    [DllImport(DllName, EntryPoint = "z_ring_handler_sample_loan", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedRingHandlerSample* z_ring_handler_sample_loan(ZOwnedRingHandlerSample* handler);

    [DllImport(DllName, EntryPoint = "z_ring_handler_sample_recv", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_ring_handler_sample_recv(ZLoanedRingHandlerSample* handler, ZOwnedSample* sample);

    [DllImport(DllName, EntryPoint = "z_ring_handler_sample_try_recv", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_ring_handler_sample_try_recv(ZLoanedRingHandlerSample* handler,
        ZOwnedSample* sample);

    [DllImport(DllName, EntryPoint = "z_sample_attachment", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedBytes* z_sample_attachment(ZLoanedSample* sample);

    [DllImport(DllName, EntryPoint = "z_sample_clone", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_sample_clone(ZLoanedSample* sample);

    [DllImport(DllName, EntryPoint = "z_sample_drop", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_sample_drop(ZMovedSample* sample);

    [DllImport(DllName, EntryPoint = "z_sample_congestion_control", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern CongestionControl z_sample_congestion_control(ZLoanedSample* sample);
}