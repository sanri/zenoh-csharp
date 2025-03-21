# Zenoh C# examples

## 介绍
每个示例都接受 `——help` 选项,这些选项提供了对其参数及其默认值的描述.

如果对运行在Docker容器中的zenoh路由器运行测试, 则需要在示例中使用 `-c` 选项指定Zenoh配置文件, 配置连接参数.   
这是因为Docker不支持UDP多播传输, 因此zenoh侦察和发现机制无法工作.

示例配置文件为 [demo.json5](demo.json5)


## 示例说明

### ZInfo

获取当前接入的Zenoh网络中, 各个节点的ID.

构建命令
```bash
dotnet build ZInfo/ZInfo.csproj --configuration Release 
```

启动命令, 在生成产物目录下运行
```bash
./ZInfo -c <zenoh_config_file>
```

### ZGet

为选择器发送查询消息.
具有匹配路径或选择器的可查询项(例如 ZQueryable)将接收此查询，并使用接收方接收到的路径进行回复.

构建命令
```bash
dotnet build ZGet/ZGet.csproj --configuration Release 
```

启动命令, 在生成产物目录下运行
```bash
./ZGet -c <zenoh_config_file>
```

### ZQueryable

声明一个带有路径的可查询函数.
这个可查询的函数将在每次调用get时触发, 调用与路径匹配的选择器, 并将向查询器返回一个值.

构建命令
```bash
dotnet build ZQueryable/ZQueryable.csproj --configuration Release 
```

启动命令, 在生成产物目录下运行
```bash
./ZQueryable -c <zenoh_config_file>
```

### ZPut
将 key/value 写入Zenoh网络.   
key/value 将被所有匹配的订阅者接收, 例如ZSub示例.

构建命令
```bash
dotnet build ZPut/ZPut.csproj --configuration Release 
```

启动命令, 在生成产物目录下运行
```bash
./ZPut -c <zenoh_config_file>
```

### ZPub
将 key/value 写入Zenoh网络.   
key/value 将被所有匹配的订阅者接收, 例如ZSub示例.   
相比于ZPut, 此示例针对经常发布数据的key进行了优化, 减少了部分开销.

构建命令
```bash
dotnet build ZPub/ZPub.csproj --configuration Release 
```

启动命令, 在生成产物目录下运行
```bash
./ZPub -c <zenoh_config_file>
```

### ZSub

使用选择器注册订阅服务器.    
订阅者将收到与选择器匹配的任何路径上的每次写入的通知, 并将打印此通知.

构建命令
```bash
dotnet build ZSub/ZSub.csproj --configuration Release 
```

启动命令, 在生成产物目录下运行
```bash
./ZSub -c <zenoh_config_file>
```
