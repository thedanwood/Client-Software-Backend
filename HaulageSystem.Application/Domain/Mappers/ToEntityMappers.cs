using HaulageSystem.Application.Models.Requests;
using HaulageSystem.Application.Dtos.Quotes;
using HaulageSystem.Application.Domain.Entities.Database;

namespace HaulageSystem.Application.Domain.Mappers;

public static class ToEntityMappers
{
    public static Quotes ToEntity(this CreateRecordRequest request)
    {
        return new Quotes()
        {
            CreatedByUsername = request.CreatedByUsername,
            CreatedDateTime = DateTime.Now,
            CustomerName = request.CustomerName,
            CompanyId = request.CompanyId,
            DeliveryLocationLatitude = request.DeliveryLocationLatitude,
            DeliveryLocationLongitude = request.DeliveryLocationLongitude,
            DeliveryLocationFullAddress = request.DeliveryLocationFullAddress,
            DeliveryDate = request.DeliveryDate,
            ActiveStateEnumValue = request.ActiveStateEnumValue,
            Comments = request.Comments,
            RecordType = request.RecordType,
        };
    }
    public static DeliveryMovements ToEntity(this CreateDeliveryMovementRequest request)
    {
        return new DeliveryMovements()
            {
                QuoteId = request.QuoteId,
                StartLocationLatitude = request.StartLocationLatitude,
                StartLocationLongitude = request.StartLocationLongitude,
                StartLocationFullAddress = request.StartLocationFullAddress,
                NumberOfLoads = request.NumberOfLoads,
                VehicleTypeId = request.VehicleTypeId,
                DefaultOnewayJourneyTime = request.DefaultOnewayJourneyTime,
                OnewayJourneyTime = request.OnewayJourneyTime,
                TotalDeliveryPrice = request.TotalDeliveryPrice,
                DeliveryPricePerTimeUnit = request.DeliveryPricePerDeliveryTimeUnit,
                DefaultTotalDeliveryPrice = request.DefaultTotalDeliveryPrice,
                DefaultDeliveryPricePerTimeUnit = request.DefaultDeliveryPricePerDeliveryTimeUnit,
                HasTrafficEnabled = request.HasTrafficEnabled
            };
    }
    public static MaterialMovements ToEntity(this CreateMaterialMovementRequest request)
    {
        return new MaterialMovements()
        {
            DeliveryMovementId = request.DeliveryMovementId,
            DepotMaterialPriceId = request.DepotMaterialPriceId,
            DefaultMaterialPricePerQuantityUnit = request.DefaultMaterialPricePerQuantityUnit,
            MaterialPricePerQuantityUnit = request.MaterialPricePerQuantityUnit,
            DefaultTotalMaterialPrice = request.DefaultTotalMaterialPrice,
            TotalMaterialPrice = request.TotalMaterialPrice,
            Quantity = request.Quantity,
            MaterialUnitId = request.MaterialUnitId,
        };
    }
    public static DepotMaterialPrices ToEntity(this CreateMaterialPricingRequest request)
    {
        return new DepotMaterialPrices()
        {
            MaterialId = request.MaterialId,
            DepotId = request.DepotId,
            MaterialUnitEnum = request.UnitId,
            MaterialPrice = request.Price,
            ActiveState = request.ActiveState,
        };
    }
    public static DepotMaterialPrices ToEntity(this UpdateMaterialPricingRequest request)
    {
        return new DepotMaterialPrices()
        {
            DepotMaterialPriceId= request.DepotMaterialPriceId,
            MaterialId = request.MaterialId,
            DepotId = request.DepotId,
            MaterialUnitEnum = request.Unit,
            MaterialPrice = request.Price,
            ActiveState = request.ActiveState
        };
    }
}