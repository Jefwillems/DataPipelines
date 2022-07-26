using Jef.DataPipeline.Contracts;
using Jef.DataPipeline.DemoApp.ViewModels;

namespace Jef.DataPipeline.DemoApp;

public class MyTransformer : BaseTransformer<InputData, OutputData>
{
    public MyTransformer(BaseDestination<OutputData> destination) : base(destination)
    {
    }

    protected override Task<OutputData> Process(InputData input, Context context)
    {
        var x = input.Hello;
        return Task.FromResult(new OutputData { World = $"!!!!!{x}!!!!!" });
    }
}