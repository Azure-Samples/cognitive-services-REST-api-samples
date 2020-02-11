using Contoso.NoteTaker.JSON.Converter;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Windows.Foundation;
using Windows.UI.Input.Inking;

namespace Contoso.NoteTaker.JSON.Format
{
    public class InkRecognizerPoint
    {
        [JsonIgnore]
        private double x;
        [JsonIgnore]
        private double y;
        [JsonProperty(PropertyName = "x")]
        public double X { get { return x; } }

        [JsonProperty(PropertyName = "y")]
        public double Y { get { return y; } }

        public InkRecognizerPoint(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
    }

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

        [JsonProperty(PropertyName = "points", NullValueHandling = NullValueHandling.Ignore)]
        public IReadOnlyList<InkRecognizerPoint> Points { get; protected set; }

        public InkRecognizerStroke(InkStroke stroke, float DpiX, float DpiY)
        {
            inkStrokeInternal = stroke;

            var pointsInPixels = inkStrokeInternal.GetInkPoints();
            Points = ConvertPixelsToMillimeters(pointsInPixels, DpiX, DpiY).AsReadOnly();
        }

        private List<InkRecognizerPoint> ConvertPixelsToMillimeters(IReadOnlyList<InkPoint> pointsInPixels, float DpiX, float DpiY)
        {
            var transformedInkPoints = new List<InkRecognizerPoint>();
            const float inchToMillimeterFactor = 25.4f;


            foreach (var point in pointsInPixels)
            {
                var transformedX = (point.Position.X / DpiX) * inchToMillimeterFactor;
                var transformedY = (point.Position.Y / DpiY) * inchToMillimeterFactor;
                var transformedInkPoint = new InkRecognizerPoint(transformedX, transformedY);

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
