using HaulageSystem.Domain.Enums;

namespace HaulageSystem.Application.Dtos.Quotes;

public class RoutingParameterValueDto
{
    public RoutingParameters RoutingParameter { get; set; }
    public bool Value { get; set; }
}