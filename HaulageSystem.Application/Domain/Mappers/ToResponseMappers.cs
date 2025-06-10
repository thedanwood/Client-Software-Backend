using HaulageSystem.Application.Core.Commands.Materials;
using HaulageSystem.Application.Domain.Dtos.Materials;
using HaulageSystem.Application.Domain.Entities.Companies;
using HaulageSystem.Application.Domain.Entities.Database;
using HaulageSystem.Application.Domain.Entities.Materials;
using HaulageSystem.Application.Domain.Entities.Quotes;
using HaulageSystem.Application.Domain.Entities.Vehicles;
using HaulageSystem.Application.Dtos.Quotes;
using HaulageSystem.Application.Models.Depots;
using HaulageSystem.Domain.Enums;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace HaulageSystem.Application.Domain.Mappers;

public static class ToResponseMappers
{
    
    public static GetCompanyResponse ToResponse(this Companies model)
    {
        return new GetCompanyResponse()
        {
            CompanyName = model.CompanyName,
            CompanyId = model.CompanyId,
            PhoneNumber = model.Telephone,
            Email = model.Email
        };
    }
    public static GetDepotMaterialPriceResponse ToResponse(this DepotMaterialPrices model)
    {
        return new GetDepotMaterialPriceResponse()
        {
            MaterialUnitId = model.MaterialUnitEnum,
            DepotId = model.DepotId,
            MaterialPricePerQuantityUnit = model.MaterialPrice,
            DepotMaterialPriceId = model.DepotMaterialPriceId,
            MaterialId = model.MaterialId,
            ActiveState = model.ActiveState
        };
    }
    public static GetDeliveryMovementResponse ToResponse(this DeliveryMovements model)
    {
        return new GetDeliveryMovementResponse()
        {
            DeliveryMovementId = model.DeliveryMovementId,
            QuoteId = model.QuoteId,
            StartLocationFullAddress = model?.StartLocationFullAddress,
            StartLocationLongitude = model?.StartLocationLongitude,
            StartLocationLatitude = model?.StartLocationLatitude,
            NumberOfLoads = model.NumberOfLoads,
            VehicleTypeId = model.VehicleTypeId,
            DefaultOnewayJourneyTime = model.DefaultOnewayJourneyTime,
            OnewayJourneyTime = model.OnewayJourneyTime,
            DefaultTotalDeliveryPrice = model.DefaultTotalDeliveryPrice,
            DeliveryPricePerTimeUnit = model.DeliveryPricePerTimeUnit,
            DefaultDeliveryPricePerTimeUnit = model.DefaultDeliveryPricePerTimeUnit,
            TotalDeliveryPrice = model.TotalDeliveryPrice,
            HasTrafficEnabled = model.HasTrafficEnabled
        };
    }
    public static GetMaterialMovementResponse ToResponse(this MaterialMovements model)
    {
        return new GetMaterialMovementResponse()
        {
            MaterialMovementId = model.MaterialMovementId,
            DeliveryMovementId = model.DeliveryMovementId,
            DepotMaterialPriceId = model.DepotMaterialPriceId,
            Quantity = model.Quantity,
            MaterialUnitId = model.MaterialUnitId,
            DefaultTotalMaterialPrice = model.DefaultTotalMaterialPrice,
            DefaultMaterialPricePerQuantityUnit = model.DefaultMaterialPricePerQuantityUnit,
            TotalMaterialPrice = model.TotalMaterialPrice,
            MaterialPricePerQuantityUnit = model.MaterialPricePerQuantityUnit,
        };
    }
    public static GetMaterialResponse ToResponse(this Materials model)
    {
        return new GetMaterialResponse()
        {
            MaterialId = model.MaterialId,
            MaterialName = model.MaterialName
        };
    }
    public static GetVehicleTypeResponse ToResponse(this VehicleTypes model)
    {
        return new GetVehicleTypeResponse()
        {
            Id = model.VehicleTypeId,
            Name = model.VehicleTypeName,
            Capacity = model.VehicleTypeCapacity
        };
    }
    public static GetDepotResponse ToResponse(this Depots model)
    {
        return new GetDepotResponse()
        {
            DepotId = model.DepotId,
            DepotName = model.DepotName,
            Address = model.Address,
            Latitude = model.Latitude,
            Longitude = model.Longitude,
            IsActive = model.Active
        };
    }
    public static GetQuoteResponse ToResponse(this Quotes model)
    {
        var mapped = new GetQuoteResponse()
        {
            QuoteId = model.QuoteId,
            CompanyID = model.CompanyId,
            Comments = model.Comments,
            CustomerName = model.CustomerName,
            DeliveryDate = model.DeliveryDate ?? new DateTime(),
            DeliveryLocationFullAddress = model.DeliveryLocationFullAddress,
            DeliveryLocationLatitude = model.DeliveryLocationLatitude,
            DeliveryLocationLongitude = model.DeliveryLocationLongitude,
            RecordType = model.RecordType,
            ActiveStateEnumValue = model.ActiveStateEnumValue,
            CreatedByUsername = model.CreatedByUsername,
            CreatedDateTime = model.CreatedDateTime
        };
        return mapped;
    }
}