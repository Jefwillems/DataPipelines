namespace Jef.DataPipeline.Extensions.Amqp;

public class AmqpConfig
{
    public string Uri { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
   
    public string Source { get; set; }
    public string Destination { get; set; }
}