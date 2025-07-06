using Tree.Core.Domain;

namespace Tree.Persistence.DTOs;

public static class ComponentMappingExtensions
{
    public static ComponentDto ToDto(this Component component)
    {
        if (component is Leaf leaf)
        {
            return new LeafDto
            {
                Name = leaf.Name,
                Text = leaf.Text
            };
        }
        else if (component is Branch node)
        {
            return new BranchDto
            {
                Name = node.Name,
                Children = [.. node.Children.Select(child => child.ToDto())]
            };
        }
        else
        {
            throw new InvalidOperationException("Tipo de componente desconhecido.");
        }
    }

    public static Component ToDomain(this ComponentDto dto)
    {
        if (dto is LeafDto leafDto)
        {
            return new Leaf(leafDto.Name, leafDto.Text);
        }
        else if (dto is ComponentDto nodeDto)
        {
            Branch node = new(nodeDto.Name);
            foreach (ComponentDto childDto in nodeDto.Children)
            {
                node.Add(childDto.ToDomain());
            }
            return node;
        }
        else
        {
            throw new InvalidOperationException("Tipo de DTO desconhecido.");
        }
    }
}

