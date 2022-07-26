using Jef.DataPipeline.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace Jef.DataPipeline.Sources.HttpPolling;

public class HttpPollingSource<TInput> : BaseDataSource<TInput>
{
    private readonly PollingConfig _config;

    public HttpPollingSource(
        PollingConfig config,
        IServiceScopeFactory scopeFactory)
        : base(scopeFactory)
    {
        _config = config;
    }

    public Task<TInput> GetData(Context context)
    {
        throw new NotImplementedException();
    }
}