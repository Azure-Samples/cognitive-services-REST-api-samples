using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Contoso.NoteTaker.JSON.Format
{
    public class InkDrawing : InkRecognitionUnit
    {
        [JsonProperty(PropertyName = "center")]
        public PointDetailsPattern Center { get; set; }

        [JsonProperty(PropertyName = "points")]
        public List<PointDetailsPattern> Points { get; set; }

        [JsonProperty(PropertyName = "confidence")]
        public float Confidence { get; set; }

        [JsonProperty(PropertyName = "recognizedObject")]
        public DrawingShapeKind RecognizedShape { get; set; }

        [JsonProperty(PropertyName = "rotationAngle")]
        public float RotationAngle { get; set; }

        [JsonProperty(PropertyName = "alternates")]
        public List<Alternates> Alternates { get; set; }
    }

    public enum DrawingShapeKind
    {
        [EnumMember(Value = "drawing")]
        Drawing,

        [EnumMember(Value = "square")]
        Square,

        [EnumMember(Value = "rectangle")]
        Rectangle,

        [EnumMember(Value = "circle")]
        Circle,

        [EnumMember(Value = "ellipse")]
        Ellipse,

        [EnumMember(Value = "triangle")]
        Triangle,

        [EnumMember(Value = "isoscelestriangle")]
        IsoscelesTriangle,

        [EnumMember(Value = "equilateraltriangle")]
        EquilateralTriangle,

        [EnumMember(Value = "righttriangle")]
        RightTriangle,

        [EnumMember(Value = "quadrilateral")]
        Quadilateral,

        [EnumMember(Value = "diamond")]
        Diamond,

        [EnumMember(Value = "trapezoid")]
        Trapezoid,

        [EnumMember(Value = "parallelogram")]
        Parallelogram,

        [EnumMember(Value = "pentagon")]
        Pentagon,

        [EnumMember(Value = "hexagon")]
        Hexagon,

        [EnumMember(Value = "blockArrow")]
        BlockArrow,

        [EnumMember(Value = "heart")]
        Heart,

        [EnumMember(Value = "starSimple")]
        StarSimple,

        [EnumMember(Value = "starCrossed")]
        StarCrossed,

        [EnumMember(Value = "cloud")]
        Cloud,

        [EnumMember(Value = "line")]
        Line,

        [EnumMember(Value = "curve")]
        Curve,

        [EnumMember(Value = "polyLine")]
        PolyLine
    }
}
