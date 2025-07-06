using Tree.Core.Domain;
using Tree.Core.Domain.Interfaces;

namespace Tree.ConsoleApp;

public class ConsoleMonitor : IMonitor
{
    public void Notify(Component component, string eventInfo)
    {
        Console.WriteLine($"Monitor: Visitando {component.Name} | Profundidade {eventInfo} | Horário: {DateTime.Now:HH:mm:ss}");
    }
}