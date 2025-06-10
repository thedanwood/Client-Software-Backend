using HaulageSystem.Domain.Enums;

namespace HaulageSystem.Application.Dtos.Quotes;

public class JouneyTimeHasTrafficDto
{
    public bool HasTrafficEnabled { get; set; }
        public int JourneyTime { get; set; }
}