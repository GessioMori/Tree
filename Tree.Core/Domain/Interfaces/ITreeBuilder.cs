namespace Tree.Core.Domain.Interfaces;

public interface ITreeBuilder
{
    ITreeBuilder AddNode(string name);
    ITreeBuilder AddLeaf(string name, string text);
    ITreeBuilder EndNode();
    Component Build();
}
