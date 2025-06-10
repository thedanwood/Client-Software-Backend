namespace HaulageSystem.Application.Extensions;

public static class CurrencyExtensions
{
    public static decimal ToPrice(this decimal price)
    {
        return Math.Round(price, 2);
    }
}