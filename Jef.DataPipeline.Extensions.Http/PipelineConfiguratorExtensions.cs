using Jef.DataPipeline.Configuration;
using Jef.DataPipeline.Extensions.Http.Destination;
using Jef.DataPipeline.Extensions.Http.Source;
using Microsoft.Extensions.DependencyInjection;

namespace Jef.DataPipeline.Extensions.Http;

public static class PipelineConfiguratorExtensions
{
    public delegate void ConfigureSource(HttpSourceConfiguration config);

    public static PipelineConfigurator<TInput, TOutput> AddHttpSource<TInput, TOutput>(
        this PipelineConfigurator<TInput, TOutput> self,
        ConfigureSource configure)
    {
        self.SetSource<HttpSource<TInput>>();
        var configuration = new HttpSourceConfiguration();
        configure(configuration);
        self.ConfigureOptions(configuration);
        self.GetServices().AddHttpClient();
        return self;
    }

    public delegate void ConfigureDestination(HttpDestinationConfiguration config);

    public static PipelineConfigurator<TInput, TOutput> AddHttpDestination<TInput, TOutput>(
        this PipelineConfigurator<TInput, TOutput> self,
        ConfigureDestination configure
    )
    {
        self.SetDestination<HttpDestination<TOutput>>();
        var configuration = new HttpDestinationConfiguration();
        configure(configuration);
        self.ConfigureOptions(configuration);
        self.GetServices().AddHttpClient();
        return self;
    }
}