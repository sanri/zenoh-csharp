![zenoh banner](./zenoh-dragon.png)

[//]: # ([![NuGet]&#40;https://img.shields.io/nuget/v/Zenoh-CS?color=blue&#41;]&#40;https://www.nuget.org/packages/Zenoh-CS/&#41;)
[![License](https://img.shields.io/badge/License-EPL%202.0-blue)](https://choosealicense.com/licenses/epl-2.0/)
[![License](https://img.shields.io/badge/License-Apache%202.0-blue.svg)](https://opensource.org/licenses/Apache-2.0)

# Zenoh C# API

[Zenoh](http://zenoh.io)是一种非常高效和容错的命名数据网络([NDN](http://named-data.net))协议, 能够在非常受限制的设备和网络中运行.

C# API是用于纯客户端的, 可以很容易地针对运行在Docker容器中的zenoh路由器进行测试 (参考[快速测试](https://zenoh.io/docs/getting-started/quick-test/)).

-------------------------------
## 如何安装

需求:
- 库 [zenoh-c](https://github.com/eclipse-zenoh/zenoh-c) 必需被安装在你的主机上.
    - 编译 zenoh-c 时使用默认构建选项
    - 目前不支持 zenoh-c 的 **UNSTABLE_API**, **SHARED_MEMORY** 编译选项
- 需要一个 .NET 运行时

### 支持的 .NET 运行时 
- .NET 8.0
- .NET 9.0 (计划,未测试)
- Unity 2022.3 (计划,未测试) 

### 支持的CPU架构
- x64
- arm64 (计划,未测试)

### Zenoh-CS 版本与 Zenoh-C 版本对应关系
|  Zenoh-C  | Zenoh-CS |
|:---------:|:--------:|
| v0.7.2-rc |  v0.1.*  |
|  v1.2.*   |  v0.2.*  |

-------------------------------
## 如何构建 

需求:
- 库 [zenoh-c](https://github.com/eclipse-zenoh/zenoh-c) 必需被安装在你的主机上.
- 主机安装有 .NET8 的 [SDK](https://dotnet.microsoft.com/zh-cn/download/dotnet)

构建命令:   
```shell
dotnet build Zenoh.csproj -c Release 
```

-------------------------------
## 运行示例

构建和运行示程序, 参考  [examples/README.zh.md](examples/README.zh.md)

