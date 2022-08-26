using Jef.DataPipeline.Contracts;

namespace Jef.DataPipeline.Extensions.File;

public class FileReaderSource : BaseDataSource<string>
{
    private readonly FileReaderConfiguration _configuration;

    public FileReaderSource(ITransformer<string> transformer, FileReaderConfiguration configuration) : base(transformer)
    {
        _configuration = configuration;
    }

    protected override Task<string> GetData(Context context)
    {
        context.StartedAt = DateTime.Now;
        var fileContent = System.IO.File.ReadAllText(_configuration.FilePath);
        return Task.FromResult(fileContent);
    }
}