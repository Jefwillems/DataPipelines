using Jef.DataPipeline.Contracts;
using Jef.DataPipeline.DemoApp.ViewModels;

namespace Jef.DataPipeline.DemoApp;

public class MyTransformer : BaseTransformer<InputData, OutputData>
{
    private readonly ILogger<MyTransformer> _logger;

    public MyTransformer(
        BaseDestination<OutputData> destination,
        ILogger<MyTransformer> logger) : base(destination)
    {
        _logger = logger;
    }

    protected override Task<OutputData> Process(InputData input, Context context)
    {
        _logger.LogInformation("transforming {InputType} to {OutputType}", nameof(InputData), nameof(OutputData));
        var x = input.Hello;
        return Task.FromResult(new OutputData { World = $"!!!!!{x}!!!!!" });
    }
}