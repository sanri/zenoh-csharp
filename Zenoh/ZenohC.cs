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

using System;
using System.Runtime.InteropServices;

namespace Zenoh
{
    // zenoh_commons.h
    // src/result.rs
    // z_result_t
    public enum Result : sbyte
    {
        /// Ok
        Ok = 0,

        /// Channel disconnected
        ChannelDisconnected = 1,

        /// Channel nodata
        ChannelNodata = 2,

        /// Invalid argument
        ErrorInvalidArgument = -1,

        /// Parse error.
        ErrorParse = -2,

        /// IO error.
        ErrorIo = -3,

        ///  Network error.
        ErrorNetwork = -4,

        /// Null
        ErrorNull = -5,

        /// Unavailable
        ErrorUnavailable = -6,

        /// Deserialize error.
        ErrorDeserialize = -7,

        /// Session closed.
        ErrorSessionClosed = -8,

        /// 
        ErrorUtf8 = -9,

        /// 
        ErrorResourceTemporarilyUnavailableMutex = -11,

        /// 
        ErrorDeviceOrResourceBusyMutex = -16,

        /// 
        ErrorInvalidArgumentMutex = -22,

        /// 
        ErrorGeneric = -128,
    }

    // zenoh_commons.h
    // z_congestion_control_t
    /// <summary>
    /// Congestion control
    /// </summary>
    public enum CongestionControl : uint
    {
        /// <summary>
        /// Messages are not dropped in case of congestion.
        /// </summary>
        Block = 0,

        /// <summary>
        /// Messages are dropped in case of congestion.
        /// </summary>
        Drop = 1
    }

    // zenoh_commons.h
    // z_sample_kind_t
    /// <summary>
    /// Sample kind.
    /// </summary>
    public enum SampleKind : uint
    {
        /// <summary>
        /// The Sample was issued by a "put" operation.
        /// </summary>
        Put = 0,

        /// <summary>
        ///  The Sample was issued by a "delete" operation.
        /// </summary>
        Delete = 1
    }

    // zenoh_commons.h
    // z_priority_t
    /// <summary>
    /// The priority of zenoh messages.
    /// </summary>
    public enum Priority : uint
    {
        /// <summary>
        /// Priority for "RealTime" messages.
        /// </summary>
        RealTime = 1,

        /// <summary>
        /// Highest priority for "Interactive" messages.
        /// </summary>
        InteractiveHigh = 2,

        /// <summary>
        /// Lowest priority for "Interactive" messages.
        /// </summary>
        InteractiveLow = 3,

        /// <summary>
        /// Highest priority for "Data" messages.
        /// </summary>
        DataHigh = 4,

        /// <summary>
        /// Default priority for "Data" messages.
        /// </summary>
        Data = 5,

        /// <summary>
        /// Lowest priority for "Data" messages.
        /// </summary>
        DataLow = 6,

        /// <summary>
        /// Priority for "Background traffic" messages.
        /// </summary>
        Background = 7
    }

    // zenoh_commons.h
    // z_consolidation_mode_t
    /// <summary>
    /// Consolidation mode values.
    /// </summary>
    public enum ConsolidationMode
    {
        /// <summary>
        /// Let Zenoh decide the best consolidation mode depending on the query selector.
        /// If the selector contains time range properties, consolidation mode "None" is used.
        /// Otherwise the "Latest" consolidation mode is used.
        /// </summary>
        Auto = -1,

        /// <summary>
        /// No consolidation is applied. Replies may come in any order and any number.
        /// </summary>
        None = 0,

        /// <summary>
        /// <para>
        /// It guarantees that any reply for a given key expression will be monotonic in time  w.r.t. the previous received replies for the same key expression. 
        /// </para>
        /// <para>
        /// I.e., for the same key expression multiple replies may be received.
        /// It is guaranteed that two replies received at t1 and t2 will have timestamp ts2 > ts1. It optimizes latency.
        /// </para>
        /// </summary>
        Monotonic = 1,

        /// <summary>
        /// It guarantees unicity of replies for the same key expression. It optimizes bandwidth.
        /// </summary>
        Latest = 2
    }

#if UNSTABLE_API
    // zenoh_commons.h
    // z_keyexpr_intersection_level_t
    /// <summary>
    /// Intersection level of 2 key expressions.
    /// </summary>
    public enum KeyexprIntersectionLevel : uint
    {
        /// 2 key expressions do not intersect.
        Disjoint = 0,

        /// 2 key expressions intersect, i.e. there exists at least one key expression that is included by both.
        Intersects = 1,

        /// First key expression is the superset of second one.
        Includes = 2,

        /// 2 key expressions are equal.
        Equals = 3,
    }
#endif

#if UNSTABLE_API
    // zenoh_commons.h
    // z_reliability_t
    /// <summary>
    /// <para>The publisher reliability.</para>
    /// <para>
    /// Currently, "reliability" does not trigger any data retransmission on the wire.
    /// It is rather used as a marker on the wire and it may be used to select the best link available (e.g. TCP for reliable data and UDP for best effort data).
    /// </para>
    /// </summary>
    public enum Reliability : uint
    {
        /// <summary>
        /// Best effort.
        /// </summary>
        BestEffort = 0,

        /// <summary>
        /// Reliable.
        /// </summary>
        Reliable = 1
    }
#endif

    // zenoh_commons.h
    // z_query_target_t 
    /// <summary>
    /// The Queryables that should be target of a "session.Get()".
    /// </summary>
    public enum QueryTarget : uint
    {
        /// <summary>
        /// The nearest complete queryable if any else all matching queryables.
        /// </summary>
        BestMatching = 0,

        /// <summary>
        /// All matching queryables.
        /// </summary>
        All = 1,

        /// <summary>
        /// All complete queryables.
        /// </summary>
        AllComplete = 2
    }

    // zenoh_commons.h
    // z_what_t
    public enum What : uint
    {
        /// <summary>
        /// Router node.
        /// </summary>
        Router = 1,

        /// <summary>
        /// Peer node.
        /// </summary>
        Peer = 2,

        /// <summary>
        /// Client node.
        /// </summary>
        Client = 4,

        /// <summary>
        /// Router and peer node.
        /// </summary>
        RouterPeer = 1 | 2,

        /// <summary>
        /// Router and client node.
        /// </summary>
        RouterClient = 1 | 4,

        /// <summary>
        /// Peer and client node.
        /// </summary>
        PeerClient = 2 | 4,

        /// <summary>
        /// Router, peer and client node.
        /// </summary>
        RouterPeerClient = 1 | 2 | 4,
    }

    // zenoh_commons.h
    // z_whatami_t
    /// <summary>
    /// What am I.
    /// </summary>
    public enum Whatami : uint
    {
        /// <summary>
        /// Router
        /// </summary>
        Router = 1,

        /// <summary>
        /// Peer
        /// </summary>
        Peer = 2,

        /// <summary>
        /// Client
        /// </summary>
        Client = 4
    }

    // zenoh_commons.h
    // zc_locality_t
    /// <summary>
    /// The locality of samples to be received by subscribers or targeted by publishers.
    /// </summary>
    public enum Locality : uint
    {
        /// <summary>
        /// Any
        /// </summary>
        Any = 0,

        /// <summary>
        /// Only from local sessions.
        /// </summary>
        Local = 1,

        /// <summary>
        /// Only from remote sessions.
        /// </summary>
        Remote = 2,
    }

    // zenoh_commons.h
    // zc_log_severity_t
    /// <summary>
    /// Severity level of Zenoh log message.
    /// </summary>
    public enum LogSeverity : uint
    {
        /// <summary>
        /// The `trace` level.
        /// Designates very low priority, often extremely verbose, information.
        /// </summary>
        Trace = 0,

        /// <summary>
        /// The "debug" level.
        /// Designates lower priority information.
        /// </summary>
        Debug = 1,

        /// <summary>
        /// The "info" level.
        /// Designates useful information.
        /// </summary>
        Info = 2,

        /// <summary>
        /// The "warn" level.
        /// Designates hazardous situations.
        /// </summary>
        Warn = 3,

        /// <summary>
        /// The "error" level.
        /// Designates very serious errors.
        /// </summary>
        Error = 4,
    }

#if UNSTABLE_API
    // zenoh_commons.h
    // zc_reply_keyexpr_t
    /// <summary>
    /// Key expressions types to which Queryable should reply to.
    /// </summary>
    public enum ReplyKeyexpr : uint
    {
        /// Replies to any key expression queries.
        Any = 0,

        /// Replies only to queries with intersecting key expressions.
        MatchingQuery = 1,
    }
#endif

    // zenoh_opaque.h
    // z_owned_bytes_t
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    internal unsafe struct ZOwnedBytes
    {
#if UNSTABLE_API
        private fixed byte data[40];
#else
        private fixed byte data[32];
#endif
    }

    // zenoh_opaque.h
    // z_owned_slice_t
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    internal unsafe struct ZOwnedSlice
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
    // z_view_string_t
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    internal unsafe struct ZViewString
    {
        private fixed byte data[32];
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
#if UNSTABLE_API
        private fixed byte data[64];
#else
        private fixed byte data[56];
#endif
    }

#if UNSTABLE_API
    // zenoh_opaque.h
    // zc_owned_concurrent_close_handle_t
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    internal unsafe struct ZcOwnedConcurrentCloseHandle
    {
        private fixed byte data[8];
    }
#endif

    // zenoh_opaque.h
    // z_owned_hello_t
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    internal unsafe struct ZOwnedHello
    {
        private fixed byte data[48];
    }

    // zenoh_commons.h
    // typedef struct z_owned_closure_hello_t {
    //   void *_context;
    //   void (*_call)(struct z_loaned_hello_t *hello, void *context);
    //   void (*_drop)(void *context);
    // } z_owned_closure_hello_t;
    [StructLayout(LayoutKind.Sequential)]
    internal struct ZOwnedClosureHello
    {
        internal IntPtr context;
        internal IntPtr call;
        internal IntPtr drop;
    }

#if UNSTABLE_API
    // zenoh_commons.h
    // z_matching_status_t
    [StructLayout(LayoutKind.Sequential)]
    internal struct ZMatchingStatus
    {
        internal bool matching;
    }
#endif

#if UNSTABLE_API
    // zenoh_commons.h
    // typedef struct z_owned_closure_matching_status_t {
    //     void *_context;
    //     void (*_call)(const struct z_matching_status_t *matching_status, void *context);
    //     void (*_drop)(void *context);
    // } z_owned_closure_matching_status_t;
    [StructLayout(LayoutKind.Sequential)]
    internal struct ZOwnedClosureMatchingStatus
    {
        internal IntPtr context;
        internal IntPtr call;
        internal IntPtr drop;
    }
#endif

    // zenoh_opaque.h
    // z_owned_query_t
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    internal unsafe struct ZOwnedQuery
    {
#if UNSTABLE_API
        private fixed byte data[144];
#else
        private fixed byte data[136];
#endif
    }

    // zenoh_commons.h
    // typedef struct z_owned_closure_query_t {
    //     void *_context;
    //     void (*_call)(struct z_loaned_query_t *query, void *context);
    //     void (*_drop)(void *context);
    // } z_owned_closure_query_t;
    [StructLayout(LayoutKind.Sequential)]
    internal struct ZOwnedClosureQuery
    {
        internal IntPtr context;
        internal IntPtr call;
        internal IntPtr drop;
    }

    // zenoh_opaque.h
    // z_owned_reply_t
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    internal unsafe struct ZOwnedReply
    {
#if UNSTABLE_API
        private fixed byte data[256];
#else
        private fixed byte data[184];
#endif
    }

    // zenoh_opaque.h
    // z_owned_reply_err_t
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    internal unsafe struct ZOwnedReplyErr
    {
#if UNSTABLE_API
        private fixed byte data[88];
#else
        private fixed byte data[72];
#endif
    }

    // zenoh_commons.h
    // typedef struct z_owned_closure_reply_t {
    //     void *_context;
    //     void (*_call)(struct z_loaned_reply_t *reply, void *context);
    //     void (*_drop)(void *context);
    // } z_owned_closure_reply_t;
    [StructLayout(LayoutKind.Sequential)]
    internal struct ZOwnedClosureReply
    {
        internal IntPtr context;
        internal IntPtr call;
        internal IntPtr drop;
    }

    // zenoh_opaque.h
    // z_owned_sample_t
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    internal unsafe struct ZOwnedSample
    {
#if UNSTABLE_API
        private fixed byte data[232];
#else
        private fixed byte data[184];
#endif
    }

    // zenoh_commons.h
    // typedef struct z_owned_closure_sample_t {
    //     void *_context;
    //     void (*_call)(struct z_loaned_sample_t *sample, void *context);
    //     void (*_drop)(void *context);
    // } z_owned_closure_sample_t;
    [StructLayout(LayoutKind.Sequential)]
    internal struct ZOwnedClosureSample
    {
        internal IntPtr context;
        internal IntPtr call;
        internal IntPtr drop;
    }

#if UNSTABLE_API
    // zenoh_opaque.h
    // z_owned_source_info_t
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    internal unsafe struct ZOwnedSourceInfo
    {
        private fixed byte data[32];
    }
#endif

    // zenoh_opaque.h
    // z_id_t
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal unsafe struct ZId
    {
        private fixed byte id[16];

        internal byte[] GetId()
        {
            var o = new byte[16];
            for (var i = 0; i < 16; i++)
            {
                o[i] = id[i];
            }

            return o;
        }
    }

    // zenoh_commons.h
    // typedef struct z_owned_closure_zid_t {
    //     void *_context;
    //     void (*_call)(const struct z_id_t *z_id, void *context);
    //     void (*_drop)(void *context);
    // } z_owned_closure_zid_t;
    [StructLayout(LayoutKind.Sequential)]
    internal struct ZOwnedClosureZid
    {
        internal IntPtr context;
        internal IntPtr call;
        internal IntPtr drop;
    }

