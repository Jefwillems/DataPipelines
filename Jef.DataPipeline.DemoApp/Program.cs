using Jef.DataPipeline.Configuration;
using Jef.DataPipeline.Contracts;
using Jef.DataPipeline.DemoApp;
using Jef.DataPipeline.DemoApp.ViewModels;
using Jef.DataPipeline.Extensions.Amqp;
using Jef.DataPipeline.Extensions.Http;

var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddPipeline<InputData, OutputData>(conf =>
// {
//     conf.AddAmqpSource(config => builder.Configuration.GetSection("Amqp").Bind(config))
//         .SetTransformer<MyTransformer>();
// });

builder.Services.AddPipeline<HttpInput, HttpOutput>(conf =>
{
    conf.AddHttpSource(config =>
    {
        config.Uri = "https://httpbin.org/get";
        config.Headers = new Dictionary<string, string> { { "accept", "application/json" } };
        config.Method = HttpMethod.Get;
    }).SetTransformer<MyHttpTransformer>();
});

var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGet("/httptest", async (HttpContext context) =>
{
    var source = context.RequestServices.GetRequiredService<BaseDataSource<HttpInput>>();
    await source.Execute();
    return Results.Ok("Hello World!");
});

app.Run();