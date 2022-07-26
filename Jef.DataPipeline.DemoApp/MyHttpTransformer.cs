using Jef.DataPipeline.Contracts;
using Jef.DataPipeline.DemoApp.ViewModels;

namespace Jef.DataPipeline.DemoApp;

public class MyHttpTransformer : BaseTransformer<HttpInput, HttpOutput>
{
    public MyHttpTransformer(BaseDestination<HttpOutput> destination) : base(destination)
    {
    }

    protected override Task<HttpOutput> Process(HttpInput input, Context context)
    {
        return Task.FromResult(new HttpOutput() { Origin = input.Origin });
    }
}