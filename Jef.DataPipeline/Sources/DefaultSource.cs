using Jef.DataPipeline.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace Jef.DataPipeline.Sources;

public class DefaultSource<TInputType> : BaseDataSource<TInputType>
{
    public DefaultSource(IServiceScopeFactory scopeFactory) : base(scopeFactory)
    {
    }
}