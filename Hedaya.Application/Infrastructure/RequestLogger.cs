using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace Hedaya.Application.Infrastructure
{
    public class RequestLogger<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly ILogger _logger;

        public RequestLogger(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var name = typeof(TRequest).Name;
            var mm = request;

            // TODO: Add User Details

            _logger.LogInformation("Orial Request: {Name} {@Request}", name, request);

            return Task.CompletedTask;
        }
    }
}
