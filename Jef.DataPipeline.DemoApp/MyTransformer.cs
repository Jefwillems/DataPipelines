using Jef.DataPipeline.Contracts;
using Jef.DataPipeline.DemoApp.ViewModels;

namespace Jef.DataPipeline.DemoApp;

public class MyTransformer : BaseTransformer<InputData, OutputData>
{
    public MyTransformer(BaseDestination<OutputData> destination) : base(destination)
    {
    }

    public override async Task Transform(InputData input, Context context)
    {
        var x = input.Hello;
        var output = new OutputData { World = $"!!!!!{x}!!!!!" };
        await SendToDestination(output, context);
    }
}