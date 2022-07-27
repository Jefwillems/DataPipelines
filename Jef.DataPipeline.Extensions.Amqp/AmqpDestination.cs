using System.Text.Json;
using Apache.NMS.AMQP;
using Apache.NMS.Util;
using Jef.DataPipeline.Contracts;
using Microsoft.Extensions.Logging;

namespace Jef.DataPipeline.Extensions.Amqp;

public class AmqpDestination<TDataType> : BaseDestination<TDataType>
{
    private readonly AmqpConfig _config;
    private readonly ILogger<AmqpDestination<TDataType>> _logger;

    public AmqpDestination(AmqpConfig config, ILogger<AmqpDestination<TDataType>> logger)
    {
        _config = config;
        _logger = logger;
    }

    public override async Task SendData(TDataType data, Context context)
    {
        var connectionFactory = new NmsConnectionFactory(_config.Uri);
        _logger.LogDebug("Creating amqp connection");
        using var connection = await connectionFactory.CreateConnectionAsync(_config.Username, _config.Password);
        _logger.LogDebug("Creating amqp session");
        using var session = await connection.CreateSessionAsync();
        using var destination = SessionUtil.GetDestination(session, _config.Destination);
        _logger.LogDebug("Creating amqp producer");
        using var producer = await session.CreateProducerAsync(destination);
        var messageText = JsonSerializer.Serialize(data);
        var message = await session.CreateTextMessageAsync(messageText);
        _logger.LogDebug("Sending amqp message");
        await producer.SendAsync(message);
    }
}