using HaulageSystem.Application.Domain.Dtos.Vehicles;

namespace HaulageSystem.Application.Dtos.Quotes;

public class GetQuoteDeliveryInfoDto
{
    public DateTime? DeliveryDate { get; set; }
    public string DeliveryLocation { get; set; }

}