using HaulageSystem.Application.Models.Requests;

namespace HaulageSystem.Application.Domain.Interfaces.Services;

public interface IPdfService
{
    MemoryStream GenerateQuotePdf(GenerateQuotePdfRequest request);
}