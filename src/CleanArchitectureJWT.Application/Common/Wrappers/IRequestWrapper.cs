using CleanArchitectureJWT.Application.Common.Interfaces;
using MediatR;

namespace CleanArchitectureJWT.Application.Common.Wrappers
{
    public interface IRequestWrapper<T> : IRequest<IResponse<T>>
    {
    }
}
