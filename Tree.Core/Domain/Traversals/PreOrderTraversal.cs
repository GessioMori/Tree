using Tree.Core.Domain.Interfaces;

namespace Tree.Core.Domain.Traversals;

public class PreOrderTraversal(IPrintStrategy printStrategy, IMonitor monitor) :
    TreeTraversalBase(printStrategy, monitor)
{
    public override void Traverse(Component root)
    {
        TraverseInternal(root, 0);
    }

    private void TraverseInternal(Component node, int depth)
    {
        NotifyAndPrint(node, depth);

        if (node is Branch composite)
        {
            foreach (Component child in composite.Children)
            {
                TraverseInternal(child, depth + 1);
            }
        }
    }
}