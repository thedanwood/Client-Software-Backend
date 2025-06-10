namespace HaulageSystem.Application.Extensions;

public static class DateTimeExtensions
{
    public static string ToDateString(this DateTime date)
    {
        return date.ToString("dd/MM/yy");
    }
    public static string ToDateTimeString(this DateTime dateTime)
    {
        return dateTime.ToString("HH:mm dd/MM/yy");
    }
}