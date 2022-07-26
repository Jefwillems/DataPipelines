using System.Text.Json.Serialization;

namespace Jef.DataPipeline.DemoApp.ViewModels;

public class HttpInput
{
    [JsonPropertyName("origin")]
    public string Origin { get; set; }
}