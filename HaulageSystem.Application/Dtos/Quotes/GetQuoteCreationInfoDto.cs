using HaulageSystem.Application.Domain.Dtos.Companies;
using HaulageSystem.Application.Domain.Dtos.Materials;

namespace HaulageSystem.Application.Dtos.Quotes;

public class GetQuoteCreationInfoDto
{
    public string CustomerName { get; set; }
    public CompanyDto Company { get; set; }
    public string CreatedByName { get; set; }
    public DateTime CreatedDateTime { get; set; }
}