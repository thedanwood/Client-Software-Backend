using System.Globalization;
using Serilog;

namespace HaulageSystem.Application.Exceptions;

public class MaterialPricingNotFoundException: Exception
{
    public MaterialPricingNotFoundException(int DepotMaterialPriceId): base($"Material pricing not found for DepotMaterialPriceId = {DepotMaterialPriceId}")
    {
    }
}