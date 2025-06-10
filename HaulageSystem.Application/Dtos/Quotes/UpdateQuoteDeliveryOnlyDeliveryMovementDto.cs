using HaulageSystem.Application.Models.Quotes;
using HaulageSystem.Application.Models.Routing;
using HaulageSystem.Domain.Enums;

namespace HaulageSystem.Application.Dtos.Quotes;

    public class UpdateQuoteDeliveryOnlyDeliveryMovementDto
    {
        public int DeliveryMovementId { get; set; }
        public int DefaultOnewayJourneyTime { get; set; }
        public decimal DefaultTotalDeliveryPrice { get; set; }
        public decimal TotalDeliveryPrice { get; set; }
        public decimal DefaultDeliveryPricePerMinute { get; set; }
        public decimal DeliveryPricePerMinute { get; set; }
        public bool HasTrafficEnabled { get; set; }    
        public int VehicleTypeId { get; set; }

        public AddressDto StartLocation { get; set; }
        public List<JouneyTimeHasTrafficDto> JourneyTimes { get; set; }
    }
