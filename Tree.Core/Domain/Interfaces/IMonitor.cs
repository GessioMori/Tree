namespace Tree.Core.Domain.Interfaces;

public interface IMonitor
{
    void Notify(Component component, string eventInfo);
}