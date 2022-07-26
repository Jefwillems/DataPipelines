using System.Text.Json;
using Apache.NMS.AMQP;
using Apache.NMS.Util;
using Jef.DataPipeline.Contracts;

namespace Jef.DataPipeline.Extensions.Amqp;

public class AmqpDestination<TDataType> : BaseDestination<TDataType>
{
    private readonly AmqpConfig _config;

    public AmqpDestination(AmqpConfig config)
    {
        _config = config;
    }

    public override async Task SendData(TDataType data, Context context)
    {
        var connectionFactory = new NmsConnectionFactory(_config.Uri);
        using var connection = await connectionFactory.CreateConnectionAsync(_config.Username, _config.Password);
        using var session = await connection.CreateSessionAsync();
        using var destination = SessionUtil.GetDestination(session, _config.Destination);
        using var producer = await session.CreateProducerAsync(destination);
        var messageText = JsonSerializer.Serialize(data);
        var message = await session.CreateTextMessageAsync(messageText);
        await producer.SendAsync(message);
    }
}