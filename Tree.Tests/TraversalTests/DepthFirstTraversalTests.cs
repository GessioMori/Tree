using Tree.Core.Domain;
using Tree.Core.Domain.Traversals;
using Tree.Tests.Mocks;

namespace Tree.Tests.TraversalTests;

public class DepthFirstTraversalTests
{
    [Fact]
    public void DepthFirstTraversal_ShouldNotifyAndPrintNodesInDepthFirstOrder()
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
        DepthFirstTraversal traversal = new(printer, monitor);

        // Act
        traversal.Traverse(tree);

        // Assert
        string[] expectedOrder = ["root", "child1", "leaf1", "leaf2"];

        Assert.Equal(expectedOrder.Length, monitor.Notifications.Count);
        Assert.Equal(expectedOrder.Length, printer.PrintedComponents.Count);

        for (int i = 0; i < expectedOrder.Length; i++)
        {
            Assert.Equal(expectedOrder[i], monitor.Notifications[i].Item1.Name);
            Assert.Equal(expectedOrder[i], printer.PrintedComponents[i].Name);
        }
    }
}