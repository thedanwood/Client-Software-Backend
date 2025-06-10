namespace HaulageSystem.Application.Configuration.ApiOptions;

public class CorsConfigOptions
{
    public string AllowedOrigins { get; set; }

    public string[] AllowedOriginsList()
    {
        return AllowedOrigins.Split(";").ToArray();
    }
}