// #pragma warning disable CS8500

// 从 NET7 开始, 已支持 LibraryImportAttribute
// 但是考虑到兼容性, 还是使用 DllImportAttribute

// 从 NET6 开始, 管理非托管内存已支持 NativeMemory Class
// 但是考虑到兼容性, 还是使用 Marshal Class 中的函数

// Marshal.GetFunctionPointerForDelegate 
// 将委托转换为可从非托管代码调用的函数指针

// Marshal.GetDelegateForFunctionPointer
// 将非托管函数指针转换为委托

// Marshal.StructureToPtr
// 将数据从托管内存对象打包到非托管内存中

// Marshal.PtrToStructure
// 将数据从非托管内存打包到托管内存对象中

using System.Runtime.InteropServices;

namespace Zenoh;

// zenoh_commons.h
// src/result.rs
/// z_result_t
public enum ZResult : sbyte
{
    Ok = 0,
    ChannelDisconnected = 1,
    ChannelNodata = 2,
    ErrorInvalidArgument = -1,
    ErrorParse = -2,
    ErrorIo = -3,
    ErrorNetwork = -4,
    ErrorNull = -5,
    ErrorUnavailable = -6,
    ErrorDeserialize = -7,
    ErrorSessionClosed = -8,
    ErrorUtf8 = -9,
    ErrorResourceTemporarilyUnavailableMutex = -11,
    ErrorDeviceOrResourceBusyMutex = -16,
    ErrorInvalidArgumentMutex = -22,
    ErrorGeneric = -128,
}

// zenoh_commons.h
/// z_congestion_control_t
public enum ZCongestionControl : uint
{
    Block = 0,
    Drop = 1
}

// zenoh_commons.h
// z_sample_kind_t
public enum ZSampleKind : uint
{
    Put = 0,
    Delete = 1
}

// zenoh_commons.h
// z_priority_t
public enum ZPriority : uint
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
public enum ZConsolidationMode
{
    Auto = -1,
    None = 0,
    Monotonic = 1,
    Latest = 2
}

// z_reliability_t
public enum ZReliability : uint
{
    BestEffort,
    Reliable
}

// zenoh_commons.h
// z_query_target_t 
public enum ZQueryTarget : uint
{
    BestMatching = 0,
    All = 1,
    AllComplete = 2
}

// zenoh_commons.h
// z_what_t
public enum ZWhat : uint
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
public enum ZWhatami : uint
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

// zenoh_commons.h
// z_bytes_slice_iterator_t
[StructLayout(LayoutKind.Sequential, Pack = 8)]
internal unsafe struct ZBytesSliceIterator
{
    private fixed byte data[24];
}

