using HaulageSystem.Application.Domain.Dtos.Materials;

namespace HaulageSystem.Application.Dtos.Quotes;

public class SupplyAndDeliveryQuoteInformationDto
{
    public List<MaterialCommand> Materials { get; set; }
}