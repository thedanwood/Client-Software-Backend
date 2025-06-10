using System.ComponentModel;

namespace HaulageSystem.Domain.Enums;

public enum QuoteActiveStates
{
    ActiveNeedToConvertTheseTo1InDB = 0,
    Active = 1,
    RestoredAsActiveFromManualArchive = 2,
    RestoredAsActiveFromExpiry = 3,
    ManuallyArchived = 4,
    PermanentlyDeleted = 5,
    ConvertedToOrder = 6,
    RestoredToQuoteFromOrder = 7,
    OrderCompleted = 8,
    RestoredFromCompletedOrders = 9,
}
