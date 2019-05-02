using Newtonsoft.Json;
using System.Collections.Generic;

namespace Contoso.NoteTaker.JSON.Format
{
    public class InkWord : InkRecognitionUnit
    {
        [JsonProperty(PropertyName = "recognizedText")]
        public string RecognizedText { get; set; }

        [JsonProperty(PropertyName = "alternates")]
        public List<Alternates> Alternates { get; set; }
    }
}
