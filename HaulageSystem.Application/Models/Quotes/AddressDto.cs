using HaulageSystem.Application.Models.Routing;

namespace HaulageSystem.Application.Models.Quotes;

public class 
    AddressDto
{
    public AddressDto(RoutePoint addressPoint, string fullAddress = "")
    {
        AddressPoint = addressPoint;
        FullAddress = fullAddress;
    }
    public RoutePoint AddressPoint { get; set; }
    public string FullAddress { get; set; }
}