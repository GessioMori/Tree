using Tree.Core.Domain;
using Tree.Core.Domain.Interfaces;

namespace Tree.Tests.Mocks;

public class FakePrintStrategy : IPrintStrategy
{
    public List<Component> PrintedComponents { get; } = [];

    public void Print(Component component)
    {
        PrintedComponents.Add(component);
    }
}
