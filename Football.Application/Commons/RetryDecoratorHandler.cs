using MediatR;
using Polly;
using System;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace Football.Application.Commons
{
    public class RetryDecoratorHandler<TRequest,TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IRequestHandler<TRequest, TResponse> _inner;

        public RetryDecoratorHandler(IRequestHandler<TRequest, TResponse> inner)
        {
            _inner = inner;
            _retryPolicy = Policy.Handle<SqlException>()
            .WaitAndRetryAsync(3,
                i => TimeSpan.FromSeconds(i));
        }

        private readonly IAsyncPolicy _retryPolicy;

      
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
        {
            return await _retryPolicy.ExecuteAsync(() => _inner.Handle(request, cancellationToken));
        }
    }
}
