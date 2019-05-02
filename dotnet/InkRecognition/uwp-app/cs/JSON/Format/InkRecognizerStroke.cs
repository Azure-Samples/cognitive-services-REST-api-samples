
using Contoso.NoteTaker.Helpers;
using Contoso.NoteTaker.JSON.Converter;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Windows.UI.Input.Inking;

namespace Contoso.NoteTaker.JSON.Format
{
    public class InkRecognizerStroke
    {
        [JsonIgnore]
        private InkStroke InkStroke { get; set; }

        [JsonProperty(PropertyName = "id")]
        public UInt64 Id { get { return InkStroke.Id; } }

        [JsonProperty(PropertyName = "language", NullValueHandling = NullValueHandling.Ignore)]
        public string Language { get; set; } = null;

        [JsonProperty(PropertyName ="kind", NullValueHandling = NullValueHandling.Ignore)]
        public StrokeKind? Kind { get; set; } = null;

        [JsonProperty(PropertyName = "points"), JsonConverter(typeof(InkPointsToStringConverter))]
        public IReadOnlyList<InkPoint> Points { get; protected set; }

        [JsonProperty(PropertyName = "drawingAttributes")]
        public InkDrawingAttributes DrawingAttributes { get { return InkStroke.DrawingAttributes; } }

        public InkRecognizerStroke(InkStroke stroke, float DpiX, float DpiY)
        {
            InkStroke = stroke;

            var pointsInPixels = GetInkPoints();
            Points = InkPointHelper.ConvertPixelsToMillimeters(pointsInPixels, DpiX, DpiY).AsReadOnly();
        }

        public IReadOnlyList<InkPoint> GetInkPoints()
        {
            return InkStroke.GetInkPoints();
        }
    }

    public enum StrokeKind
    {
        [EnumMember(Value = "inkDrawing")]
        Drawing,

        [EnumMember(Value = "inkWriting")]
        Writing
    }
}
