using HaulageSystem.Application.Commands.MaterialPricing;
using HaulageSystem.Application.Dtos.Materials;
using HaulageSystem.Application.Models.Quotes;
using HaulageSystem.Application.Models.Requests;
using HaulageSystem.Domain.Enums;

namespace HaulageSystem.Application.Mappers;

public static class ToRequestMappers
{
    public static CreateMaterialMovementRequest ToRequest(this CreateMaterialMovementCommand model, int haulageMovementId)
    {
        return new CreateMaterialMovementRequest()
        {
            MaterialUnitId = model.MaterialUnitId,
            Quantity = model.Quantity,
            DeliveryMovementId = haulageMovementId,
        };
    }
}