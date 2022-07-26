using Jef.DataPipeline.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Jef.DataPipeline.Extensions.Http;

public static class PipelineConfiguratorExtensions
{
    public delegate void ConfigureSource(HttpConfiguration config);

    public static PipelineConfigurator<TInput, TOutput> AddHttpSource<TInput, TOutput>(
        this PipelineConfigurator<TInput, TOutput> self,
        ConfigureSource configure)
    {
        self.SetSource<HttpSource<TInput>>();
        var configuration = new HttpConfiguration();
        configure(configuration);
        self.ConfigureOptions(configuration);
        return self;
    }
}