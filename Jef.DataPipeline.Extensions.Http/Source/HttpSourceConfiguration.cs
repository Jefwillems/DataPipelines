namespace Jef.DataPipeline.Extensions.Http.Source;

public class HttpSourceConfiguration : CommonHttpConfiguration
{
    public int Interval { get; set; }

    public TimeSpan? GetInterval()
    {
        return Interval == 0 ? null : TimeSpan.FromSeconds(Interval);
    }
}