namespace Tree.Core.Domain;

public abstract class Component(string name)
{
    public string Name { get; } = name;
    public List<Component> Children { get; } = [];

    public void Add(Component child) => Children.Add(child);
}
