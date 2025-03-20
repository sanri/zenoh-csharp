![zenoh banner](./zenoh-dragon.png)

[//]: # ([![NuGet]&#40;https://img.shields.io/nuget/v/Zenoh-CS?color=blue&#41;]&#40;https://www.nuget.org/packages/Zenoh-CS/&#41;)
[![License](https://img.shields.io/badge/License-EPL%202.0-blue)](https://choosealicense.com/licenses/epl-2.0/)
[![License](https://img.shields.io/badge/License-Apache%202.0-blue.svg)](https://opensource.org/licenses/Apache-2.0)

# Zenoh C# API

[中文/chinese readme](README.zh.md)

[Zenoh](http://zenoh.io) is an extremely efficient and fault-tolerant [Named Data Networking](http://named-data.net) (NDN) protocol that is able to scale down to extremely constrainded devices and networks.

The C# API is for pure clients, in other terms does not support peer-to-peer communication, 
can be easily tested against a zenoh router running in a Docker container (see https://zenoh.io/docs/getting-started/quick-test/).


-------------------------------
## How to install it

Requirements:
- The [zenoh-c](https://github.com/eclipse-zenoh/zenoh-c) library must be installed on your host.
    - zenoh-c is compiled with default build options.
    - **UNSTABLE_API**, **SHARED_MEMORY** compilation options are currently not supported.

### Supported .NET Runtime
- .NET 8.0
- .NET 9.0 (planned, untested)
- Unity 2022.3 (planned, untested)

### Supported CPU arch
- x64
- arm64 (planned, untested)

### Mapping between Zenoh-CS and Zenoh-C versions
|  Zenoh-C  | Zenoh-CS |
|:---------:|:--------:|
| v0.7.2-rc |  v0.1.*  |
|  v1.2.*   |  v0.2.*  |


-------------------------------
## How to build it

Requirements:  
 * The [zenoh-c](https://github.com/eclipse-zenoh/zenoh-c) library must be installed on your host
 * A .NET environment. .NET8 [SDK](https://dotnet.microsoft.com/zh-cn/download/dotnet)

Build:   
```shell
dotnet build Zenoh.csproj -c Release 
```

-------------------------------
## Running the Examples

Build and run the zenoh-csharp examples following the instructions in [examples/README.md](examples/README.md)
