using System.Text;
using Jef.DataPipeline.Contracts;

namespace Jef.DataPipeline.Extensions.File;

public class FileDestination : BaseDestination<string>
{
    private readonly FileWriterConfigurationn _configuration;

    public FileDestination(FileWriterConfigurationn configuration)
    {
        _configuration = configuration;
    }

    public override Task SendData(string data, Context context)
    {
        System.IO.File.WriteAllText(_configuration.FilePath, data, Encoding.Default);
        return Task.CompletedTask;
    }
}