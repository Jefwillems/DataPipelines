using Microsoft.Extensions.DependencyInjection;

namespace Jef.DataPipeline.Configuration;

public static class ServiceCollectionExtensions
{
    public delegate void BuildPipeline<TInput, TOutput>(PipelineConfigurator<TInput, TOutput> conf);

    public static void AddPipeline<TInputType, TOutputType>(this IServiceCollection services,
        BuildPipeline<TInputType, TOutputType> builder)
    {
        var config = new PipelineConfigurator<TInputType, TOutputType>(services);
        builder(config);
    }
}