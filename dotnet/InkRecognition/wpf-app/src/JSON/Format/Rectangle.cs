using Newtonsoft.Json;

namespace Contoso.NoteTaker.JSON.Format
{
    public class Rectangle
    {
        [JsonProperty(PropertyName = "topX")]
        public float TopX { get; set; }

        [JsonProperty(PropertyName = "topY")]
        public float TopY { get; set; }

        [JsonProperty(PropertyName = "width")]
        public float Width { get; set; }

        [JsonProperty(PropertyName = "height")]
        public float Height { get; set; }
    }
}
