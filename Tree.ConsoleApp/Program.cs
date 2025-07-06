using Tree.Core.Domain;
using Tree.Core.Domain.Traversals;
using Tree.Persistence.JSON;

namespace Tree.ConsoleApp;

internal class Program
{
    private static async Task Main(string[] args)
    {
        const string storagePath = "/app/storage";

        string[] jsonFiles = Directory.GetFiles(storagePath, "*.json");

        if (jsonFiles.Length == 0)
        {
            Console.WriteLine("Nenhum arquivo JSON encontrado em " + storagePath);
            return;
        }

        JsonTreeRepository repo = new();
        ConsoleMonitor monitor = new();
        ConsolePrintStrategy printer = new();
        DepthFirstTraversal traversal = new(printer, monitor);

        foreach (string jsonFile in jsonFiles)
        {
            Console.WriteLine($"\nRestaurando árvore do arquivo: {Path.GetFileName(jsonFile)}");

            Component tree = await repo.LoadAsync(jsonFile);

            Console.WriteLine("Percorrendo árvore restaurada:");
            traversal.Traverse(tree);
            Console.WriteLine($"\nTraversal do arquivo {jsonFile} concluído.");
        }
    }
}