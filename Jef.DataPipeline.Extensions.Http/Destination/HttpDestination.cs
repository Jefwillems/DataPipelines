using System.Net.Mime;
using System.Text;
using System.Text.Json;
using Jef.DataPipeline.Contracts;

namespace Jef.DataPipeline.Extensions.Http.Destination;

public class HttpDestination<TDataType> : BaseDestination<TDataType>
{
    private readonly HttpDestinationConfiguration _configuration;
    private readonly IHttpClientFactory _httpClientFactory;

    public HttpDestination(HttpDestinationConfiguration configuration, IHttpClientFactory httpClientFactory)
    {
        _configuration = configuration;
        _httpClientFactory = httpClientFactory;
    }

    public override async Task SendData(TDataType data, Context context)
    {
        var client = _httpClientFactory.CreateClient();
        var request = new HttpRequestMessage(_configuration.Method, _configuration.Uri);
        var body = JsonSerializer.Serialize(data);
        request.Content = new StringContent(body, Encoding.UTF8, MediaTypeNames.Application.Json);
        foreach (var (name, value) in _configuration.Headers)
        {
            request.Headers.Add(name, value);
        }

        await client.SendAsync(request);
    }
}