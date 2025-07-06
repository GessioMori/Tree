using Tree.Core.Domain;
using Tree.Core.Domain.Traversals;
using Tree.Tests.Mocks;

namespace Tree.Tests.TraversalTests;

public class PreOrderTraversalTests
{
    [Fact]
    public void Traverse_ShouldNotifyAndPrintNodesInPreOrder()
    {
        // Arrange
        Component root = new TreeBuilder("root")
            .AddNode("child1")
                .AddLeaf("leaf1", "text1")
            .EndNode()
            .AddLeaf("leaf2", "text2")
            .Build();

        FakeMonitor monitor = new();
        FakePrintStrategy printer = new();
        PreOrderTraversal traversal = new(printer, monitor);

        // Act
        traversal.Traverse(root);

        // Assert
        Assert.Equal(4, monitor.Notifications.Count);
        Assert.Equal(4, printer.PrintedComponents.Count);

        Assert.Equal("root", monitor.Notifications[0].Item1.Name);
        Assert.Equal("child1", monitor.Notifications[1].Item1.Name);
        Assert.Equal("leaf1", monitor.Notifications[2].Item1.Name);
        Assert.Equal("leaf2", monitor.Notifications[3].Item1.Name);
    }
}