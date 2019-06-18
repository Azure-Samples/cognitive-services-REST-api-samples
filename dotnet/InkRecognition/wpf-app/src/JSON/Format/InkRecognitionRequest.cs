using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Contoso.NoteTaker.JSON.Format
{
    class InkRecognitionRequest
    {
        [JsonProperty(PropertyName = "applicationType"), JsonConverter(typeof(StringEnumConverter))]
        public InkContentType ApplicationType { get; set; } = InkContentType.Mixed;

        [JsonProperty(PropertyName = "unit"), JsonConverter(typeof(StringEnumConverter))]
        public InkPointUnitType Unit { get; set; } = InkPointUnitType.Millimeter;

        [JsonProperty(PropertyName = "language")]
        public string Language { get; set; } = "en-US";

        [JsonProperty(PropertyName = "strokes")]
        public IReadOnlyList<InkRecognizerStroke> Strokes {get; set;}

        public InkRecognitionRequest(IReadOnlyList<InkRecognizerStroke> strokes)
        {
            this.Strokes = strokes;
        }
    }

    public enum InkContentType
    {
        [EnumMember(Value = "drawing")]
        Drawing,
        [EnumMember(Value = "writing")]
        Writing,
        [EnumMember(Value = "mixed")]
        Mixed
    }

    public enum InkPointUnitType
    {
        [EnumMember(Value = "mm")]
        Millimeter,
        [EnumMember(Value = "cm")]
        Centimeter,
        [EnumMember(Value = "in")]
        Inch
    }
}
