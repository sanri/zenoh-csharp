![zenoh banner](./zenoh-dragon.png)

[//]: # ([![NuGet]&#40;https://img.shields.io/nuget/v/Zenoh-CS?color=blue&#41;]&#40;https://www.nuget.org/packages/Zenoh-CS/&#41;)
[![License](https://img.shields.io/badge/License-EPL%202.0-blue)](https://choosealicense.com/licenses/epl-2.0/)
[![License](https://img.shields.io/badge/License-Apache%202.0-blue.svg)](https://opensource.org/licenses/Apache-2.0)

# Zenoh C# API

[中文/chinese readme](README.zh.md)

[Zenoh](http://zenoh.io) is an extremely efficient and fault-tolerant [Named Data Networking](http://named-data.net) (NDN) protocol that is able to scale down to extremely constrainded devices and networks.

Zenoh-CS provides the common interface of Zenoh-C.
can be easily tested against a zenoh router running in a Docker container (see [quick test](https://zenoh.io/docs/getting-started/quick-test/)).


-------------------------------
## Supported runtime environments
The library targets.NET Standard 2.0, which means it should work on multiple runtimes.
Reference Links [netstandard2.0](https://learn.microsoft.com/en-us/dotnet/standard/net-standard?tabs=net-standard-2-0)

### .NET Runtime
| .NET implementation | Version support         |
|:--------------------|-------------------------|
| .NET                | 5.0, 6.0, 7.0, 8.0, 9.0 |
| .NET Framework      | 4.8.1                   |
| Unity               | 2022.3                  |

### Supported CPU arch
- x64
- arm64 (planned, untested)

### Mapping between Zenoh-CS and Zenoh-C versions
|     Zenoh-C     |    Zenoh-CS    |
|:---------------:|:--------------:|
|    v0.7.2-rc    |     v0.1.*     |
|     v1.3.0      |     v0.2.0     |
| v1.3.2 ~ v1.3.3 |     v0.2.2     |
|     v1.5.0      |     v0.3.0     |
|     v1.6.2      | v0.4.0 ~ 0.4.1 |

### Development and test environment composition
| OS           | CPU | .NET implementation  | Notes |
|--------------|-----|----------------------|-------|
| Windows 11   | x64 | .NET 8.0             | main  |
| Windows 11   | x64 | .NET Framework 4.8.1 |       |
| macOS 15     | x64 | .NET 8.0             |       |
| macOS 15     | x64 | Unity 2022.3         |       |
| Ubuntu 24.04 | x64 | .NET 8.0             |       |


-------------------------------
## How to install and use

Requirements:
- The [zenoh-c](https://github.com/eclipse-zenoh/zenoh-c) library must be [installed](https://zenoh.io/docs/getting-started/installation/) on your host.
  - You need to select the zenoh-c version that corresponds to the zenoh-cs version (for example, zenoh-c V1.6.2).
  - Compatible with zenoh compiled [library](https://github.com/eclipse-zenoh/zenoh-c/releases).
  - If you build zenoh-c yourself, you will need to use the **UNSTABLE_API**, **SHARED_MEMORY** compilation options.
  
### Use source code in your project
(todo)

### Build library files

Requirements:
- a .NET environment. .NET8 (or other compatible versions) [SDK](https://dotnet.microsoft.com/zh-cn/download/dotnet)

Run the command in directory Zenoh.
```shell
dotnet build Zenoh.csproj -c Release 
```

### Download the library using Nuget
Links to https://www.nuget.org/packages/Zenoh-CS/


-------------------------------
## Running the Examples

Build and run the zenoh-csharp examples following the instructions in [examples/README.md](examples/README.md)
