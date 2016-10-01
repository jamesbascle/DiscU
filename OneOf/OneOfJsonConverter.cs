using System;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;

namespace OneOf
{
    public class OneOfJsonConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var oneOfValue = ((IOneOf)value).Value;

            var packedValue = new PackedOneOfValue
            {
                Type = oneOfValue.GetType().AssemblyQualifiedName,
                Value = oneOfValue,
            };

            serializer.Serialize(writer, packedValue);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            // recreate the OneOf's underlying value

            var packedValue = serializer.Deserialize<PackedOneOfValue>(reader);
            var oneOfValueType = Type.GetType(packedValue.Type);
            var oneOfValue = packedValue.Value;

            if (oneOfValue is Newtonsoft.Json.Linq.JObject)
            {
                oneOfValue = RecreateComplexType(
                    oneOfValueType, 
                    (Newtonsoft.Json.Linq.JObject)oneOfValue);
            }

            // create the OneOf with that value

            var createMethod = existingValue.GetType().GetTypeInfo().GetDeclaredMethod("Create");
            var oneOf = createMethod.Invoke(null, new[] { oneOfValue });

            return oneOf;
        }

        object RecreateComplexType(Type type, Newtonsoft.Json.Linq.JObject jObject)
        {
            var value = jObject.ToObject(type);
            return value;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType.GetTypeInfo().ImplementedInterfaces.Contains(typeof(IOneOf));
        }

        class PackedOneOfValue
        {
            public string Type { get; set; }
            public object Value { get; set; }
        }
    }
}
