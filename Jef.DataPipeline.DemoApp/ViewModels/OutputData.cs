using System.Text.Json.Serialization;

namespace Jef.DataPipeline.DemoApp.ViewModels;

public class OutputData
{
    [JsonPropertyName("world")]
    public string World { get; set; }
}