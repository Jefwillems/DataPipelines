namespace Jef.DataPipeline.Sources.HttpPolling;

public class PollingConfig
{
    public TimeSpan Interval { get; set; }
    public string Url { get; set; }
}