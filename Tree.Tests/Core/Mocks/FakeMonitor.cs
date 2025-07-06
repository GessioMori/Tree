using Tree.Core.Domain;
using Tree.Core.Domain.Interfaces;

namespace Tree.Tests.Core.Mocks;

public class FakeMonitor : IMonitor
{
    public List<(Component, string)> Notifications { get; } = [];

    public void Notify(Component component, string eventInfo)
    {
        Notifications.Add((component, eventInfo));
    }
}