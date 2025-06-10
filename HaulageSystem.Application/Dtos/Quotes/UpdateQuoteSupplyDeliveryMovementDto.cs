using HaulageSystem.Application.Domain.Dtos.Materials;
using HaulageSystem.Application.Domain.Dtos.Vehicles;
using HaulageSystem.Application.Dtos.Materials;
using HaulageSystem.Application.Models.Quotes;
using HaulageSystem.Domain.Enums;

namespace HaulageSystem.Application.Dtos.Quotes;

    public class UpdateQuoteSupplyDeliveryMovementDto
    {
        public int MaterialMovementId { get; set; }
        public int NumberOfLoads { get; set; }
        public int MaterialId { get; set; }
        public string DepotName { get; set; }
        public int Quantity { get; set; }
        public int MaterialUnitId { get; set; }
        public int VehicleTypeId { get; set; }
        public int DepotMaterialPriceId { get; set; }
        public int DefaultOnewayJourneyTime { get; set; }
        public int OnewayJourneyTime { get; set; }
        public decimal DefaultTotalDeliveryPrice { get; set; }
        public decimal TotalDeliveryPrice { get; set; }
        public decimal DefaultDeliveryPricePerMinute { get; set; }
        public decimal DeliveryPricePerMinute { get; set; }
        public decimal MaterialAndDeliveryPricePerQuantityUnit { get; set; }
        public decimal DefaultMaterialAndDeliveryPricePerQuantityUnit { get; set; }
        public decimal TotalMaterialPrice { get; set; }
        public decimal DefaultTotalMaterialPrice { get; set; }
        public decimal MaterialPricePerQuantityUnit { get; set; }
        public decimal DefaultMaterialPricePerQuantityUnit { get; set; }
        public bool HasTrafficEnabled { get; set; }
        public List<UpdateQuoteSupplyDeliveryDepotPricing> DepotPricings { get; set; }
    }

    public class UpdateQuoteSupplyDeliveryDepotPricing
    {
        public int DepotMaterialPriceId { get; set; }
        public decimal Price { get; set; }
        public string DepotName { get; set; }
        public int JourneyTime { get; set; }
    }
