using Newtonsoft.Json;
using System.Collections.Generic;

namespace Contoso.NoteTaker.JSON.Format
{
    public class InkRecognitionResponse
    {
        [JsonProperty(PropertyName = "recognitionUnits")]
        public List<InkRecognitionUnit> RecognitionUnits { get; set; }
    }
}
