namespace HaulageSystem.Application.Domain.Dtos.Materials;

public class MaterialUnitDto
{
    public MaterialUnitDto(int id, string name)
    {
        Id = id;
        UnitName = name;
    }
    public int Id { get; set; }
    public string UnitName { get; set; }
}