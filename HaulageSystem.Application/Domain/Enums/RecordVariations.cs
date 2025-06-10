using System.ComponentModel;

namespace HaulageSystem.Domain.Enums;

public enum RecordVariations
{
    [Description("Quote")]
    Quote =1,
    [Description("Order")]
    Order =2,
}