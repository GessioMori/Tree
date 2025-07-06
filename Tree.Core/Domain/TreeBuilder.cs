using Tree.Core.Domain.Interfaces;

namespace Tree.Core.Domain;

public class TreeBuilder : ITreeBuilder
{
    private readonly Stack<CompositeNode> _nodeStack = new();
    private readonly CompositeNode _root;

    public TreeBuilder(string rootName)
    {
        this._root = new(rootName);
        this._nodeStack.Push(_root);
    }

    public ITreeBuilder AddNode(string name)
    {
        CompositeNode node = new(name);
        this._nodeStack.Peek().Add(node);
        this._nodeStack.Push(node);
        return this;
    }

    public ITreeBuilder AddLeaf(string name, string text)
    {
        Leaf leaf = new(name, text);
        this._nodeStack.Peek().Add(leaf);
        return this;
    }

    public ITreeBuilder EndNode()
    {
        if (this._nodeStack.Count > 1)
        {
            this._nodeStack.Pop();
        }
        return this;
    }

    public Component Build()
    {
        return this._root;
    }
}
