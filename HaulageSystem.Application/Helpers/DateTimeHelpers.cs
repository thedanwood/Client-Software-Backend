namespace HaulageSystem.Application.Helpers;

public static class DateTimeHelpers
{
    public static int ConvertSecondsToMinutes(int seconds)
    {
        return (int)TimeSpan.FromSeconds(seconds).TotalMinutes;
    }
}