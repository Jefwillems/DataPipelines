using System.Text.Json;
using Jef.DataPipeline.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace Jef.DataPipeline.Extensions.Http.Source;

public class HttpSource<TInput> : BaseDataSource<TInput>
{
    private readonly HttpSourceConfiguration _configuration;
    private readonly IHttpClientFactory _httpClientFactory;

    public HttpSource(
        IServiceScopeFactory scopeFactory,
        HttpSourceConfiguration config,
        IHttpClientFactory httpClientFactory)
        : base(scopeFactory.CreateScope().ServiceProvider.GetRequiredService<ITransformer<TInput>>())
    {
        _configuration = config;
        _httpClientFactory = httpClientFactory;
    }


    protected override async Task<TInput> GetData(Context context)
    {
        var client = _httpClientFactory.CreateClient();
        var request = new HttpRequestMessage(_configuration.Method, _configuration.Uri);
        foreach (var (name, value) in _configuration.Headers)
        {
            request.Headers.Add(name, value);
        }

        var response = await client.SendAsync(request);
        var data = await JsonSerializer.DeserializeAsync<TInput>(await response.Content.ReadAsStreamAsync());
        return data;
    }
}