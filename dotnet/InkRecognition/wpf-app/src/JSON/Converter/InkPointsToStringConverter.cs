using Newtonsoft.Json;
using Contoso.NoteTaker.Helpers;
using System;
using System.Collections.Generic;
using Windows.UI.Input.Inking;

namespace Contoso.NoteTaker.JSON.Converter
{
    public class InkPointsToStringConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var points = value as IReadOnlyList<InkPoint>;
            if (points != null)
            {
                var pointsStr = InkPointHelper.InkPointsToString(points);
                serializer.Serialize(writer, pointsStr);
            }
            else
            {
                throw new InvalidCastException("Unable to cast object to type 'IReadOnlyList<InkPoint>'");
            }
        }
    }
}
