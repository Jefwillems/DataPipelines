using System.Text.Json.Serialization;

namespace Jef.DataPipeline.DemoApp.ViewModels;

public class InputData
{
    [JsonPropertyName("hello")]
    public string Hello { get; set; }
}