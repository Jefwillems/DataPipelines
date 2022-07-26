using Jef.DataPipeline.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Jef.DataPipeline.Extensions.Amqp;

public static class PipelineConfigExtensions
{
    public delegate void Configuresource(AmqpConfig config);

    public static PipelineConfigurator<TInput, TOutput> AddAmqpSource<TInput, TOutput>(
        this PipelineConfigurator<TInput, TOutput> self,
        Configuresource configure)
    {
        self.SetSource<AmqpSource<TInput>>();
        var configuration = new AmqpConfig();
        configure(configuration);
        self.ConfigureOptions(configuration);
        self.GetServices().AddHostedService<AmqpSource<TInput>>();
        return self;
    }
}