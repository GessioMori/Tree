using Tree.Core.Domain.Interfaces;

namespace Tree.Core.Domain.Traversals;

public class DepthFirstTraversal(IPrintStrategy printStrategy, IMonitor monitor) : TreeTraversalBase(printStrategy, monitor)
{
    public override void Traverse(Component root)
    {
        Stack<(Component, int)> stack = new();
        stack.Push((root, 0));

        while (stack.Count > 0)
        {
            (Component node, int depth) = stack.Pop();

            NotifyAndPrint(node, depth);

            if (node is CompositeNode composite)
            {
                for (int i = composite.Children.Count - 1; i >= 0; i--)
                {
                    stack.Push((composite.Children[i], depth + 1));
                }
            }
        }
    }
}