using Newtonsoft.Json;

namespace Contoso.NoteTaker.JSON.Format
{
    public class Rectangle
    {
        [JsonProperty(PropertyName = "topX")]
        public float topX { get; set; }

        [JsonProperty(PropertyName = "topY")]
        public float topY { get; set; }

        [JsonProperty(PropertyName = "width")]
        public float width { get; set; }

        [JsonProperty(PropertyName = "height")]
        public float height { get; set; }
    }
}
