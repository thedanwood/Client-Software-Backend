using System.util;
using HaulageSystem.Application.Models.Quotes;
using MediatR;

namespace HaulageSystem.Application.Core.Commands.Quotes;

public class DeleteQuoteCommand : IRequest
{
    public int QuoteId { get; set; }
}