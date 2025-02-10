using System;
using System.Runtime.InteropServices;

namespace Zenoh;


public sealed class PublisherOptions : IDisposable
{
    // z_publisher_options_t*
    internal nint HandleZPublisherOptions { get; private set; }

    public PublisherOptions()
    {
        var pPublisherOptions = Marshal.AllocHGlobal(Marshal.SizeOf<ZPublisherOptions>());
        ZenohC.z_publisher_options_default(pPublisherOptions);
        HandleZPublisherOptions = pPublisherOptions;
    }

    public PublisherOptions(PublisherOptions other)
    {
        
    }
    
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    
    ~PublisherOptions() => Dispose(false);

    private void Dispose(bool disposing)
    {
        // todo
    }
}


public sealed class Publisher : IDisposable
{
    // z_owned_publisher_t*
    internal nint HandleZOwnedPublisher { get; private set; }
    
    private Publisher(){}
    

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~Publisher() => Dispose(false);

    private void Dispose(bool disposing)
    {
        // todo
    }
}