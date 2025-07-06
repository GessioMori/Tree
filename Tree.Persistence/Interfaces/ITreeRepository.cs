using Tree.Core.Domain;

namespace Tree.Persistence.Interfaces;

public interface ITreeRepository
{
    Task SaveAsync(Component root, string filePath);
    Task<Component> LoadAsync(string filePath);
}
