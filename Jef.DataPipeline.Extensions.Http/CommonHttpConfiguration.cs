namespace Jef.DataPipeline.Extensions.Http;

public class CommonHttpConfiguration
{
    public string Uri { get; set; }

    public string Method { get; set; }
    
    public IDictionary<string, string> Headers { get; set; }
}