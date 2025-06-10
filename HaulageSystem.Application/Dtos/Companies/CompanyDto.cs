namespace HaulageSystem.Application.Domain.Dtos.Companies;

public class CompanyDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
}