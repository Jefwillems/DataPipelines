using System.Text.Json.Serialization;

namespace Jef.DataPipeline.DemoApp.ViewModels;

public class HttpGetData
{
    [JsonPropertyName("headers")]
    public Headers Headers { get; set; }

    [JsonPropertyName("origin")]
    public string Origin { get; set; }

    [JsonPropertyName("url")]
    public string Url { get; set; }
}

public class Headers
{
    [JsonPropertyName("Accept")]
    public string Accept { get; set; }

    [JsonPropertyName("Accept-Encoding")]
    public string AcceptEncoding { get; set; }

    [JsonPropertyName("Accept-Language")]
    public string AcceptLanguage { get; set; }

    [JsonPropertyName("Host")]
    public string Host { get; set; }

    [JsonPropertyName("Referer")]
    public string Referer { get; set; }

    [JsonPropertyName("Sec-Ch-Ua")]
    public string SecChUa { get; set; }

    [JsonPropertyName("Sec-Ch-Ua-Mobile")]
    public string SecChUaMobile { get; set; }

    [JsonPropertyName("Sec-Ch-Ua-Platform")]
    public string SecChUaPlatform { get; set; }

    [JsonPropertyName("Sec-Fetch-Dest")]
    public string SecFetchDest { get; set; }

    [JsonPropertyName("Sec-Fetch-Mode")]
    public string SecFetchMode { get; set; }

    [JsonPropertyName("Sec-Fetch-Site")]
    public string SecFetchSite { get; set; }

    [JsonPropertyName("User-Agent")]
    public string UserAgent { get; set; }

    [JsonPropertyName("X-Amzn-Trace-Id")]
    public string XAmznTraceId { get; set; }
}