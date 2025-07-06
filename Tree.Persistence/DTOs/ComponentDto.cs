namespace Tree.Persistence.DTOs;

public abstract class ComponentDto
{
    public required string Name { get; set; }
    public List<ComponentDto> Children { get; set; } = [];
}
