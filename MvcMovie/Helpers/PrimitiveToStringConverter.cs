using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NewtonJsonTest.Helpers
{
    public class StringNumericConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return null;
            var value = JToken.Load(reader);
            if (value.Type == JTokenType.Float)
                return System.ComponentModel.TypeDescriptor.GetConverter(((JValue)value).Value).ConvertToInvariantString(((JValue)value).Value);
            return (string)value;
        }

        public override bool CanWrite { get { return false; } }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}