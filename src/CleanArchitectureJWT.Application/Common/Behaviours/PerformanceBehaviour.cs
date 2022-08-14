using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace CleanArchitectureJWT.Application.Common.Behaviours
{
    public class PerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<TRequest> _logger;
        private readonly IConfiguration _configuration;
        private readonly Stopwatch _timer;

        public PerformanceBehaviour(ILogger<TRequest> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            _timer.Start();

            var response = await next();

            _timer.Stop();

            var longRunningRequestTime = Convert.ToInt32(_configuration.GetSection("LongRunningRequestTime").Value);
            var elapsedMilliseconds = _timer.ElapsedMilliseconds;
            if (elapsedMilliseconds > longRunningRequestTime)
            {
                _logger.LogWarning($"Long Running Request {typeof(TRequest).Name} ({elapsedMilliseconds} milliseconds) {request}");
            }

            return response;
        }
    }
}
