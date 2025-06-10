using System.ComponentModel;
using EnumsNET;
using HaulageSystem.Domain.Enums;

namespace HaulageSystem.Application.Helpers;

public static class EnumHelpers
{
    private static Dictionary<QuoteActiveStates, string> ActiveStateDisplayableTexts = new()
    {
        {QuoteActiveStates.ActiveNeedToConvertTheseTo1InDB, "Active"},
        {QuoteActiveStates.Active, "Active"},
        {QuoteActiveStates.RestoredFromCompletedOrders, "Active"},
        {QuoteActiveStates.RestoredAsActiveFromExpiry, "Active"},
        {QuoteActiveStates.RestoredToQuoteFromOrder, "Active"},
        {QuoteActiveStates.RestoredAsActiveFromManualArchive, "Active"},
        {QuoteActiveStates.ConvertedToOrder, "Active"},
        {QuoteActiveStates.ManuallyArchived, "Archived"},
        {QuoteActiveStates.PermanentlyDeleted, "Deleted"},
        {QuoteActiveStates.OrderCompleted, "Completed"},
    };
    
    public static List<int> ActiveQuoteStates = new()
    {
        { QuoteActiveStates.Active.ToInt() },
        { QuoteActiveStates.RestoredFromCompletedOrders.ToInt()},
        { QuoteActiveStates.RestoredToQuoteFromOrder.ToInt()},
        { QuoteActiveStates.RestoredAsActiveFromManualArchive.ToInt() },
    };

    public static string GetActiveStateDisplayableText(int activeState)
    {
;        return ActiveStateDisplayableTexts[(QuoteActiveStates) activeState];
    }
    public static string ToDescription<T>(this T enumValue) 
        where T : struct, IConvertible
    {
        if (!typeof(T).IsEnum)
            return null;

        var description = enumValue.ToString();
        var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());

        if (fieldInfo != null)
        {
            var attrs = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), true);
            if (attrs != null && attrs.Length > 0)
            {
                description = ((DescriptionAttribute)attrs[0]).Description;
            }
        }

        return description;
    }
    public static int ToInt<T>(this T enumValue) where T : Enum
    {
        return Convert.ToInt32(enumValue);
    }
}