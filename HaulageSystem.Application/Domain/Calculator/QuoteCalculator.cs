using HaulageSystem.Application.Extensions;

namespace HaulageSystem.Application.Core.Domain.Calculator;

public static class QuoteCalculator
{
    public static decimal CalculateTotalDeliveryPrice(int numberOfLoads, int oneWayJourneyTime, decimal deliveryPricePerDeliveryTimeUnit)
    {
        var total = (deliveryPricePerDeliveryTimeUnit * (oneWayJourneyTime * numberOfLoads));
        return total.ToPrice();
    }

    public static decimal CalculateDeliveryPricePerDeliveryTimeUnit(int VehicleTypeId, int totalOneWayJourneyTime)
    {
        //TODO convert to be profile settings
        decimal pricePerDeliveryTimeUnit = 0;
        //8 wheeler
        if (VehicleTypeId == 1)
        {
            //0 - 20
            if (totalOneWayJourneyTime > 0 && totalOneWayJourneyTime < 20)
            {
                pricePerDeliveryTimeUnit = 5;
            }
            //20-24
            else if (totalOneWayJourneyTime >= 20 && totalOneWayJourneyTime < 24)
            {
                pricePerDeliveryTimeUnit = 4.75M;
            }
            //24-28
            else if (totalOneWayJourneyTime >= 24 && totalOneWayJourneyTime < 28)
            {
                pricePerDeliveryTimeUnit = 4.50M;
            }
            //28-32
            else if (totalOneWayJourneyTime >= 28 && totalOneWayJourneyTime < 32)
            {
                pricePerDeliveryTimeUnit = 4.25M;
            }
            //32-36
            else if (totalOneWayJourneyTime >= 32 && totalOneWayJourneyTime < 36)
            {
                pricePerDeliveryTimeUnit = 4.00M;
            }
            //36-40
            else if (totalOneWayJourneyTime >= 36 && totalOneWayJourneyTime < 40)
            {
                pricePerDeliveryTimeUnit = 3.75M;
            }
            //40-44
            else if (totalOneWayJourneyTime >= 40 && totalOneWayJourneyTime < 44)
            {
                pricePerDeliveryTimeUnit = 3.50M;
            }
            //44-48
            else if (totalOneWayJourneyTime >= 44 && totalOneWayJourneyTime < 48)
            {
                pricePerDeliveryTimeUnit = 3.25M;
            }
            //48+
            else if (totalOneWayJourneyTime >= 48)
            {
                pricePerDeliveryTimeUnit = 3;
            }
        }
        //artic
        else if (VehicleTypeId == 2)
        {
            //0 - 20
            if (totalOneWayJourneyTime > 0 && totalOneWayJourneyTime < 20)
            {
                pricePerDeliveryTimeUnit = 5.50M;
            }
            //20-24
            else if (totalOneWayJourneyTime >= 20 && totalOneWayJourneyTime < 24)
            {
                pricePerDeliveryTimeUnit = 5.25M;
            }
            //24-28
            else if (totalOneWayJourneyTime >= 24 && totalOneWayJourneyTime < 28)
            {
                pricePerDeliveryTimeUnit = 5.00M;
            }
            //28-32
            else if (totalOneWayJourneyTime >= 28 && totalOneWayJourneyTime < 32)
            {
                pricePerDeliveryTimeUnit = 4.75M;
            }
            //32-36
            else if (totalOneWayJourneyTime >= 32 && totalOneWayJourneyTime < 36)
            {
                pricePerDeliveryTimeUnit = 4.50M;
            }
            //36-40
            else if (totalOneWayJourneyTime >= 36 && totalOneWayJourneyTime < 40)
            {
                pricePerDeliveryTimeUnit = 4.25M;
            }
            //40-44
            else if (totalOneWayJourneyTime >= 40 && totalOneWayJourneyTime < 44)
            {
                pricePerDeliveryTimeUnit = 4.00M;
            }
            //44-48
            else if (totalOneWayJourneyTime >= 44 && totalOneWayJourneyTime < 48)
            {
                pricePerDeliveryTimeUnit = 3.75M;
            }
            //48-52
            else if (totalOneWayJourneyTime >= 48 && totalOneWayJourneyTime < 52)
            {
                pricePerDeliveryTimeUnit = 3.50M;
            }
            //52+
            else if (totalOneWayJourneyTime >= 52)
            {
                pricePerDeliveryTimeUnit = 3.25M;
            }
        }

        return pricePerDeliveryTimeUnit.ToPrice();
    }
    public static decimal CalculateMaterialPricePerQuantityUnit(decimal totalMaterialPrice, int unitQuantity)
    {
        var MaterialPricePerQuantityUnit = totalMaterialPrice / unitQuantity;
        return MaterialPricePerQuantityUnit.ToPrice();
    }
    
    public static decimal CalculateDeliveryPricePerQuantityUnit(decimal totalDeliveryPrice, int unitQuantity)
    {
        var DeliveryPricePerQuantityUnit = totalDeliveryPrice / unitQuantity;
        return DeliveryPricePerQuantityUnit.ToPrice();
    }

    public static decimal CalculateTotalMaterialPrice(decimal MaterialPricePerQuantityUnit, int unitQuantity)
    {
        var totalPrice = MaterialPricePerQuantityUnit * unitQuantity;
        return totalPrice.ToPrice();
    }

    public static decimal CalculateTotalMaterialAndDeliveryPrice(decimal totalMaterialPrice, decimal totalDeliveryPrice)
    {
        var totalPrice = totalMaterialPrice + totalDeliveryPrice;
        return totalPrice.ToPrice();
    }
    
    public static decimal CalculateMaterialAndDeliveryPricePerQuantityUnit(decimal totalMaterialAndDeliveryPrice, int quantity)
    {
        var price = totalMaterialAndDeliveryPrice / quantity;
        return price.ToPrice();
    }
}