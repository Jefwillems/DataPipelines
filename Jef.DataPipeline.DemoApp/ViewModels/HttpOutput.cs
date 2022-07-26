using System.Text.Json.Serialization;

namespace Jef.DataPipeline.DemoApp.ViewModels;

public class HttpOutput
{
    [JsonPropertyName("origin")]
    public string Origin { get; set; }
}