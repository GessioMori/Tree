using Tree.Core.Domain;
using Tree.Core.Domain.Traversals;

namespace Tree.ConsoleApp;

internal class Program
{
    private static void Main(string[] args)
    {
        Component tree = new TreeBuilder("root")
            .AddNode("section1")
                .AddLeaf("leaf1", "content A")
                .AddLeaf("leaf2", "content B")
            .EndNode()
            .AddNode("section2")
                .AddLeaf("leaf3", "content C")
            .EndNode()
            .Build();

        ConsoleMonitor monitor = new();
        ConsolePrintStrategy printer = new();

        PreOrderTraversal traversal = new(printer, monitor);
        traversal.Traverse(tree);

        Console.WriteLine("Traversal concluído.");

        if (!args.Contains("--nowait"))
        {
            Console.WriteLine("Pressione qualquer tecla para sair.");
            Console.ReadKey();
        }
    }
}