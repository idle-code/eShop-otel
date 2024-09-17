using System.Diagnostics;

namespace eShop.Ordering.API.Application.Behaviors;

public class OtelSpanBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private static readonly ActivitySource ActivitySource = new("eShop");

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var typeName = request.GetGenericTypeName();
        using var _ = ActivitySource.StartActivity(typeName);
        return await next();
    }
}