    // zenoh_opaque.h
    // z_owned_condvar_t
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    internal unsafe struct ZOwnedCondvar
    {
        private fixed byte data[24];
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
    internal struct ZClock
    {
        internal ulong t;

        // void*
        internal IntPtr t_base;
    }

    // zenoh_commons.h
    // z_close_options_t
    [StructLayout(LayoutKind.Sequential)]
    internal struct ZCloseOptions
    {
#if UNSTABLE_API
        uint timeout_ms;

        // zc_owned_concurrent_close_handle_t*
        private IntPtr out_concurrent;
#else
        internal byte dummy;
#endif
    }

    // zenoh_opaque.h
    // z_owned_config_t
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    internal unsafe struct ZOwnedConfig
    {
        private fixed byte data[1912];
    }

    // zenoh_opaque.h
    // z_owned_keyexpr_t
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    internal unsafe struct ZOwnedKeyexpr
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

    // zenoh_opaque.h
    // z_owned_encoding_t
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    internal unsafe struct ZOwnedEncoding
    {
#if UNSTABLE_API
        private fixed byte data[48];
#else
        private fixed byte data[40];
#endif
    }

#if UNSTABLE_API
    // zenoh_opaque.h
    // z_entity_global_id_t
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    internal unsafe struct ZEntityGlobalId
    {
        private fixed byte data[20];
    }
#endif

    // zenoh_opaque.h
    // z_owned_publisher_t
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    internal unsafe struct ZOwnedPublisher
    {
#if UNSTABLE_API
        private fixed byte data[112];
#else
        private fixed byte data[96];
#endif
    }

#if UNSTABLE_API
    // zenoh_opaque.h
    // z_owned_querier_t
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    internal unsafe struct ZOwnedQuerier
    {
        private fixed byte data[80];
    }
#endif

    // zenoh_opaque.h
    // z_owned_queryable_t
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    internal unsafe struct ZOwnedQueryable
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
    // z_owned_fifo_handler_query_t
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    internal unsafe struct ZOwnedFifoHandlerQuery
    {
        private fixed byte data[8];
    }

    // zenoh_opaque.h
    // z_owned_fifo_handler_reply_t
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    internal unsafe struct ZOwnedFifoHandlerReply
    {
        private fixed byte data[8];
    }

    // zenoh_opaque.h
    // z_owned_fifo_handler_sample_t
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    internal unsafe struct ZOwnedFifoHandlerSample
    {
        private fixed byte data[8];
    }

    // zenoh_opaque.h
    // z_owned_string_array_t
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    internal unsafe struct ZOwnedStringArray
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

    // zenoh_commons.h
    // z_liveliness_subscriber_options_t
    [StructLayout(LayoutKind.Sequential)]
    internal struct ZLivelinessSubscriberOptions
    {
        [MarshalAs(UnmanagedType.U1)] internal bool history;
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
        internal ulong timeout_ms;
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
    // z_owned_ring_handler_reply_t
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    internal unsafe struct ZOwnedRingHandlerReply
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

    // zenoh_commons.h
    // z_task_attr_t
    [StructLayout(LayoutKind.Sequential)]
    internal struct ZTaskAttr
    {
        internal UIntPtr attr;
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

    // zenoh_commons.h
    // z_publisher_options_t 
    [StructLayout(LayoutKind.Sequential)]
    internal struct ZPublisherOptions
    {
        // z_moved_encoding_t*
        internal IntPtr encoding;
        internal CongestionControl congestion_control;
        internal Priority priority;
        [MarshalAs(UnmanagedType.U1)] internal bool is_express;
#if UNSTABLE_API
        internal Reliability reliability;
        internal Locality allowed_destination;
#endif
    }

    // zenoh_commons.h
    // z_publisher_put_options_t 
    [StructLayout(LayoutKind.Sequential)]
    internal struct ZPublisherPutOptions
    {
        // z_moved_encoding_t*
        internal IntPtr encoding;

        // z_timestamp_t*
        internal IntPtr timestamp;

#if UNSTABLE_API
        // z_moved_source_info_t*
        internal IntPtr source_info;
#endif

        // z_moved_bytes_t*
        internal IntPtr attachment;
    }

    // zenoh_commons.h
    // z_publisher_delete_options_t
    [StructLayout(LayoutKind.Sequential)]
    internal struct ZPublisherDeleteOptions
    {
        // z_timestamp_t*
        internal IntPtr timestamp;
    }

    // zenoh_commons.h
    // z_queryable_options_t 
    [StructLayout(LayoutKind.Sequential)]
    internal struct ZQueryableOptions
    {
        [MarshalAs(UnmanagedType.U1)] internal bool complete;

#if UNSTABLE_API
        internal Locality allowed_origin;
#endif
    }

    // zenoh_commons.h
    // z_subscriber_options_t 
    [StructLayout(LayoutKind.Sequential)]
    internal struct ZSubscriberOptions
    {
#if UNSTABLE_API
        internal Locality allowed_origin;
#else
        internal byte dummy;
#endif
    }

    // zenoh_commons.h
    // z_delete_options_t 
    [StructLayout(LayoutKind.Sequential)]
    internal struct ZDeleteOptions
    {
        internal CongestionControl congestion_control;

        internal Priority priority;

        [MarshalAs(UnmanagedType.U1)] internal bool is_express;

        // z_timestamp_t*
        internal IntPtr timestamp;

#if UNSTABLE_API
        internal Reliability reliability;
        internal Locality allowed_destination;
#endif
    }

    // zenoh_commons.h
    // z_query_consolidation_t 
    [StructLayout(LayoutKind.Sequential)]
    internal struct ZQueryConsolidation
    {
        internal ConsolidationMode mode;
    }

#if UNSTABLE_API
    // zenoh_commons.h
    // z_querier_options_t
    [StructLayout(LayoutKind.Sequential)]
    internal struct ZQuerierOptions
    {
        internal QueryTarget target;
        internal ZQueryConsolidation consolidation;
        internal CongestionControl congestion_control;
        internal bool is_express;
        internal Locality allowed_destination;
        internal ReplyKeyexpr accept_replies;
        internal Priority priority;
        internal ulong timeout_ms;
    }
#endif

#if UNSTABLE_API
    // zenoh_commons.h
    // z_querier_get_options_t
    [StructLayout(LayoutKind.Sequential)]
    internal struct ZQuerierGetOptions
    {
        // z_moved_bytes_t*
        internal IntPtr payload;

        // z_moved_encoding_t* 
        internal IntPtr encoding;

        // z_moved_source_info_t*
        internal IntPtr source_info;

        // z_moved_bytes_t*
        internal IntPtr attachment;
    }
#endif

    // zenoh_commons.h
    // z_get_options_t 
    [StructLayout(LayoutKind.Sequential)]
    internal struct ZGetOptions
    {
        internal QueryTarget target;

        internal ZQueryConsolidation consolidation;

        // z_moved_bytes_t*
        internal IntPtr payload;

        // z_moved_encoding_t*
        internal IntPtr encoding;

        internal CongestionControl congestion_control;

        [MarshalAs(UnmanagedType.U1)] internal bool is_express;

#if UNSTABLE_API
        internal Locality allowed_destination;
        internal ReplyKeyexpr accept_replies;
#endif

        internal Priority priority;

#if UNSTABLE_API
        // z_moved_source_info_t*
        internal IntPtr source_info;
#endif

        // z_moved_bytes_t*
        internal IntPtr attachment;

        internal ulong timeout_ms;
    }

    // zenoh_commons.h
    // z_put_options_t 
    [StructLayout(LayoutKind.Sequential)]
    internal struct ZPutOptions
    {
        // z_moved_encoding_t*
        internal IntPtr encoding;

        internal CongestionControl congestion_control;

        internal Priority priority;

        [MarshalAs(UnmanagedType.U1)] internal bool is_express;

        // z_timestamp_t*
        internal IntPtr timestamp;

#if UNSTABLE_API
        internal Reliability reliability;

        internal Locality allowed_destination;

        // z_moved_source_info_t*
        internal IntPtr source_info;
#endif

        // z_moved_bytes_t*
        internal IntPtr attachment;
    }

    // zenoh_commons.h
    // z_query_reply_options_t 
    [StructLayout(LayoutKind.Sequential)]
    internal struct ZQueryReplyOptions
    {
        // z_moved_encoding_t*
        internal IntPtr encoding;

        internal CongestionControl congestion_control;

        internal Priority priority;

        [MarshalAs(UnmanagedType.U1)] internal bool is_express;

        // z_timestamp_t*
        internal IntPtr timestamp;

#if UNSTABLE_API
        // z_moved_source_info_t*
        internal IntPtr source_info;
#endif

        // z_moved_bytes_t*
        internal IntPtr attachment;
    }

    // zenoh_commons.h
    // z_query_reply_del_options_t 
    [StructLayout(LayoutKind.Sequential)]
    internal struct ZQueryReplyDelOptions
    {
        internal CongestionControl congestion_control;

        internal Priority priority;

        [MarshalAs(UnmanagedType.U1)] internal bool is_express;

        // timestamp*
        internal IntPtr timestamp;

#if UNSTABLE_API
        // z_moved_source_info_t*
        internal IntPtr source_info;
#endif

        // z_moved_bytes_t*
        internal IntPtr attachment;
    }

    // zenoh_commons.h
    // z_query_reply_err_options_t 
    [StructLayout(LayoutKind.Sequential)]
    internal struct ZQueryReplyErrOptions
    {
        // struct z_moved_encoding_t*
        internal IntPtr encoding;
    }

    // zenoh_commons.h
    // zc_owned_closure_log_t
    [StructLayout(LayoutKind.Sequential)]
    internal struct ZcOwnedClosureLog
    {
        internal IntPtr context;
        internal IntPtr call;
        internal IntPtr drop;
    }

    // zenoh_commons.h
    // typedef struct zc_internal_encoding_data_t {
    //     uint16_t id;
    //     const uint8_t *schema_ptr;
    //     size_t schema_len;
    // } zc_internal_encoding_data_t;
    [StructLayout(LayoutKind.Sequential)]
    internal struct ZcInternalEncodingData
    {
        internal ushort id;
        internal IntPtr schema_ptr;
        internal UIntPtr schema_len;
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
#if UNSTABLE_API
        private fixed byte data[64];
#else
        private fixed byte data[56];
#endif
    }

    internal static class ZenohC
    {
        internal delegate void Cb2(IntPtr data, IntPtr context);

        internal delegate void Cb1(IntPtr context);

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

#if UNSTABLE_API
        /// enum z_reliability_t
        /// z_reliability_default(
        ///     void
        /// )
        [DllImport(DllName, EntryPoint = "z_reliability_default", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Reliability z_reliability_default();
#endif

        /// void
        /// z_bytes_clone(
        ///      struct z_owned_bytes_t *dst,
        ///      const struct z_loaned_bytes_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_bytes_clone", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_bytes_clone(IntPtr dst, IntPtr src);

        /// z_result_t
        /// z_bytes_copy_from_buf(
        ///      struct z_owned_bytes_t *this_,
        ///      const uint8_t *data,
        ///      size_t len
        /// )
        [DllImport(DllName, EntryPoint = "z_bytes_copy_from_buf", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_bytes_copy_from_buf(IntPtr dst, IntPtr src, UIntPtr len);

        /// void
        /// z_bytes_copy_from_slice(
        ///      struct z_owned_bytes_t *this_,
        ///      const struct z_loaned_slice_t *slice
        /// )
        [DllImport(DllName, EntryPoint = "z_bytes_copy_from_slice", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_bytes_copy_from_slice(IntPtr dst, IntPtr src);

        /// z_result_t
        /// z_bytes_copy_from_str(
        ///     struct z_owned_bytes_t *this_,
        ///     const char *str
        /// )
        [DllImport(DllName, EntryPoint = "z_bytes_copy_from_str", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern Result z_bytes_copy_from_str(IntPtr dst, IntPtr src);

        /// void
        /// z_bytes_copy_from_string(
        ///     struct z_owned_bytes_t *this_,
        ///     const struct z_loaned_string_t *str
        /// )
        [DllImport(DllName, EntryPoint = "z_bytes_copy_from_string", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_bytes_copy_from_string(IntPtr dst, IntPtr src);

        /// void
        /// z_bytes_drop(
        ///     struct z_moved_bytes_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_bytes_drop", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_bytes_drop(IntPtr bytes);

        /// void
        /// z_bytes_empty(
        ///     struct z_owned_bytes_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_bytes_empty", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_bytes_empty(IntPtr bytes);

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
        internal static extern Result z_bytes_from_buf(IntPtr dst, IntPtr src, UIntPtr len, IntPtr deleter,
            IntPtr context);

        /// void
        /// z_bytes_from_slice(
        ///     struct z_owned_bytes_t *this_,
        ///     struct z_moved_slice_t *slice
        /// )
        [DllImport(DllName, EntryPoint = "z_bytes_from_slice", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_bytes_from_slice(IntPtr dst, IntPtr src);

        [DllImport(DllName, EntryPoint = "z_bytes_from_static_buf", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result z_bytes_from_static_buf(IntPtr dst, IntPtr src, UIntPtr len);

        [DllImport(DllName, EntryPoint = "z_bytes_from_static_str", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result z_bytes_from_static_str(IntPtr dst, IntPtr src);

        [DllImport(DllName, EntryPoint = "z_bytes_from_str", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result z_bytes_from_str(IntPtr dst, IntPtr src, IntPtr deleter, IntPtr context);

        /// void
        /// z_bytes_from_string(
        ///     struct z_owned_bytes_t *this_,
        ///     struct z_moved_string_t *s
        /// )
        [DllImport(DllName, EntryPoint = "z_bytes_from_string", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_bytes_from_string(IntPtr dst, IntPtr src);

        /// bool
        /// z_bytes_is_empty(
        ///     const struct z_loaned_bytes_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_bytes_is_empty", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool z_bytes_is_empty(IntPtr data);

        /// size_t
        /// z_bytes_len(
        ///     const struct z_loaned_bytes_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_bytes_len", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern UIntPtr z_bytes_len(IntPtr data);

        /// const struct z_loaned_bytes_t*
        /// z_bytes_loan(
        ///     const struct z_owned_bytes_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_bytes_loan", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_bytes_loan(IntPtr data);

        /// struct z_loaned_bytes_t*
        /// z_bytes_loan_mut(
        ///     struct z_owned_bytes_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_bytes_loan_mut", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_bytes_loan_mut(IntPtr data);

        /// struct z_bytes_reader_t
        /// z_bytes_get_reader(
        ///     const struct z_loaned_bytes_t *data
        /// )
        [DllImport(DllName, EntryPoint = "z_bytes_get_reader", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern ZBytesReader z_bytes_get_reader(IntPtr data);

        /// struct z_bytes_slice_iterator_t
        /// z_bytes_get_slice_iterator(
        ///     const struct z_loaned_bytes_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_bytes_get_slice_iterator", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern ZBytesSliceIterator z_bytes_get_slice_iterator(IntPtr data);

        /// size_t
        /// z_bytes_reader_read(
        ///     struct z_bytes_reader_t *this_,
        ///     uint8_t *dst,
        ///     size_t len
        /// )
        [DllImport(DllName, EntryPoint = "z_bytes_reader_read", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern UIntPtr z_bytes_reader_read(IntPtr reader, IntPtr dst, UIntPtr len);

        /// size_t
        /// z_bytes_reader_remaining(
        ///     const struct z_bytes_reader_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_bytes_reader_remaining", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern UIntPtr z_bytes_reader_remaining(IntPtr reader);

        /// z_result_t
        /// z_bytes_reader_seek(
        ///     struct z_bytes_reader_t *this_,
        ///     int64_t offset,
        ///     int origin
        /// )
        [DllImport(DllName, EntryPoint = "z_bytes_reader_seek", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result z_bytes_reader_seek(IntPtr reader, long offset, int origin);

        /// int64_t
        /// z_bytes_reader_tell(
        ///     struct z_bytes_reader_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_bytes_reader_tell", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern long z_bytes_reader_tell(IntPtr reader);

        /// bool
        /// z_bytes_slice_iterator_next(
        ///     struct z_bytes_slice_iterator_t *this_,
        ///     struct z_view_slice_t *slice
        /// )
        [DllImport(DllName, EntryPoint = "z_bytes_slice_iterator_next", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool z_bytes_slice_iterator_next(IntPtr iter, IntPtr slice);

        /// z_result_t
        /// z_bytes_to_slice(
        ///     const struct z_loaned_bytes_t *this_,
        ///     struct z_owned_slice_t *dst
        /// )
        [DllImport(DllName, EntryPoint = "z_bytes_to_slice", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result z_bytes_to_slice(IntPtr bytes, IntPtr dst);

        /// z_result_t
        /// z_bytes_to_string(
        ///     const struct z_loaned_bytes_t *this_,
        ///     struct z_owned_string_t *dst
        /// )
        [DllImport(DllName, EntryPoint = "z_bytes_to_string", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result z_bytes_to_string(IntPtr bytes, IntPtr dst);

        /// z_result_t
        /// z_bytes_writer_append(
        ///     struct z_loaned_bytes_writer_t *this_,
        ///     struct z_moved_bytes_t *bytes
        /// )
        [DllImport(DllName, EntryPoint = "z_bytes_writer_append", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result z_bytes_writer_append(IntPtr writer, IntPtr bytes);

        /// void
        /// z_bytes_writer_drop(
        ///     struct z_moved_bytes_writer_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_bytes_writer_drop", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_bytes_writer_drop(IntPtr writer);

        /// z_result_t
        /// z_bytes_writer_empty(
        ///     struct z_owned_bytes_writer_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_bytes_writer_empty", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result z_bytes_writer_empty(IntPtr writer);

        /// void
        /// z_bytes_writer_finish(
        ///     struct z_moved_bytes_writer_t *this_,
        ///     struct z_owned_bytes_t *bytes
        /// )
        [DllImport(DllName, EntryPoint = "z_bytes_writer_finish", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_bytes_writer_finish(IntPtr writer, IntPtr bytes);

        /// const struct z_loaned_bytes_writer_t*
        /// z_bytes_writer_loan(
        ///     const struct z_owned_bytes_writer_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_bytes_writer_loan", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_bytes_writer_loan(IntPtr writer);

        /// struct z_loaned_bytes_writer_t*
        /// z_bytes_writer_loan_mut(
        ///     struct z_owned_bytes_writer_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_bytes_writer_loan_mut", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_bytes_writer_loan_mut(IntPtr writer);

        /// z_result_t
        /// z_bytes_writer_write_all(
        ///     struct z_loaned_bytes_writer_t *this_,
        ///     const uint8_t *src,
        ///     size_t len
        /// )
        [DllImport(DllName, EntryPoint = "z_bytes_writer_write_all", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result z_bytes_writer_write_all(IntPtr writer, IntPtr src, UIntPtr len);

        [DllImport(DllName, EntryPoint = "z_clock_elapsed_s", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern ulong z_clock_elapsed_s(IntPtr time);

        [DllImport(DllName, EntryPoint = "z_clock_elapsed_ms", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern ulong z_clock_elapsed_ms(IntPtr time);

        [DllImport(DllName, EntryPoint = "z_clock_elapsed_us", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern ulong z_clock_elapsed_us(IntPtr time);

        [DllImport(DllName, EntryPoint = "z_clock_now", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern ZClock z_clock_now();

        [DllImport(DllName, EntryPoint = "z_close", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        internal static extern Result z_close(IntPtr session, IntPtr options);

        [DllImport(DllName, EntryPoint = "z_close_options_default", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_close_options_default(IntPtr options);

        /// void
        /// z_closure_hello(
        ///     struct z_owned_closure_hello_t *this_,
        ///     void (*call)(struct z_loaned_hello_t *hello, void *context),
        ///     void (*drop)(void *context),
        ///     void *context
        /// )
        [DllImport(DllName, EntryPoint = "z_closure_hello", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_closure_hello(IntPtr closure, Cb2 call, Cb1 drop, IntPtr context);

        [DllImport(DllName, EntryPoint = "z_closure_hello_call", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_closure_hello_call(IntPtr closure, IntPtr hello);

        /// void
        /// z_closure_hello_drop(
        ///     struct z_moved_closure_hello_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_closure_hello_drop", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_closure_hello_drop(IntPtr closure);

        [DllImport(DllName, EntryPoint = "z_closure_hello_loan", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_closure_hello_loan(IntPtr closure);

        [DllImport(DllName, EntryPoint = "z_closure_hello_loan_mut", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_closure_hello_loan_mut(IntPtr closure);

#if UNSTABLE_API
        /// void
        /// z_closure_matching_status(
        ///     struct z_owned_closure_matching_status_t *this_,
        ///     void (*call)(
        ///         const struct z_matching_status_t *matching_status,
        ///         void *context
        ///     ),
        ///     void (*drop)(
        ///         void *context
        ///     ),
        ///     void *context
        /// )
        [DllImport(DllName, EntryPoint = "z_closure_matching_status", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_closure_matching_status(IntPtr closure, Cb2 call, Cb1 drop, IntPtr context);
#endif

#if UNSTABLE_API
        /// void
        /// z_closure_matching_status_call(
        ///     const struct z_loaned_closure_matching_status_t *closure,
        ///     const struct z_matching_status_t *mathing_status
        /// )
        [DllImport(DllName, EntryPoint = "z_closure_matching_status_call", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_closure_matching_status_call(IntPtr closure, IntPtr mathing_status);
#endif

#if UNSTABLE_API
        /// void
        /// z_closure_matching_status_drop(
        ///     struct z_moved_closure_matching_status_t *closure_
        /// )
        [DllImport(DllName, EntryPoint = "z_closure_matching_status_drop", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_closure_matching_status_drop(IntPtr closure, IntPtr mathing_status);
#endif

#if UNSTABLE_API
        /// const struct z_loaned_closure_matching_status_t*
        /// z_closure_matching_status_loan(
        ///     const struct z_owned_closure_matching_status_t *closure
        /// )
        [DllImport(DllName, EntryPoint = "z_closure_matching_status_loan", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_closure_matching_status_loan(IntPtr closure);
#endif

        /// void
        /// z_closure_query(
        ///     struct z_owned_closure_query_t *this_,
        ///     void (*call)(struct z_loaned_query_t *query, void *context),
        ///     void (*drop)(void *context),
        ///     void *context
        /// )
        [DllImport(DllName, EntryPoint = "z_closure_query", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_closure_query(IntPtr closure, Cb2 call, Cb1 drop, IntPtr context);

        /// void
        /// z_closure_query_call(
        ///     const struct z_loaned_closure_query_t *closure,
        ///     struct z_loaned_query_t *query
        /// )
        [DllImport(DllName, EntryPoint = "z_closure_query_call", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_closure_query_call(IntPtr closure, IntPtr query);

        /// void
        /// z_closure_query_drop(
        ///     struct z_moved_closure_query_t *closure_
        /// )
        [DllImport(DllName, EntryPoint = "z_closure_query_drop", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_closure_query_drop(IntPtr closure);

        /// const struct z_loaned_closure_query_t*
        /// z_closure_query_loan(
        ///     const struct z_owned_closure_query_t *closure
        /// )
        [DllImport(DllName, EntryPoint = "z_closure_query_loan", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_closure_query_loan(IntPtr closure);

        /// struct z_loaned_closure_query_t*
        /// z_closure_query_loan_mut(
        ///     struct z_owned_closure_query_t *closure
        /// )
        [DllImport(DllName, EntryPoint = "z_closure_query_loan_mut", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_closure_query_loan_mut(IntPtr closure);

        /// void
        /// z_closure_reply(
        ///     struct z_owned_closure_reply_t *this_,
        ///     void (*call)(struct z_loaned_reply_t *reply, void *context),
        ///     void (*drop)(void *context),
        ///     void *context
        /// )
        [DllImport(DllName, EntryPoint = "z_closure_reply", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_closure_reply(IntPtr closure, IntPtr call, IntPtr drop, IntPtr context);

        /// void
        /// z_closure_reply_call(
        ///     const struct z_loaned_closure_reply_t *closure,
        ///     struct z_loaned_reply_t *reply
        /// )
        [DllImport(DllName, EntryPoint = "z_closure_reply_call", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_closure_reply_call(IntPtr closure, IntPtr reply);

        /// void
        /// z_closure_reply_drop(
        ///     struct z_moved_closure_reply_t *closure_
        /// )
        [DllImport(DllName, EntryPoint = "z_closure_reply_drop", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_closure_reply_drop(IntPtr closure);

        /// const struct z_loaned_closure_reply_t*
        /// z_closure_reply_loan(
        ///     const struct z_owned_closure_reply_t *closure
        /// )
        [DllImport(DllName, EntryPoint = "z_closure_reply_loan", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_closure_reply_loan(IntPtr closure);

        /// struct z_loaned_closure_reply_t*
        /// z_closure_reply_loan_mut(
        ///     struct z_owned_closure_reply_t *closure
        /// )
        [DllImport(DllName, EntryPoint = "z_closure_reply_loan_mut", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_closure_reply_loan_mut(IntPtr closure);

        /// void
        /// z_closure_sample(
        ///     struct z_owned_closure_sample_t *this_,
        ///     void (*call)(struct z_loaned_sample_t *sample, void *context),
        ///     void (*drop)(void *context),
        ///     void *context
        /// );
        [DllImport(DllName, EntryPoint = "z_closure_sample", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_closure_sample(IntPtr closure, Cb2 call, Cb1 drop, IntPtr context);

        /// void
        /// z_closure_sample_call(
        ///     const struct z_loaned_closure_sample_t *closure,
        ///     struct z_loaned_sample_t *sample
        /// )
        [DllImport(DllName, EntryPoint = "z_closure_sample_call", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_closure_sample_call(IntPtr closure, IntPtr sample);

        /// void
        /// z_closure_sample_drop(
        ///     struct z_moved_closure_sample_t *closure_
        /// )
        [DllImport(DllName, EntryPoint = "z_closure_sample_drop", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_closure_sample_drop(IntPtr closure);

        /// const struct z_loaned_closure_sample_t*
        /// z_closure_sample_loan(
        ///     const struct z_owned_closure_sample_t *closure
        /// )
        [DllImport(DllName, EntryPoint = "z_closure_sample_loan", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_closure_sample_loan(IntPtr closure);

        /// struct z_loaned_closure_sample_t*
        /// z_closure_sample_loan_mut(
        ///     struct z_owned_closure_sample_t *closure
        /// )
        [DllImport(DllName, EntryPoint = "z_closure_sample_loan_mut", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_closure_sample_loan_mut(IntPtr closure);

        /// void
        /// z_closure_zid(
        ///     struct z_owned_closure_zid_t *this_,
        ///     void (*call)(const struct z_id_t *z_id, void *context),
        ///     void (*drop)(void *context),
        ///     void *context
        /// )
        [DllImport(DllName, EntryPoint = "z_closure_zid", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_closure_zid(IntPtr closure, Cb2 call, Cb1 drop, IntPtr context);

        /// void
        /// z_closure_zid_call(
        ///     const struct z_loaned_closure_zid_t *closure,
        ///     const struct z_id_t *z_id
        /// )
        [DllImport(DllName, EntryPoint = "z_closure_zid_call", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_closure_zid_call(IntPtr closure, IntPtr zId);

        /// void
        /// z_closure_zid_drop(
        ///     struct z_moved_closure_zid_t *closure_
        /// )
        [DllImport(DllName, EntryPoint = "z_closure_zid_drop", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_closure_zid_drop(IntPtr closure);

        /// const struct z_loaned_closure_zid_t*
        /// z_closure_zid_loan(
        ///     const struct z_owned_closure_zid_t *closure
        /// )
        [DllImport(DllName, EntryPoint = "z_closure_zid_loan", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_closure_zid_loan(IntPtr closure);

        /// const struct z_loaned_closure_zid_t*
        /// z_closure_zid_loan_mut(
        ///     const struct z_owned_closure_zid_t *closure
        /// )
        [DllImport(DllName, EntryPoint = "z_closure_zid_loan_mut", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_closure_zid_loan_mut(IntPtr closure);

        /// void
        /// z_condvar_drop(
        ///     struct z_moved_condvar_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_condvar_drop", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_condvar_drop(IntPtr condvar);

        [DllImport(DllName, EntryPoint = "z_condvar_init", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_condvar_init(IntPtr condvar);

        [DllImport(DllName, EntryPoint = "z_condvar_loan", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_condvar_loan(IntPtr condvar);

        [DllImport(DllName, EntryPoint = "z_condvar_loan_mut", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_condvar_loan_mut(IntPtr condvar);

        [DllImport(DllName, EntryPoint = "z_condvar_signal", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result z_condvar_signal(IntPtr condvar);

        [DllImport(DllName, EntryPoint = "z_condvar_wait", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result z_condvar_wait(IntPtr condvar, IntPtr mutex);

        /// void
        /// z_config_clone(
        ///     struct z_owned_config_t *dst,
        ///     const struct z_loaned_config_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_config_clone", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_config_clone(IntPtr dst, IntPtr src);

        /// z_result_t
        /// z_config_default(
        ///     struct z_owned_config_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_config_default", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern Result z_config_default(IntPtr config);

        /// void
        /// z_config_drop(
        ///     struct z_moved_config_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_config_drop", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_config_drop(IntPtr config);

        /// const struct z_loaned_config_t*
        /// z_config_loan(
        ///     const struct z_owned_config_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_config_loan", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_config_loan(IntPtr config);

        /// struct z_loaned_config_t*
        /// z_config_loan_mut(
        ///     struct z_owned_config_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_config_loan_mut", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_config_loan_mut(IntPtr config);

        [DllImport(DllName, EntryPoint = "z_declare_keyexpr", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result z_declare_keyexpr(
            IntPtr session,
            IntPtr declaredKeyexpr,
            IntPtr keyexpr
        );

        /// z_result_t
        /// z_declare_publisher(
        ///     const struct z_loaned_session_t *session,
        ///     struct z_owned_publisher_t *publisher,
        ///     const struct z_loaned_keyexpr_t *key_expr,
        ///     struct z_publisher_options_t *options
        /// )
        [DllImport(DllName, EntryPoint = "z_declare_publisher", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result z_declare_publisher(IntPtr session, IntPtr publisher, IntPtr keyexpr,
            IntPtr options);

        /// z_result_t
        /// z_declare_queryable(
        ///     const struct z_loaned_session_t *session,
        ///     struct z_owned_queryable_t *queryable,
        ///     const struct z_loaned_keyexpr_t *key_expr,
        ///     struct z_moved_closure_query_t *callback,
        ///     struct z_queryable_options_t *options
        /// )
        [DllImport(DllName, EntryPoint = "z_declare_queryable", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result z_declare_queryable(IntPtr session, IntPtr queryable, IntPtr keyexpr,
            IntPtr callback,
            IntPtr options);

        /// z_result_t
        /// z_declare_subscriber(
        ///     const struct z_loaned_session_t *session,
        ///     struct z_owned_subscriber_t *subscriber,
        ///     const struct z_loaned_keyexpr_t *key_expr,
        ///     struct z_moved_closure_sample_t *callback,
        ///     struct z_subscriber_options_t *options
        /// )
        [DllImport(DllName, EntryPoint = "z_declare_subscriber", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result z_declare_subscriber(IntPtr session, IntPtr subscriber, IntPtr keyexpr,
            IntPtr callback,
            IntPtr options);

#if UNSTABLE_API
        /// z_result_t
        /// z_declare_querier(
        ///     const struct z_loaned_session_t *session,
        ///     struct z_owned_querier_t *querier,
        ///     const struct z_loaned_keyexpr_t *key_expr,
        ///     struct z_querier_options_t *options
        /// )
        [DllImport(DllName, EntryPoint = "z_declare_querier", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result z_declare_querier(IntPtr session, IntPtr querier, IntPtr keyexpr, IntPtr options);
#endif

        /// z_result_t
        /// z_delete(
        ///     const struct z_loaned_session_t *session,
        ///     const struct z_loaned_keyexpr_t *key_expr,
        ///     struct z_delete_options_t *options
        /// )
        [DllImport(DllName, EntryPoint = "z_delete", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result z_delete(IntPtr session, IntPtr keyexpr, IntPtr options);

        /// void
        /// z_delete_options_default(
        ///     struct z_delete_options_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_delete_options_default", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_delete_options_default(IntPtr options);

        /// const struct z_loaned_encoding_t*
        /// z_encoding_application_cbor(
        ///     void
        /// )
        [DllImport(DllName, EntryPoint = "z_encoding_application_cbor", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_encoding_application_cbor();

        /// const struct z_loaned_encoding_t*
        /// z_encoding_application_cdr(
        ///     void
        /// )
        [DllImport(DllName, EntryPoint = "z_encoding_application_cdr", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_encoding_application_cdr();

        /// const struct z_loaned_encoding_t*
        /// z_encoding_application_coap_payload(
        ///     void
        /// )
        [DllImport(DllName, EntryPoint = "z_encoding_application_coap_payload",
            CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_encoding_application_coap_payload();

        /// const struct z_loaned_encoding_t*
        /// z_encoding_application_java_serialized_object(
        ///     void
        /// )
        [DllImport(DllName, EntryPoint = "z_encoding_application_java_serialized_object",
            CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_encoding_application_java_serialized_object();

        /// const struct z_loaned_encoding_t*
        /// z_encoding_application_json(
        ///     void
        /// )
        [DllImport(DllName, EntryPoint = "z_encoding_application_json", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_encoding_application_json();

        /// const struct z_loaned_encoding_t*
        /// z_encoding_application_json_patch_json(
        ///     void
        /// )
        [DllImport(DllName, EntryPoint = "z_encoding_application_json_patch_json",
            CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_encoding_application_json_patch_json();

        /// const struct z_loaned_encoding_t*
        /// z_encoding_application_json_seq(
        ///     void
        /// )
        [DllImport(DllName, EntryPoint = "z_encoding_application_json_seq", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_encoding_application_json_seq();

        /// const struct z_loaned_encoding_t*
        /// z_encoding_application_jsonpath(
        ///     void
        /// )
        [DllImport(DllName, EntryPoint = "z_encoding_application_jsonpath", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_encoding_application_jsonpath();

        /// const struct z_loaned_encoding_t*
        /// z_encoding_application_jwt(
        ///     void
        /// )
        [DllImport(DllName, EntryPoint = "z_encoding_application_jwt", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_encoding_application_jwt();

        /// const struct z_loaned_encoding_t*
        /// z_encoding_application_mp4(
        ///     void
        /// )
        [DllImport(DllName, EntryPoint = "z_encoding_application_mp4", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_encoding_application_mp4();

        /// const struct z_loaned_encoding_t*
        /// z_encoding_application_octet_stream(
        ///     void
        /// )
        [DllImport(DllName, EntryPoint = "z_encoding_application_octet_stream",
            CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_encoding_application_octet_stream();

        /// const struct z_loaned_encoding_t*
        /// z_encoding_application_openmetrics_text(
        ///     void
        /// )
        [DllImport(DllName, EntryPoint = "z_encoding_application_openmetrics_text",
            CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_encoding_application_openmetrics_text();

        /// const struct z_loaned_encoding_t*
        /// z_encoding_application_protobuf(
        ///     void
        /// )
        [DllImport(DllName, EntryPoint = "z_encoding_application_protobuf", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_encoding_application_protobuf();

        /// const struct z_loaned_encoding_t*
        /// z_encoding_application_python_serialized_object(
        ///     void
        /// )
        [DllImport(DllName, EntryPoint = "z_encoding_application_python_serialized_object",
            CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_encoding_application_python_serialized_object();

        /// const struct z_loaned_encoding_t*
        /// z_encoding_application_soap_xml(
        ///     void
        /// )
        [DllImport(DllName, EntryPoint = "z_encoding_application_soap_xml", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_encoding_application_soap_xml();

        /// const struct z_loaned_encoding_t*
        /// z_encoding_application_sql(
        ///     void
        /// )
        [DllImport(DllName, EntryPoint = "z_encoding_application_sql", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_encoding_application_sql();

        /// const struct z_loaned_encoding_t*
        /// z_encoding_application_x_www_form_urlencoded(
        ///     void
        /// )
        [DllImport(DllName, EntryPoint = "z_encoding_application_x_www_form_urlencoded",
            CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_encoding_application_x_www_form_urlencoded();

        /// const struct z_loaned_encoding_t*
        /// z_encoding_application_xml(
        ///     void
        /// )
        [DllImport(DllName, EntryPoint = "z_encoding_application_xml", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_encoding_application_xml();

        /// const struct z_loaned_encoding_t*
        /// z_encoding_application_yaml(
        ///     void
        /// )
        [DllImport(DllName, EntryPoint = "z_encoding_application_yaml", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_encoding_application_yaml();

        /// const struct z_loaned_encoding_t*
        /// z_encoding_application_yang(
        ///     void
        /// )
        [DllImport(DllName, EntryPoint = "z_encoding_application_yang", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_encoding_application_yang();

        /// const struct z_loaned_encoding_t*
        /// z_encoding_audio_aac(
        ///     void
        /// )
        [DllImport(DllName, EntryPoint = "z_encoding_audio_aac", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_encoding_audio_aac();

        /// const struct z_loaned_encoding_t*
        /// z_encoding_audio_flac(
        ///     void
        /// )
        [DllImport(DllName, EntryPoint = "z_encoding_audio_flac", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_encoding_audio_flac();

        /// const struct z_loaned_encoding_t*
        /// z_encoding_audio_mp4(
        ///     void
        /// )
        [DllImport(DllName, EntryPoint = "z_encoding_audio_mp4", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_encoding_audio_mp4();

        /// const struct z_loaned_encoding_t*
        /// z_encoding_audio_ogg(
        ///     void
        /// )
        [DllImport(DllName, EntryPoint = "z_encoding_audio_ogg", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_encoding_audio_ogg();

        /// const struct z_loaned_encoding_t*
        /// z_encoding_audio_vorbis(
        ///     void
        /// )
        [DllImport(DllName, EntryPoint = "z_encoding_audio_vorbis", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_encoding_audio_vorbis();

        /// const struct z_loaned_encoding_t*
        /// z_encoding_image_bmp(
        ///     void
        /// )
        [DllImport(DllName, EntryPoint = "z_encoding_image_bmp", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_encoding_image_bmp();

        /// const struct z_loaned_encoding_t*
        /// z_encoding_image_gif(
        ///     void
        /// )
        [DllImport(DllName, EntryPoint = "z_encoding_image_gif", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_encoding_image_gif();

        /// const struct z_loaned_encoding_t*
        /// z_encoding_image_jpeg(
        ///     void
        /// )
        [DllImport(DllName, EntryPoint = "z_encoding_image_jpeg", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_encoding_image_jpeg();

        /// const struct z_loaned_encoding_t*
        /// z_encoding_image_png(
        ///     void
        /// )
        [DllImport(DllName, EntryPoint = "z_encoding_image_png", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_encoding_image_png();

        /// const struct z_loaned_encoding_t*
        /// z_encoding_image_webp(
        ///     void
        /// )
        [DllImport(DllName, EntryPoint = "z_encoding_image_webp", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_encoding_image_webp();

        /// const struct z_loaned_encoding_t*
        /// z_encoding_text_css(
        ///     void
        /// )
        [DllImport(DllName, EntryPoint = "z_encoding_text_css", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_encoding_text_css();

        /// const struct z_loaned_encoding_t*
        /// z_encoding_text_csv(
        ///     void
        /// )
        [DllImport(DllName, EntryPoint = "z_encoding_text_csv", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_encoding_text_csv();

        /// const struct z_loaned_encoding_t*
        /// z_encoding_text_html(
        ///     void
        /// )
        [DllImport(DllName, EntryPoint = "z_encoding_text_html", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_encoding_text_html();

        /// const struct z_loaned_encoding_t*
        /// z_encoding_text_javascript(
        ///     void
        /// )
        [DllImport(DllName, EntryPoint = "z_encoding_text_javascript", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_encoding_text_javascript();

        /// const struct z_loaned_encoding_t*
        /// z_encoding_text_json(
        ///     void
        /// )
        [DllImport(DllName, EntryPoint = "z_encoding_text_json", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_encoding_text_json();

        /// const struct z_loaned_encoding_t*
        /// z_encoding_text_json5(
        ///     void
        /// )
        [DllImport(DllName, EntryPoint = "z_encoding_text_json5", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_encoding_text_json5();

        /// const struct z_loaned_encoding_t*
        /// z_encoding_text_markdown(
        ///     void
        /// )
        [DllImport(DllName, EntryPoint = "z_encoding_text_markdown", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_encoding_text_markdown();

        /// const struct z_loaned_encoding_t*
        /// z_encoding_text_plain(
        ///     void
        /// )
        [DllImport(DllName, EntryPoint = "z_encoding_text_plain", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_encoding_text_plain();

        /// const struct z_loaned_encoding_t*
        /// z_encoding_text_xml(
        ///     void
        /// )
        [DllImport(DllName, EntryPoint = "z_encoding_text_xml", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_encoding_text_xml();

        /// const struct z_loaned_encoding_t*
        /// z_encoding_text_yaml(
        ///     void
        /// )
        [DllImport(DllName, EntryPoint = "z_encoding_text_yaml", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_encoding_text_yaml();

        /// const struct z_loaned_encoding_t*
        /// z_encoding_video_h261(
        ///     void
        /// )
        [DllImport(DllName, EntryPoint = "z_encoding_video_h261", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_encoding_video_h261();

        /// const struct z_loaned_encoding_t*
        /// z_encoding_video_h263(
        ///     void
        /// )
        [DllImport(DllName, EntryPoint = "z_encoding_video_h263", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_encoding_video_h263();

        /// const struct z_loaned_encoding_t*
        /// z_encoding_video_h264(
        ///     void
        /// )
        [DllImport(DllName, EntryPoint = "z_encoding_video_h264", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_encoding_video_h264();

        /// const struct z_loaned_encoding_t*
        /// z_encoding_video_h265(
        ///     void
        /// )
        [DllImport(DllName, EntryPoint = "z_encoding_video_h265", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_encoding_video_h265();

        /// const struct z_loaned_encoding_t*
        /// z_encoding_video_h266(
        ///     void
        /// )
        [DllImport(DllName, EntryPoint = "z_encoding_video_h266", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_encoding_video_h266();

        /// const struct z_loaned_encoding_t*
        /// z_encoding_video_mp4(
        ///     void
        /// )
        [DllImport(DllName, EntryPoint = "z_encoding_video_mp4", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_encoding_video_mp4();

        /// const struct z_loaned_encoding_t*
        /// z_encoding_video_ogg(
        ///     void
        /// )
        [DllImport(DllName, EntryPoint = "z_encoding_video_ogg", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_encoding_video_ogg();

        /// const struct z_loaned_encoding_t*
        /// z_encoding_video_raw(
        ///     void
        /// )
        [DllImport(DllName, EntryPoint = "z_encoding_video_raw", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_encoding_video_raw();

        /// const struct z_loaned_encoding_t *
        /// z_encoding_video_vp8(
        ///     void
        /// )
        [DllImport(DllName, EntryPoint = "z_encoding_video_vp8", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_encoding_video_vp8();

        /// const struct z_loaned_encoding_t *
        /// z_encoding_video_vp9(
        ///     void
        /// )
        [DllImport(DllName, EntryPoint = "z_encoding_video_vp9", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_encoding_video_vp9();

        /// const struct z_loaned_encoding_t*
        /// z_encoding_zenoh_bytes(
        ///     void
        /// )
        [DllImport(DllName, EntryPoint = "z_encoding_zenoh_bytes", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_encoding_zenoh_bytes();

        /// const struct z_loaned_encoding_t*
        /// z_encoding_zenoh_serialized(
        ///     void
        /// )
        [DllImport(DllName, EntryPoint = "z_encoding_zenoh_serialized", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_encoding_zenoh_serialized();

        /// const struct z_loaned_encoding_t*
        /// z_encoding_zenoh_string(
        ///     void
        /// )
        [DllImport(DllName, EntryPoint = "z_encoding_zenoh_string", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_encoding_zenoh_string();

        /// void
        /// z_encoding_clone(
        ///     struct z_owned_encoding_t *dst,
        ///     const struct z_loaned_encoding_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_encoding_clone", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_encoding_clone(IntPtr dst, IntPtr src);

        /// void
        /// z_encoding_drop(
        ///     struct z_moved_encoding_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_encoding_drop", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_encoding_drop(IntPtr encoding);

        /// bool
        /// z_encoding_equals(
        ///     const struct z_loaned_encoding_t *this_,
        ///     const struct z_loaned_encoding_t *other
        /// )
        [DllImport(DllName, EntryPoint = "z_encoding_equals", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool z_encoding_equals(IntPtr encoding, IntPtr other);

        [DllImport(DllName, EntryPoint = "z_encoding_from_str", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result z_encoding_from_str(IntPtr encoding, IntPtr s);

        [DllImport(DllName, EntryPoint = "z_encoding_from_substr", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result z_encoding_from_substr(IntPtr encoding, IntPtr s, UIntPtr len);

        /// const struct z_loaned_encoding_t*
        /// z_encoding_loan_default(
        ///     void
        /// )
        [DllImport(DllName, EntryPoint = "z_encoding_loan_default", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_encoding_loan_default();

        /// const struct z_loaned_encoding_t*
        /// z_encoding_loan(
        ///     const struct z_owned_encoding_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_encoding_loan", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_encoding_loan(IntPtr encoding);

        /// struct z_loaned_encoding_t*
        /// z_encoding_loan_mut(
        ///     struct z_owned_encoding_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_encoding_loan_mut", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_encoding_loan_mut(IntPtr encoding);

        /// z_result_t
        /// z_encoding_set_schema_from_str(
        ///     struct z_loaned_encoding_t *this_,
        ///     const char *s
        /// )
        [DllImport(DllName, EntryPoint = "z_encoding_set_schema_from_str", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result z_encoding_set_schema_from_str(IntPtr encoding, IntPtr s);

        /// z_result_t
        /// z_encoding_set_schema_from_substr(
        ///     struct z_loaned_encoding_t *this_,
        ///     const char *s,
        ///     size_t len
        /// )
        [DllImport(DllName, EntryPoint = "z_encoding_set_schema_from_substr",
            CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result z_encoding_set_schema_from_substr(IntPtr encoding, IntPtr s, UIntPtr len);

        /// void
        /// z_encoding_to_string(
        ///     const struct z_loaned_encoding_t *this_,
        ///     struct z_owned_string_t *out_str
        /// )
        [DllImport(DllName, EntryPoint = "z_encoding_to_string", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_encoding_to_string(IntPtr encoding, IntPtr outStr);

#if UNSTABLE_API
        /// uint32_t 
        /// z_entity_global_id_eid(
        ///     const struct z_entity_global_id_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_entity_global_id_eid", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern uint z_entity_global_id_eid(IntPtr entity);
#endif

#if UNSTABLE_API
        /// z_id_t 
        /// z_entity_global_id_zid(
        ///     const struct z_entity_global_id_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_entity_global_id_zid", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern ZId z_entity_global_id_zid(IntPtr entity);
#endif


        /// void
        /// z_fifo_channel_query_new(
        ///     struct z_owned_closure_query_t *callback,
        ///     struct z_owned_fifo_handler_query_t *handler,
        ///     size_t capacity
        /// )
        [DllImport(DllName, EntryPoint = "z_fifo_channel_query_new", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_fifo_channel_query_new(IntPtr callback, IntPtr handler, UIntPtr capacity);

        /// void
        /// z_fifo_handler_query_drop(
        ///     struct z_moved_fifo_handler_query_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_fifo_handler_query_drop", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_fifo_handler_query_drop(IntPtr handler);

        /// const struct z_loaned_fifo_handler_query_t*
        /// z_fifo_handler_query_loan(
        ///     const struct z_owned_fifo_handler_query_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_fifo_handler_query_loan", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_fifo_handler_query_loan(IntPtr handler);

        /// z_result_t
        /// z_fifo_handler_query_recv(
        ///     const struct z_loaned_fifo_handler_query_t *this_,
        ///     struct z_owned_query_t *query
        /// )
        [DllImport(DllName, EntryPoint = "z_fifo_handler_query_recv", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result z_fifo_handler_query_recv(IntPtr handler, IntPtr query);

        /// z_result_t
        /// z_fifo_handler_query_try_recv(
        ///     const struct z_loaned_fifo_handler_query_t *this_,
        ///     struct z_owned_query_t *query
        /// )
        [DllImport(DllName, EntryPoint = "z_fifo_handler_query_try_recv", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result z_fifo_handler_query_try_recv(IntPtr handler, IntPtr query);

        /// void
        /// z_fifo_channel_reply_new(
        ///     struct z_owned_closure_reply_t *callback,
        ///     struct z_owned_fifo_handler_reply_t *handler,
        ///     size_t capacity
        /// )
        [DllImport(DllName, EntryPoint = "z_fifo_channel_reply_new", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_fifo_channel_reply_new(IntPtr callback, IntPtr handler, UIntPtr capacity);

        /// void
        /// z_fifo_handler_reply_drop(
        ///     struct z_moved_fifo_handler_reply_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_fifo_handler_reply_drop", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_fifo_handler_reply_drop(IntPtr handler);

        /// const struct z_loaned_fifo_handler_reply_t*
        /// z_fifo_handler_reply_loan(
        ///     const struct z_owned_fifo_handler_reply_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_fifo_handler_reply_loan", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_fifo_handler_reply_loan(IntPtr handler);

        /// z_result_t
        /// z_fifo_handler_reply_recv(
        ///     const struct z_loaned_fifo_handler_reply_t *this_,
        ///     struct z_owned_reply_t *reply
        /// )
        [DllImport(DllName, EntryPoint = "z_fifo_handler_reply_recv", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result z_fifo_handler_reply_recv(IntPtr handler, IntPtr reply);

        /// z_result_t
        /// z_fifo_handler_reply_try_recv(
        ///     const struct z_loaned_fifo_handler_reply_t *this_,
        ///     struct z_owned_reply_t *reply
        /// )
        [DllImport(DllName, EntryPoint = "z_fifo_handler_reply_try_recv", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result z_fifo_handler_reply_try_recv(IntPtr handler, IntPtr reply);

        /// void
        /// z_fifo_channel_sample_new(
        ///     struct z_owned_closure_sample_t *callback,
        ///     struct z_owned_fifo_handler_sample_t *handler,
        ///     size_t capacity
        /// )
        [DllImport(DllName, EntryPoint = "z_fifo_channel_sample_new", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_fifo_channel_sample_new(IntPtr callback, IntPtr handler, UIntPtr capacity);

        /// void
        /// z_fifo_handler_sample_drop(
        ///     struct z_moved_fifo_handler_sample_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_fifo_handler_sample_drop", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_fifo_handler_sample_drop(IntPtr handler);

        /// const struct z_loaned_fifo_handler_sample_t*
        /// z_fifo_handler_sample_loan(
        ///     const struct z_owned_fifo_handler_sample_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_fifo_handler_sample_loan", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_fifo_handler_sample_loan(IntPtr handler);

        /// z_result_t
        /// z_fifo_handler_sample_recv(
        ///     const struct z_loaned_fifo_handler_sample_t *this_,
        ///     struct z_owned_sample_t *sample
        /// )
        [DllImport(DllName, EntryPoint = "z_fifo_handler_sample_recv", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result z_fifo_handler_sample_recv(IntPtr handler, IntPtr sample);

        /// z_result_t
        /// z_fifo_handler_sample_try_recv(
        ///     const struct z_loaned_fifo_handler_sample_t *this_,
        ///     struct z_owned_sample_t *sample
        /// )
        [DllImport(DllName, EntryPoint = "z_fifo_handler_sample_try_recv", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result z_fifo_handler_sample_try_recv(IntPtr handler, IntPtr sample);

        /// z_result_t
        /// z_get(
        ///     const struct z_loaned_session_t *session,
        ///     const struct z_loaned_keyexpr_t *key_expr,
        ///     const char *parameters,
        ///     struct z_moved_closure_reply_t *callback,
        ///     struct z_get_options_t *options
        /// )
        [DllImport(DllName, EntryPoint = "z_get", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        internal static extern Result z_get(
            IntPtr session,
            IntPtr keyexpr,
            IntPtr parameters,
            IntPtr callback,
            IntPtr options
        );

        /// void
        /// z_get_options_default(
        ///     struct z_get_options_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_get_options_default", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_get_options_default(IntPtr options);

        [DllImport(DllName, EntryPoint = "z_hello_clone", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_hello_clone(IntPtr dst, IntPtr src);

        /// void
        /// z_hello_drop(
        ///     struct z_moved_hello_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_hello_drop", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_hello_drop(IntPtr hello);

        [DllImport(DllName, EntryPoint = "z_hello_loan", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_hello_loan(IntPtr hello);

        [DllImport(DllName, EntryPoint = "z_hello_locators", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_hello_locators(IntPtr hello, IntPtr locatorsOut);

        [DllImport(DllName, EntryPoint = "z_hello_whatami", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Whatami z_hello_whatami(IntPtr hello);

        [DllImport(DllName, EntryPoint = "z_hello_zid", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern ZId z_hello_zid(IntPtr hello);

        /// void
        /// z_id_to_string(
        ///     const struct z_id_t *zid,
        ///     struct z_owned_string_t *dst
        /// )
        [DllImport(DllName, EntryPoint = "z_id_to_string", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_id_to_string(IntPtr id, IntPtr dst);

        /// z_result_t
        /// z_info_peers_zid(
        ///     const struct z_loaned_session_t *session,
        ///     struct z_moved_closure_zid_t *callback
        /// )
        [DllImport(DllName, EntryPoint = "z_info_peers_zid", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result z_info_peers_zid(IntPtr session, IntPtr callback);

        /// z_result_t
        /// z_info_routers_zid(
        ///     const struct z_loaned_session_t *session,
        ///     struct z_moved_closure_zid_t *callback
        /// )
        [DllImport(DllName, EntryPoint = "z_info_routers_zid", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result z_info_routers_zid(IntPtr session, IntPtr callback);

        /// struct z_id_t
        /// z_info_zid(
        ///     const struct z_loaned_session_t *session
        /// )
        [DllImport(DllName, EntryPoint = "z_info_zid", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern ZId z_info_zid(IntPtr session);

        [DllImport(DllName, EntryPoint = "z_internal_bytes_check", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool z_internal_bytes_check(IntPtr b);

        [DllImport(DllName, EntryPoint = "z_internal_bytes_null", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_internal_bytes_null(IntPtr b);

        [DllImport(DllName, EntryPoint = "z_internal_bytes_writer_check", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool z_internal_bytes_writer_check(IntPtr writer);

        [DllImport(DllName, EntryPoint = "z_internal_bytes_writer_null", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_internal_bytes_writer_null(IntPtr writer);

        [DllImport(DllName, EntryPoint = "z_internal_closure_hello_check", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool z_internal_closure_hello_check(IntPtr hello);

        [DllImport(DllName, EntryPoint = "z_internal_closure_hello_null", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_internal_closure_hello_null(IntPtr hello);

#if UNSTABLE_API
        /// bool
        /// z_internal_closure_matching_status_check(
        ///     const struct z_owned_closure_matching_status_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_internal_closure_matching_status_check",
            CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool z_internal_closure_matching_status_check(IntPtr matching_status);
#endif

#if UNSTABLE_API
        /// void
        /// z_internal_closure_matching_status_null(
        ///     struct z_owned_closure_matching_status_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_internal_closure_matching_status_null",
            CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_internal_closure_matching_status_null(IntPtr matching_status);
#endif

        [DllImport(DllName, EntryPoint = "z_internal_closure_query_check", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool z_internal_closure_query_check(IntPtr query);

        [DllImport(DllName, EntryPoint = "z_internal_closure_query_null", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_internal_closure_query_null(IntPtr query);

        [DllImport(DllName, EntryPoint = "z_internal_closure_reply_check", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool z_internal_closure_reply_check(IntPtr reply);

        [DllImport(DllName, EntryPoint = "z_internal_closure_reply_null", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_internal_closure_reply_null(IntPtr reply);

        [DllImport(DllName, EntryPoint = "z_internal_closure_sample_check", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool z_internal_closure_sample_check(IntPtr sample);

        [DllImport(DllName, EntryPoint = "z_internal_closure_sample_null", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_internal_closure_sample_null(IntPtr sample);

        [DllImport(DllName, EntryPoint = "z_internal_closure_zid_check", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool z_internal_closure_zid_check(IntPtr zid);

        [DllImport(DllName, EntryPoint = "z_internal_closure_zid_null", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_internal_closure_zid_null(IntPtr zid);

        [DllImport(DllName, EntryPoint = "z_internal_condvar_check", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool z_internal_condvar_check(IntPtr condvar);

        [DllImport(DllName, EntryPoint = "z_internal_condvar_null", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_internal_condvar_null(IntPtr condvar);

        /// bool
        /// z_internal_config_check(
        ///     const struct z_owned_config_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_internal_config_check", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool z_internal_config_check(IntPtr config);

        /// void
        /// z_internal_config_null(
        ///     struct z_owned_config_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_internal_config_null", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_internal_config_null(IntPtr config);

        /// bool
        /// z_internal_encoding_check(
        ///     const struct z_owned_encoding_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_internal_encoding_check", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool z_internal_encoding_check(IntPtr encoding);

        /// void
        /// z_internal_encoding_null(
        ///     struct z_owned_encoding_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_internal_encoding_null", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_internal_encoding_null(IntPtr encoding);

        /// bool
        /// z_internal_fifo_handler_query_check(
        ///     const struct z_owned_fifo_handler_query_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_internal_fifo_handler_query_check",
            CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool z_internal_fifo_handler_query_check(IntPtr handler);

        /// void
        /// z_internal_fifo_handler_query_null(
        ///     struct z_owned_fifo_handler_query_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_internal_fifo_handler_query_null",
            CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_internal_fifo_handler_query_null(IntPtr handler);

        /// bool
        /// z_internal_fifo_handler_reply_check(
        ///     const struct z_owned_fifo_handler_reply_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_internal_fifo_handler_reply_check",
            CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool z_internal_fifo_handler_reply_check(IntPtr handler);

        /// void
        /// z_internal_fifo_handler_reply_null(
        ///     struct z_owned_fifo_handler_reply_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_internal_fifo_handler_reply_null",
            CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_internal_fifo_handler_reply_null(IntPtr handler);

        /// bool
        /// z_internal_fifo_handler_sample_check(
        ///     const struct z_owned_fifo_handler_sample_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_internal_fi/fo_handler_sample_check",
            CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool z_internal_fifo_handler_sample_check(IntPtr handler);

        /// void
        /// z_internal_fifo_handler_sample_null(
        ///     struct z_owned_fifo_handler_sample_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_internal_fifo_handler_sample_null",
            CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_internal_fifo_handler_sample_null(IntPtr handler);

        [DllImport(DllName, EntryPoint = "z_internal_hello_check", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool z_internal_hello_check(IntPtr hello);

        [DllImport(DllName, EntryPoint = "z_internal_hello_null", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_internal_hello_null(IntPtr hello);

        /// bool
        /// z_internal_keyexpr_check(
        ///     const struct z_owned_keyexpr_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_internal_keyexpr_check", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool z_internal_keyexpr_check(IntPtr keyexpr);

        /// void
        /// z_internal_keyexpr_null(
        ///     struct z_owned_keyexpr_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_internal_keyexpr_null", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_internal_keyexpr_null(IntPtr keyexpr);

        /// bool
        /// z_internal_liveliness_token_check(
        ///     const struct z_owned_liveliness_token_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_internal_liveliness_token_check",
            CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool z_internal_liveliness_token_check(IntPtr token);

        /// void
        /// z_internal_liveliness_token_null(
        ///     struct z_owned_liveliness_token_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_internal_liveliness_token_null",
            CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_internal_liveliness_token_null(IntPtr token);

#if UNSTABLE_API
        /// bool 
        /// z_internal_matching_listener_check(
        ///     const struct z_owned_matching_listener_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_internal_matching_listener_check",
            CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool z_internal_matching_listener_check(IntPtr matching_listener);
#endif

#if UNSTABLE_API
        /// void  
        /// z_internal_matching_listener_null(
        ///     struct z_owned_matching_listener_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_internal_matching_listener_null",
            CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_internal_matching_listener_null(IntPtr matching_listener);
#endif

        [DllImport(DllName, EntryPoint = "z_internal_mutex_check", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool z_internal_mutex_check(IntPtr mutex);

        [DllImport(DllName, EntryPoint = "z_internal_mutex_null", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_internal_mutex_null(IntPtr mutex);

#if UNSTABLE_API
        /// bool
        /// z_internal_querier_check(
        ///     const struct z_owned_querier_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_internal_querier_check", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool z_internal_querier_check(IntPtr querier);
#endif

#if UNSTABLE_API
        /// void
        /// z_internal_querier_null(
        ///     struct z_owned_querier_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_internal_querier_null", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_internal_querier_null(IntPtr querier);
#endif

        /// bool
        /// z_internal_publisher_check(
        ///     const struct z_owned_publisher_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_internal_publisher_check", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool z_internal_publisher_check(IntPtr publisher);

        /// void
        /// z_internal_publisher_null(
        ///     struct z_owned_publisher_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_internal_publisher_null", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_internal_publisher_null(IntPtr publisher);

        /// bool
        /// z_internal_query_check(
        ///     const struct z_owned_query_t *query
        /// )
        [DllImport(DllName, EntryPoint = "z_internal_query_check", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool z_internal_query_check(IntPtr query);

        /// void
        /// z_internal_query_null(
        ///     struct z_owned_query_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_internal_query_null", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_internal_query_null(IntPtr query);

        /// bool
        /// z_internal_queryable_check(
        ///     const struct z_owned_queryable_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_internal_queryable_check", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool z_internal_queryable_check(IntPtr queryable);

        /// void
        /// z_internal_queryable_null(
        ///     struct z_owned_queryable_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_internal_queryable_null", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_internal_queryable_null(IntPtr queryable);

        /// bool
        /// z_internal_reply_check(
        ///     const struct z_owned_reply_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_internal_reply_check", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool z_internal_reply_check(IntPtr reply);

        /// void
        /// z_internal_reply_null(
        ///     struct z_owned_reply_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_internal_reply_null", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_internal_reply_null(IntPtr reply);

        /// bool
        /// z_internal_reply_err_check(
        ///     const struct z_owned_reply_err_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_internal_reply_err_check", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool z_internal_reply_err_check(IntPtr replyErr);

        /// void
        /// z_internal_reply_err_null(
        ///     struct z_owned_reply_err_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_internal_reply_err_null", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_internal_reply_err_null(IntPtr replyErr);

        /// bool
        /// z_internal_ring_handler_query_check(
        ///     const struct z_owned_ring_handler_query_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_internal_ring_handler_query_check",
            CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool z_internal_ring_handler_query_check(IntPtr handler);

        /// void
        /// z_internal_ring_handler_query_null(
        ///     struct z_owned_ring_handler_query_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_internal_ring_handler_query_null",
            CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_internal_ring_handler_query_null(IntPtr handler);

        /// bool
        /// z_internal_ring_handler_reply_check(
        ///     const struct z_owned_ring_handler_reply_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_internal_ring_handler_reply_check",
            CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool z_internal_ring_handler_reply_check(IntPtr handler);

        /// void
        /// z_internal_ring_handler_reply_null(
        ///     struct z_owned_ring_handler_reply_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_internal_ring_handler_reply_null",
            CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_internal_ring_handler_reply_null(IntPtr handler);

        /// bool
        /// z_internal_ring_handler_sample_check(
        ///     const struct z_owned_ring_handler_sample_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_internal_ring_handler_sample_check",
            CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool z_internal_ring_handler_sample_check(IntPtr handler);

        /// void
        /// z_internal_ring_handler_sample_null(
        ///     struct z_owned_ring_handler_sample_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_internal_ring_handler_sample_null",
            CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_internal_ring_handler_sample_null(IntPtr handler);

        /// bool
        /// z_internal_sample_check(
        ///     const struct z_owned_sample_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_internal_sample_check", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool z_internal_sample_check(IntPtr sample);

        /// void
        /// z_internal_sample_null(
        ///     struct z_owned_sample_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_internal_sample_null", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_internal_sample_null(IntPtr sample);

        /// bool
        /// z_internal_session_check(
        ///     const struct z_owned_session_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_internal_session_check", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool z_internal_session_check(IntPtr session);

        /// void
        /// z_internal_session_null(
        ///     struct z_owned_session_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_internal_session_null", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_internal_session_null(IntPtr session);

        [DllImport(DllName, EntryPoint = "z_internal_slice_check", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool z_internal_slice_check(IntPtr slice);

        [DllImport(DllName, EntryPoint = "z_internal_slice_null", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_internal_slice_null(IntPtr slice);

#if UNSTABLE_API
        /// bool
        /// z_internal_source_info_check(
        ///     const struct z_owned_source_info_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_internal_source_info_check", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool z_internal_source_info_check(IntPtr info);
#endif

#if UNSTABLE_API
        /// void
        /// z_internal_source_info_null(
        ///     struct z_owned_source_info_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_internal_source_info_null", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_internal_source_info_null(IntPtr info);
#endif


        [DllImport(DllName, EntryPoint = "z_internal_string_array_check", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool z_internal_string_array_check(IntPtr strings);

        [DllImport(DllName, EntryPoint = "z_internal_string_array_null", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_internal_string_array_null(IntPtr strings);

        [DllImport(DllName, EntryPoint = "z_internal_string_check", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool z_internal_string_check(IntPtr str);

        [DllImport(DllName, EntryPoint = "z_internal_string_null", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_internal_string_null(IntPtr str);

        /// bool
        /// z_internal_subscriber_check(
        ///     const struct z_owned_subscriber_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_internal_subscriber_check", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool z_internal_subscriber_check(IntPtr subscriber);

        /// void
        /// z_internal_subscriber_null(
        ///     struct z_owned_subscriber_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_internal_subscriber_null", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_internal_subscriber_null(IntPtr subscriber);

        [DllImport(DllName, EntryPoint = "z_internal_task_check", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool z_internal_task_check(IntPtr task);

        [DllImport(DllName, EntryPoint = "z_internal_task_null", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_internal_task_null(IntPtr task);

        /// void
        /// z_keyexpr_as_view_string(
        ///     const struct z_loaned_keyexpr_t *this_,
        ///     struct z_view_string_t *out_string
        /// )
        [DllImport(DllName, EntryPoint = "z_keyexpr_as_view_string", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_keyexpr_as_view_string(IntPtr keyexpr, IntPtr outString);

        /// z_result_t
        /// z_keyexpr_canonize(
        ///     char *start,
        ///     size_t *len
        /// )
        [DllImport(DllName, EntryPoint = "z_keyexpr_canonize", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result z_keyexpr_canonize(IntPtr start, IntPtr len);

        /// z_result_t
        /// z_keyexpr_canonize_null_terminated(
        ///     char *start
        /// )
        [DllImport(DllName, EntryPoint = "z_keyexpr_canonize_null_terminated",
            CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result z_keyexpr_canonize_null_terminated(IntPtr start);

        /// void
        /// z_keyexpr_clone(
        ///     struct z_owned_keyexpr_t *dst,
        ///     const struct z_loaned_keyexpr_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_keyexpr_clone", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_keyexpr_clone(IntPtr dst, IntPtr src);

        /// z_result_t
        /// z_keyexpr_concat(
        ///     struct z_owned_keyexpr_t *this_,
        ///     const struct z_loaned_keyexpr_t *left,
        ///     const char *right_start,
        ///     size_t right_len
        /// )
        [DllImport(DllName, EntryPoint = "z_keyexpr_concat", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result
            z_keyexpr_concat(IntPtr keyexpr, IntPtr left, IntPtr rightStart, UIntPtr rightLen);

        /// void
        /// z_keyexpr_drop(
        ///     struct z_moved_keyexpr_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_keyexpr_drop", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_keyexpr_drop(IntPtr keyexpr);

        /// bool
        /// z_keyexpr_equals(
        ///     const struct z_loaned_keyexpr_t *left,
        ///     const struct z_loaned_keyexpr_t *right
        /// )
        [DllImport(DllName, EntryPoint = "z_keyexpr_equals", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool z_keyexpr_equals(IntPtr left, IntPtr right);

        /// z_result_t
        /// z_keyexpr_from_str(
        ///     struct z_owned_keyexpr_t *this_,
        ///     const char *expr
        /// )
        [DllImport(DllName, EntryPoint = "z_keyexpr_from_str", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result z_keyexpr_from_str(IntPtr keyexpr, IntPtr str);

        /// z_result_t
        /// z_keyexpr_from_str_autocanonize(
        ///     struct z_owned_keyexpr_t *this_,
        ///     const char *expr
        /// )
        [DllImport(DllName, EntryPoint = "z_keyexpr_from_str_autocanonize", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern Result z_keyexpr_from_str_autocanonize(IntPtr keyexpr, IntPtr str);

        /// z_result_t
        /// z_keyexpr_from_substr(
        ///     struct z_owned_keyexpr_t *this_,
        ///     const char *expr,
        ///     size_t len
        /// )
        [DllImport(DllName, EntryPoint = "z_keyexpr_from_substr", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result z_keyexpr_from_substr(IntPtr keyexpr, IntPtr str, UIntPtr len);

        /// z_result_t
        /// z_keyexpr_from_substr_autocanonize(
        ///     struct z_owned_keyexpr_t *this_,
        ///     const char *start,
        ///     size_t *len
        /// )
        [DllImport(DllName, EntryPoint = "z_keyexpr_from_substr_autocanonize",
            CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result z_keyexpr_from_substr_autocanonize(IntPtr keyexpr, IntPtr str, IntPtr len);

        /// bool
        /// z_keyexpr_includes(
        ///     const struct z_loaned_keyexpr_t *left,
        ///     const struct z_loaned_keyexpr_t *right
        /// )
        [DllImport(DllName, EntryPoint = "z_keyexpr_includes", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool z_keyexpr_includes(IntPtr left, IntPtr right);

        /// bool
        /// z_keyexpr_intersects(
        ///     const struct z_loaned_keyexpr_t *left,
        ///     const struct z_loaned_keyexpr_t *right
        /// )
        [DllImport(DllName, EntryPoint = "z_keyexpr_intersects", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool z_keyexpr_intersects(IntPtr left, IntPtr right);

        /// z_result_t
        /// z_keyexpr_is_canon(
        ///     const char *start,
        ///     size_t len
        /// )
        [DllImport(DllName, EntryPoint = "z_keyexpr_is_canon", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result z_keyexpr_is_canon(IntPtr start, UIntPtr len);

        /// z_result_t
        /// z_keyexpr_join(
        ///     struct z_owned_keyexpr_t *this_,
        ///     const struct z_loaned_keyexpr_t *left,
        ///     const struct z_loaned_keyexpr_t *right
        /// )
        [DllImport(DllName, EntryPoint = "z_keyexpr_join", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result z_keyexpr_join(IntPtr keyexpr, IntPtr left, IntPtr right);

        /// const struct z_loaned_keyexpr_t*
        /// z_keyexpr_loan(
        ///     const struct z_owned_keyexpr_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_keyexpr_loan", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_keyexpr_loan(IntPtr keyexpr);

#if UNSTABLE_API
        /// enum z_keyexpr_intersection_level_t
        /// z_keyexpr_relation_to(
        ///     const struct z_loaned_keyexpr_t *left,
        ///     const struct z_loaned_keyexpr_t *right
        /// )
        [DllImport(DllName, EntryPoint = "z_keyexpr_relation_to", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern KeyexprIntersectionLevel z_keyexpr_relation_to(IntPtr left, IntPtr right);
#endif

        /// z_result_t
        /// z_liveliness_declare_subscriber(
        ///     const struct z_loaned_session_t *session,
        ///     struct z_owned_subscriber_t *subscriber,
        ///     const struct z_loaned_keyexpr_t *key_expr,
        ///     struct z_moved_closure_sample_t *callback,
        ///     struct z_liveliness_subscriber_options_t *options
        /// )
        [DllImport(DllName, EntryPoint = "z_liveliness_declare_subscriber", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result z_liveliness_declare_subscriber(IntPtr session, IntPtr subscriber, IntPtr keyexpr,
            IntPtr callback, IntPtr options);

        /// z_result_t
        /// z_liveliness_declare_token(
        ///     const struct z_loaned_session_t *session,
        ///     struct z_owned_liveliness_token_t *token,
        ///     const struct z_loaned_keyexpr_t *key_expr,
        ///     const struct z_liveliness_token_options_t *_options
        /// )
        [DllImport(DllName, EntryPoint = "z_liveliness_declare_token", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result z_liveliness_declare_token(IntPtr session, IntPtr token, IntPtr keyexpr,
            IntPtr options);

        /// z_result_t
        /// z_liveliness_get(
        ///     const struct z_loaned_session_t *session,
        ///     const struct z_loaned_keyexpr_t *key_expr,
        ///     struct z_moved_closure_reply_t *callback,
        ///     struct z_liveliness_get_options_t *options
        /// )
        [DllImport(DllName, EntryPoint = "z_liveliness_get", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result z_liveliness_get(IntPtr session, IntPtr keyexpr, IntPtr callback, IntPtr options);

        /// void
        /// z_liveliness_get_options_default(
        ///     struct z_liveliness_get_options_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_liveliness_get_options_default",
            CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_liveliness_get_options_default(IntPtr options);

        /// void
        /// z_liveliness_subscriber_options_default(
        ///     struct z_liveliness_subscriber_options_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_liveliness_subscriber_options_default",
            CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_liveliness_subscriber_options_default(IntPtr options);

        /// void
        /// z_liveliness_token_drop(
        ///     struct z_moved_liveliness_token_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_liveliness_token_drop", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_liveliness_token_drop(IntPtr token);

        /// const struct z_loaned_liveliness_token_t*
        /// z_liveliness_token_loan(
        ///     const struct z_owned_liveliness_token_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_liveliness_token_loan", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_liveliness_token_loan(IntPtr token);

        /// void
        /// z_liveliness_token_options_default(
        ///     struct z_liveliness_token_options_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_liveliness_token_options_default",
            CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_liveliness_token_options_default(IntPtr options);

        /// z_result_t
        /// z_liveliness_undeclare_token(
        ///     struct z_moved_liveliness_token_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_liveliness_undeclare_token", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result z_liveliness_undeclare_token(IntPtr token);

#if UNSTABLE_API
        /// void 
        /// z_matching_listener_drop(
        ///     struct z_moved_matching_listener_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_matching_listener_drop", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_matching_listener_drop(IntPtr listener);
#endif

        /// void
        /// z_mutex_drop(
        ///     struct z_moved_mutex_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_mutex_drop", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_mutex_drop(IntPtr mutex);

        [DllImport(DllName, EntryPoint = "z_mutex_init", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result z_mutex_init(IntPtr mutex);

        [DllImport(DllName, EntryPoint = "z_mutex_loan_mut", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_mutex_loan_mut(IntPtr mutex);

        [DllImport(DllName, EntryPoint = "z_mutex_lock", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result z_mutex_lock(IntPtr mutex);

        [DllImport(DllName, EntryPoint = "z_mutex_try_lock", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result z_mutex_try_lock(IntPtr mutex);

        [DllImport(DllName, EntryPoint = "z_mutex_unlock", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result z_mutex_unlock(IntPtr mutex);

        /// z_result_t
        /// z_open(
        ///     struct z_owned_session_t *this_,
        ///     struct z_moved_config_t *config,
        ///     const struct z_open_options_t *_options
        /// )
        [DllImport(DllName, EntryPoint = "z_open", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern Result z_open(IntPtr session, IntPtr config, IntPtr options);

        /// void
        /// z_open_options_default(
        ///     struct z_open_options_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_open_options_default", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_open_options_default(IntPtr options);

        [DllImport(DllName, EntryPoint = "z_priority_default", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Priority z_priority_default();

#if UNSTABLE_API
        /// z_result_t
        /// z_publisher_declare_matching_listener(
        ///     const struct z_loaned_publisher_t *publisher,
        ///     struct z_owned_matching_listener_t *matching_listener,
        ///     struct z_moved_closure_matching_status_t *callback
        /// )
        [DllImport(DllName, EntryPoint = "z_publisher_declare_matching_listener",
            CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result z_publisher_declare_matching_listener(IntPtr publisher, IntPtr listener,
            IntPtr callback);
#endif

        /// z_result_t
        /// z_publisher_delete(
        ///     const struct z_loaned_publisher_t *publisher,
        ///     struct z_publisher_delete_options_t *options
        /// )
        [DllImport(DllName, EntryPoint = "z_publisher_delete", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result z_publisher_delete(IntPtr publisher, IntPtr options);

        /// void
        /// z_publisher_delete_options_default(
        ///     struct z_publisher_delete_options_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_publisher_delete_options_default",
            CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_publisher_delete_options_default(IntPtr options);

        /// void
        /// z_publisher_drop(
        ///     struct z_moved_publisher_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_publisher_drop", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_publisher_drop(IntPtr publisher);


#if UNSTABLE_API
        /// z_result_t
        /// z_publisher_get_matching_status(
        ///     const struct z_loaned_publisher_t *this_,
        ///     struct z_matching_status_t *matching_status
        /// )
        [DllImport(DllName, EntryPoint = "z_publisher_get_matching_status", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result z_publisher_get_matching_status(IntPtr publisher, IntPtr status);
#endif

#if UNSTABLE_API
        /// struct z_entity_global_id_t
        /// z_publisher_id(
        ///     const struct z_loaned_publisher_t *publisher
        /// )
        [DllImport(DllName, EntryPoint = "z_publisher_id", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern ZEntityGlobalId z_publisher_id(IntPtr publisher);
#endif

        /// const struct z_loaned_keyexpr_t*
        /// z_publisher_keyexpr(
        ///     const struct z_loaned_publisher_t *publisher
        /// )
        [DllImport(DllName, EntryPoint = "z_publisher_keyexpr", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_publisher_keyexpr(IntPtr publisher);

        /// const struct z_loaned_publisher_t*
        /// z_publisher_loan(
        ///     const struct z_owned_publisher_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_publisher_loan", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_publisher_loan(IntPtr publisher);

        /// struct z_loaned_publisher_t*
        /// z_publisher_loan_mut(
        ///     struct z_owned_publisher_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_publisher_loan_mut", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_publisher_loan_mut(IntPtr publisher);

        /// void
        /// z_publisher_options_default(
        ///     struct z_publisher_options_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_publisher_options_default", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_publisher_options_default(IntPtr options);

        /// z_result_t
        /// z_publisher_put(
        ///     const struct z_loaned_publisher_t *this_,
        ///     struct z_moved_bytes_t *payload,
        ///     struct z_publisher_put_options_t *options
        /// )
        [DllImport(DllName, EntryPoint = "z_publisher_put", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result z_publisher_put(IntPtr publisher, IntPtr payload, IntPtr options);

        /// void
        /// z_publisher_put_options_default(
        ///     struct z_publisher_put_options_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_publisher_put_options_default", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_publisher_put_options_default(IntPtr options);

        /// z_result_t
        /// z_put(
        ///     const struct z_loaned_session_t *session,
        ///     const struct z_loaned_keyexpr_t *key_expr,
        ///     struct z_moved_bytes_t *payload,
        ///     struct z_put_options_t *options
        /// )
        [DllImport(DllName, EntryPoint = "z_put", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern Result z_put(IntPtr session, IntPtr keyexpr, IntPtr payload, IntPtr options);

        /// void
        /// z_put_options_default(
        ///     struct z_put_options_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_put_options_default", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_put_options_default(IntPtr options);

#if UNSTABLE_API
        /// void
        /// z_querier_drop(
        ///     struct z_moved_querier_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_querier_drop", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_querier_drop(IntPtr querier);
#endif

#if UNSTABLE_API
        /// z_result_t
        /// z_querier_get(
        ///     const struct z_loaned_querier_t *querier,
        ///     const char *parameters,
        ///     struct z_moved_closure_reply_t *callback,
        ///     struct z_querier_get_options_t *options
        /// )
        [DllImport(DllName, EntryPoint = "z_querier_get", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result z_querier_get(IntPtr querier, IntPtr parameters, IntPtr callback, IntPtr options);
#endif

#if UNSTABLE_API
        /// void
        /// z_querier_get_options_default(
        ///     struct z_querier_get_options_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_querier_get_options_default", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_querier_get_options_default(IntPtr options);
#endif

#if UNSTABLE_API
        /// const struct z_loaned_keyexpr_t*
        /// z_querier_keyexpr(
        ///     const struct z_loaned_querier_t *querier
        /// )
        [DllImport(DllName, EntryPoint = "z_querier_keyexpr", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_querier_keyexpr(IntPtr querier);
#endif

#if UNSTABLE_API
        /// const struct z_loaned_querier_t*
        /// z_querier_loan(
        ///     const struct z_owned_querier_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_querier_loan", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_querier_loan(IntPtr querier);
#endif

#if UNSTABLE_API
        /// struct z_loaned_querier_t*
        /// z_querier_loan_mut(
        ///     struct z_owned_querier_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_querier_loan_mut", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_querier_loan_mut(IntPtr querier);
#endif

#if UNSTABLE_API
        /// void
        /// z_querier_options_default(
        ///     struct z_querier_options_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_querier_options_default", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_querier_options_default(IntPtr querier);
#endif

        /// const struct z_loaned_bytes_t*
        /// z_query_attachment(
        ///     const struct z_loaned_query_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_query_attachment", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_query_attachment(IntPtr query);

        /// struct z_loaned_bytes_t*
        /// z_query_attachment_mut(
        ///     struct z_loaned_query_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_query_attachment_mut", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_query_attachment_mut(IntPtr query);

        /// void
        /// z_query_clone(
        ///     struct z_owned_query_t *dst,
        ///     const struct z_loaned_query_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_query_clone", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_query_clone(IntPtr dst, IntPtr src);

        /// struct z_query_consolidation_t
        /// z_query_consolidation_auto(
        ///     void
        /// )
        [DllImport(DllName, EntryPoint = "z_query_consolidation_auto", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern ZQueryConsolidation z_query_consolidation_auto();

        /// struct z_query_consolidation_t
        /// z_query_consolidation_default(
        ///     void
        /// )
        [DllImport(DllName, EntryPoint = "z_query_consolidation_default", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern ZQueryConsolidation z_query_consolidation_default();

        /// struct z_query_consolidation_t
        /// z_query_consolidation_latest(
        ///     void
        /// )
        [DllImport(DllName, EntryPoint = "z_query_consolidation_latest", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern ZQueryConsolidation z_query_consolidation_latest();

        /// struct z_query_consolidation_t
        /// z_query_consolidation_monotonic(
        ///     void
        /// )
        [DllImport(DllName, EntryPoint = "z_query_consolidation_monotonic", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern ZQueryConsolidation z_query_consolidation_monotonic();

        /// struct z_query_consolidation_t
        /// z_query_consolidation_none(
        ///     void
        /// )
        [DllImport(DllName, EntryPoint = "z_query_consolidation_none", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern ZQueryConsolidation z_query_consolidation_none();

        /// void
        /// z_query_drop(
        ///     struct z_moved_query_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_query_drop", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_query_drop(IntPtr query);

        /// const struct z_loaned_encoding_t*
        /// z_query_encoding(
        ///     const struct z_loaned_query_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_query_encoding", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_query_encoding(IntPtr query);

        /// const struct z_loaned_keyexpr_t*
        /// z_query_keyexpr(
        ///     const struct z_loaned_query_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_query_keyexpr", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_query_keyexpr(IntPtr query);

        /// const struct z_loaned_query_t*
        /// z_query_loan(
        ///     const struct z_owned_query_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_query_loan", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_query_loan(IntPtr query);

        /// struct z_loaned_query_t*
        /// z_query_loan_mut(
        ///     struct z_owned_query_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_query_loan_mut", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_query_loan_mut(IntPtr query);

        /// void
        /// z_query_parameters(
        ///     const struct z_loaned_query_t *this_,
        ///     struct z_view_string_t *parameters
        /// )
        [DllImport(DllName, EntryPoint = "z_query_parameters", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_query_parameters(IntPtr query, IntPtr parameters);

        /// const struct z_loaned_bytes_t*
        /// z_query_payload(
        ///     const struct z_loaned_query_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_query_payload", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_query_payload(IntPtr query);

        /// struct z_loaned_bytes_t*
        /// z_query_payload_mut(
        ///     struct z_loaned_query_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_query_payload_mut", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_query_payload_mut(IntPtr query);

        /// z_result_t
        /// z_query_reply(
        ///     const struct z_loaned_query_t *this_,
        ///     const struct z_loaned_keyexpr_t *key_expr,
        ///     struct z_moved_bytes_t *payload,
        ///     struct z_query_reply_options_t *options
        /// )
        [DllImport(DllName, EntryPoint = "z_query_reply", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result z_query_reply(IntPtr query, IntPtr keyexpr, IntPtr payload, IntPtr options);

        /// z_result_t
        /// z_query_reply_del(
        ///     const struct z_loaned_query_t *this_,
        ///     const struct z_loaned_keyexpr_t *key_expr,
        ///     struct z_query_reply_del_options_t *options
        /// )
        [DllImport(DllName, EntryPoint = "z_query_reply_del", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result z_query_reply_del(IntPtr query, IntPtr keyexpr, IntPtr options);

        /// void
        /// z_query_reply_del_options_default(
        ///     struct z_query_reply_del_options_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_query_reply_del_options_default",
            CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_query_reply_del_options_default(IntPtr options);

        /// z_result_t
        /// z_query_reply_err(
        ///     const struct z_loaned_query_t *this_,
        ///     struct z_moved_bytes_t *payload,
        ///     struct z_query_reply_err_options_t *options
        /// )
        [DllImport(DllName, EntryPoint = "z_query_reply_err", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result z_query_reply_err(IntPtr query, IntPtr payload, IntPtr options);

        /// void
        /// z_query_reply_err_options_default(
        ///     struct z_query_reply_err_options_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_query_reply_err_options_default",
            CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_query_reply_err_options_default(IntPtr options);

        /// void
        /// z_query_reply_options_default(
        ///     struct z_query_reply_options_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_query_reply_options_default", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_query_reply_options_default(IntPtr options);

        /// enum z_query_target_t
        /// z_query_target_default(
        ///     void
        /// )
        [DllImport(DllName, EntryPoint = "z_query_target_default", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern QueryTarget z_query_target_default();

        /// void
        /// z_queryable_drop(
        ///     struct z_moved_queryable_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_queryable_drop", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_queryable_drop(IntPtr queryable);

#if UNSTABLE_API
        /// struct z_entity_global_id_t
        /// z_queryable_id(
        ///     const struct z_loaned_queryable_t *queryable
        /// )
        [DllImport(DllName, EntryPoint = "z_queryable_id", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern ZEntityGlobalId z_queryable_id(IntPtr queryable);
#endif

        /// const struct z_loaned_queryable_t*
        /// z_queryable_loan(
        ///     const struct z_owned_queryable_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_queryable_loan", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_queryable_loan(IntPtr queryable);

        /// void
        /// z_queryable_options_default(
        ///     struct z_queryable_options_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_queryable_options_default", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_queryable_options_default(IntPtr options);

        [DllImport(DllName, EntryPoint = "z_random_fill", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_random_fill(IntPtr buf, UIntPtr len);

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

        /// void
        /// z_reply_clone(
        ///     struct z_owned_reply_t *dst,
        ///     const struct z_loaned_reply_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_reply_clone", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_reply_clone(IntPtr dst, IntPtr src);

        /// void
        /// z_reply_drop(
        ///     struct z_moved_reply_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_reply_drop", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_reply_drop(IntPtr reply);

#if UNSTABLE_API
        /// bool
        /// z_reply_replier_id(
        ///     const struct z_loaned_reply_t *this_,
        ///     struct z_id_t *out_id
        /// )
        [DllImport(DllName, EntryPoint = "z_reply_replier_id", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool z_reply_replier_id(IntPtr reply, IntPtr id);
#endif

        /// void
        /// z_reply_take_from_loaned(
        ///     struct z_owned_reply_t *dst,
        ///     struct z_loaned_reply_t *src
        /// )
        [DllImport(DllName, EntryPoint = "z_reply_take_from_loaned", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_reply_take_from_loaned(IntPtr dst, IntPtr src);

        /// bool
        /// z_reply_is_ok(
        ///     const struct z_loaned_reply_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_reply_is_ok", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool z_reply_is_ok(IntPtr reply);

        /// const struct z_loaned_reply_t*
        /// z_reply_loan(
        ///     const struct z_owned_reply_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_reply_loan", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_reply_loan(IntPtr reply);

        /// struct z_loaned_reply_t*
        /// z_reply_loan(
        ///     struct z_owned_reply_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_reply_loan_mut", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_reply_loan_mut(IntPtr reply);

        /// const struct z_loaned_sample_t*
        /// z_reply_ok(
        ///     const struct z_loaned_reply_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_reply_ok", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_reply_ok(IntPtr reply);

        /// struct z_loaned_sample_t*
        /// z_reply_ok(
        ///     struct z_loaned_reply_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_reply_ok_mut", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_reply_ok_mut(IntPtr reply);

        /// const struct z_loaned_reply_err_t*
        /// z_reply_err(
        ///     const struct z_loaned_reply_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_reply_err", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_reply_err(IntPtr reply);

        /// void
        /// z_reply_err_clone(
        ///     struct z_owned_reply_err_t *dst,
        ///     const struct z_loaned_reply_err_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_reply_err_clone", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_reply_err_clone(IntPtr dst, IntPtr src);

        /// void
        /// z_reply_err_drop(
        ///     struct z_moved_reply_err_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_reply_err_drop", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_reply_err_drop(IntPtr replyErr);

        /// const struct z_loaned_reply_err_t*
        /// z_reply_err_loan(
        ///     const struct z_owned_reply_err_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_reply_err_loan", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_reply_err_loan(IntPtr replyErr);

        /// struct z_loaned_reply_err_t*
        /// z_reply_err_loan_mut(
        ///     struct z_owned_reply_err_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_reply_err_loan_mut", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_reply_err_loan_mut(IntPtr replyErr);

        /// const struct z_loaned_bytes_t*
        /// z_reply_err_payload(
        ///     const struct z_loaned_reply_err_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_reply_err_payload", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_reply_err_payload(IntPtr replyErr);

        /// const struct z_loaned_encoding_t*
        /// z_reply_err_encoding(
        ///     const struct z_loaned_reply_err_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_reply_err_encoding", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_reply_err_encoding(IntPtr replyErr);

        /// void
        /// z_ring_channel_query_new(
        ///     struct z_owned_closure_query_t *callback,
        ///     struct z_owned_ring_handler_query_t *handler,
        ///     size_t capacity
        /// )
        [DllImport(DllName, EntryPoint = "z_ring_channel_query_new", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_ring_channel_query_new(IntPtr callback, IntPtr handler, UIntPtr capacity);

        /// void
        /// z_ring_channel_reply_new(
        ///     struct z_owned_closure_reply_t *callback,
        ///     struct z_owned_ring_handler_reply_t *handler,
        ///     size_t capacity
        /// )
        [DllImport(DllName, EntryPoint = "z_ring_channel_reply_new", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_ring_channel_reply_new(IntPtr callback, IntPtr handler, UIntPtr capacity);

        /// void
        /// z_ring_channel_sample_new(
        ///     struct z_owned_closure_sample_t *callback,
        ///     struct z_owned_ring_handler_sample_t *handler,
        ///     size_t capacity
        /// )
        [DllImport(DllName, EntryPoint = "z_ring_channel_sample_new", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_ring_channel_sample_new(IntPtr callback, IntPtr handler, UIntPtr capacity);

        /// void
        /// z_ring_handler_sample_drop(
        ///     struct z_moved_ring_handler_sample_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_ring_handler_sample_drop", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_ring_handler_sample_drop(IntPtr handler);

        /// const struct z_loaned_ring_handler_sample_t*
        /// z_ring_handler_sample_loan(
        ///     const struct z_owned_ring_handler_sample_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_ring_handler_sample_loan", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_ring_handler_sample_loan(IntPtr handler);

        /// z_result_t
        /// z_ring_handler_sample_recv(
        ///     const struct z_loaned_ring_handler_sample_t *this_,
        ///     struct z_owned_sample_t *sample
        /// )
        [DllImport(DllName, EntryPoint = "z_ring_handler_sample_recv", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result z_ring_handler_sample_recv(IntPtr handler, IntPtr sample);

        /// z_result_t
        /// z_ring_handler_sample_try_recv(
        ///     const struct z_loaned_ring_handler_sample_t *this_,
        ///     struct z_owned_sample_t *sample
        /// )
        [DllImport(DllName, EntryPoint = "z_ring_handler_sample_try_recv", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result z_ring_handler_sample_try_recv(IntPtr handler, IntPtr sample);

        /// void
        /// z_ring_handler_query_drop(
        ///     struct z_moved_ring_handler_query_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_ring_handler_query_drop", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_ring_handler_query_drop(IntPtr handler);

        /// const struct z_loaned_ring_handler_query_t*
        /// z_ring_handler_query_loan(
        ///     const struct z_owned_ring_handler_query_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_ring_handler_query_loan", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_ring_handler_query_loan(IntPtr handler);

        /// z_result_t
        /// z_ring_handler_query_recv(
        ///     const struct z_loaned_ring_handler_query_t *this_,
        ///     struct z_owned_query_t *query
        /// )
        [DllImport(DllName, EntryPoint = "z_ring_handler_query_recv", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result z_ring_handler_query_recv(IntPtr handler, IntPtr query);

        /// z_result_t
        /// z_ring_handler_query_recv(
        ///     const struct z_loaned_ring_handler_query_t *this_,
        ///     struct z_owned_query_t *query
        /// )
        [DllImport(DllName, EntryPoint = "z_ring_handler_query_try_recv", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result z_ring_handler_query_try_recv(IntPtr handler, IntPtr query);

        /// void
        /// z_ring_handler_reply_drop(
        ///     struct z_moved_ring_handler_reply_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_ring_handler_reply_drop", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_ring_handler_reply_drop(IntPtr handler);

        /// const struct z_loaned_ring_handler_reply_t*
        /// z_ring_handler_reply_loan(
        ///     const struct z_owned_ring_handler_reply_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_ring_handler_reply_loan", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_ring_handler_reply_loan(IntPtr handler);

        /// z_result_t
        /// z_ring_handler_reply_recv(
        ///     const struct z_loaned_ring_handler_reply_t *this_,
        ///     struct z_owned_reply_t *reply
        /// )
        [DllImport(DllName, EntryPoint = "z_ring_handler_reply_recv", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result z_ring_handler_reply_recv(IntPtr handler, IntPtr reply);

        /// z_result_t
        /// z_ring_handler_reply_try_recv(
        ///     const struct z_loaned_ring_handler_reply_t *this_,
        ///     struct z_owned_reply_t *reply
        /// )
        [DllImport(DllName, EntryPoint = "z_ring_handler_reply_try_recv", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result z_ring_handler_reply_try_recv(IntPtr handler, IntPtr reply);

        /// const struct z_loaned_bytes_t*
        /// z_sample_attachment(
        ///     const struct z_loaned_sample_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_sample_attachment", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_sample_attachment(IntPtr sample);

        /// void
        /// z_sample_clone(
        ///     struct z_owned_sample_t *dst,
        ///     const struct z_loaned_sample_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_sample_clone", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_sample_clone(IntPtr dst, IntPtr other);

        /// void
        /// z_sample_drop(
        ///     struct z_moved_sample_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_sample_drop", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_sample_drop(IntPtr sample);

        /// enum z_congestion_control_t
        /// z_sample_congestion_control(
        ///     const struct z_loaned_sample_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_sample_congestion_control", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern CongestionControl z_sample_congestion_control(IntPtr sample);

        /// const struct z_loaned_encoding_t*
        /// z_sample_encoding(
        ///     const struct z_loaned_sample_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_sample_encoding", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_sample_encoding(IntPtr sample);

        /// bool
        /// z_sample_express(
        ///     const struct z_loaned_sample_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_sample_express", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool z_sample_express(IntPtr sample);

        /// const struct z_loaned_keyexpr_t*
        /// z_sample_keyexpr(
        ///     const struct z_loaned_sample_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_sample_keyexpr", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_sample_keyexpr(IntPtr sample);

        /// enum z_sample_kind_t
        /// z_sample_kind(
        ///     const struct z_loaned_sample_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_sample_kind", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern SampleKind z_sample_kind(IntPtr sample);

        /// const struct z_loaned_sample_t*
        /// z_sample_loan_mut(
        ///     struct z_owned_sample_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_sample_loan", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_sample_loan(IntPtr sample);

        /// struct z_loaned_sample_t*
        /// z_sample_loan_mut(
        ///     struct z_owned_sample_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_sample_loan_mut", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_sample_loan_mut(IntPtr sample);

        /// void
        /// z_sample_take_from_loaned(
        ///     struct z_owned_sample_t *dst,
        ///     struct z_loaned_sample_t *src
        /// )
        [DllImport(DllName, EntryPoint = "z_sample_take_from_loaned", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_sample_take_from_loaned(IntPtr dst, IntPtr src);

        /// const struct z_loaned_bytes_t*
        /// z_sample_payload(
        ///     const struct z_loaned_sample_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_sample_payload", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_sample_payload(IntPtr sample);

        /// enum z_priority_t
        /// z_sample_priority(
        ///     const struct z_loaned_sample_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_sample_priority", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Priority z_sample_priority(IntPtr sample);

#if UNSTABLE_API
        /// enum z_reliability_t
        /// z_sample_reliability(
        ///     const struct z_loaned_sample_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_sample_reliability", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Reliability z_sample_reliability(IntPtr sample);
#endif

#if UNSTABLE_API
        /// const struct z_loaned_source_info_t*
        /// z_sample_source_info(
        ///     const struct z_loaned_sample_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_sample_source_info", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_sample_source_info(IntPtr sample);
#endif

        /// const struct z_timestamp_t*
        /// z_sample_timestamp(
        ///     const struct z_loaned_sample_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_sample_timestamp", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_sample_timestamp(IntPtr sample);

        /// z_result_t
        /// z_scout(
        ///     struct z_moved_config_t *config,
        ///     struct z_moved_closure_hello_t *callback,
        ///     const struct z_scout_options_t *options
        /// )
        [DllImport(DllName, EntryPoint = "z_scout", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result z_scout(IntPtr config, IntPtr callback, IntPtr options);

        /// void
        /// z_scout_options_default(
        ///     struct z_scout_options_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_scout_options_default", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_scout_options_default(IntPtr options);

        /// void
        /// z_session_drop(
        ///     struct z_moved_session_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_session_drop", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_session_drop(IntPtr session);

        /// bool
        /// z_session_is_closed(
        ///     const struct z_loaned_session_t *session
        /// )
        [DllImport(DllName, EntryPoint = "z_session_is_closed", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool z_session_is_closed(IntPtr session);

        /// const struct z_loaned_session_t*
        /// z_session_loan(
        ///     const struct z_owned_session_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_session_loan", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_session_loan(IntPtr session);

        /// struct z_loaned_session_t*
        /// z_session_loan_mut(
        ///     struct z_owned_session_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_session_loan_mut", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_session_loan_mut(IntPtr session);

        [DllImport(DllName, EntryPoint = "z_sleep_s", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result z_sleep_s(UIntPtr time);

        [DllImport(DllName, EntryPoint = "z_sleep_ms", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result z_sleep_ms(UIntPtr time);

        [DllImport(DllName, EntryPoint = "z_sleep_us", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result z_sleep_us(UIntPtr time);

        /// void
        /// z_slice_clone(
        ///     struct z_owned_slice_t *dst,
        ///     const struct z_loaned_slice_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_slice_clone", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_slice_clone(IntPtr dst, IntPtr src);

        [DllImport(DllName, EntryPoint = "z_slice_copy_from_buf", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result z_slice_copy_from_buf(IntPtr slice, IntPtr start, UIntPtr len);

        [DllImport(DllName, EntryPoint = "z_slice_data", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_slice_data(IntPtr slice);

        /// void
        /// z_slice_drop(
        ///     struct z_moved_slice_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_slice_drop", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_slice_drop(IntPtr slice);

        /// void
        /// z_slice_empty(
        ///     struct z_owned_slice_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_slice_empty", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_slice_empty(IntPtr slice);

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
        internal static extern Result
            z_slice_from_buf(IntPtr slice, IntPtr data, UIntPtr len, Cb2 drop, IntPtr context);

        /// bool
        /// z_slice_is_empty(
        ///     const struct z_loaned_slice_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_slice_is_empty", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool z_slice_is_empty(IntPtr slice);

        /// size_t
        /// z_slice_len(
        ///     const struct z_loaned_slice_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_slice_len", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern UIntPtr z_slice_len(IntPtr slice);

        /// const struct z_loaned_slice_t*
        /// z_slice_loan(
        ///     const struct z_owned_slice_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_slice_loan", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_slice_loan(IntPtr slice);

#if UNSTABLE_API
        /// z_result_t
        /// z_source_info_new(
        ///     struct z_owned_source_info_t *this_,
        ///     const struct z_entity_global_id_t *source_id,
        ///     uint32_t source_sn
        /// )
        [DllImport(DllName, EntryPoint = "z_source_info_new", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result z_source_info_new(IntPtr info, IntPtr id, uint sn);
#endif

#if UNSTABLE_API
        /// void
        /// z_source_info_drop(
        ///     struct z_moved_source_info_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_source_info_drop", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_source_info_drop(IntPtr info);
#endif

#if UNSTABLE_API
        /// const struct z_loaned_source_info_t*
        /// z_source_info_loan(
        ///     const struct z_owned_source_info_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_source_info_loan", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_source_info_loan(IntPtr info);
#endif

#if UNSTABLE_API
        /// struct z_entity_global_id_t
        /// z_source_info_id(
        ///     const struct z_loaned_source_info_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_source_info_id", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern ZEntityGlobalId z_source_info_id(IntPtr info);
#endif


#if UNSTABLE_API
        /// uint32_t
        /// z_source_info_sn(
        ///     const struct z_loaned_source_info_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_source_info_sn", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern uint z_source_info_sn(IntPtr info);
#endif

        [DllImport(DllName, EntryPoint = "z_string_array_clone", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_string_array_clone(IntPtr dst, IntPtr src);

        /// void
        /// z_string_array_drop(
        ///     struct z_moved_string_array_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_string_array_drop", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_string_array_drop(IntPtr stringArray);

        [DllImport(DllName, EntryPoint = "z_string_array_get", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_string_array_get(IntPtr stringArray, UIntPtr index);

        [DllImport(DllName, EntryPoint = "z_string_array_is_empty", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool z_string_array_is_empty(IntPtr stringArray);

        [DllImport(DllName, EntryPoint = "z_string_array_len", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern UIntPtr z_string_array_len(IntPtr stringArray);

        [DllImport(DllName, EntryPoint = "z_string_array_loan", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_string_array_loan(IntPtr stringArray);

        [DllImport(DllName, EntryPoint = "z_string_array_loan_mut", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_string_array_loan_mut(IntPtr stringArray);

        [DllImport(DllName, EntryPoint = "z_string_array_new", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_string_array_new(IntPtr stringArray);

        [DllImport(DllName, EntryPoint = "z_string_push_by_alias", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern UIntPtr
            z_string_array_push_by_alias(IntPtr stringArray, IntPtr value);

        [DllImport(DllName, EntryPoint = "z_string_push_by_copy", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern UIntPtr z_string_array_push_by_copy(IntPtr stringArray,
            IntPtr value);

        [DllImport(DllName, EntryPoint = "z_string_as_slice", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_string_as_slice(IntPtr str);

        /// void
        /// z_string_clone(
        ///     struct z_owned_string_t *dst,
        ///     const struct z_loaned_string_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_string_clone", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_string_clone(IntPtr dst, IntPtr src);

        /// z_result_t
        /// z_string_copy_from_str(
        ///     struct z_owned_string_t *this_,
        ///     const char *str
        /// )
        [DllImport(DllName, EntryPoint = "z_string_copy_from_str", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result z_string_copy_from_str(IntPtr dst, IntPtr str);

        /// z_result_t
        /// z_string_copy_from_substr(
        ///     struct z_owned_string_t *this_,
        ///     const char *str,
        ///     size_t len
        /// )
        [DllImport(DllName, EntryPoint = "z_string_copy_from_substr", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result z_string_copy_from_substr(IntPtr dst, IntPtr str, UIntPtr len);

        /// const char*
        /// z_string_data(
        ///     const struct z_loaned_string_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_string_data", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_string_data(IntPtr str);

        /// void
        /// z_string_drop(
        ///     struct z_moved_string_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_string_drop", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_string_drop(IntPtr str);

        /// void
        /// z_string_empty(
        ///     struct z_owned_string_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_string_empty", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_string_empty(IntPtr str);

        /// z_result_t
        /// z_string_from_str(
        ///     struct z_owned_string_t *this_,
        ///     char *str,
        ///     void (*drop)(void *value, void *context),
        ///     void *context
        /// )
        [DllImport(DllName, EntryPoint = "z_string_from_str", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result z_string_from_str(IntPtr dst, IntPtr str, Cb2 drop, IntPtr context);

        /// bool
        /// z_string_is_empty(
        ///     const struct z_loaned_string_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_string_is_empty", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool z_string_is_empty(IntPtr str);

        /// size_t
        /// z_string_len(
        ///     const struct z_loaned_string_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_string_len", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern UIntPtr z_string_len(IntPtr str);

        /// const struct z_loaned_string_t*
        /// z_string_loan(
        ///     const struct z_owned_string_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_string_loan", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_string_loan(IntPtr str);

        /// void
        /// z_subscriber_drop(
        ///     struct z_moved_subscriber_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_subscriber_drop", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_subscriber_drop(IntPtr subscriber);

#if UNSTABLE_API
        /// struct z_entity_global_id_t
        /// z_subscriber_id(
        ///     const struct z_loaned_subscriber_t *subscriber
        /// )
        [DllImport(DllName, EntryPoint = "z_subscriber_id", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern ZEntityGlobalId z_subscriber_id(IntPtr subscriber);
#endif

        /// const struct z_loaned_keyexpr_t*
        /// z_subscriber_keyexpr(
        ///     const struct z_loaned_subscriber_t *subscriber
        /// )
        [DllImport(DllName, EntryPoint = "z_subscriber_keyexpr", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_subscriber_keyexpr(IntPtr subscriber);

        /// const struct z_loaned_subscriber_t*
        /// z_subscriber_loan(
        ///     const struct z_owned_subscriber_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_subscriber_loan", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_subscriber_loan(IntPtr subscriber);

        /// void
        /// z_subscriber_options_default(
        ///     struct z_subscriber_options_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_subscriber_options_default", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_subscriber_options_default(IntPtr options);

        /// void
        /// z_task_detach(
        ///     struct z_moved_task_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_task_detach", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_task_detach(IntPtr task);

        /// void
        /// z_task_drop(
        ///     struct z_moved_task_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_task_drop", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_task_drop(IntPtr task);

        /// z_result_t
        /// z_task_init(
        ///     struct z_owned_task_t *this_,
        ///     const struct z_task_attr_t *_attr,
        ///     void *(*fun)(void *arg),
        ///     void *arg
        /// )
        [DllImport(DllName, EntryPoint = "z_task_init", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result z_task_init(IntPtr task, IntPtr attr, Cb1 fun, IntPtr arg);

        /// z_result_t
        /// z_task_join(
        ///     struct z_moved_task_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_task_join", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result z_task_join(IntPtr task);

        [DllImport(DllName, EntryPoint = "z_time_elapsed_s", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.U8)]
        internal static extern ulong z_time_elapsed_s(IntPtr time);

        [DllImport(DllName, EntryPoint = "z_time_elapsed_ms", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.U8)]
        internal static extern ulong z_time_elapsed_ms(IntPtr time);

        [DllImport(DllName, EntryPoint = "z_time_elapsed_us", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.U8)]
        internal static extern ulong z_time_elapsed_us(IntPtr time);

        [DllImport(DllName, EntryPoint = "z_time_now", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern ZTime z_time_now();

        [DllImport(DllName, EntryPoint = "z_time_now_as_str", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_time_now_as_str(IntPtr buf, UIntPtr len);

        /// struct z_id_t
        /// z_timestamp_id(
        ///     const struct z_timestamp_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_timestamp_id", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern ZId z_timestamp_id(IntPtr timestamp);

        /// z_result_t
        /// z_timestamp_new(
        ///     struct z_timestamp_t *this_,
        ///     const struct z_loaned_session_t *session
        /// )
        [DllImport(DllName, EntryPoint = "z_timestamp_new", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result z_timestamp_new(IntPtr timestamp, IntPtr session);

        /// uint64_t
        /// z_timestamp_ntp64_time(
        ///     const struct z_timestamp_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_timestamp_ntp64_time", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.U8)]
        internal static extern ulong z_timestamp_ntp64_time(IntPtr timestamp);

        /// z_result_t
        /// z_undeclare_keyexpr(
        ///     const struct z_loaned_session_t *session,
        ///     struct z_moved_keyexpr_t *key_expr
        /// )
        [DllImport(DllName, EntryPoint = "z_undeclare_keyexpr", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result z_undeclare_keyexpr(IntPtr session, IntPtr keyexpr);

#if UNSTABLE_API
        /// z_result_t
        /// z_undeclare_matching_listener(
        ///     struct z_moved_matching_listener_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_undeclare_matching_listener", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result z_undeclare_matching_listener(IntPtr listener);
#endif

        /// z_result_t
        /// z_undeclare_publisher(
        ///     struct z_moved_publisher_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_undeclare_publisher", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result z_undeclare_publisher(IntPtr publisher);

#if UNSTABLE_API
        /// z_result_t
        /// z_undeclare_querier(
        ///     struct z_moved_querier_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_undeclare_querier", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result z_undeclare_querier(IntPtr querier);
#endif

        /// z_result_t
        /// z_undeclare_queryable(
        ///     struct z_moved_queryable_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_undeclare_queryable", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result z_undeclare_queryable(IntPtr queryable);

        /// z_result_t
        /// z_undeclare_subscriber(
        ///     struct z_moved_subscriber_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_undeclare_subscriber", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result z_undeclare_subscriber(IntPtr subscriber);

        [DllImport(DllName, EntryPoint = "z_view_keyexpr_empty", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_view_keyexpr_empty(IntPtr viewKeyexpr);

        [DllImport(DllName, EntryPoint = "z_view_keyexpr_from_str", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result z_view_keyexpr_from_str(IntPtr viewKeyexpr, IntPtr str);

        [DllImport(DllName, EntryPoint = "z_view_keyexpr_from_str_autocanonize",
            CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result z_view_keyexpr_from_str_autocanonize(IntPtr viewKeyexpr, IntPtr str);

        [DllImport(DllName, EntryPoint = "z_view_keyexpr_from_str_unchecked",
            CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_view_keyexpr_from_str_unchecked(IntPtr viewKeyexpr, IntPtr str);

        [DllImport(DllName, EntryPoint = "z_view_keyexpr_from_substr", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result z_view_keyexpr_from_substr(IntPtr viewKeyexpr, IntPtr str, UIntPtr len);

        [DllImport(DllName, EntryPoint = "z_view_keyexpr_from_substr_autocanonize",
            CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result z_view_keyexpr_from_substr_autocanonize(IntPtr viewKeyexpr, IntPtr start,
            UIntPtr len);

        [DllImport(DllName, EntryPoint = "z_view_keyexpr_from_substr_unchecked",
            CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_view_keyexpr_from_substr_unchecked(IntPtr viewKeyexpr, IntPtr start,
            UIntPtr len);

        [DllImport(DllName, EntryPoint = "z_view_keyexpr_is_empty", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool z_view_keyexpr_is_empty(IntPtr viewKeyexpr);

        [DllImport(DllName, EntryPoint = "z_view_keyexpr_loan", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_view_keyexpr_loan(IntPtr viewKeyexpr);

        [DllImport(DllName, EntryPoint = "z_view_slice_empty", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_view_slice_empty(IntPtr viewSlice);

        [DllImport(DllName, EntryPoint = "z_view_slice_from_buf", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result z_view_slice_from_buf(IntPtr viewSlice, IntPtr start, UIntPtr len);

        [DllImport(DllName, EntryPoint = "z_view_slice_is_empty", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool z_view_slice_is_empty(IntPtr viewSlice);

        [DllImport(DllName, EntryPoint = "z_view_slice_loan", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_view_slice_loan(IntPtr viewSlice);

        /// void
        /// z_view_string_empty(
        ///     struct z_view_string_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_view_string_empty", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void z_view_string_empty(IntPtr viewString);

        /// z_result_t
        /// z_view_string_from_str(struct z_view_string_t *this_,
        // const char *str);
        [DllImport(DllName, EntryPoint = "z_view_string_from_str", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result z_view_string_from_str(IntPtr viewString, IntPtr str);

        /// z_result_t
        /// z_view_string_from_substr(
        ///     struct z_view_string_t *this_,
        ///     const char *str,
        ///     size_t len
        /// )
        [DllImport(DllName, EntryPoint = "z_view_string_from_substr", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result z_view_string_from_substr(IntPtr viewString, IntPtr str, UIntPtr len);

        /// bool
        /// z_view_string_is_empty(
        ///     const struct z_view_string_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_view_string_is_empty", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool z_view_string_is_empty(IntPtr viewString);

        /// const struct z_loaned_string_t*
        /// z_view_string_loan(
        ///     const struct z_view_string_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "z_view_string_loan", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr z_view_string_loan(IntPtr viewString);

        [DllImport(DllName, EntryPoint = "z_whatami_to_view_string", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result z_whatami_to_view_string(Whatami whatami, IntPtr strOut);

        /// void
        /// (*call)(
        ///     enum zc_log_severity_t severity,
        ///     const struct z_loaned_string_t *msg,
        ///     void *context
        /// )
        internal delegate void CbClosureLogCall(LogSeverity severity, IntPtr msg, IntPtr context);

        /// void
        /// zc_closure_log(
        ///     struct zc_owned_closure_log_t *this_,
        ///     void (*call)(
        ///         enum zc_log_severity_t severity,
        ///         const struct z_loaned_string_t *msg,
        ///         void *context
        ///     ),
        ///     void (*drop)(
        ///         void *context
        ///     ),
        ///     void *context
        /// );
        [DllImport(DllName, EntryPoint = "zc_closure_log", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void zc_closure_log(IntPtr closure, CbClosureLogCall call, Cb1 drop, IntPtr context);

        [DllImport(DllName, EntryPoint = "zc_closure_log_call", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void zc_closure_log_call(IntPtr closure, LogSeverity serverity,
            IntPtr msg);

        /// void
        /// zc_closure_log_drop(
        ///     struct zc_moved_closure_log_t *closure_
        /// )
        [DllImport(DllName, EntryPoint = "zc_closure_log_drop", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void zc_closure_log_call_drop(IntPtr closure);

        [DllImport(DllName, EntryPoint = "zc_closure_log_loan", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr zc_closure_log_call_loan(IntPtr closure);

#if UNSTABLE_API
        /// void
        /// zc_concurrent_close_handle_drop(
        ///     struct zc_moved_concurrent_close_handle_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "zc_concurrent_close_handle_drop", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void zc_concurrent_close_handle_drop(IntPtr handle);
#endif

#if UNSTABLE_API
        /// void
        /// zc_concurrent_close_handle_drop(
        ///     struct zc_moved_concurrent_close_handle_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "zc_concurrent_close_handle_wait", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void zc_concurrent_close_handle_wait(IntPtr handle);
#endif

        /// z_result_t
        /// zc_config_from_env(
        ///     struct z_owned_config_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "zc_config_from_env", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result zc_config_from_env(IntPtr config);

        /// z_result_t
        /// zc_config_from_file(
        ///     struct z_owned_config_t *this_,
        ///     const char *path
        /// )
        [DllImport(DllName, EntryPoint = "zc_config_from_file", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result zc_config_from_file(IntPtr config, IntPtr path);

        /// z_result_t
        /// zc_config_from_str(
        ///     struct z_owned_config_t *this_,
        ///     const char *s
        /// )
        [DllImport(DllName, EntryPoint = "zc_config_from_str", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result zc_config_from_str(IntPtr config, IntPtr str);

        [DllImport(DllName, EntryPoint = "zc_config_get_from_str", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result zc_config_get_from_str(IntPtr config, IntPtr key,
            IntPtr outValueString);

        [DllImport(DllName, EntryPoint = "zc_config_get_from_substr", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result zc_config_get_from_substr(IntPtr config, IntPtr key, UIntPtr keyLen,
            IntPtr outValueString);

        [DllImport(DllName, EntryPoint = "zc_config_insert_json5", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result zc_config_insert_json5(IntPtr config, IntPtr key, IntPtr value);

        [DllImport(DllName, EntryPoint = "zc_config_insert_json5_from_substr",
            CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result zc_config_insert_json5_from_substr(IntPtr config, IntPtr key,
            UIntPtr keyLen,
            IntPtr value, UIntPtr valueLen);

        /// z_result_t
        /// zc_config_to_string(
        ///     const struct z_loaned_config_t *config,
        ///     struct z_owned_string_t *out_config_string
        /// )
        [DllImport(DllName, EntryPoint = "zc_config_to_string", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result zc_config_to_string(IntPtr config, IntPtr outConfigString);

        [DllImport(DllName, EntryPoint = "zc_try_init_log_from_env", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void zc_try_init_log_from_env();

        [DllImport(DllName, EntryPoint = "zc_init_log_from_env_or", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result zc_init_log_from_env_or(IntPtr fallbackFilter);

        /// void
        /// zc_init_log_with_callback(
        ///     enum zc_log_severity_t min_severity,
        ///     struct zc_moved_closure_log_t *callback
        /// )
        [DllImport(DllName, EntryPoint = "zc_init_log_with_callback", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void zc_init_log_with_callback(LogSeverity minSeverity, IntPtr callback);

        [DllImport(DllName, EntryPoint = "zc_internal_closure_log_check", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool zc_internal_closure_log_check(IntPtr closure);

        [DllImport(DllName, EntryPoint = "zc_internal_closure_log_null", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void zc_internal_closure_log_null(IntPtr closure);

#if UNSTABLE_API
        /// bool
        /// zc_internal_concurrent_close_handle_check(
        ///     const struct zc_owned_concurrent_close_handle_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "zc_internal_concurrent_close_handle_check",
            CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool zc_internal_concurrent_close_handle_check(IntPtr handle);
#endif

#if UNSTABLE_API
        /// void
        /// zc_internal_concurrent_close_handle_null(
        ///     struct zc_owned_concurrent_close_handle_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "zc_internal_concurrent_close_handle_null",
            CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void zc_internal_concurrent_close_handle_null(IntPtr handle);
#endif

#if UNSTABLE_API
        /// enum zc_locality_t
        /// zc_locality_default(
        ///     void
        /// )
        [DllImport(DllName, EntryPoint = "zc_locality_default", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Locality zc_locality_default();
#endif

#if UNSTABLE_API
        /// enum zc_reply_keyexpr_t
        /// zc_reply_keyexpr_default(
        ///     void
        /// )
        [DllImport(DllName, EntryPoint = "zc_reply_keyexpr_default", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern ReplyKeyexpr zc_reply_keyexpr_default();
#endif

        /// void
        /// zc_internal_encoding_from_data(
        ///     struct z_owned_encoding_t *this_,
        ///     struct zc_internal_encoding_data_t data
        /// )
        [DllImport(DllName, EntryPoint = "zc_internal_encoding_from_data", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void zc_internal_encoding_from_data(IntPtr encoding, ZcInternalEncodingData data);

        /// struct zc_internal_encoding_data_t
        /// zc_internal_encoding_get_data(
        ///     const struct z_loaned_encoding_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "zc_internal_encoding_get_data", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern ZcInternalEncodingData zc_internal_encoding_get_data(IntPtr encoding);

        [DllImport(DllName, EntryPoint = "zc_stop_z_runtime", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void zc_stop_z_runtime();

        [DllImport(DllName, EntryPoint = "ze_deserialize_bool", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result ze_deserialize_bool(IntPtr bytes, IntPtr dst);

        [DllImport(DllName, EntryPoint = "ze_deserialize_double", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result ze_deserialize_double(IntPtr bytes, IntPtr dst);

        [DllImport(DllName, EntryPoint = "ze_deserialize_float", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result ze_deserialize_float(IntPtr bytes, IntPtr dst);

        [DllImport(DllName, EntryPoint = "ze_deserialize_int8", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result ze_deserialize_int8(IntPtr bytes, IntPtr dst);

        [DllImport(DllName, EntryPoint = "ze_deserialize_int16", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result ze_deserialize_int16(IntPtr bytes, IntPtr dst);

        [DllImport(DllName, EntryPoint = "ze_deserialize_int32", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result ze_deserialize_int32(IntPtr bytes, IntPtr dst);

        [DllImport(DllName, EntryPoint = "ze_deserialize_int64", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result ze_deserialize_int64(IntPtr bytes, IntPtr dst);

        [DllImport(DllName, EntryPoint = "ze_deserialize_uint8", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result ze_deserialize_uint8(IntPtr bytes, IntPtr dst);

        [DllImport(DllName, EntryPoint = "ze_deserialize_uint16", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result ze_deserialize_uint16(IntPtr bytes, IntPtr dst);

        [DllImport(DllName, EntryPoint = "ze_deserialize_uint32", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result ze_deserialize_uint32(IntPtr bytes, IntPtr dst);

        [DllImport(DllName, EntryPoint = "ze_deserialize_uint64", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result ze_deserialize_uint64(IntPtr bytes, IntPtr dst);

        [DllImport(DllName, EntryPoint = "ze_deserialize_slice", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result ze_deserialize_slice(IntPtr bytes, IntPtr slice);

        [DllImport(DllName, EntryPoint = "ze_deserialize_string", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result ze_deserialize_string(IntPtr bytes, IntPtr str);

        [DllImport(DllName, EntryPoint = "ze_deserializer_deserialize_bool",
            CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result ze_deserializer_deserialize_bool(IntPtr deserializer, IntPtr dst);

        [DllImport(DllName, EntryPoint = "ze_deserializer_deserialize_double",
            CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result ze_deserializer_deserialize_double(IntPtr deserializer, IntPtr dst);

        [DllImport(DllName, EntryPoint = "ze_deserializer_deserialize_float",
            CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result ze_deserializer_deserialize_float(IntPtr deserializer, IntPtr dst);

        [DllImport(DllName, EntryPoint = "ze_deserializer_deserialize_int8",
            CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result ze_deserializer_deserialize_int8(IntPtr deserializer, IntPtr dst);

        [DllImport(DllName, EntryPoint = "ze_deserializer_deserialize_int16",
            CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result ze_deserializer_deserialize_int16(IntPtr deserializer, IntPtr dst);

        [DllImport(DllName, EntryPoint = "ze_deserializer_deserialize_int32",
            CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result ze_deserializer_deserialize_int32(IntPtr deserializer, IntPtr dst);

        [DllImport(DllName, EntryPoint = "ze_deserializer_deserialize_int64",
            CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result ze_deserializer_deserialize_int64(IntPtr deserializer, IntPtr dst);

        [DllImport(DllName, EntryPoint = "ze_deserializer_deserialize_uint8",
            CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result ze_deserializer_deserialize_uint8(IntPtr deserializer, IntPtr dst);

        [DllImport(DllName, EntryPoint = "ze_deserializer_deserialize_uint16",
            CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result ze_deserializer_deserialize_uint16(IntPtr deserializer, IntPtr dst);

        [DllImport(DllName, EntryPoint = "ze_deserializer_deserialize_uint32",
            CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result ze_deserializer_deserialize_uint32(IntPtr deserializer, IntPtr dst);

        [DllImport(DllName, EntryPoint = "ze_deserializer_deserialize_uint64",
            CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result ze_deserializer_deserialize_uint64(IntPtr deserializer, IntPtr dst);

        [DllImport(DllName, EntryPoint = "ze_deserializer_deserialize_sequence_length",
            CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result
            ze_deserializer_deserialize_sequence_length(IntPtr deserializer, IntPtr len);

        [DllImport(DllName, EntryPoint = "ze_deserializer_deserialize_slice",
            CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result ze_deserializer_deserialize_slice(IntPtr deserializer,
            IntPtr slice);

        [DllImport(DllName, EntryPoint = "ze_deserializer_deserialize_string",
            CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result ze_deserializer_deserialize_string(IntPtr deserializer,
            IntPtr str);

        [DllImport(DllName, EntryPoint = "ze_deserializer_from_bytes", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern ZeDeserializer ze_deserializer_from_bytes(IntPtr bytes);

        [DllImport(DllName, EntryPoint = "ze_deserializer_is_done", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool ze_deserializer_is_done(IntPtr deserializer);

        [DllImport(DllName, EntryPoint = "ze_internal_serializer_check", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool ze_internal_serializer_check(IntPtr serializer);

        [DllImport(DllName, EntryPoint = "ze_internal_serializer_null", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void ze_internal_serializer_null(IntPtr serializer);

        [DllImport(DllName, EntryPoint = "ze_serialize_buf", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result ze_serialize_buf(IntPtr bytes, IntPtr data, UIntPtr len);

        [DllImport(DllName, EntryPoint = "ze_serialize_bool", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result ze_serialize_bool(IntPtr bytes, byte val);

        [DllImport(DllName, EntryPoint = "ze_serialize_double", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result ze_serialize_double(IntPtr bytes, double val);

        [DllImport(DllName, EntryPoint = "ze_serialize_float", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result ze_serialize_float(IntPtr bytes, float val);

        [DllImport(DllName, EntryPoint = "ze_serialize_int8", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result ze_serialize_int8(IntPtr bytes, sbyte val);

        [DllImport(DllName, EntryPoint = "ze_serialize_int16", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result ze_serialize_int16(IntPtr bytes, short val);

        [DllImport(DllName, EntryPoint = "ze_serialize_int32", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result ze_serialize_int32(IntPtr bytes, int val);

        [DllImport(DllName, EntryPoint = "ze_serialize_int64", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result ze_serialize_int64(IntPtr bytes, long val);

        [DllImport(DllName, EntryPoint = "ze_serialize_uint8", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result ze_serialize_uint8(IntPtr bytes, byte val);

        [DllImport(DllName, EntryPoint = "ze_serialize_uint16", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result ze_serialize_uint16(IntPtr bytes, ushort val);

        [DllImport(DllName, EntryPoint = "ze_serialize_uint32", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result ze_serialize_uint32(IntPtr bytes, uint val);

        [DllImport(DllName, EntryPoint = "ze_serialize_uint64", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result ze_serialize_uint64(IntPtr bytes, ulong val);

        [DllImport(DllName, EntryPoint = "ze_serialize_slice", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result ze_serialize_slice(IntPtr bytes, IntPtr slice);

        [DllImport(DllName, EntryPoint = "ze_serialize_str", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result ze_serialize_str(IntPtr bytes, IntPtr str);

        [DllImport(DllName, EntryPoint = "ze_serialize_substr", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result ze_serialize_substr(IntPtr bytes, IntPtr start, UIntPtr len);

        [DllImport(DllName, EntryPoint = "ze_serialize_string", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result ze_serialize_string(IntPtr bytes, IntPtr str);

        /// void
        /// ze_serializer_drop(
        ///     struct ze_moved_serializer_t *this_
        /// )
        [DllImport(DllName, EntryPoint = "ze_serializer_drop", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void ze_serializer_drop(IntPtr serializer);

        [DllImport(DllName, EntryPoint = "ze_serializer_empty", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result ze_serializer_empty(IntPtr serializer);

        /// void
        /// ze_serializer_finish(
        ///     struct ze_moved_serializer_t *this_,
        ///     struct z_owned_bytes_t *bytes
        /// )
        [DllImport(DllName, EntryPoint = "ze_serializer_finish", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern void ze_serializer_finish(IntPtr serializer, IntPtr bytes);

        [DllImport(DllName, EntryPoint = "ze_serializer_loan", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern IntPtr ze_serializer_loan(IntPtr serializer);


        [DllImport(DllName, EntryPoint = "ze_serializer_serialize_bool", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result ze_serializer_serialize_bool(IntPtr serializer, byte val);

        [DllImport(DllName, EntryPoint = "ze_serializer_serialize_double", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result ze_serializer_serialize_double(IntPtr serializer, double val);

        [DllImport(DllName, EntryPoint = "ze_serializer_serialize_float", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result ze_serializer_serialize_float(IntPtr serializer, float val);

        [DllImport(DllName, EntryPoint = "ze_serializer_serialize_int8", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result ze_serializer_serialize_int8(IntPtr serializer, sbyte val);

        [DllImport(DllName, EntryPoint = "ze_serializer_serialize_int16", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result ze_serializer_serialize_int16(IntPtr serializer, short val);

        [DllImport(DllName, EntryPoint = "ze_serializer_serialize_int32", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result ze_serializer_serialize_int32(IntPtr serializer, int val);

        [DllImport(DllName, EntryPoint = "ze_serializer_serialize_int64", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result ze_serializer_serialize_int64(IntPtr serializer, long val);

        [DllImport(DllName, EntryPoint = "ze_serializer_serialize_uint8", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result ze_serializer_serialize_uint8(IntPtr serializer, byte val);

        [DllImport(DllName, EntryPoint = "ze_serializer_serialize_uint16", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result ze_serializer_serialize_uint16(IntPtr serializer, ushort val);

        [DllImport(DllName, EntryPoint = "ze_serializer_serialize_uint32", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result ze_serializer_serialize_uint32(IntPtr serializer, uint val);

        [DllImport(DllName, EntryPoint = "ze_serializer_serialize_uint64", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result ze_serializer_serialize_uint64(IntPtr serializer, ulong val);

        [DllImport(DllName, EntryPoint = "ze_serializer_serialize_buf", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result
            ze_serializer_serialize_buf(IntPtr serializer, IntPtr data, UIntPtr len);

        [DllImport(DllName, EntryPoint = "ze_serializer_serialize_sequence_length",
            CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result
            ze_serializer_serialize_sequence_length(IntPtr serializer, UIntPtr len);

        [DllImport(DllName, EntryPoint = "ze_serializer_serialize_slice", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result
            ze_serializer_serialize_slice(IntPtr serializer, IntPtr slice);

        [DllImport(DllName, EntryPoint = "ze_serializer_serialize_str", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result ze_serializer_serialize_str(IntPtr serializer, IntPtr str);

        [DllImport(DllName, EntryPoint = "ze_serializer_serialize_substr", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result ze_serializer_serialize_substr(IntPtr serializer, IntPtr start,
            UIntPtr len);

        [DllImport(DllName, EntryPoint = "ze_serializer_serialize_string", CallingConvention = CallingConvention.Cdecl,
            ExactSpelling = true)]
        internal static extern Result
            ze_serializer_serialize_string(IntPtr serializer, IntPtr str);
    }
}