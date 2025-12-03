# Zenoh C# examples

## 介绍

每个示例都接受 `——help` 选项,这些选项提供了对其参数及其默认值的描述.

如果对运行在Docker容器中的zenoh路由器运行测试, 则需要在示例中使用 `-c` 选项指定Zenoh配置文件, 配置连接参数.   
这是因为Docker不支持UDP多播传输, 因此zenoh侦察和发现机制无法工作.

示例配置文件为 [demo.json5](demo.json5)

## 示例说明

### ZGet

发送一个选择器的查询消息.
具有匹配路径或选择器的可查询项(例如 ZQueryable)将接收此查询，并使用接收方接收到的路径进行回复.

构建命令

```bash
dotnet build ZGet/ZGet.csproj 
```

启动命令, 在生成产物目录下运行

```bash
./ZGet -c <zenoh_config_file>
```

### ZGetLiveliness

查询当前匹配给定键表达式的所有活动标记(默认为 "group1/**").
这些标记可以通过 ZLiveliness 示例声明。

构建命令

```bash
dotnet build ZGetLiveliness/ZGetLiveliness.csproj 
```

启动命令, 在生成产物目录下运行

```bash
./ZGetLiveliness -c <zenoh_config_file>
```

### ZInfo

获取关于Zenoh会话的信息.

构建命令

```bash
dotnet build ZInfo/ZInfo.csproj 
```

启动命令, 在生成产物目录下运行

```bash
./ZInfo -c <zenoh_config_file>
```

### ZLiveliness

在给定的键表达式上声明一个活性令牌(默认为 "group1/zenoh-cs-liveliness" ).
该令牌将被ZGetLiveliness和ZSubLiveliness看到, 直到用户通过按 `q` 显式删除令牌, 或通过终止或终止 ZLiveliness 示例隐式删除令牌.

构建命令

```bash
dotnet build ZLiveliness/ZLiveliness.csproj 
```

启动命令, 在生成产物目录下运行

```bash
./ZLiveliness -c <zenoh_config_file>
```

### ZPub

声明一个键表达式和一个发布者, 然后周期性地在声明的键表达式上写入值.
发布的值将由所有匹配的订阅者接收, 例如 ZSub .

构建命令

```bash
dotnet build ZPub/ZPub.csproj 
```

启动命令, 在生成产物目录下运行

```bash
./ZPub -c <zenoh_config_file>
```

### ZPubShm

一个涉及共享内存特性的pub示例.

构建命令

```bash
dotnet build ZPubShm/ZPubShm.csproj 
```

启动命令, 在生成产物目录下运行

```bash
./ZPubShm -c <zenoh_config_file>
```

### ZPull

声明一个key表达式和一个 pull subscriber.
收到的消息都会被缓存在一个 channel 中, 按需从 channel 中取出使用.

构建命令

```bash
dotnet build ZPull/ZPull.csproj 
```

启动命令, 在生成产物目录下运行

```bash
./ZPull -c <zenoh_config_file>
```

### ZPut

将 key/value 写入Zenoh网络.   
key/value 将被所有匹配的订阅者接收, 例如ZSub示例.

构建命令

```bash
dotnet build ZPut/ZPut.csproj 
```

启动命令, 在生成产物目录下运行

```bash
./ZPut -c <zenoh_config_file>
```

### ZQueryable

使用路径声明一个可查询的函数.
这个可查询的函数将在每次使用与路径匹配的选择器调ccc用get时触发, 并将值返回给查询器.

构建命令

```bash
dotnet build ZQueryable/ZQueryable.csproj 
```

启动命令, 在生成产物目录下运行

```bash
./ZQueryable -c <zenoh_config_file>
```

### ZQueryableWithChannels

使用路径声明一个可查询的函数.
收到的查询请求会被缓存到一个 channel 中.

构建命令

```bash
dotnet build ZQueryableWithChannels/ZQueryableWithChannels.csproj 
```

启动命令, 在生成产物目录下运行

```bash
./ZQueryableWithChannels -c <zenoh_config_file>
```

### ZSub

使用选择器注册订阅服务器.    
订阅者将收到与选择器匹配的任何路径上的每次写入的通知, 并将打印此通知.

构建命令

```bash
dotnet build ZSub/ZSub.csproj 
```

启动命令, 在生成产物目录下运行

```bash
./ZSub -c <zenoh_config_file>
```

### ZSubLiveliness

订阅与给定键表达式(默认为 `group1/**`) 匹配的所有 liveliness 更改 (liveliness tokens 激活或删除).
这些标记可以通过ZLiveliness示例声明.

Build

```bash
dotnet build ZSubLiveliness/ZSubLiveliness.csproj 
```

Run

```bash
./ZSubLiveliness -c <zenoh_config_file>
```
