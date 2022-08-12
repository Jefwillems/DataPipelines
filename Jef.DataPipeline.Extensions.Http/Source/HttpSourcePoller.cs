using Jef.DataPipeline.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Jef.DataPipeline.Extensions.Http.Source;

public class HttpSourcePoller<TDataType> : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;

    public HttpSourcePoller(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = _scopeFactory.CreateScope();
        var conf = scope.ServiceProvider.GetRequiredService<HttpSourceConfiguration>();
        var source = scope.ServiceProvider.GetRequiredService<BaseDataSource<TDataType>>();
        var interval = conf.GetInterval();
        if (interval == null)
        {
            return;
        }

        using var timer = new PeriodicTimer(interval.Value);
        while (!stoppingToken.IsCancellationRequested && await timer.WaitForNextTickAsync(stoppingToken))
        {
            await source.Execute();
        }
    }
}