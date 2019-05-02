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
            if (ErrorCode != null)
            {
                msg += "Http Error code: " + ErrorCode;
            }

            if (Target != null)
            {
                msg += " Target: " + Target;
            }

            if (Message != null)
            {
                msg += " Message : " + Message;
            }

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
