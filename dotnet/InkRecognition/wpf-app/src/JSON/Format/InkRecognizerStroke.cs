using Contoso.NoteTaker.JSON.Converter;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Windows.Foundation;
using Windows.UI.Input.Inking;

namespace Contoso.NoteTaker.JSON.Format
{
    public class InkRecognizerStroke
    {
        [JsonIgnore]
        private InkStroke inkStrokeInternal { get; set; }

        [JsonProperty(PropertyName = "id")]
        public UInt64 Id { get { return inkStrokeInternal.Id; } }

        [JsonProperty(PropertyName = "language", NullValueHandling = NullValueHandling.Ignore)]
        public string Language { get; set; } = null;

        [JsonProperty(PropertyName ="kind", NullValueHandling = NullValueHandling.Ignore)]
        public StrokeKind? Kind { get; set; } = null;

        [JsonProperty(PropertyName = "points"), JsonConverter(typeof(InkPointsToStringConverter))]
        public IReadOnlyList<InkPoint> Points { get; protected set; }

        public InkRecognizerStroke(InkStroke stroke, float DpiX, float DpiY)
        {
            inkStrokeInternal = stroke;

            var pointsInPixels = inkStrokeInternal.GetInkPoints();
            Points = ConvertPixelsToMillimeters(pointsInPixels, DpiX, DpiY).AsReadOnly();
        }

        private List<InkPoint> ConvertPixelsToMillimeters(IReadOnlyList<InkPoint> pointsInPixels, float DpiX, float DpiY)
        {
            var transformedInkPoints = new List<InkPoint>();
            const float inchToMillimeterFactor = 25.4f;


            foreach (var point in pointsInPixels)
            {
                var transformedX = (point.Position.X / DpiX) * inchToMillimeterFactor;
                var transformedY = (point.Position.Y / DpiY) * inchToMillimeterFactor;
                var transformedPoint = new Point(transformedX, transformedY);
                var transformedInkPoint = new InkPoint(transformedPoint, point.Pressure);

                transformedInkPoints.Add(transformedInkPoint);
            }

            return transformedInkPoints;
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
