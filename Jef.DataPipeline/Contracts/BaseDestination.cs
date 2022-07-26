namespace Jef.DataPipeline.Contracts;

public abstract class BaseDestination<TDataType>
{
    public abstract Task SendData(TDataType data, Context context);
}