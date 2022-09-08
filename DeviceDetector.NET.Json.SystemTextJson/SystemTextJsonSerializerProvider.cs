using System;
using System.Text.Json;

namespace DeviceDetector.NET.Json.SystemTextJson
{
    public class SystemTextJsonSerializerProvider : IJsonSerializerProvider
    {
        public bool CanHandle(Type type)
        {
            return true;
        }

        public T Deserialize<T>(string jsonString, bool camelCase = true)
        {
            return JsonSerializer.Deserialize<T>(jsonString, CreateJsonSerializerOptions(camelCase));
        }

        public object Deserialize(Type type, string jsonString, bool camelCase = true)
        {
            return JsonSerializer.Deserialize(jsonString, type, CreateJsonSerializerOptions(camelCase));
        }

        public string Serialize(object obj, bool camelCase = true, bool indented = false)
        {
            return JsonSerializer.Serialize(obj, CreateJsonSerializerOptions(camelCase, indented));
        }

        protected virtual JsonSerializerOptions CreateJsonSerializerOptions(bool camelCase = true, bool indented = false)
        {
            var settings = new JsonSerializerOptions();

            if (camelCase)
            {
                settings.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            }

            if (indented)
            {
                settings.WriteIndented = true;
            }

            return settings;
        }
    }
}
