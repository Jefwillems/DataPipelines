namespace Jef.DataPipeline.Extensions.Http.Source;

public class HttpSourceConfiguration : CommonHttpConfiguration
{
    public TimeSpan Interval { get; set; }
}