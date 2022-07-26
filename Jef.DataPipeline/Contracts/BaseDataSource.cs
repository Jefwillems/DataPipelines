namespace Jef.DataPipeline.Contracts;

public abstract class BaseDataSource<TDataType>
{
    private readonly ITransformer<TDataType> _transformer;

    protected BaseDataSource(ITransformer<TDataType> transformer)
    {
        _transformer = transformer;
    }

    protected async Task SendToTransformer(TDataType data, Context context)
    {
        await _transformer.Execute(data, context);
    }
}