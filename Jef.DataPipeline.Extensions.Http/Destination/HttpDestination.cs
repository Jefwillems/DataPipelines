using System.Net.Mime;
using System.Text;
using System.Text.Json;
using Jef.DataPipeline.Contracts;
using Microsoft.Extensions.Logging;

namespace Jef.DataPipeline.Extensions.Http.Destination;

public class HttpDestination<TDataType> : BaseDestination<TDataType>
{
    private readonly HttpDestinationConfiguration _configuration;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<HttpDestination<TDataType>> _logger;

    public HttpDestination(
        HttpDestinationConfiguration configuration,
        IHttpClientFactory httpClientFactory,
        ILogger<HttpDestination<TDataType>> logger)
    {
        _configuration = configuration;
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    public override async Task SendData(TDataType data, Context context)
    {
        var client = _httpClientFactory.CreateClient();
        var request = new HttpRequestMessage(new HttpMethod(_configuration.Method), _configuration.Uri);
        var body = JsonSerializer.Serialize(data);
        request.Content = new StringContent(body, Encoding.UTF8, MediaTypeNames.Application.Json);
        foreach (var (name, value) in _configuration.Headers)
        {
            request.Headers.Add(name, value);
        }

        var response = await client.SendAsync(request);
        var respContent = await response.Content.ReadAsStringAsync();
        _logger.LogInformation("Response came back with status code {Status} and content {Content}",
            (int)response.StatusCode, respContent);
    }
}