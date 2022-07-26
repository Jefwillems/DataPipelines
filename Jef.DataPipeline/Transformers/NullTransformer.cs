using Jef.DataPipeline.Contracts;

namespace Jef.DataPipeline.Transformers;

public class NullTransformer<TInputType, TOutputType> : BaseTransformer<TInputType, TOutputType>

{
    public NullTransformer(BaseDestination<TOutputType> destination) : base(destination)
    {
    }

    protected override Task<TOutputType> Process(TInputType input, Context context)
    {
        return Task.FromResult(default(TOutputType)!);
    }
}