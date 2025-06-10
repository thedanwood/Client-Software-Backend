using System.ComponentModel;

namespace HaulageSystem.Domain.Enums;

public enum RecordTypes
{
    [Description("Delivery Only")]
    DeliveryOnly = 1,
    [Description("Supply and Delivery")]
    SupplyAndDelivery = 2,
}