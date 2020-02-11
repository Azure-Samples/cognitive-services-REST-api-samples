using Newtonsoft.Json;

namespace Contoso.NoteTaker.JSON.Format
{
    public class Alternates
    {
        [JsonProperty(PropertyName = "category")]
        public RecognitionUnitKind Kind { get; set; }

        [JsonProperty(PropertyName ="center")]
        public PointDetailsPattern Center { get; set; }

        [JsonProperty(PropertyName ="points")]
        public PointDetailsPattern Points { get; set; }

        [JsonProperty(PropertyName ="rotationAngle")]
        public PointDetailsPattern RotationAngle { get; set; }

        [JsonProperty(PropertyName = "confidence")]
        public float Confidence { get; set; }

        [JsonProperty(PropertyName = "recognizedString")]
        public string RecognizedText { get; set; }
    }
}
