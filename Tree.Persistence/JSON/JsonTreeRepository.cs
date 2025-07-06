using System.Text.Json;
using Tree.Core.Domain;
using Tree.Persistence.DTOs;
using Tree.Persistence.Interfaces;

namespace Tree.Persistence.JSON;

public class JsonTreeRepository : ITreeRepository
{
    private readonly JsonSerializerOptions _jsonSerializerWriteOptions;
    private readonly JsonSerializerOptions _jsonSerializerReadOptions;

    public JsonTreeRepository()
    {
        this._jsonSerializerWriteOptions = new JsonSerializerOptions
        {
            WriteIndented = true,
            Converters = { new ComponentDtoJsonConverter() },
        };

        this._jsonSerializerReadOptions = new JsonSerializerOptions
        {
            Converters = { new ComponentDtoJsonConverter() },
            PropertyNameCaseInsensitive = true
        };
    }

    public async Task SaveAsync(Component root, string filePath)
    {
        ComponentDto dto = root.ToDto();

        string json = JsonSerializer.Serialize(dto, this._jsonSerializerWriteOptions);

        await File.WriteAllTextAsync(filePath, json);
    }

    public async Task<Component> LoadAsync(string filePath)
    {
        string json = await File.ReadAllTextAsync(filePath);

        ComponentDto? dto = JsonSerializer.Deserialize<ComponentDto>(json, this._jsonSerializerReadOptions);

        return dto == null ?
            throw new InvalidOperationException("Failed to deserialize the component from JSON.") :
            dto.ToDomain();
    }
}

