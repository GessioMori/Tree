using Tree.Core.Domain;
using Tree.Core.Domain.Interfaces;

namespace Tree.ConsoleApp;

public class ConsolePrintStrategy : IPrintStrategy
{
    public void Print(Component component)
    {
        if (component is Leaf leaf)
        {
            Console.WriteLine($"[Leaf] {leaf.Name} - {leaf.Text}");
        }
        else
        {
            Console.WriteLine($"[Node] {component.Name}");
        }
    }
}