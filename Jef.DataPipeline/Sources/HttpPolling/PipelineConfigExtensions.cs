using Jef.DataPipeline.Configuration;

namespace Jef.DataPipeline.Sources.HttpPolling;

public static class PipelineConfigExtensions
{
    public delegate void ConfigurePolling(PollingConfig config);

    public static PipelineConfigurator<TInput, TOutput> AddHttpPollingSource<TInput, TOutput>(
        this PipelineConfigurator<TInput, TOutput> conf, ConfigurePolling config)
    {
        conf.SetSource<HttpPollingSource<TInput>>();
        var pollingConfig = new PollingConfig();
        config(pollingConfig);
        conf.ConfigureOptions(pollingConfig);
        return conf;
    }
}