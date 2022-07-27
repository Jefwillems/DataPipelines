using Jef.DataPipeline.Contracts;
using Jef.DataPipeline.DemoApp.ViewModels;

namespace Jef.DataPipeline.DemoApp;

public class MyReverseTransformer : BaseTransformer<OutputData, InputData>
{
    private readonly ILogger<MyReverseTransformer> _logger;

    public MyReverseTransformer(BaseDestination<InputData> destination,
        ILogger<MyReverseTransformer> logger) : base(destination)
    {
        _logger = logger;
    }

    protected override Task<InputData> Process(OutputData input, Context context)
    {
        _logger.LogInformation("transforming {InputType} to {OutputType}", nameof(InputData), nameof(OutputData));
        var x = input.World;
        return Task.FromResult(new InputData { Hello = x.Replace("!", "") });
    }
}