namespace HaulageSystem.Application.Models.Routing;

public class RoutePoint
{
    public RoutePoint() { } 
    public RoutePoint(decimal latitude, decimal longtiude)
    {
        Latitude = latitude;
         Longitude = longtiude;
    }
    public decimal Longitude { get; set; }
    public decimal Latitude { get; set; }
}