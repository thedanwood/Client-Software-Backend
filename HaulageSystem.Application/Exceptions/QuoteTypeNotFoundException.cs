using System.Globalization;

namespace HaulageSystem.Application.Exceptions;

public class QuoteTypeNotFoundException: Exception
{
    public QuoteTypeNotFoundException(int quoteId, string quoteType): base(
        $"Quote type not found for quoteId = {quoteId}. QuoteType {quoteType}")
    {
    }
}