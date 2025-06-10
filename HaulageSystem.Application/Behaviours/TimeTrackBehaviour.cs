using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;
using Serilog;

namespace HaulageSystem.Application.Behaviours;

public class TimeTrackBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    private readonly ILogger<TimeTrackBehaviour<TRequest, TResponse>> _logger;

    public TimeTrackBehaviour(ILogger<TimeTrackBehaviour<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        Stopwatch timer = new Stopwatch();
        timer.Start();
        var response = await next();
        timer.Stop();
        if (timer.ElapsedMilliseconds > 3000)
        {
            var logMessage = $"Request: {request}\n took {timer.ElapsedMilliseconds}ms";
            _logger.LogWarning(logMessage);
        }

        return response;
    }
}