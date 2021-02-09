using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Har.Domain.Components;

namespace Har.Application.JsonConverters
{
    public class ComponentJsonConverter: JsonConverter<IComponent>
    {
        public override IComponent Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, IComponent value, JsonSerializerOptions options)
        {
            switch (value)
            {
                case TextComponent textComponent:
                    JsonSerializer.Serialize(writer, textComponent, typeof(TextComponent), options);
                    break;
                case ImagesComponent imageComponent:
                    JsonSerializer.Serialize(writer, imageComponent, typeof(TextComponent), options);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(value), $"Unknown implementation of the interface {nameof(IComponent)} for the parameter {nameof(value)}. Unknown implementation: {value?.GetType().Name}");
            }
        }
    }
}