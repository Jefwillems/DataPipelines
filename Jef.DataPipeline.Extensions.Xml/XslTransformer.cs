using System.Text;
using System.Text.Json;
using System.Xml;
using System.Xml.Xsl;
using Jef.DataPipeline.Contracts;

namespace Jef.DataPipeline.Extensions.Xml;

public class XslTransformer<TOutputData> : BaseTransformer<string, TOutputData>
{
    private readonly XsltConfiguration _configuration;

    public XslTransformer(BaseDestination<TOutputData> destination, XsltConfiguration configuration) : base(destination)
    {
        _configuration = configuration;
    }

    protected override async Task<TOutputData> Process(string input, Context context)
    {
        var transform = new XslCompiledTransform();
        var settings = XsltSettings.Default;
        transform.Load(_configuration.XslDocumentPath, settings, null);
        var outStream = new MemoryStream();
        await using var outStreamWriter = XmlWriter.Create(outStream);
        using var inputReader = XmlReader.Create(StringToStream(input));
        transform.Transform(inputReader, outStreamWriter);

        var resultObj = await JsonSerializer.DeserializeAsync<TOutputData>(outStream);
        return resultObj;
    }

    private static Stream StringToStream(string input)
    {
        return new MemoryStream(Encoding.Unicode.GetBytes(input));
    }
}