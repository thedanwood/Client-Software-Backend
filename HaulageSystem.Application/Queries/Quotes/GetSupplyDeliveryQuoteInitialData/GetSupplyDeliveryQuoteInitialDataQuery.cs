using HaulageSystem.Application.Dtos.Quotes;
using HaulageSystem.Application.Models.Quotes;
using HaulageSystem.Domain.Enums;
using MediatR;

namespace HaulageSystem.Application.Queries.Quotes;

public class GetSupplyDeliveryQuoteInitialDataQuery : IRequest<QuoteSupplyDeliveryInitialDataDto>
{
}