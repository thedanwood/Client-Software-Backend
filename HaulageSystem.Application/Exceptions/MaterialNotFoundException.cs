using System.Globalization;
using Serilog;

namespace HaulageSystem.Application.Exceptions;

public class MaterialNotFoundException: Exception
{
    public MaterialNotFoundException(int materialId, string extraMessage = ""): base($"Material not found for MaterialId = {materialId}\n" + extraMessage)
    {
    }
}