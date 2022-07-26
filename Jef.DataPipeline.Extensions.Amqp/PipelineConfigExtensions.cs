using Jef.DataPipeline.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Jef.DataPipeline.Extensions.Amqp;

public static class PipelineConfigExtensions
{
    public delegate void ConfigureSource(AmqpConfig config);

    public static PipelineConfigurator<TInput, TOutput> AddAmqpSource<TInput, TOutput>(
        this PipelineConfigurator<TInput, TOutput> self,
        ConfigureSource configure)
    {
        self.SetSource<AmqpSource<TInput>>();
        var configuration = new AmqpConfig();
        configure(configuration);
        self.ConfigureOptions(configuration);
        self.GetServices().AddHostedService<AmqpSource<TInput>>();
        return self;
    }

    public delegate void ConfigureDestination(AmqpConfig config);

    public static PipelineConfigurator<TInput, TOutput> AddAmqpDestination<TInput, TOutput>(
        this PipelineConfigurator<TInput, TOutput> self,
        ConfigureDestination configure)
    {
        self.SetDestination<AmqpDestination<TOutput>>();
        var configuration = new AmqpConfig();
        configure(configuration);
        self.ConfigureOptions(configuration);
        return self;
    }
}