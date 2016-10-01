using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;

namespace OneOf
{
    public class OneOfJsonConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var oneOf = (IOneOf)value;
            var oneOfValue = oneOf.Value;

            var packedValue = new PackedOneOfValue
            {
                Value = oneOfValue,
                Type = oneOfValue.GetType().Name,
            };

            serializer.Serialize(writer, packedValue);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            // recreate the OneOf's underlying value

            var packedValue = serializer.Deserialize<PackedOneOfValue>(reader);

            var oneOfType = objectType;
            var oneOfValue = packedValue.Value;
            var oneOfValueType = oneOfType.GenericTypeArguments
                .FirstOrDefault(t => t.Name == packedValue.Type);

            if (oneOfValueType == null)
            {
                var genericArgs = string.Join(",", oneOfType.GenericTypeArguments.Select(t => t.Name));
                throw new InvalidOperationException($"Value of type {packedValue.Type} is not compatible with OneOf<{genericArgs}>");
            }

            if (oneOfValue is Newtonsoft.Json.Linq.JObject)
            {
                // The packed value must be something more complex than String, Int32 etc,
                // so the deserializer needs us to help convert it.
                var jObject = (Newtonsoft.Json.Linq.JObject)oneOfValue;
                oneOfValue = jObject.ToObject(oneOfValueType);
            }

            // create the OneOf with that value

            var createMethod = oneOfType.GetTypeInfo().GetDeclaredMethod("Create");
            var oneOf = createMethod.Invoke(null, new[] { oneOfValue });

            return oneOf;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType.GetTypeInfo().ImplementedInterfaces.Contains(typeof(IOneOf));
        }

        class PackedOneOfValue
        {
            public object Value;
            public string Type;
        }
    }
}
