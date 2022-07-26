namespace Jef.DataPipeline.Contracts;

public interface ITransformer<in TInput>
{
    Task Transform(TInput input, Context context);
}

public abstract class BaseTransformer<TInput, TOutput> : ITransformer<TInput>
{
    private readonly BaseDestination<TOutput> _destination;

    protected BaseTransformer(BaseDestination<TOutput> destination)
    {
        _destination = destination;
    }

    public abstract Task Transform(TInput input, Context context);

    public async Task SendToDestination(TOutput output, Context context)
    {
        await _destination.SendData(output, context);
    }
}