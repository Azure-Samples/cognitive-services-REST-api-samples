using Newtonsoft.Json;

namespace Contoso.NoteTaker.JSON.Format
{
    public class HttpErrorDetails
    {
        [JsonProperty(PropertyName = "statusCode")]
        public string ErrorCode { get; set; }

        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }

        [JsonProperty(PropertyName = "target")]
        public string Target { get; set; }

        [JsonProperty(PropertyName = "details")]
        public HttpErrorDetails[] Details { get; set; }

        public override string ToString()
        {
            string msg = "";
            msg += (ErrorCode != null) ? " Http Error code : " + ErrorCode : "";
            msg += (Target != null) ? " Target : " + Target : "";
            msg += (Message != null) ? " Message : " + Message : "";

            if (Details != null)
            {
                msg += "\n Error Details : ";
                foreach (var errDetail in Details)
                {
                    msg += "\n" + errDetail.ToString();
                }
            }
            return msg;
        }
    }
}
