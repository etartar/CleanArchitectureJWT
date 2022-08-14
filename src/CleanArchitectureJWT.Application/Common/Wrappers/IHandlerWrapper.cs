using CleanArchitectureJWT.Application.Common.Interfaces;
using MediatR;

namespace CleanArchitectureJWT.Application.Common.Wrappers
{
    public interface IHandlerWrapper<in TRequest, TResponse> : IRequestHandler<TRequest, IResponse<TResponse>>
        where TRequest : IRequestWrapper<TResponse>
    {
    }
}
