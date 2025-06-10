namespace HaulageSystem.Application.Dtos.Materials;

public class MaterialDto
{
    public MaterialDto(int id, string name)
    {
        Id = id;
        MaterialName = name;
    }
    public int Id { get; set; }
    public string MaterialName { get; set; }
}