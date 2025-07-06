using Tree.Core.Domain.Interfaces;

namespace Tree.Core.Domain.Traversals;

public class BreadthFirstTraversal(IPrintStrategy printStrategy, IMonitor monitor) : TreeTraversalBase(printStrategy, monitor)
{
    public override void Traverse(Component root)
    {
        Queue<(Component, int)> queue = new();
        queue.Enqueue((root, 0));

        while (queue.Count > 0)
        {
            (Component node, int depth) = queue.Dequeue();

            NotifyAndPrint(node, depth);

            if (node is CompositeNode composite)
            {
                foreach (Component child in composite.Children)
                {
                    queue.Enqueue((child, depth + 1));
                }
            }
        }
    }
}