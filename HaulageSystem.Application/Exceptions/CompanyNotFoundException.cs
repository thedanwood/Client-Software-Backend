using System.Globalization;
using HaulageSystem.Application.Models.Depots;
using HaulageSystem.Domain.Enums;
using Serilog;

namespace HaulageSystem.Application.Exceptions;

public class CompanyNotFoundException: Exception
{
    public CompanyNotFoundException(int? companyId): base("No company found for CompanyId = " + companyId)
    {
    }
}