using System.Net;
using Jef.DataPipeline.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Jef.DataPipeline.Extensions.Http;

public static class MiddlewareExtensions
{
    public static IEndpointRouteBuilder UseHttpPipelineTrigger<TInputType>(this IEndpointRouteBuilder app, string path)
    {
        app.MapPost($"pipeline/{path}", async (TInputType input, HttpContext context) =>
        {
            var source = context.RequestServices.GetRequiredService<BaseDataSource<TInputType>>();
            await source.ReceiveData(input, new Context());
            return Results.Ok();
        });
        return app;
    }
}