# Data pipelines

## Usage

```csharp
builder.Services.AddPipeline<InputData, OutputData>(conf =>
{
    conf.AddAmqpSource(config => builder.Configuration.GetSection("Amqp").Bind(config))
        .SetTransformer<MyTransformer>();
});
```

```csharp
builder.Services.AddPipeline<InputData,OutputData>(conf =>
{
    conf.SetSource<MySource>()
        .SetTransformer<MyTransformer>()
        .SetDestination<MyDestination>();
});
```
