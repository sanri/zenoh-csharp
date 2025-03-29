# Zenoh C# examples

## Introduce

Each example accepts the `--help` option that provides a description of its arguments and their default values.

If you run the tests against the zenoh router running in a Docker container,
you need to add the `-c` option in the examples, configuring the connection parameters.
That's because Docker don't support UDP multicast transport,
and therefore the zenoh scouting and discovery mechanism cannot work with.

The example configuration file is [demo.json5](demo.json5)

## Examples description

### ZGet

Sends a query message for a selector.
The queryables with a matching path or selector (for instance ZQueryable)
will receive this query and reply with paths/values that will be received by the receiver stream.

Build

```bash
dotnet build ZGet/ZGet.csproj 
```

Run

```bash
./ZGet -c <zenoh_config_file> 
```

### ZGetLiveliness

Queries all the currently alive liveliness tokens that match a given key expression (group1/** by default).
Those tokens could be declared by the ZLiveliness example.

Build

```bash
dotnet build ZGetLiveliness/ZGetLiveliness.csproj
```

Run

```bash
./ZGetLiveliness -c <zenoh_config_file> 
```

### ZInfo

Gets information about the Zenoh session.

Build

```bash
dotnet build ZInfo/ZInfo.csproj 
```

Run

```bash
./ZInfo -c <zenoh_config_file> 
```

### ZLiveliness

Declares a liveliness token on a given key expression (group1/zenoh-rs by default).
This token will be seen alive by the ZGetLiveliness and ZSubLiveliness until
user explicitly drops the token by pressing 'q' or
implicitly dropped by terminating or killing the ZLiveliness example.

Build

```bash
dotnet build ZLiveliness/ZLiveliness.csproj
```

Run

```bash
./ZLiveliness -c <zenoh_config_file> 
```

### ZPub

Declares a key expression and a publisher.
Then writes values periodically on the declared key expression.
The published value will be received by all matching subscribers,
for instance the ZSub example.

Build

```bash
dotnet build ZPub/ZPub.csproj 
```

Run

```bash
./ZPub -c <zenoh_config_file>
```

### ZPull

Declares a key expression and a pull subscriber.
On each pull, the pull subscriber will be notified of the last N put or delete made
on each key expression matching the subscriber key expression, and will print this notification.

Build

```bash
dotnet build ZPull/ZPull.csproj 
```

Run

```bash
./ZPull -c <zenoh_config_file>
```

### ZPut

Puts a path/value into Zenoh.
The path/value will be received by all matching subscribers,
for instance the ZSub example.

Build

```bash
dotnet build ZPut/ZPut.csproj 
```

Run

```bash
./ZPut -c <zenoh_config_file>
```

### ZQueryable

Declares a queryable function with a path.
This queryable function will be triggered by each call to get with a selector that matches the path,
and will return a value to the querier.

Build

```bash
dotnet build ZQueryable/ZQueryable.csproj 
```

Run

```bash
./ZQueryable -c <zenoh_config_file>
```

### ZQueryableWithChannels

Declares a queryable function with a path.
Incoming queries are cached in a channel.

Build

```bash
dotnet build ZQueryableWithChannels/ZQueryableWithChannels.csproj 
```

Run

```bash
./ZQueryableWithChannels -c <zenoh_config_file>
```

### ZSub

Declares a key expression and a subscriber.
The subscriber will be notified of each put or delete made on any key expression matching the subscriber key expression,
and will print this notification.

Build

```bash
dotnet build ZSub/ZSub.csproj 
```

Run

```bash
./ZSub -c <zenoh_config_file>
```

### ZSubLiveliness

Subscribe to all liveliness changes
(liveliness tokens getting alive or liveliness tokens being dropped)
that match a given key expression (group1/** by default).
Those tokens could be declared by the ZLiveliness example.

Build

```bash
dotnet build ZSubLiveliness/ZSubLiveliness.csproj 
```

Run

```bash
./ZSubLiveliness -c <zenoh_config_file>
```
