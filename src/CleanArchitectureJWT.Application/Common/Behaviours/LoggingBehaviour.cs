using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitectureJWT.Application.Common.Behaviours
{
    public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<TRequest> _logger;

        public LoggingBehaviour(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            _logger.LogTrace($"Request : {request}");
            var response = await next();
            _logger.LogTrace($"Response : {response}");
            return response;
        }
    }
}
