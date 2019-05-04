using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Windows.UI.Input.Inking;
using System.Linq;
using System.Reflection;

namespace Contoso.NoteTaker.JSON.Converter
{
    public class InkPointsToStringConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(IReadOnlyList<InkPoint>).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            // Since CanRead flag is false, this function will not be called
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var points = value as IReadOnlyList<InkPoint>;
            if (points != null)
            {
                var pointsStr = string.Join(",",
                                    points.Select(p => Convert.ToSingle(p.Position.X) + "," +
                                                        Convert.ToSingle(p.Position.Y))
                                );
                serializer.Serialize(writer, pointsStr);
            }
            else
            {
                throw new InvalidCastException("Unable to cast object to type 'IReadOnlyList<InkPoint>'");
            }
        }
    }
}
