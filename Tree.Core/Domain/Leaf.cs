namespace Tree.Core.Domain;

public class Leaf(string name, string text) : Component(name)
{
    public string Text { get; } = text;
}