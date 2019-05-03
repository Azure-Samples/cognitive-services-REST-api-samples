using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Contoso.NoteTaker.JSON.Format
{
    abstract public class InkRecognitionUnit
    {
        [JsonProperty(PropertyName = "id")]
        public UInt64 Id { get; set; }

        [JsonProperty(PropertyName = "category")]
        public RecognitionUnitKind Kind { get; set; }

        [JsonProperty(PropertyName = "childIds")]
        public List<UInt64> ChildIds { get; set; }

        [JsonProperty(PropertyName = "class")]
        public RecognitionUnitType Type { get; set; }

        [JsonProperty(PropertyName = "parentId")]
        public UInt64 ParentId { get; set; }

        [JsonProperty(PropertyName = "boundingRectangle")]
        public Rectangle BoundingRect { get; set; }

        [JsonProperty(PropertyName = "rotatedBoundingRectangle")]
        public List<PointDetailsPattern> RotatedBoundingRect { get; set; }

        [JsonProperty(PropertyName = "strokeIds")]
        public List<UInt64> StrokeIds { get; set; }
    }

    public enum RecognitionUnitType
    {
        [EnumMember(Value = "leaf")]
        Leaf,
        [EnumMember(Value = "container")]
        Container
    }
}
