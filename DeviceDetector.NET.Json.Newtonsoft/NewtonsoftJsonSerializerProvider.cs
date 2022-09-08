using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;

namespace DeviceDetector.NET.Json.Newtonsoft
{
    public class NewtonsoftJsonSerializerProvider : IJsonSerializerProvider
    {

        private static readonly CamelCaseExceptDictionaryKeysResolver SharedCamelCaseExceptDictionaryKeysResolver = new CamelCaseExceptDictionaryKeysResolver();

        public bool CanHandle(Type type)
        {
            return true;
        }


        public T Deserialize<T>(string jsonString, bool camelCase = true)
        {
            return JsonConvert.DeserializeObject<T>(jsonString, CreateSerializerSettings(camelCase));
        }

        public object Deserialize(Type type, string jsonString, bool camelCase = true)
        {
            return JsonConvert.DeserializeObject(jsonString, type, CreateSerializerSettings(camelCase));
        }


        public string Serialize(object obj, bool camelCase = true, bool indented = false)
        {
            return JsonConvert.SerializeObject(obj, CreateSerializerSettings(camelCase, indented));
        }

        protected virtual JsonSerializerSettings CreateSerializerSettings(bool camelCase = true, bool indented = false)
        {
            var settings = new JsonSerializerSettings();
            
            if (camelCase)
            {
                settings.ContractResolver = SharedCamelCaseExceptDictionaryKeysResolver;
            }

            if (indented)
            {
                settings.Formatting = Formatting.Indented;
            }

            return settings;
        }

        private class CamelCaseExceptDictionaryKeysResolver : CamelCasePropertyNamesContractResolver
        {
            protected override JsonDictionaryContract CreateDictionaryContract(Type objectType)
            {
                var contract = base.CreateDictionaryContract(objectType);

                contract.DictionaryKeyResolver = propertyName => propertyName;

                return contract;
            }
        }
    }
}
