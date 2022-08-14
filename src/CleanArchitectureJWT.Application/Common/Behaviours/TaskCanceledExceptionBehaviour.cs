using CleanArchitectureJWT.Application.Common.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitectureJWT.Application.Common.Behaviours
{
    public class TaskCanceledExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<TRequest> _logger;

        public TaskCanceledExceptionBehaviour(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                return await next();
            }
            catch (TaskCanceledException ex)
            {
                var requestName = typeof(TRequest).Name;
                var fail = Response.Fail<string>($"Task Canceled Exception : {request}");
                _logger.LogError(ex, $"Request : Task Canceled Exception for Request {requestName}, {fail}");
                throw;
            }
        }
    }
}
