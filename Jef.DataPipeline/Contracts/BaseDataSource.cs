using Microsoft.Extensions.DependencyInjection;

namespace Jef.DataPipeline.Contracts;

public abstract class BaseDataSource<TDataType>
{
    private readonly IServiceScopeFactory _scopeFactory;

    protected BaseDataSource(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    protected async Task SendToTransformer(TDataType data, Context context)
    {
        using var scope = _scopeFactory.CreateScope();
        var s = scope.ServiceProvider.GetRequiredService<ITransformer<TDataType>>();
        await s.Execute(data, context);
    }
}