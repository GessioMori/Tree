using Tree.Core.Domain;

namespace Tree.Tests.Core.ComponentTests;

public class ComponentTests
{
    [Fact]
    public void AddChild_ShouldAddToChildren()
    {
        Branch parent = new("Parent");
        Leaf child = new("Leaf1", "text");

        parent.Add(child);

        Assert.Single(parent.Children);
        Assert.Contains(child, parent.Children);
    }
}