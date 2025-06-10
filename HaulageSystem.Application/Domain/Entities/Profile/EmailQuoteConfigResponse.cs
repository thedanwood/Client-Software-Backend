using HaulageSystem.Application.Models.Requests;

namespace HaulageSystem.Application.Domain.Entities.Profile;

public class EmailQuoteConfigResponse
{
    public List<SendEmailAddressRequest> CcEmails { get; set; }
}