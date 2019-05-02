using Newtonsoft.Json;

namespace Contoso.NoteTaker.JSON.Format
{
    public class PointDetailsPattern
    {
        [JsonProperty(PropertyName = "x")]
        public float X { get; set; }

        [JsonProperty(PropertyName = "y")]
        public float Y { get; set; }
    }
}
