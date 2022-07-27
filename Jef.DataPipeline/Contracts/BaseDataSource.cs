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

    public async Task Execute()
    {
        var context = new Context();
        var data = await GetData(context);
        await SendToTransformer(data, context);
    }

    public async Task ReceiveData(TDataType input, Context context)
    {
        await SendToTransformer(input, context);
    }

    protected abstract Task<TDataType> GetData(Context context);
}