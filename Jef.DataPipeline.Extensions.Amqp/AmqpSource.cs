using System.Text.Json;
using Apache.NMS;
using Apache.NMS.AMQP;
using Apache.NMS.Util;
using Jef.DataPipeline.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Jef.DataPipeline.Extensions.Amqp;

public class AmqpSource<TDataType> : BaseDataSource<TDataType>, IHostedService, IDisposable
{
    private readonly AmqpConfig _config;
    private readonly ILogger<AmqpSource<TDataType>> _logger;
    private ISession? _session;
    private IConnection? _connection;
    private IMessageConsumer? _consumer;

    public AmqpSource(
        IServiceScopeFactory scopeFactory,
        AmqpConfig config, ILogger<AmqpSource<TDataType>> logger)
        : base(scopeFactory.CreateScope().ServiceProvider.GetRequiredService<ITransformer<TDataType>>())
    {
        _config = config;
        _logger = logger;
    }

    private void ReceiveMessage(IMessage message)
    {
        _logger.LogInformation("Received message");
        var messageContent = JsonSerializer.Deserialize<TDataType>(((ITextMessage)message).Text);
        Task.WaitAny(SendToTransformer(messageContent!,
            new Context(message.NMSCorrelationID ?? Guid.NewGuid().ToString("D"))));
        message.Acknowledge();
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("starting hosted service");
        var connectionFactory = new NmsConnectionFactory(_config.Uri);
        _connection = await connectionFactory.CreateConnectionAsync(_config.Username, _config.Password);
        _session = await _connection.CreateSessionAsync(AcknowledgementMode.IndividualAcknowledge);
        var destination = SessionUtil.GetDestination(_session, _config.Source);
        var consumer = await _session.CreateConsumerAsync(destination);
        consumer.Listener += ReceiveMessage;
        _consumer = consumer;
        await _connection.StartAsync();
    }


    public Task StopAsync(CancellationToken cancellationToken)
    {
        Close();
        return Task.CompletedTask;
    }

    private void Close()
    {
        _consumer?.Close();
        _session?.Close();
        _connection?.Close();
    }

    private void Dispose(bool disposing)
    {
        Close();
        if (!disposing) return;
        _session?.Dispose();
        _connection?.Dispose();
        _consumer?.Dispose();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~AmqpSource()
    {
        Dispose(false);
    }

    protected override Task<TDataType> GetData(Context context)
    {
        throw new NotImplementedException($"AmqpSource is a listener and does not use {nameof(GetData)}");
    }
}