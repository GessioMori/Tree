using System.Text.Json;
using System.Text.Json.Serialization;
using Tree.Persistence.DTOs;

namespace Tree.Persistence.JSON;

public class ComponentDtoJsonConverter : JsonConverter<ComponentDto>
{
    public override ComponentDto? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using JsonDocument jsonDoc = JsonDocument.ParseValue(ref reader);
        JsonElement jsonObject = jsonDoc.RootElement;

        string name = jsonObject.GetProperty("Name").GetString() ?? throw new JsonException("Missing Name");

        if (jsonObject.TryGetProperty("Text", out JsonElement textElement))
        {
            return new LeafDto
            {
                Name = name,
                Text = textElement.GetString() ?? string.Empty
            };
        }
        else
        {
            BranchDto dto = new()
            {
                Name = name
            };

            if (jsonObject.TryGetProperty("Children", out JsonElement childrenElement) && childrenElement.ValueKind == JsonValueKind.Array)
            {
                foreach (JsonElement child in childrenElement.EnumerateArray())
                {
                    string rawText = child.GetRawText();
                    Utf8JsonReader childReader = new(System.Text.Encoding.UTF8.GetBytes(rawText));
                    childReader.Read();

                    ComponentDto? childDto = Read(ref childReader, typeof(ComponentDto), options);
                    if (childDto != null)
                    {
                        dto.Children.Add(childDto);
                    }
                }
            }

            return dto;
        }
    }


    public override void Write(Utf8JsonWriter writer, ComponentDto value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        writer.WriteString("Name", value.Name);

        if (value is LeafDto leaf)
        {
            writer.WriteString("Text", leaf.Text);
        }

        if (value.Children is { Count: > 0 })
        {
            writer.WritePropertyName("Children");
            writer.WriteStartArray();

            foreach (ComponentDto child in value.Children)
            {
                Write(writer, child, options);
            }

            writer.WriteEndArray();
        }

        writer.WriteEndObject();
    }
}
