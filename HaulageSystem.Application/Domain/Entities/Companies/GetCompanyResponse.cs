namespace HaulageSystem.Application.Domain.Entities.Companies;

public class GetCompanyResponse
{
    public int CompanyId { get; set; }
    public string CompanyName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
}

