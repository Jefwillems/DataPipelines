using Jef.DataPipeline.Contracts;
using Jef.DataPipeline.Destinations;
using Jef.DataPipeline.Sources;
using Jef.DataPipeline.Transformers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Jef.DataPipeline.Configuration;

public sealed class PipelineConfigurator<TInputType, TOutputType>
{
    private readonly IServiceCollection _services;

    public PipelineConfigurator(IServiceCollection services)
    {
        _services = services;
        AddDefaults();
    }

    private void AddDefaults()
    {
        _services.AddScoped<BaseDataSource<TInputType>, DefaultSource<TInputType>>();
        _services.AddScoped<BaseTransformer<TInputType, TOutputType>, NullTransformer<TInputType, TOutputType>>();
        _services.AddScoped<ITransformer<TInputType>, NullTransformer<TInputType, TOutputType>>();
        _services.AddScoped<BaseDestination<TOutputType>, DefaultDestination<TOutputType>>();
    }

    public PipelineConfigurator<TInputType, TOutputType> SetSource<TSourceType>()
        where TSourceType : BaseDataSource<TInputType>
    {
        _services.Replace(new ServiceDescriptor(
            typeof(BaseDataSource<TInputType>),
            typeof(TSourceType),
            ServiceLifetime.Scoped));
        return this;
    }

    public PipelineConfigurator<TInputType, TOutputType> SetTransformer<TTransformerType>()
        where TTransformerType : BaseTransformer<TInputType, TOutputType>
    {
        _services.Replace(new ServiceDescriptor(
            typeof(BaseTransformer<TInputType, TOutputType>),
            typeof(TTransformerType),
            ServiceLifetime.Scoped));
        _services.Replace(new ServiceDescriptor(
            typeof(ITransformer<TInputType>),
            typeof(TTransformerType),
            ServiceLifetime.Scoped));
        return this;
    }

    public PipelineConfigurator<TInputType, TOutputType> SetDestination<TDestinationType>()
        where TDestinationType : BaseDestination<TOutputType>
    {
        _services.Replace(new ServiceDescriptor(
            typeof(BaseDestination<TOutputType>),
            typeof(TDestinationType),
            ServiceLifetime.Scoped));
        return this;
    }

    public void ConfigureOptions<TOptionsType>(TOptionsType options) where TOptionsType : class
    {
        _services.AddSingleton(options);
    }

    public IServiceCollection GetServices()
    {
        return _services;
    }
}