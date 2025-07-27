![zenoh banner](./zenoh-dragon.png)

[//]: # ([![NuGet]&#40;https://img.shields.io/nuget/v/Zenoh-CS?color=blue&#41;]&#40;https://www.nuget.org/packages/Zenoh-CS/&#41;)
[![License](https://img.shields.io/badge/License-EPL%202.0-blue)](https://choosealicense.com/licenses/epl-2.0/)
[![License](https://img.shields.io/badge/License-Apache%202.0-blue.svg)](https://opensource.org/licenses/Apache-2.0)

# Zenoh C# API

[Zenoh](http://zenoh.io)是一种非常高效和容错的命名数据网络([NDN](http://named-data.net))协议, 能够在非常受限制的设备和网络中运行.

Zenoh-CS 提供 Zenoh-C 的常用接口, 可以很容易地针对运行在Docker容器中的zenoh路由器进行测试 (参考[快速测试](https://zenoh.io/docs/getting-started/quick-test/)).


-------------------------------
## 支持的运行环境 
库本的目标框架为 **.Net Standard 2.0** , 意味着这个库应该能在多个运行时上正常工作, 
参考链接 [netstandard2.0](https://learn.microsoft.com/zh-cn/dotnet/standard/net-standard?tabs=net-standard-2-0)

### NET 运行时
| .NET 实现        | 版本支持                    | 
|:---------------|-------------------------|
| .NET           | 5.0, 6.0, 7.0, 8.0, 9.0 | 
| .NET Framework | 4.8.1                   | 
| Unity          | 2022.3                  | 

### 支持的CPU架构
- x64
- arm64 (计划,未测试)

#### Zenoh-CS 版本与 Zenoh-C 版本对应关系
|     Zenoh-C     | Zenoh-CS |
|:---------------:|:--------:|
|    v0.7.2-rc    |  v0.1.*  |
|     v1.3.0      |  v0.2.0  |
| v1.3.2 ~ v1.3.3 |  v0.2.2  |
|     v1.5.0      |  v0.3.0  |

### 开发和测试环境组合

| 操作系统         | CPU | .NET 实现              | 备注   |
|--------------|-----|----------------------|------|
| macOS 15     | x64 | .NET 8.0             | 主要环境 |
| macOS 15     | x64 | Unity 2022.3         |      |
| Windows 11   | x64 | .NET 8.0             |      |
| Windows 11   | x64 | .NET Framework 4.8.1 |      |
| Ubuntu 24.04 | x64 | .NET 8.0             |      |


-------------------------------
## 如何安装使用

需求:
- [zenoh-c](https://github.com/eclipse-zenoh/zenoh-c) 必需被 [安装](https://zenoh.io/docs/getting-started/installation/) 在你的主机上.
  - 需要选择与 zenoh-cs 版本对应的 zenoh-c 版本 (例如 zenoh-c V1.5.0).
  - 兼容 zenoh 官方提供的编译好的 [库文件](https://github.com/eclipse-zenoh/zenoh-c/releases).
  - 若自行编译 zenoh-c, 则需使用 **UNSTABLE_API**, **SHARED_MEMORY** 编译选项.

### 在项目里使用源码
(待补充)

### 构建库文件

需求:
- 主机安装有 .NET8 (或其它兼容版本) 的 [SDK](https://dotnet.microsoft.com/zh-cn/download/dotnet)

在目录Zenoh内执行构建命令:
```shell
dotnet build Zenoh.csproj -c Release 
```

### 使用Nuget下载库文件
链接地址为 https://www.nuget.org/packages/Zenoh-CS/

-------------------------------
## 运行示例

构建和运行示程序, 参考  [examples/README.zh.md](examples/README.zh.md)

