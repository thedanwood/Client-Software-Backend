using System.Globalization;
using HaulageSystem.Application.Core.Commands.Materials;
using HaulageSystem.Application.Core.Commands.Quotes;
using HaulageSystem.Application.Domain.Dtos.Materials;
using HaulageSystem.Application.Domain.Dtos.Quotes;
using HaulageSystem.Application.Domain.Interfaces.Repositories;
using HaulageSystem.Application.Domain.Interfaces.Services;
using HaulageSystem.Application.Dtos.Materials;
using HaulageSystem.Application.Dtos.Quotes;
using HaulageSystem.Application.Exceptions;
using HaulageSystem.Application.Extensions;
using HaulageSystem.Application.Models.Routing;
using HaulageSystem.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Serilog;

namespace HaulageSystem.Application.Commands.Quotes;

public class
    GetDeliveryOnlyMovementInitialDataQueryHandler : IRequestHandler<GetDeliveryOnlyMovementInitialDataQuery,
    DeliveryOnlyMovementForDisplayDto>
{
    private readonly IQuotesService _quotesService;
    private readonly IRoutingService _routingService;

    public GetDeliveryOnlyMovementInitialDataQueryHandler(IQuotesService quotesService, IRoutingService routingService)
    {
        _quotesService = quotesService;
        _routingService = routingService;
    }

    public async Task<DeliveryOnlyMovementForDisplayDto> Handle(GetDeliveryOnlyMovementInitialDataQuery query,
        CancellationToken cancellationToken)
    {
        var journeyTimes = await _routingService.GetJourneyTimesWithHasTrafficOptions(
            new RoutePoint(query.StartLocationLatitude, query.StartLocationLongitude),
            new RoutePoint(query.DeliveryLocationLatitude, query.DeliveryLocationLongitude));
        
        return new()
        {
            JourneyTimes = journeyTimes
        };
    }
}