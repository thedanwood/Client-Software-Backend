using HaulageSystem.Application.Dtos.Quotes;
using HaulageSystem.Domain.Enums;

namespace HaulageSystem.Application.Domain.Interfaces.Services;

public interface IDeliveryService
{
    Task<int> GetNumberOfLoads(MaterialUnits materialUnit, int quantity, int VehicleTypeId);
    decimal CalculateDeliveryPricePerDeliveryTimeUnit();
    decimal CalculateTotalDeliveryPrice(decimal deliveryPricePerDeliveryTimeUnit, int totalMinuteJourney);
}