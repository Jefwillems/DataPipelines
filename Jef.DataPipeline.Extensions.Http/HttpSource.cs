using Jef.DataPipeline.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace Jef.DataPipeline.Extensions.Http;

public class HttpSource<TInput> : BaseDataSource<TInput>
{
    private readonly HttpConfiguration _config;

    public HttpSource(
        IServiceScopeFactory scopeFactory,
        HttpConfiguration config)
        : base(scopeFactory.CreateScope().ServiceProvider.GetRequiredService<ITransformer<TInput>>())
    {
        _config = config;
    }
}