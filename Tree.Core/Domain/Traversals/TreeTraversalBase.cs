using Tree.Core.Domain.Interfaces;

namespace Tree.Core.Domain.Traversals;

public abstract class TreeTraversalBase(IPrintStrategy printStrategy, IMonitor monitor) : ITreeTraversal
{
    protected readonly IPrintStrategy PrintStrategy = printStrategy;
    protected readonly IMonitor Monitor = monitor;

    public abstract void Traverse(Component root);

    protected void NotifyAndPrint(Component node, int depth)
    {
        Monitor?.Notify(node, depth.ToString());
        PrintStrategy.Print(node);
    }
}