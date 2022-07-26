using Jef.DataPipeline.Configuration;
using Jef.DataPipeline.DemoApp;
using Jef.DataPipeline.DemoApp.ViewModels;
using Jef.DataPipeline.Extensions.Amqp;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPipeline<InputData, OutputData>(conf =>
{
    conf.AddAmqpSource(config => builder.Configuration.GetSection("Amqp").Bind(config))
        .SetTransformer<MyTransformer>();
});

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();