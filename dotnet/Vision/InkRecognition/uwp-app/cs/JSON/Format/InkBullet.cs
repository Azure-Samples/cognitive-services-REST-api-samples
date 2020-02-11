using Newtonsoft.Json;

namespace Contoso.NoteTaker.JSON.Format
{
    public class InkBullet : InkRecognitionUnit
    {
        [JsonProperty(PropertyName = "recognizedText")]
        public string RecognizedText { get; set; }
    }
}
