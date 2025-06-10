using System.Globalization;
using Serilog;

namespace HaulageSystem.Application.Exceptions;

public class QuoteNotFoundException: Exception
{
    public QuoteNotFoundException(int quoteId): base($"Quote not found for quote Id {quoteId}")
    {
    }
}