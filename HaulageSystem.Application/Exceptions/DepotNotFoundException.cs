using System.Globalization;
using HaulageSystem.Application.Models.Depots;
using HaulageSystem.Domain.Enums;
using Serilog;

namespace HaulageSystem.Application.Exceptions;

public class DepotNotFoundException: Exception
{
    public DepotNotFoundException(int? depotId, int? materialId): base(GetMessage(depotId, materialId))
    {
    }

    private static string GetMessage(int? depotId, int? materialId)
    {
        var msg = "Depot not found.";
        if (depotId.HasValue)
        {
            msg += $"Depot id = {depotId.Value.ToString()}.";
        }
        if(materialId.HasValue)
        {
            msg += $"Material id {materialId.Value.ToString()}.";
        }
        return msg;
    }
}