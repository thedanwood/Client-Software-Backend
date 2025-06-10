using System.Globalization;
using Serilog;

namespace HaulageSystem.Application.Exceptions;

public class MaterialMovementNotFoundException: Exception
{
    public MaterialMovementNotFoundException(int deliveryMovement): base($"Material movement not found for DeliveryMovementId = {deliveryMovement}")
    {
    }
}