// zenoh_opaque.h
// z_owned_string_t
[StructLayout(LayoutKind.Sequential, Pack = 8)]
internal unsafe struct ZOwnedString
{
    private fixed byte data[32];
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
    internal ZWhat ZWhat;
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
internal struct ZPublisherOptions
{
    // z_moved_encoding_t*
    internal nint encoding;
    internal ZCongestionControl ZCongestionControl;
    internal ZPriority ZPriority;
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
    internal ZCongestionControl ZCongestionControl;
    internal ZPriority ZPriority;
    [MarshalAs(UnmanagedType.U1)] internal bool is_express;
    internal ZTimestamp* timestamp;
}

// zenoh_commons.h
// z_query_consolidation_t 
[StructLayout(LayoutKind.Sequential)]
internal struct ZQueryConsolidation
{
    internal ZConsolidationMode mode;
}

// zenoh_commons.h
// z_get_options_t 
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct ZGetOptions
{
    internal ZQueryTarget target;
    internal ZQueryConsolidation consolidation;
    internal ZMovedBytes* payload;
    internal ZMovedEncoding* encoding;
    internal ZCongestionControl ZCongestionControl;
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
    internal ZCongestionControl ZCongestionControl;
    internal ZPriority ZPriority;
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
    internal ZCongestionControl ZCongestionControl;
    internal ZPriority ZPriority;
    [MarshalAs(UnmanagedType.U1)] internal bool is_express;
    internal ZTimestamp* timestamp;
    internal ZMovedBytes* attachment;
}

// zenoh_commons.h
// z_query_reply_del_options_t 
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct ZQueryReplyDelOptions
{
    internal ZCongestionControl ZCongestionControl;
    internal ZPriority ZPriority;
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

// zenoh_commons.h
// zc_owned_closure_log_t
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct ZcOwnedClosureLog
{
    internal void* context;
    internal delegate*<ZcLogSeverity, ZLoanedString*, void*, void> call;
    internal delegate*<void*, void> drop;
}

// zenoh_commons.h
// zc_moved_closure_log_t
[StructLayout(LayoutKind.Sequential)]
internal struct ZcMovedClosureLog
{
    internal ZcOwnedClosureLog owned_closure_log;
}

// zenoh_opaque.h
// zc_loaned_closure_log_t
[StructLayout(LayoutKind.Sequential, Pack = 8)]
internal unsafe struct ZcLoanedClosureLog
{
    private fixed nuint data[3];
}

// zenoh_commons.h
// zc_internal_encoding_data_t
[StructLayout(LayoutKind.Sequential)]
internal struct ZcInternalEncodingData
{
    internal ushort id;
    internal nint schema_ptr;
    internal nuint schema_len;
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
    internal static uint Router = 1;
    internal static uint Peer = 2;
    internal static uint Client = 4;
    internal static string ConfigModeKey = "mode";
    internal static string ConfigConnectKey = "connect/endpoints";
    internal static string ConfigListenKey = "listen/endpoints";
    internal static string ConfigUserKey = "transport/auth/usrpwd/user";
    internal static string ConfigPasswordKey = "transport/auth/usrpwd/password";
    internal static string ConfigMulticastScoutingKey = "scouting/multicast/enabled";
    internal static string ConfigMulticastInterfaceKey = "scouting/multicast/interface";
    internal static string ConfigMulticastIpv4AddressKey = "scouting/multicast/address";
    internal static string ConfigScoutingTimeoutKey = "scouting/timeout";
    internal static string ConfigScoutingDelayKey = "scouting/delay";
    internal static string ConfigAddTimestampKey = "timestamping/enabled";

    /// void
    /// z_bytes_clone(
    ///      struct z_owned_bytes_t *dst,
    ///      const struct z_loaned_bytes_t *this_
    /// )
    [DllImport(DllName, EntryPoint = "z_bytes_clone", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_bytes_clone(nint dst, nint src);

    /// z_result_t
    /// z_bytes_copy_from_buf(
    ///      struct z_owned_bytes_t *this_,
    ///      const uint8_t *data,
    ///      size_t len
    /// )
    [DllImport(DllName, EntryPoint = "z_bytes_copy_from_buf", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_bytes_copy_from_buf(nint dst, nint src, nuint len);

    /// void
    /// z_bytes_copy_from_slice(
    ///      struct z_owned_bytes_t *this_,
    ///      const struct z_loaned_slice_t *slice
    /// )
    [DllImport(DllName, EntryPoint = "z_bytes_copy_from_slice", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_bytes_copy_from_slice(nint dst, nint src);

    /// z_result_t
    /// z_bytes_copy_from_str(
    ///     struct z_owned_bytes_t *this_,
    ///     const char *str
    /// )
    [DllImport(DllName, EntryPoint = "z_bytes_copy_from_str", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_bytes_copy_from_str(nint dst, nint src);

    /// void
    /// z_bytes_copy_from_string(
    ///     struct z_owned_bytes_t *this_,
    ///     const struct z_loaned_string_t *str
    /// )
    [DllImport(DllName, EntryPoint = "z_bytes_copy_from_string", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_bytes_copy_from_string(nint dst, nint src);

    /// void
    /// z_bytes_drop(
    ///     struct z_moved_bytes_t *this_
    /// )
    [DllImport(DllName, EntryPoint = "z_bytes_drop", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    internal static extern void z_bytes_drop(nint bytes);

    /// void
    /// z_bytes_empty(
    ///     struct z_owned_bytes_t *this_
    /// )
    [DllImport(DllName, EntryPoint = "z_bytes_empty", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_bytes_empty(nint bytes);

    /// z_result_t
    /// z_bytes_from_buf(
    ///     struct z_owned_bytes_t *this_,
    ///     uint8_t *data,
    ///     size_t len,
    ///     void (*deleter)(void *data, void *context),
    ///     void *context
    /// );
    [DllImport(DllName, EntryPoint = "z_bytes_from_buf", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_bytes_from_buf(nint dst, nint src, nuint len, nint deleter, nint context);

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

    /// bool
    /// z_bytes_is_empty(
    ///     const struct z_loaned_bytes_t *this_
    /// )
    [DllImport(DllName, EntryPoint = "z_bytes_is_empty", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool z_bytes_is_empty(nint data);

    /// size_t
    /// z_bytes_len(
    ///     const struct z_loaned_bytes_t *this_
    /// )
    [DllImport(DllName, EntryPoint = "z_bytes_len", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    internal static extern nuint z_bytes_len(nint data);

    /// const struct z_loaned_bytes_t*
    /// z_bytes_loan(
    ///     const struct z_owned_bytes_t *this_
    /// )
    [DllImport(DllName, EntryPoint = "z_bytes_loan", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    internal static extern nint z_bytes_loan(nint data);

    /// struct z_loaned_bytes_t*
    /// z_bytes_loan_mut(
    ///     struct z_owned_bytes_t *this_
    /// )
    [DllImport(DllName, EntryPoint = "z_bytes_loan_mut", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern nint z_bytes_loan_mut(nint data);

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

    /// void
    /// z_config_clone(
    ///     struct z_owned_config_t *dst,
    ///     const struct z_loaned_config_t *this_
    /// )
    [DllImport(DllName, EntryPoint = "z_config_clone", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_config_clone(nint dst, nint src);

    /// z_result_t
    /// z_config_default(
    ///     struct z_owned_config_t *this_
    /// )
    [DllImport(DllName, EntryPoint = "z_config_default", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_config_default(nint config);

    /// void
    /// z_config_drop(
    ///     struct z_moved_config_t *this_
    /// )
    [DllImport(DllName, EntryPoint = "z_config_drop", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_config_drop(nint config);

    /// const struct z_loaned_config_t*
    /// z_config_loan(
    ///     const struct z_owned_config_t *this_
    /// )
    [DllImport(DllName, EntryPoint = "z_config_loan", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern nint z_config_loan(nint config);

    /// struct z_loaned_config_t*
    /// z_config_loan_mut(
    ///     struct z_owned_config_t *this_
    /// )
    [DllImport(DllName, EntryPoint = "z_config_loan_mut", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern nint z_config_loan_mut(nint config);

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

    /// const struct z_loaned_encoding_t*
    /// z_encoding_application_cbor(
    ///     void
    /// )
    [DllImport(DllName, EntryPoint = "z_encoding_application_cbor", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern nint z_encoding_application_cbor();

    /// const struct z_loaned_encoding_t*
    /// z_encoding_application_cdr(
    ///     void
    /// )
    [DllImport(DllName, EntryPoint = "z_encoding_application_cdr", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern nint z_encoding_application_cdr();

    /// const struct z_loaned_encoding_t*
    /// z_encoding_application_coap_payload(
    ///     void
    /// )
    [DllImport(DllName, EntryPoint = "z_encoding_application_coap_payload", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern nint z_encoding_application_coap_payload();

    /// const struct z_loaned_encoding_t*
    /// z_encoding_application_java_serialized_object(
    ///     void
    /// )
    [DllImport(DllName, EntryPoint = "z_encoding_application_java_serialized_object",
        CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern nint z_encoding_application_java_serialized_object();

    /// const struct z_loaned_encoding_t*
    /// z_encoding_application_json(
    ///     void
    /// )
    [DllImport(DllName, EntryPoint = "z_encoding_application_json", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern nint z_encoding_application_json();

    /// const struct z_loaned_encoding_t*
    /// z_encoding_application_json_patch_json(
    ///     void
    /// )
    [DllImport(DllName, EntryPoint = "z_encoding_application_json_patch_json",
        CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern nint z_encoding_application_json_patch_json();

    /// const struct z_loaned_encoding_t*
    /// z_encoding_application_json_seq(
    ///     void
    /// )
    [DllImport(DllName, EntryPoint = "z_encoding_application_json_seq", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern nint z_encoding_application_json_seq();

    /// const struct z_loaned_encoding_t*
    /// z_encoding_application_jsonpath(
    ///     void
    /// )
    [DllImport(DllName, EntryPoint = "z_encoding_application_jsonpath", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern nint z_encoding_application_jsonpath();

    /// const struct z_loaned_encoding_t*
    /// z_encoding_application_jwt(
    ///     void
    /// )
    [DllImport(DllName, EntryPoint = "z_encoding_application_jwt", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern nint z_encoding_application_jwt();

    /// const struct z_loaned_encoding_t*
    /// z_encoding_application_mp4(
    ///     void
    /// )
    [DllImport(DllName, EntryPoint = "z_encoding_application_mp4", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern nint z_encoding_application_mp4();

    /// const struct z_loaned_encoding_t*
    /// z_encoding_application_octet_stream(
    ///     void
    /// )
    [DllImport(DllName, EntryPoint = "z_encoding_application_octet_stream", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern nint z_encoding_application_octet_stream();

    /// const struct z_loaned_encoding_t*
    /// z_encoding_application_openmetrics_text(
    ///     void
    /// )
    [DllImport(DllName, EntryPoint = "z_encoding_application_openmetrics_text",
        CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern nint z_encoding_application_openmetrics_text();

    /// const struct z_loaned_encoding_t*
    /// z_encoding_application_protobuf(
    ///     void
    /// )
    [DllImport(DllName, EntryPoint = "z_encoding_application_protobuf", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern nint z_encoding_application_protobuf();

    /// const struct z_loaned_encoding_t*
    /// z_encoding_application_python_serialized_object(
    ///     void
    /// )
    [DllImport(DllName, EntryPoint = "z_encoding_application_python_serialized_object",
        CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern nint z_encoding_application_python_serialized_object();

    /// const struct z_loaned_encoding_t*
    /// z_encoding_application_soap_xml(
    ///     void
    /// )
    [DllImport(DllName, EntryPoint = "z_encoding_application_soap_xml", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern nint z_encoding_application_soap_xml();

    /// const struct z_loaned_encoding_t*
    /// z_encoding_application_sql(
    ///     void
    /// )
    [DllImport(DllName, EntryPoint = "z_encoding_application_sql", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern nint z_encoding_application_sql();

    /// const struct z_loaned_encoding_t*
    /// z_encoding_application_x_www_form_urlencoded(
    ///     void
    /// )
    [DllImport(DllName, EntryPoint = "z_encoding_application_x_www_form_urlencoded",
        CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern nint z_encoding_application_x_www_form_urlencoded();

    /// const struct z_loaned_encoding_t*
    /// z_encoding_application_xml(
    ///     void
    /// )
    [DllImport(DllName, EntryPoint = "z_encoding_application_xml", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern nint z_encoding_application_xml();

    /// const struct z_loaned_encoding_t*
    /// z_encoding_application_yaml(
    ///     void
    /// )
    [DllImport(DllName, EntryPoint = "z_encoding_application_yaml", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern nint z_encoding_application_yaml();

    /// const struct z_loaned_encoding_t*
    /// z_encoding_application_yang(
    ///     void
    /// )
    [DllImport(DllName, EntryPoint = "z_encoding_application_yang", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern nint z_encoding_application_yang();

    /// const struct z_loaned_encoding_t*
    /// z_encoding_audio_aac(
    ///     void
    /// )
    [DllImport(DllName, EntryPoint = "z_encoding_audio_aac", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern nint z_encoding_audio_aac();

    /// const struct z_loaned_encoding_t*
    /// z_encoding_audio_flac(
    ///     void
    /// )
    [DllImport(DllName, EntryPoint = "z_encoding_audio_flac", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern nint z_encoding_audio_flac();

    /// const struct z_loaned_encoding_t*
    /// z_encoding_audio_mp4(
    ///     void
    /// )
    [DllImport(DllName, EntryPoint = "z_encoding_audio_mp4", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern nint z_encoding_audio_mp4();

    /// const struct z_loaned_encoding_t*
    /// z_encoding_audio_ogg(
    ///     void
    /// )
    [DllImport(DllName, EntryPoint = "z_encoding_audio_ogg", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern nint z_encoding_audio_ogg();

    /// const struct z_loaned_encoding_t*
    /// z_encoding_audio_vorbis(
    ///     void
    /// )
    [DllImport(DllName, EntryPoint = "z_encoding_audio_vorbis", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern nint z_encoding_audio_vorbis();

    /// const struct z_loaned_encoding_t*
    /// z_encoding_image_bmp(
    ///     void
    /// )
    [DllImport(DllName, EntryPoint = "z_encoding_image_bmp", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern nint z_encoding_image_bmp();

    /// const struct z_loaned_encoding_t*
    /// z_encoding_image_gif(
    ///     void
    /// )
    [DllImport(DllName, EntryPoint = "z_encoding_image_gif", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern nint z_encoding_image_gif();

    /// const struct z_loaned_encoding_t*
    /// z_encoding_image_jpeg(
    ///     void
    /// )
    [DllImport(DllName, EntryPoint = "z_encoding_image_jpeg", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern nint z_encoding_image_jpeg();

    /// const struct z_loaned_encoding_t*
    /// z_encoding_image_png(
    ///     void
    /// )
    [DllImport(DllName, EntryPoint = "z_encoding_image_png", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern nint z_encoding_image_png();

    /// const struct z_loaned_encoding_t*
    /// z_encoding_image_webp(
    ///     void
    /// )
    [DllImport(DllName, EntryPoint = "z_encoding_image_webp", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern nint z_encoding_image_webp();

    /// const struct z_loaned_encoding_t*
    /// z_encoding_text_css(
    ///     void
    /// )
    [DllImport(DllName, EntryPoint = "z_encoding_text_css", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern nint z_encoding_text_css();

    /// const struct z_loaned_encoding_t*
    /// z_encoding_text_csv(
    ///     void
    /// )
    [DllImport(DllName, EntryPoint = "z_encoding_text_csv", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern nint z_encoding_text_csv();

    /// const struct z_loaned_encoding_t*
    /// z_encoding_text_html(
    ///     void
    /// )
    [DllImport(DllName, EntryPoint = "z_encoding_text_html", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern nint z_encoding_text_html();

    /// const struct z_loaned_encoding_t*
    /// z_encoding_text_javascript(
    ///     void
    /// )
    [DllImport(DllName, EntryPoint = "z_encoding_text_javascript", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern nint z_encoding_text_javascript();

    /// const struct z_loaned_encoding_t*
    /// z_encoding_text_json(
    ///     void
    /// )
    [DllImport(DllName, EntryPoint = "z_encoding_text_json", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern nint z_encoding_text_json();

    /// const struct z_loaned_encoding_t*
    /// z_encoding_text_json5(
    ///     void
    /// )
    [DllImport(DllName, EntryPoint = "z_encoding_text_json5", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern nint z_encoding_text_json5();

    /// const struct z_loaned_encoding_t*
    /// z_encoding_text_markdown(
    ///     void
    /// )
    [DllImport(DllName, EntryPoint = "z_encoding_text_markdown", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern nint z_encoding_text_markdown();

    /// const struct z_loaned_encoding_t*
    /// z_encoding_text_plain(
    ///     void
    /// )
    [DllImport(DllName, EntryPoint = "z_encoding_text_plain", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern nint z_encoding_text_plain();

    /// const struct z_loaned_encoding_t*
    /// z_encoding_text_xml(
    ///     void
    /// )
    [DllImport(DllName, EntryPoint = "z_encoding_text_xml", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern nint z_encoding_text_xml();

    /// const struct z_loaned_encoding_t*
    /// z_encoding_text_yaml(
    ///     void
    /// )
    [DllImport(DllName, EntryPoint = "z_encoding_text_yaml", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern nint z_encoding_text_yaml();

    /// const struct z_loaned_encoding_t*
    /// z_encoding_video_h261(
    ///     void
    /// )
    [DllImport(DllName, EntryPoint = "z_encoding_video_h261", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern nint z_encoding_video_h261();

    /// const struct z_loaned_encoding_t*
    /// z_encoding_video_h263(
    ///     void
    /// )
    [DllImport(DllName, EntryPoint = "z_encoding_video_h263", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern nint z_encoding_video_h263();

    /// const struct z_loaned_encoding_t*
    /// z_encoding_video_h264(
    ///     void
    /// )
    [DllImport(DllName, EntryPoint = "z_encoding_video_h264", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern nint z_encoding_video_h264();

    /// const struct z_loaned_encoding_t*
    /// z_encoding_video_h265(
    ///     void
    /// )
    [DllImport(DllName, EntryPoint = "z_encoding_video_h265", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern nint z_encoding_video_h265();

    /// const struct z_loaned_encoding_t*
    /// z_encoding_video_h266(
    ///     void
    /// )
    [DllImport(DllName, EntryPoint = "z_encoding_video_h266", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern nint z_encoding_video_h266();

    /// const struct z_loaned_encoding_t*
    /// z_encoding_video_mp4(
    ///     void
    /// )
    [DllImport(DllName, EntryPoint = "z_encoding_video_mp4", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern nint z_encoding_video_mp4();

    /// const struct z_loaned_encoding_t*
    /// z_encoding_video_ogg(
    ///     void
    /// )
    [DllImport(DllName, EntryPoint = "z_encoding_video_ogg", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern nint z_encoding_video_ogg();

    /// const struct z_loaned_encoding_t*
    /// z_encoding_video_raw(
    ///     void
    /// )
    [DllImport(DllName, EntryPoint = "z_encoding_video_raw", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern nint z_encoding_video_raw();

    /// const struct z_loaned_encoding_t *
    /// z_encoding_video_vp8(
    ///     void
    /// )
    [DllImport(DllName, EntryPoint = "z_encoding_video_vp8", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern nint z_encoding_video_vp8();

    /// const struct z_loaned_encoding_t *
    /// z_encoding_video_vp9(
    ///     void
    /// )
    [DllImport(DllName, EntryPoint = "z_encoding_video_vp9", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern nint z_encoding_video_vp9();

    /// const struct z_loaned_encoding_t*
    /// z_encoding_zenoh_bytes(
    ///     void
    /// )
    [DllImport(DllName, EntryPoint = "z_encoding_zenoh_bytes", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern nint z_encoding_zenoh_bytes();

    /// const struct z_loaned_encoding_t*
    /// z_encoding_zenoh_serialized(
    ///     void
    /// )
    [DllImport(DllName, EntryPoint = "z_encoding_zenoh_serialized", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern nint z_encoding_zenoh_serialized();

    /// const struct z_loaned_encoding_t*
    /// z_encoding_zenoh_string(
    ///     void
    /// )
    [DllImport(DllName, EntryPoint = "z_encoding_zenoh_string", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern nint z_encoding_zenoh_string();

    /// void
    /// z_encoding_clone(
    ///     struct z_owned_encoding_t *dst,
    ///     const struct z_loaned_encoding_t *this_
    /// )
    [DllImport(DllName, EntryPoint = "z_encoding_clone", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_encoding_clone(nint dst, nint src);

    /// void
    /// z_encoding_drop(
    ///     struct z_moved_encoding_t *this_
    /// )
    [DllImport(DllName, EntryPoint = "z_encoding_drop", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_encoding_drop(nint encoding);

    /// bool
    /// z_encoding_equals(
    ///     const struct z_loaned_encoding_t *this_,
    ///     const struct z_loaned_encoding_t *other
    /// )
    [DllImport(DllName, EntryPoint = "z_encoding_equals", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool z_encoding_equals(nint encoding, nint other);

    [DllImport(DllName, EntryPoint = "z_encoding_from_str", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_encoding_from_str(ZOwnedEncoding* encoding, byte* s);

    [DllImport(DllName, EntryPoint = "z_encoding_from_substr", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_encoding_from_substr(ZOwnedEncoding* encoding, byte* s, nuint len);

    /// const struct z_loaned_encoding_t*
    /// z_encoding_loan_default(
    ///     void
    /// )
    [DllImport(DllName, EntryPoint = "z_encoding_loan_default", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern nint z_encoding_loan_default();

    /// const struct z_loaned_encoding_t*
    /// z_encoding_loan(
    ///     const struct z_owned_encoding_t *this_
    /// )
    [DllImport(DllName, EntryPoint = "z_encoding_loan", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern nint z_encoding_loan(nint encoding);

    /// struct z_loaned_encoding_t*
    /// z_encoding_loan_mut(
    ///     struct z_owned_encoding_t *this_
    /// )
    [DllImport(DllName, EntryPoint = "z_encoding_loan_mut", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern nint z_encoding_loan_mut(nint encoding);

    /// z_result_t
    /// z_encoding_set_schema_from_str(
    ///     struct z_loaned_encoding_t *this_,
    ///     const char *s
    /// )
    [DllImport(DllName, EntryPoint = "z_encoding_set_schema_from_str", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_encoding_set_schema_from_str(nint encoding, nint s);

    /// z_result_t
    /// z_encoding_set_schema_from_substr(
    ///     struct z_loaned_encoding_t *this_,
    ///     const char *s,
    ///     size_t len
    /// )
    [DllImport(DllName, EntryPoint = "z_encoding_set_schema_from_substr", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_encoding_set_schema_from_substr(nint encoding, nint s, nuint len);

    /// void
    /// z_encoding_to_string(
    ///     const struct z_loaned_encoding_t *this_,
    ///     struct z_owned_string_t *out_str
    /// )
    [DllImport(DllName, EntryPoint = "z_encoding_to_string", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_encoding_to_string(nint encoding, nint outStr);

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
    internal static extern ZWhatami z_hello_whatami(ZLoanedHello* hello);

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

    /// bool
    /// z_internal_config_check(
    ///     const struct z_owned_config_t *this_
    /// )
    [DllImport(DllName, EntryPoint = "z_internal_config_check", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool z_internal_config_check(nint config);

    /// void
    /// z_internal_config_null(
    ///     struct z_owned_config_t *this_
    /// )
    [DllImport(DllName, EntryPoint = "z_internal_config_null", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_internal_config_null(nint config);

    /// bool
    /// z_internal_encoding_check(
    ///     const struct z_owned_encoding_t *this_
    /// )
    [DllImport(DllName, EntryPoint = "z_internal_encoding_check", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool z_internal_encoding_check(nint encoding);

    /// void
    /// z_internal_encoding_null(
    ///     struct z_owned_encoding_t *this_
    /// )
    [DllImport(DllName, EntryPoint = "z_internal_encoding_null", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_internal_encoding_null(nint encoding);

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

    /// bool
    /// z_internal_keyexpr_check(
    ///     const struct z_owned_keyexpr_t *this_
    /// )
    [DllImport(DllName, EntryPoint = "z_internal_keyexpr_check", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool z_internal_keyexpr_check(nint keyexpr);

    /// void
    /// z_internal_keyexpr_null(
    ///     struct z_owned_keyexpr_t *this_
    /// )
    [DllImport(DllName, EntryPoint = "z_internal_keyexpr_null", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_internal_keyexpr_null(nint keyexpr);

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

    /// bool
    /// z_internal_session_check(
    ///     const struct z_owned_session_t *this_
    /// )
    [DllImport(DllName, EntryPoint = "z_internal_session_check", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool z_internal_session_check(nint session);

    /// void
    /// z_internal_session_null(
    ///     struct z_owned_session_t *this_
    /// )
    [DllImport(DllName, EntryPoint = "z_internal_session_null", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_internal_session_null(nint session);

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

    /// void
    /// z_keyexpr_as_view_string(
    ///     const struct z_loaned_keyexpr_t *this_,
    ///     struct z_view_string_t *out_string
    /// )
    [DllImport(DllName, EntryPoint = "z_keyexpr_as_view_string", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_keyexpr_as_view_string(nint keyexpr, nint outString);

    /// z_result_t
    /// z_keyexpr_canonize(
    ///     char *start,
    ///     size_t *len
    /// )
    [DllImport(DllName, EntryPoint = "z_keyexpr_canonize", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_keyexpr_canonize(nint start, nint len);

    /// z_result_t
    /// z_keyexpr_canonize_null_terminated(
    ///     char *start
    /// )
    [DllImport(DllName, EntryPoint = "z_keyexpr_canonize_null_terminated", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_keyexpr_canonize_null_terminated(nint start);

    /// void
    /// z_keyexpr_clone(
    ///     struct z_owned_keyexpr_t *dst,
    ///     const struct z_loaned_keyexpr_t *this_
    /// )
    [DllImport(DllName, EntryPoint = "z_keyexpr_clone", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_keyexpr_clone(nint dst, nint src);

    /// z_result_t
    /// z_keyexpr_concat(
    ///     struct z_owned_keyexpr_t *this_,
    ///     const struct z_loaned_keyexpr_t *left,
    ///     const char *right_start,
    ///     size_t right_len
    /// )
    [DllImport(DllName, EntryPoint = "z_keyexpr_concat", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_keyexpr_concat(nint keyexpr, nint left, nint rightStart, nuint rightLen);

    /// void
    /// z_keyexpr_drop(
    ///     struct z_moved_keyexpr_t *this_
    /// )
    [DllImport(DllName, EntryPoint = "z_keyexpr_drop", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_keyexpr_drop(nint keyexpr);

    /// bool
    /// z_keyexpr_equals(
    ///     const struct z_loaned_keyexpr_t *left,
    ///     const struct z_loaned_keyexpr_t *right
    /// )
    [DllImport(DllName, EntryPoint = "z_keyexpr_equals", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool z_keyexpr_equals(nint left, nint right);

    /// z_result_t
    /// z_keyexpr_from_str(
    ///     struct z_owned_keyexpr_t *this_,
    ///     const char *expr
    /// )
    [DllImport(DllName, EntryPoint = "z_keyexpr_from_str", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_keyexpr_from_str(nint keyexpr, nint str);

    /// z_result_t
    /// z_keyexpr_from_str_autocanonize(
    ///     struct z_owned_keyexpr_t *this_,
    ///     const char *expr
    /// )
    [DllImport(DllName, EntryPoint = "z_keyexpr_from_str_autocanonize", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_keyexpr_from_str_autocanonize(nint keyexpr, nint str);

    /// z_result_t
    /// z_keyexpr_from_substr(
    ///     struct z_owned_keyexpr_t *this_,
    ///     const char *expr,
    ///     size_t len
    /// )
    [DllImport(DllName, EntryPoint = "z_keyexpr_from_substr", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_keyexpr_from_substr(nint keyexpr, nint str, nuint len);

    /// z_result_t
    /// z_keyexpr_from_substr_autocanonize(
    ///     struct z_owned_keyexpr_t *this_,
    ///     const char *start,
    ///     size_t *len
    /// )
    [DllImport(DllName, EntryPoint = "z_keyexpr_from_substr_autocanonize", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_keyexpr_from_substr_autocanonize(nint keyexpr, nint str, nuint len);

    /// bool
    /// z_keyexpr_includes(
    ///     const struct z_loaned_keyexpr_t *left,
    ///     const struct z_loaned_keyexpr_t *right
    /// )
    [DllImport(DllName, EntryPoint = "z_keyexpr_includes", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool z_keyexpr_includes(nint left, nint right);

    /// bool
    /// z_keyexpr_intersects(
    ///     const struct z_loaned_keyexpr_t *left,
    ///     const struct z_loaned_keyexpr_t *right
    /// )
    [DllImport(DllName, EntryPoint = "z_keyexpr_intersects", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool z_keyexpr_intersects(nint left, nint right);

    /// z_result_t
    /// z_keyexpr_is_canon(
    ///     const char *start,
    ///     size_t len
    /// )
    [DllImport(DllName, EntryPoint = "z_keyexpr_is_canon", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_keyexpr_is_canon(nint start, nuint len);

    /// z_result_t
    /// z_keyexpr_join(
    ///     struct z_owned_keyexpr_t *this_,
    ///     const struct z_loaned_keyexpr_t *left,
    ///     const struct z_loaned_keyexpr_t *right
    /// )
    [DllImport(DllName, EntryPoint = "z_keyexpr_join", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_keyexpr_join(nint keyexpr, nint left, nint right);

    /// const struct z_loaned_keyexpr_t*
    /// z_keyexpr_loan(
    ///     const struct z_owned_keyexpr_t *this_
    /// )
    [DllImport(DllName, EntryPoint = "z_keyexpr_loan", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern nint z_keyexpr_loan(nint keyexpr);

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

    /// z_result_t
    /// z_open(
    ///     struct z_owned_session_t *this_,
    ///     struct z_moved_config_t *config,
    ///     const struct z_open_options_t *_options
    /// )
    [DllImport(DllName, EntryPoint = "z_open", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_open(nint session, nint config, nint options);

    /// void
    /// z_open_options_default(
    ///     struct z_open_options_t *this_
    /// )
    [DllImport(DllName, EntryPoint = "z_open_options_default", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_open_options_default(nint options);

    [DllImport(DllName, EntryPoint = "z_priority_default", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZPriority z_priority_default();

    /// z_result_t
    /// z_publisher_delete(
    ///     const struct z_loaned_publisher_t *publisher,
    ///     struct z_publisher_delete_options_t *options
    /// )
    [DllImport(DllName, EntryPoint = "z_publisher_delete", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_publisher_delete(ZLoanedPublisher* publisher, ZPublisherDeleteOptions* options);

    /// void
    /// z_publisher_delete_options_default(
    ///     struct z_publisher_delete_options_t *this_
    /// )
    [DllImport(DllName, EntryPoint = "z_publisher_delete_options_default", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_publisher_delete_options_default(ZPublisherDeleteOptions* options);

    /// void
    /// z_publisher_drop(
    ///     struct z_moved_publisher_t *this_
    /// )
    [DllImport(DllName, EntryPoint = "z_publisher_drop", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_publisher_drop(nint publisher);

    /// const struct z_loaned_keyexpr_t*
    /// z_publisher_keyexpr(
    ///     const struct z_loaned_publisher_t *publisher
    /// )
    [DllImport(DllName, EntryPoint = "z_publisher_keyexpr", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedKeyexpr* z_publisher_keyexpr(ZLoanedPublisher* publisher);

    /// const struct z_loaned_publisher_t*
    /// z_publisher_loan(
    ///     const struct z_owned_publisher_t *this_
    /// )
    [DllImport(DllName, EntryPoint = "z_publisher_loan", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern nint z_publisher_loan(nint publisher);

    /// struct z_loaned_publisher_t*
    /// z_publisher_loan_mut(
    ///     struct z_owned_publisher_t *this_
    /// )
    [DllImport(DllName, EntryPoint = "z_publisher_loan_mut", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern nint z_publisher_loan_mut(nint publisher);

    /// void
    /// z_publisher_put_options_default(
    ///     struct z_publisher_put_options_t *this_
    /// )
    [DllImport(DllName, EntryPoint = "z_publisher_options_default", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_publisher_options_default(nint options);

    /// z_result_t
    /// z_publisher_put(
    ///     const struct z_loaned_publisher_t *this_,
    ///     struct z_moved_bytes_t *payload,
    ///     struct z_publisher_put_options_t *options
    /// )
    [DllImport(DllName, EntryPoint = "z_publisher_put", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_publisher_put(nint publisher, nint payload, nint options);

    /// void
    /// z_publisher_put_options_default(
    ///     struct z_publisher_put_options_t *this_
    /// )
    [DllImport(DllName, EntryPoint = "z_publisher_put_options_default", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_publisher_put_options_default(nint options);

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
    internal static extern ZQueryTarget z_query_target_default();

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
    internal static extern ZCongestionControl z_sample_congestion_control(ZLoanedSample* sample);

    [DllImport(DllName, EntryPoint = "z_sample_encoding", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedEncoding* z_sample_encoding(ZLoanedSample* sample);

    [DllImport(DllName, EntryPoint = "z_sample_express", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool z_sample_express(ZLoanedSample* sample);

    [DllImport(DllName, EntryPoint = "z_sample_keyexpr", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedKeyexpr* z_sample_keyexpr(ZLoanedSample* sample);

    [DllImport(DllName, EntryPoint = "z_sample_kind", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZSampleKind z_sample_kind(ZLoanedSample* sample);

    [DllImport(DllName, EntryPoint = "z_sample_loan", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedSample* z_sample_loan(ZOwnedSample* sample);

    [DllImport(DllName, EntryPoint = "z_sample_payload", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedBytes* z_sample_payload(ZLoanedSample* sample);

    [DllImport(DllName, EntryPoint = "z_sample_priority", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZPriority z_sample_priority(ZLoanedSample* sample);

    [DllImport(DllName, EntryPoint = "z_sample_timestamp", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZTimestamp* z_sample_timestamp(ZLoanedSample* sample);

    [DllImport(DllName, EntryPoint = "z_scout", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_scout(ZMovedConfig* config, ZMovedClosureHello* callback, ZScoutOptions* options);

    [DllImport(DllName, EntryPoint = "z_scout_options_default", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_scout_options_default(ZScoutOptions* options);

    [DllImport(DllName, EntryPoint = "z_session_drop", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_session_drop(ZMovedSession* session);

    /// bool
    /// z_session_is_closed(
    ///     const struct z_loaned_session_t *session
    /// )
    [DllImport(DllName, EntryPoint = "z_session_is_closed", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool z_session_is_closed(nint session);

    /// const struct z_loaned_session_t*
    /// z_session_loan(
    ///     const struct z_owned_session_t *this_
    /// )
    [DllImport(DllName, EntryPoint = "z_session_loan", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern nint z_session_loan(nint session);

    /// struct z_loaned_session_t*
    /// z_session_loan_mut(
    ///     struct z_owned_session_t *this_
    /// )
    [DllImport(DllName, EntryPoint = "z_session_loan_mut", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern nint z_session_loan_mut(nint session);

    [DllImport(DllName, EntryPoint = "z_sleep_s", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_sleep_s(nuint time);

    [DllImport(DllName, EntryPoint = "z_sleep_ms", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_sleep_ms(nuint time);

    [DllImport(DllName, EntryPoint = "z_sleep_us", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_sleep_us(nuint time);

    [DllImport(DllName, EntryPoint = "z_slice_clone", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_slice_clone(ZOwnedSlice* dst, ZLoanedSlice* src);

    [DllImport(DllName, EntryPoint = "z_slice_copy_from_buf", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_slice_copy_from_buf(ZOwnedSlice* slice, byte* start, nuint len);

    [DllImport(DllName, EntryPoint = "z_slice_data", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern byte* z_slice_data(ZLoanedSlice* slice);

    /// void
    /// z_slice_drop(
    ///     struct z_moved_slice_t *this_
    /// )
    [DllImport(DllName, EntryPoint = "z_slice_drop", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_slice_drop(nint slice);

    /// void
    /// z_slice_empty(
    ///     struct z_owned_slice_t *this_
    /// )
    [DllImport(DllName, EntryPoint = "z_slice_empty", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_slice_empty(nint slice);

    /// z_result_t
    /// z_slice_from_buf(
    ///     struct z_owned_slice_t *this_,
    ///     uint8_t *data,
    ///     size_t len,
    ///     void (*drop)(void *data, void *context),
    ///     void *context
    /// )
    [DllImport(DllName, EntryPoint = "z_slice_from_buf", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_slice_from_buf(
        nint slice, nint data, nuint len, delegate*<void*, void*, void> drop, nint context);

    /// bool
    /// z_slice_is_empty(
    ///     const struct z_loaned_slice_t *this_
    /// )
    [DllImport(DllName, EntryPoint = "z_slice_is_empty", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool z_slice_is_empty(nint slice);

    /// size_t
    /// z_slice_len(
    ///     const struct z_loaned_slice_t *this_
    /// )
    [DllImport(DllName, EntryPoint = "z_slice_len", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern nuint z_slice_len(nint slice);

    /// const struct z_loaned_slice_t*
    /// z_slice_loan(
    ///     const struct z_owned_slice_t *this_
    /// )
    [DllImport(DllName, EntryPoint = "z_slice_loan", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern nint z_slice_loan(nint slice);

    [DllImport(DllName, EntryPoint = "z_string_array_clone", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_string_array_clone(ZOwnedStringArray* dst, ZLoanedStringArray* src);

    [DllImport(DllName, EntryPoint = "z_string_array_drop", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_string_array_drop(ZMovedStringArray* stringArray);

    [DllImport(DllName, EntryPoint = "z_string_array_get", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedString* z_string_array_get(ZLoanedStringArray* stringArray, nuint index);

    [DllImport(DllName, EntryPoint = "z_string_array_is_empty", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool z_string_array_is_empty(ZLoanedStringArray* stringArray);

    [DllImport(DllName, EntryPoint = "z_string_array_len", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern nuint z_string_array_len(ZLoanedStringArray* stringArray);

    [DllImport(DllName, EntryPoint = "z_string_array_loan", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedStringArray* z_string_array_loan(ZOwnedStringArray* stringArray);

    [DllImport(DllName, EntryPoint = "z_string_array_loan_mut", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedStringArray* z_string_array_loan_mut(ZOwnedStringArray* stringArray);

    [DllImport(DllName, EntryPoint = "z_string_array_new", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_string_array_new(ZOwnedStringArray* stringArray);

    [DllImport(DllName, EntryPoint = "z_string_push_by_alias", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern nuint z_string_array_push_by_alias(ZLoanedStringArray* stringArray, ZLoanedString* value);

    [DllImport(DllName, EntryPoint = "z_string_push_by_copy", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern nuint z_string_array_push_by_copy(ZLoanedStringArray* stringArray, ZLoanedString* value);

    [DllImport(DllName, EntryPoint = "z_string_as_slice", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedSlice* z_string_as_slice(ZLoanedString* str);

    /// void
    /// z_string_clone(
    ///     struct z_owned_string_t *dst,
    ///     const struct z_loaned_string_t *this_
    /// )
    [DllImport(DllName, EntryPoint = "z_string_clone", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_string_clone(nint dst, nint src);

    /// z_result_t
    /// z_string_copy_from_str(
    ///     struct z_owned_string_t *this_,
    ///     const char *str
    /// )
    [DllImport(DllName, EntryPoint = "z_string_copy_from_str", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_string_copy_from_str(nint dst, nint str);

    [DllImport(DllName, EntryPoint = "z_string_copy_from_substr", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_string_copy_from_substr(ZOwnedString* dst, byte* str, nuint len);

    /// const char*
    /// z_string_data(
    ///     const struct z_loaned_string_t *this_
    /// )
    [DllImport(DllName, EntryPoint = "z_string_data", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern nint z_string_data(nint str);

    /// void
    /// z_string_drop(
    ///     struct z_moved_string_t *this_
    /// )
    [DllImport(DllName, EntryPoint = "z_string_drop", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_string_drop(nint str);

    /// void
    /// z_string_empty(
    ///     struct z_owned_string_t *this_
    /// )
    [DllImport(DllName, EntryPoint = "z_string_empty", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_string_empty(nint str);

    [DllImport(DllName, EntryPoint = "z_string_from_str", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_string_from_str(
        ZOwnedString* dst, byte* str, delegate*<void*, void*, void> drop, void* context);

    /// bool
    /// z_string_is_empty(
    ///     const struct z_loaned_string_t *this_
    /// )
    [DllImport(DllName, EntryPoint = "z_string_is_empty", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool z_string_is_empty(nint str);

    /// size_t
    /// z_string_len(
    ///     const struct z_loaned_string_t *this_
    /// )
    [DllImport(DllName, EntryPoint = "z_string_len", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern nuint z_string_len(nint str);

    /// const struct z_loaned_string_t*
    /// z_string_loan(
    ///     const struct z_owned_string_t *this_
    /// )
    [DllImport(DllName, EntryPoint = "z_string_loan", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern nint z_string_loan(nint str);

    [DllImport(DllName, EntryPoint = "z_subscriber_drop", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_subscriber_drop(ZMovedSubscriber* subscriber);

    [DllImport(DllName, EntryPoint = "z_subscriber_keyexpr", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedKeyexpr* z_subscriber_keyexpr(ZLoanedSubscriber* subscriber);

    [DllImport(DllName, EntryPoint = "z_subscriber_loan", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedSubscriber* z_subscriber_loan(ZOwnedSubscriber* subscriber);

    [DllImport(DllName, EntryPoint = "z_subscriber_options_default", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_subscriber_options_default(ZSubscriberOptions* options);

    [DllImport(DllName, EntryPoint = "z_task_detach", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_task_detach(ZMovedTask* task);

    [DllImport(DllName, EntryPoint = "z_task_drop", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_task_drop(ZMovedTask* task);

    [DllImport(DllName, EntryPoint = "z_task_init", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_task_init(ZOwnedTask* task, ZTaskAttr* attr, delegate*<void*, void*> fun,
        void* arg);

    [DllImport(DllName, EntryPoint = "z_task_join", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_task_join(ZMovedTask* task);

    [DllImport(DllName, EntryPoint = "z_time_elapsed_s", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.U8)]
    internal static extern ulong z_time_elapsed_s(ZTime* time);

    [DllImport(DllName, EntryPoint = "z_time_elapsed_ms", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.U8)]
    internal static extern ulong z_time_elapsed_ms(ZTime* time);

    [DllImport(DllName, EntryPoint = "z_time_elapsed_us", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.U8)]
    internal static extern ulong z_time_elapsed_us(ZTime* time);

    [DllImport(DllName, EntryPoint = "z_time_now", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZTime z_time_now();

    [DllImport(DllName, EntryPoint = "z_time_now_as_str", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern byte* z_time_now_as_str(byte* buf, nuint len);

    [DllImport(DllName, EntryPoint = "z_timestamp_id", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZId z_timestamp_id(ZTimestamp* timestamp);

    [DllImport(DllName, EntryPoint = "z_timestamp_new", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_timestamp_new(ZTimestamp* timestamp, ZLoanedSession* session);

    [DllImport(DllName, EntryPoint = "z_timestamp_ntp64_time", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.U8)]
    internal static extern ulong z_timestamp_ntp64_time(ZTimestamp* timestamp);

    [DllImport(DllName, EntryPoint = "z_undeclare_keyexpr", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_undeclare_keyexpr(ZLoanedSession* session, ZMovedKeyexpr* keyexpr);

    [DllImport(DllName, EntryPoint = "z_undeclare_publisher", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_undeclare_publisher(ZMovedPublisher* publisher);

    [DllImport(DllName, EntryPoint = "z_undeclare_queryable", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_undeclare_queryable(ZMovedQueryable* queryable);

    [DllImport(DllName, EntryPoint = "z_undeclare_subscriber", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_undeclare_subscriber(ZMovedSubscriber* subscriber);

    [DllImport(DllName, EntryPoint = "z_view_keyexpr_empty", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_view_keyexpr_empty(ZViewKeyexpr* viewKeyexpr);

    [DllImport(DllName, EntryPoint = "z_view_keyexpr_from_str", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_view_keyexpr_from_str(ZViewKeyexpr* viewKeyexpr, byte* str);

    [DllImport(DllName, EntryPoint = "z_view_keyexpr_from_str_autocanonize",
        CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_view_keyexpr_from_str_autocanonize(ZViewKeyexpr* viewKeyexpr, byte* str);

    [DllImport(DllName, EntryPoint = "z_view_keyexpr_from_str_unchecked", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_view_keyexpr_from_str_unchecked(ZViewKeyexpr* viewKeyexpr, byte* str);

    [DllImport(DllName, EntryPoint = "z_view_keyexpr_from_substr", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_view_keyexpr_from_substr(ZViewKeyexpr* viewKeyexpr, byte* str, nuint len);

    [DllImport(DllName, EntryPoint = "z_view_keyexpr_from_substr_autocanonize",
        CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_view_keyexpr_from_substr_autocanonize(ZViewKeyexpr* viewKeyexpr, byte* start,
        nuint len);

    [DllImport(DllName, EntryPoint = "z_view_keyexpr_from_substr_unchecked",
        CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_view_keyexpr_from_substr_unchecked(ZViewKeyexpr* viewKeyexpr, byte* start, nuint len);

    [DllImport(DllName, EntryPoint = "z_view_keyexpr_is_empty", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool z_view_keyexpr_is_empty(ZViewKeyexpr* viewKeyexpr);

    [DllImport(DllName, EntryPoint = "z_view_keyexpr_loan", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedKeyexpr* z_view_keyexpr_loan(ZViewKeyexpr* viewKeyexpr);

    [DllImport(DllName, EntryPoint = "z_view_slice_empty", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_view_slice_empty(ZViewSlice* viewSlice);

    [DllImport(DllName, EntryPoint = "z_view_slice_from_buf", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_view_slice_from_buf(ZViewSlice* viewSlice, byte* start, nuint len);

    [DllImport(DllName, EntryPoint = "z_view_slice_is_empty", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool z_view_slice_is_empty(ZViewSlice* viewSlice);

    [DllImport(DllName, EntryPoint = "z_view_slice_loan", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZLoanedSlice* z_view_slice_loan(ZViewSlice* viewSlice);

    /// void
    /// z_view_string_empty(
    ///     struct z_view_string_t *this_
    /// )
    [DllImport(DllName, EntryPoint = "z_view_string_empty", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void z_view_string_empty(nint viewString);

    /// z_result_t
    /// z_view_string_from_str(struct z_view_string_t *this_,
    // const char *str);
    [DllImport(DllName, EntryPoint = "z_view_string_from_str", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_view_string_from_str(ZViewString* viewString, byte* str);

    /// z_result_t
    /// z_view_string_from_substr(
    ///     struct z_view_string_t *this_,
    ///     const char *str,
    ///     size_t len
    /// )
    [DllImport(DllName, EntryPoint = "z_view_string_from_substr", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_view_string_from_substr(nint viewString, nint str, nuint len);

    /// bool
    /// z_view_string_is_empty(
    ///     const struct z_view_string_t *this_
    /// )
    [DllImport(DllName, EntryPoint = "z_view_string_is_empty", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool z_view_string_is_empty(nint viewString);

    /// const struct z_loaned_string_t*
    /// z_view_string_loan(
    ///     const struct z_view_string_t *this_
    /// )
    [DllImport(DllName, EntryPoint = "z_view_string_loan", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern nint z_view_string_loan(nint viewString);

    [DllImport(DllName, EntryPoint = "z_whatami_to_view_string", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult z_whatami_to_view_string(ZWhatami zWhatami, ZViewString* strOut);

    [DllImport(DllName, EntryPoint = "zc_closure_log", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void zc_closure_log(
        ZcOwnedClosureLog* closure,
        delegate*<ZcLogSeverity, ZLoanedString*, void*, void> call,
        delegate*<void*, void> drop,
        void* context
    );

    [DllImport(DllName, EntryPoint = "zc_closure_log_call", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void zc_closure_log_call(ZcLoanedClosureLog* closure, ZcLogSeverity serverity,
        ZLoanedString* msg);

    [DllImport(DllName, EntryPoint = "zc_closure_log_drop", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void zc_closure_log_call_drop(ZcMovedClosureLog* closure);

    [DllImport(DllName, EntryPoint = "zc_closure_log_loan", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZcLoanedClosureLog* zc_closure_log_call_loan(ZcOwnedClosureLog* closure);

    /// z_result_t
    /// zc_config_from_env(
    ///     struct z_owned_config_t *this_
    /// )
    [DllImport(DllName, EntryPoint = "zc_config_from_env", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult zc_config_from_env(nint config);

    /// z_result_t
    /// zc_config_from_file(
    ///     struct z_owned_config_t *this_,
    ///     const char *path
    /// )
    [DllImport(DllName, EntryPoint = "zc_config_from_file", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult zc_config_from_file(nint config, nint path);

    /// z_result_t
    /// zc_config_from_str(
    ///     struct z_owned_config_t *this_,
    ///     const char *s
    /// )
    [DllImport(DllName, EntryPoint = "zc_config_from_str", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult zc_config_from_str(nint config, nint str);

    [DllImport(DllName, EntryPoint = "zc_config_get_from_str", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult zc_config_get_from_str(ZLoanedConfig* config, byte* key,
        ZOwnedString* outValueString);

    [DllImport(DllName, EntryPoint = "zc_config_get_from_substr", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult zc_config_get_from_substr(ZLoanedConfig* config, byte* key, nuint keyLen,
        ZOwnedString* outValueString);

    [DllImport(DllName, EntryPoint = "zc_config_insert_json5", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult zc_config_insert_json5(ZLoanedConfig* config, byte* key, byte* value);

    [DllImport(DllName, EntryPoint = "zc_config_insert_json5_from_substr", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult zc_config_insert_json5_from_substr(ZLoanedConfig* config, byte* key, nuint keyLen,
        byte* value, nuint valueLen);

    /// z_result_t
    /// zc_config_to_string(
    ///     const struct z_loaned_config_t *config,
    ///     struct z_owned_string_t *out_config_string
    /// )
    [DllImport(DllName, EntryPoint = "zc_config_to_string", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult zc_config_to_string(nint config, nint outConfigString);

    [DllImport(DllName, EntryPoint = "zc_try_init_log_from_env", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void zc_try_init_log_from_env();

    [DllImport(DllName, EntryPoint = "zc_init_log_from_env_or", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult zc_init_log_from_env_or(byte* fallbackFilter);

    [DllImport(DllName, EntryPoint = "zc_init_log_with_callback", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void zc_init_log_with_callback(ZcLogSeverity minSeverity, ZcMovedClosureLog* callback);

    [DllImport(DllName, EntryPoint = "zc_internal_closure_log_check", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool zc_internal_closure_log_check(ZcOwnedClosureLog* closure);

    [DllImport(DllName, EntryPoint = "zc_internal_closure_log_null", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void zc_internal_closure_log_null(ZcOwnedClosureLog* closure);

    /// void
    /// zc_internal_encoding_from_data(
    ///     struct z_owned_encoding_t *this_,
    ///     struct zc_internal_encoding_data_t data
    /// )
    [DllImport(DllName, EntryPoint = "zc_internal_encoding_from_data", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void zc_internal_encoding_from_data(nint encoding, ZcInternalEncodingData data );
    
    /// struct zc_internal_encoding_data_t
    /// zc_internal_encoding_get_data(
    ///     const struct z_loaned_encoding_t *this_
    /// )
    [DllImport(DllName, EntryPoint = "zc_internal_encoding_get_data", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZcInternalEncodingData zc_internal_encoding_get_data(nint encoding);
    
    [DllImport(DllName, EntryPoint = "zc_stop_z_runtime", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void zc_stop_z_runtime();

    [DllImport(DllName, EntryPoint = "ze_deserialize_bool", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult ze_deserialize_bool(ZLoanedBytes* bytes, byte* dst);

    [DllImport(DllName, EntryPoint = "ze_deserialize_double", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult ze_deserialize_double(ZLoanedBytes* bytes, double* dst);

    [DllImport(DllName, EntryPoint = "ze_deserialize_float", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult ze_deserialize_float(ZLoanedBytes* bytes, float* dst);

    [DllImport(DllName, EntryPoint = "ze_deserialize_int8", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult ze_deserialize_int8(ZLoanedBytes* bytes, sbyte* dst);

    [DllImport(DllName, EntryPoint = "ze_deserialize_int16", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult ze_deserialize_int16(ZLoanedBytes* bytes, short* dst);

    [DllImport(DllName, EntryPoint = "ze_deserialize_int32", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult ze_deserialize_int32(ZLoanedBytes* bytes, int* dst);

    [DllImport(DllName, EntryPoint = "ze_deserialize_int64", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult ze_deserialize_int64(ZLoanedBytes* bytes, long* dst);

    [DllImport(DllName, EntryPoint = "ze_deserialize_uint8", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult ze_deserialize_uint8(ZLoanedBytes* bytes, byte* dst);

    [DllImport(DllName, EntryPoint = "ze_deserialize_uint16", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult ze_deserialize_uint16(ZLoanedBytes* bytes, ushort* dst);

    [DllImport(DllName, EntryPoint = "ze_deserialize_uint32", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult ze_deserialize_uint32(ZLoanedBytes* bytes, uint* dst);

    [DllImport(DllName, EntryPoint = "ze_deserialize_uint64", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult ze_deserialize_uint64(ZLoanedBytes* bytes, ulong* dst);

    [DllImport(DllName, EntryPoint = "ze_deserialize_slice", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult ze_deserialize_slice(ZLoanedBytes* bytes, ZOwnedSlice* slice);

    [DllImport(DllName, EntryPoint = "ze_deserialize_string", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult ze_deserialize_string(ZLoanedBytes* bytes, ZOwnedString* str);

    [DllImport(DllName, EntryPoint = "ze_deserializer_deserialize_bool", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult ze_deserializer_deserialize_bool(ZeDeserializer* deserializer, byte* dst);

    [DllImport(DllName, EntryPoint = "ze_deserializer_deserialize_double", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult ze_deserializer_deserialize_double(ZeDeserializer* deserializer, double* dst);

    [DllImport(DllName, EntryPoint = "ze_deserializer_deserialize_float", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult ze_deserializer_deserialize_float(ZeDeserializer* deserializer, float* dst);

    [DllImport(DllName, EntryPoint = "ze_deserializer_deserialize_int8", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult ze_deserializer_deserialize_int8(ZeDeserializer* deserializer, sbyte* dst);

    [DllImport(DllName, EntryPoint = "ze_deserializer_deserialize_int16", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult ze_deserializer_deserialize_int16(ZeDeserializer* deserializer, short* dst);

    [DllImport(DllName, EntryPoint = "ze_deserializer_deserialize_int32", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult ze_deserializer_deserialize_int32(ZeDeserializer* deserializer, int* dst);

    [DllImport(DllName, EntryPoint = "ze_deserializer_deserialize_int64", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult ze_deserializer_deserialize_int64(ZeDeserializer* deserializer, long* dst);

    [DllImport(DllName, EntryPoint = "ze_deserializer_deserialize_uint8", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult ze_deserializer_deserialize_uint8(ZeDeserializer* deserializer, byte* dst);

    [DllImport(DllName, EntryPoint = "ze_deserializer_deserialize_uint16", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult ze_deserializer_deserialize_uint16(ZeDeserializer* deserializer, ushort* dst);

    [DllImport(DllName, EntryPoint = "ze_deserializer_deserialize_uint32", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult ze_deserializer_deserialize_uint32(ZeDeserializer* deserializer, uint* dst);

    [DllImport(DllName, EntryPoint = "ze_deserializer_deserialize_uint64", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult ze_deserializer_deserialize_uint64(ZeDeserializer* deserializer, ulong* dst);

    [DllImport(DllName, EntryPoint = "ze_deserializer_deserialize_sequence_length",
        CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult
        ze_deserializer_deserialize_sequence_length(ZeDeserializer* deserializer, nuint* len);

    [DllImport(DllName, EntryPoint = "ze_deserializer_deserialize_slice", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult ze_deserializer_deserialize_slice(ZeDeserializer* deserializer, ZOwnedSlice* slice);

    [DllImport(DllName, EntryPoint = "ze_deserializer_deserialize_string", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult ze_deserializer_deserialize_string(ZeDeserializer* deserializer, ZOwnedString* str);

    [DllImport(DllName, EntryPoint = "ze_deserializer_from_bytes", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZeDeserializer ze_deserializer_from_bytes(ZLoanedBytes* bytes);

    [DllImport(DllName, EntryPoint = "ze_deserializer_is_done", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool ze_deserializer_is_done(ZeDeserializer* deserializer);

    [DllImport(DllName, EntryPoint = "ze_internal_serializer_check", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static extern bool ze_internal_serializer_check(ZeOwnedSerializer* serializer);

    [DllImport(DllName, EntryPoint = "ze_internal_serializer_null", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void ze_internal_serializer_null(ZeOwnedSerializer* serializer);

    [DllImport(DllName, EntryPoint = "ze_serialize_buf", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult ze_serialize_buf(ZOwnedBytes* bytes, byte* data, nuint len);

    [DllImport(DllName, EntryPoint = "ze_serialize_bool", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult ze_serialize_bool(ZOwnedBytes* bytes, byte val);

    [DllImport(DllName, EntryPoint = "ze_serialize_double", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult ze_serialize_double(ZOwnedBytes* bytes, double val);

    [DllImport(DllName, EntryPoint = "ze_serialize_float", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult ze_serialize_float(ZOwnedBytes* bytes, float val);

    [DllImport(DllName, EntryPoint = "ze_serialize_int8", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult ze_serialize_int8(ZOwnedBytes* bytes, sbyte val);

    [DllImport(DllName, EntryPoint = "ze_serialize_int16", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult ze_serialize_int16(ZOwnedBytes* bytes, short val);

    [DllImport(DllName, EntryPoint = "ze_serialize_int32", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult ze_serialize_int32(ZOwnedBytes* bytes, int val);

    [DllImport(DllName, EntryPoint = "ze_serialize_int64", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult ze_serialize_int64(ZOwnedBytes* bytes, long val);

    [DllImport(DllName, EntryPoint = "ze_serialize_uint8", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult ze_serialize_uint8(ZOwnedBytes* bytes, byte val);

    [DllImport(DllName, EntryPoint = "ze_serialize_uint16", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult ze_serialize_uint16(ZOwnedBytes* bytes, ushort val);

    [DllImport(DllName, EntryPoint = "ze_serialize_uint32", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult ze_serialize_uint32(ZOwnedBytes* bytes, uint val);

    [DllImport(DllName, EntryPoint = "ze_serialize_uint64", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult ze_serialize_uint64(ZOwnedBytes* bytes, ulong val);

    [DllImport(DllName, EntryPoint = "ze_serialize_slice", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult ze_serialize_slice(ZOwnedBytes* bytes, ZLoanedSlice* slice);

    [DllImport(DllName, EntryPoint = "ze_serialize_str", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult ze_serialize_str(ZOwnedBytes* bytes, byte* str);

    [DllImport(DllName, EntryPoint = "ze_serialize_substr", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult ze_serialize_substr(ZOwnedBytes* bytes, byte* start, nuint len);

    [DllImport(DllName, EntryPoint = "ze_serialize_string", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult ze_serialize_string(ZOwnedBytes* bytes, ZLoanedString* str);

    [DllImport(DllName, EntryPoint = "ze_serializer_drop", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void ze_serializer_drop(ZeMovedSerializer* serializer);

    [DllImport(DllName, EntryPoint = "ze_serializer_empty", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult ze_serializer_empty(ZeOwnedSerializer* serializer);

    [DllImport(DllName, EntryPoint = "ze_serializer_finish", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern void ze_serializer_finish(ZeMovedSerializer* serializer, ZOwnedBytes* bytes);

    [DllImport(DllName, EntryPoint = "ze_serializer_loan", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZeLoanedSerializer* ze_serializer_loan(ZeOwnedSerializer* serializer);


    [DllImport(DllName, EntryPoint = "ze_serializer_serialize_bool", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult ze_serializer_serialize_bool(ZeLoanedSerializer* serializer, byte val);

    [DllImport(DllName, EntryPoint = "ze_serializer_serialize_double", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult ze_serializer_serialize_double(ZeLoanedSerializer* serializer, double val);

    [DllImport(DllName, EntryPoint = "ze_serializer_serialize_float", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult ze_serializer_serialize_float(ZeLoanedSerializer* serializer, float val);

    [DllImport(DllName, EntryPoint = "ze_serializer_serialize_int8", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult ze_serializer_serialize_int8(ZeLoanedSerializer* serializer, sbyte val);

    [DllImport(DllName, EntryPoint = "ze_serializer_serialize_int16", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult ze_serializer_serialize_int16(ZeLoanedSerializer* serializer, short val);

    [DllImport(DllName, EntryPoint = "ze_serializer_serialize_int32", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult ze_serializer_serialize_int32(ZeLoanedSerializer* serializer, int val);

    [DllImport(DllName, EntryPoint = "ze_serializer_serialize_int64", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult ze_serializer_serialize_int64(ZeLoanedSerializer* serializer, long val);

    [DllImport(DllName, EntryPoint = "ze_serializer_serialize_uint8", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult ze_serializer_serialize_uint8(ZeLoanedSerializer* serializer, byte val);

    [DllImport(DllName, EntryPoint = "ze_serializer_serialize_uint16", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult ze_serializer_serialize_uint16(ZeLoanedSerializer* serializer, ushort val);

    [DllImport(DllName, EntryPoint = "ze_serializer_serialize_uint32", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult ze_serializer_serialize_uint32(ZeLoanedSerializer* serializer, uint val);

    [DllImport(DllName, EntryPoint = "ze_serializer_serialize_uint64", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult ze_serializer_serialize_uint64(ZeLoanedSerializer* serializer, ulong val);

    [DllImport(DllName, EntryPoint = "ze_serializer_serialize_buf", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult ze_serializer_serialize_buf(ZeLoanedSerializer* serializer, byte* data, nuint len);

    [DllImport(DllName, EntryPoint = "ze_serializer_serialize_sequence_length",
        CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult ze_serializer_serialize_sequence_length(ZeLoanedSerializer* serializer, nuint len);

    [DllImport(DllName, EntryPoint = "ze_serializer_serialize_slice", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult ze_serializer_serialize_slice(ZeLoanedSerializer* serializer, ZLoanedSlice* slice);

    [DllImport(DllName, EntryPoint = "ze_serializer_serialize_str", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult ze_serializer_serialize_str(ZeLoanedSerializer* serializer, byte* str);

    [DllImport(DllName, EntryPoint = "ze_serializer_serialize_substr", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult ze_serializer_serialize_substr(ZeLoanedSerializer* serializer, byte* start,
        nuint len);

    [DllImport(DllName, EntryPoint = "ze_serializer_serialize_string", CallingConvention = CallingConvention.Cdecl,
        ExactSpelling = true)]
    internal static extern ZResult ze_serializer_serialize_string(ZeLoanedSerializer* serializer, ZLoanedString* str);
}