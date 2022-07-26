using System.Text.Json;
using Jef.DataPipeline.Contracts;

namespace Jef.DataPipeline.Destinations;

public class DefaultDestination<TOutputType> : BaseDestination<TOutputType>
{
    public override Task SendData(TOutputType data, Context context)
    {
        Console.WriteLine(JsonSerializer.Serialize(data));
        Console.WriteLine(JsonSerializer.Serialize(context));
        return Task.CompletedTask;
    }
}