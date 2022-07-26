namespace Jef.DataPipeline;

public class Context
{
    public string CorrelationId { get; set; }
    public DateTime StartedAt { get; set; }
    public DateTime EndedAt { get; set; }
    public long ProcessTimeInMilliseconds => (long)EndedAt.Subtract(StartedAt).TotalMilliseconds;

    public Context()
    {
        CorrelationId = Guid.NewGuid().ToString("D");
    }

    public Context(string correlationId)
    {
        CorrelationId = correlationId;
    }
}