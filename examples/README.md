# Zenoh C# examples

## Introduce 

Each example accepts the `--help` option that provides a description of its arguments and their default values.

If you run the tests against the zenoh router running in a Docker container, 
you need to add the `-c` option in the examples, configuring the connection parameters. 
That's because Docker don't support UDP multicast transport, 
and therefore the zenoh scouting and discovery mechanism cannot work with.

The example configuration file is [demo.json5](demo.json5)

## Examples description

### ZInfo

Get the ID of each node in the Zenoh network.

Build
```bash
dotnet build ZInfo/ZInfo.csproj --configuration Release 
```

Run
```bash
./ZInfo -c <zenoh_config_file> 
```

### ZGet

Sends a query message for a selector.
The queryables with a matching path or selector (for instance ZQueryable ) will receive this query and reply with paths/values that will be received by the receiver stream.

Build
```bash
dotnet build ZGet/ZGet.csproj --configuration Release 
```

Run
```bash
./ZGet -c <zenoh_config_file> 
```

### ZQueryable

Declares a queryable function with a path.
This queryable function will be triggered by each call to get with a selector that matches the path, and will return a value to the querier.

Build
```bash
dotnet build ZQueryable/ZQueryable.csproj --configuration Release 
```

Run
```bash
./ZQueryable -c <zenoh_config_file>
```


### ZPut

Writes a path/value into Zenoh.  
The path/value will be received by all matching subscribers, for instance the [ZSub](#ZSub) examples.

Build
```bash
dotnet build ZPut/ZPut.csproj --configuration Release 
```

Run
```bash
./ZPut -c <zenoh_config_file>
```

### ZPub
Writes a path/value into Zenoh.  
The path/value will be received by all matching subscribers, for instance the [ZSub](#ZSub) examples.    
Compared to ZPut, this example is optimized for keys that publish data frequently, reducing some of the overhead.

Build
```bash
dotnet build ZPub/ZPub.csproj --configuration Release 
```

Run
```bash
./ZPub -c <zenoh_config_file>
```

### ZSub

Registers a subscriber with a selector.  
The subscriber will be notified of each write made on any path matching the selector,
and will print this notification.

Build
```bash
dotnet build ZSub/ZSub.csproj --configuration Release 
```

Run
```bash
./ZSub -c <zenoh_config_file>
```