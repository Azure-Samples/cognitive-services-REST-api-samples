using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Contoso.NoteTaker.JSON.Format;
using System;
using System.Reflection;

namespace Contoso.NoteTaker.JSON.Converter
{
    public class InkRecognitionResponseConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(InkRecognitionUnit).IsAssignableFrom(objectType);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            // Since CanWrite flag is false, this function will not be called
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            try
            {
                var json = JToken.Load(reader);
                object relationship = CreateObject(json);
                if (relationship != null)
                {
                    serializer.Populate(json.CreateReader(), relationship);
                }
                return relationship;
            }
            catch(Exception e)
            {
                throw;
            }
        }

        private object CreateObject(JToken token)
        {
            if (token.Type == JTokenType.Null)
            {
                return null;
            }
            if (token["category"] == null)
            {
                throw new FormatException("Missing field in InkRecognitionResponse : category");
            }

            switch (token.Value<string>("category"))
            {
                case "writingRegion":
                    return new InkWritingRegion();
                case "paragraph":
                    return new InkParagraph();
                case "line":
                    return new InkLine();
                case "inkWord":
                    return new InkWord();
                case "listItem":
                    return new InkListItem();
                case "inkBullet":
                    return new InkBullet();
                case "inkDrawing":
                    return new InkDrawing();
                default:
                    throw new FormatException("Invalid value in InkRecognitionResponse for: category");
            }
        }
    }
}
