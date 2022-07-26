using System.Text.Json;
using Jef.DataPipeline.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Jef.DataPipeline.Extensions.Http.Source;

public class HttpSource<TInput> : BaseDataSource<TInput>
{
    private readonly HttpSourceConfiguration _configuration;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<HttpSource<TInput>> _logger;

    public HttpSource(
        IServiceScopeFactory scopeFactory,
        HttpSourceConfiguration config,
        IHttpClientFactory httpClientFactory,
        ILogger<HttpSource<TInput>> logger)
        : base(scopeFactory.CreateScope().ServiceProvider.GetRequiredService<ITransformer<TInput>>())
    {
        _configuration = config;
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }


    protected override async Task<TInput> GetData(Context context)
    {
        var client = _httpClientFactory.CreateClient();
        var request = new HttpRequestMessage(new HttpMethod(_configuration.Method), _configuration.Uri);
        foreach (var (name, value) in _configuration.Headers)
        {
            request.Headers.Add(name, value);
        }

        _logger.LogDebug("fetching data from httpsource");
        var response = await client.SendAsync(request);
        _logger.LogDebug("deserializing http response");
        var data = await JsonSerializer.DeserializeAsync<TInput>(await response.Content.ReadAsStreamAsync());
        return data;
    }
}