using HaulageSystem.Application.Domain.Interfaces.Repositories;
using HaulageSystem.Application.Domain.Interfaces.Services;
using HaulageSystem.Application.Dtos.Quotes;
using HaulageSystem.Application.Extensions;
using HaulageSystem.Application.Helpers;
using HaulageSystem.Domain.Enums;

namespace HaulageSystem.Application.Domain.Services;

public class DeliveryService : IDeliveryService
{
    private readonly IVehiclesRepository _vehiclesRepository;
    public DeliveryService(IVehiclesRepository vehiclesRepository)
    {
        _vehiclesRepository = vehiclesRepository;
    }
    


    public async Task<int> GetNumberOfLoads(MaterialUnits materialUnit, int quantity, int VehicleTypeId)
    {
        int? numberOfLoads = null;
        if (materialUnit == MaterialUnits.Loads)
        {
            numberOfLoads = quantity;
        }
        else
        {
            var vehicleCapacity = await _vehiclesRepository.GetVehicleCapacity(VehicleTypeId);
            numberOfLoads = CalculateNumberOfLoads(quantity, vehicleCapacity);
        }

        if (!numberOfLoads.HasValue)
        {
            //TODO log failure
            //throw
        }
        return numberOfLoads.Value;
    }
    private int CalculateNumberOfLoads(int quantity, int vehicleCapacity)
    {
        var numberOfLoads = (quantity + vehicleCapacity - 1) / vehicleCapacity;
        return numberOfLoads;
    }

    public decimal CalculateDeliveryPricePerDeliveryTimeUnit()
    {
        //TODO set in db
        return 0;
    }

    public decimal CalculateTotalDeliveryPrice(decimal deliveryPricePerDeliveryTimeUnit, int totalMinuteJourney)
    {
        var totalDeliveryPrice = deliveryPricePerDeliveryTimeUnit * totalMinuteJourney;
        return totalDeliveryPrice.ToPrice();
    }

    // public decimal GetDeliveryPricePerDeliveryTimeUnit()
    // {
    //     
    // }
    // public int GetHaulageMovementInfo(RoutePoint startLocation, RoutePoint endLocation)
    // {
    //     int totalOnewayJourneyTime = await _routingService.GetTotalJourneyTimeInMinutes(
    //         new() {Latitude = command.DeliveryLocation.Latitude, Longitude = command.DeliveryLocation.Longitude},
    //         new() {Latitude = command.st.Latitude, Longitude = command.DeliveryLocation.Longitude}
    //     );
    // }
}