using Tree.Core.Domain;
using Tree.Core.Domain.Traversals;
using Tree.Tests.Core.Mocks;

namespace Tree.Tests.Core.TraversalTests;

public class BreadthFirstTraversalTests
{
    [Fact]
    public void BreadthFirstTraversal_ShouldNotifyAndPrintNodesInBreadthFirstOrder()
    {
        // Arrange
        Component tree = new TreeBuilder("root")
            .AddNode("child1")
                .AddLeaf("leaf1", "text1")
            .EndNode()
            .AddLeaf("leaf2", "text2")
            .Build();

        FakeMonitor monitor = new();
        FakePrintStrategy printer = new();
        BreadthFirstTraversal traversal = new(printer, monitor);

        // Act
        traversal.Traverse(tree);

        // Assert
        string[] expectedOrder = ["root", "child1", "leaf2", "leaf1"];

        Assert.Equal(expectedOrder.Length, monitor.Notifications.Count);
        Assert.Equal(expectedOrder.Length, printer.PrintedComponents.Count);

        for (int i = 0; i < expectedOrder.Length; i++)
        {
            Assert.Equal(expectedOrder[i], monitor.Notifications[i].Item1.Name);
            Assert.Equal(expectedOrder[i], printer.PrintedComponents[i].Name);
        }
    }
}