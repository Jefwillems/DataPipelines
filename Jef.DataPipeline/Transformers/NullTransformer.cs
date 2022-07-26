using Jef.DataPipeline.Contracts;

namespace Jef.DataPipeline.Transformers;

public class NullTransformer<TInputType, TOutputType> : BaseTransformer<TInputType, TOutputType>

{
    public NullTransformer(BaseDestination<TOutputType> destination) : base(destination)
    {
    }

    public override async Task Transform(TInputType input, Context context)
    {
        await SendToDestination(default!, context);
    }
}