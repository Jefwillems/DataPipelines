using Jef.DataPipeline.Configuration;
using Jef.DataPipeline.DemoApp;
using Jef.DataPipeline.DemoApp.ViewModels;
using Jef.DataPipeline.Extensions.Amqp;
using Jef.DataPipeline.Extensions.Http;

var b = WebApplication.CreateBuilder(args);
b.Configuration.AddCommandLine(args);

var programToRun = b.Configuration.GetValue<int>("Program");

switch (programToRun)
{
    case 1:
        AmqpInHttpOut(b);
        break;
    case 2:
        AmqpToAmqp(b);
        break;
    case 3:
        HttpInHttpOut(b);
        break;
    default:
        Console.WriteLine("No program selected");
        break;
}

void AmqpInHttpOut(WebApplicationBuilder builder)
{
    builder.Services.AddPipeline<InputData, OutputData>(conf =>
    {
        // docker run  -it -p 8161:8161 -p 61616:61616 -p 5672:5672 vromero/activemq-artemis
        conf.AddAmqpSource(config => builder.Configuration.GetSection("Amqp").Bind(config))
            .SetTransformer<MyTransformer>()
            .AddHttpDestination(config => builder.Configuration.GetSection("HttpDestination").Bind(config));
    });
    var app = builder.Build();
    app.Run();
}

void AmqpToAmqp(WebApplicationBuilder builder)
{
    builder.Services.AddPipeline<InputData, OutputData>(conf =>
    {
        conf.AddAmqpSource(config => builder.Configuration.GetSection("Amqp").Bind(config))
            .SetTransformer<MyTransformer>()
            .AddAmqpDestination(config => builder.Configuration.GetSection("Amqp").Bind(config));
    });
    var app = builder.Build();
    app.Run();
}

void HttpInHttpOut(WebApplicationBuilder builder)
{
    builder.Services.AddPipeline<HttpGetData, OutputData>(conf =>
    {
        conf.AddHttpSource(config => builder.Configuration.GetSection("HttpSource").Bind(config))
            .SetTransformer<HttpExampleTransformer>()
            .AddHttpDestination(config => builder.Configuration.GetSection("HttpDestination").Bind(config));
    });
    var app = builder.Build();
    app.Run();
}