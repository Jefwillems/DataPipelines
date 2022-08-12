using Jef.DataPipeline.Contracts;
using Jef.DataPipeline.DemoApp.ViewModels;

namespace Jef.DataPipeline.DemoApp;

public class HttpExampleTransformer : BaseTransformer<HttpGetData, OutputData>
{
    private readonly ILogger<HttpExampleTransformer> _logger;

    public HttpExampleTransformer(BaseDestination<OutputData> destination,
        ILogger<HttpExampleTransformer> logger) : base(destination)
    {
        _logger = logger;
    }

    protected override Task<OutputData> Process(HttpGetData input, Context context)
    {
        return Task.FromResult(new OutputData { World = input.Origin });
    }
}