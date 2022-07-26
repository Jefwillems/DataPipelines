namespace Jef.DataPipeline.Contracts;

public interface ITransformer<in TInput>
{
    Task Execute(TInput input, Context context);
}

public abstract class BaseTransformer<TInput, TOutput> : ITransformer<TInput>
{
    private readonly BaseDestination<TOutput> _destination;

    protected BaseTransformer(BaseDestination<TOutput> destination)
    {
        _destination = destination;
    }

    public async Task Execute(TInput input, Context context)
    {
        var output = await Process(input, context);
        await SendToDestination(output, context);
    }

    protected abstract Task<TOutput> Process(TInput input, Context context);

    private async Task SendToDestination(TOutput output, Context context)
    {
        await _destination.SendData(output, context);
    }
}