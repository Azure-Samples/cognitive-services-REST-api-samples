using Contoso.NoteTaker.JSON;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Contoso.NoteTaker.Http
{
    public class HttpManager
    {
        string destinationUrl;
        HttpClient httpClient;

        public HttpManager(string appKey, string baseAddress, string destinationUrl)
        {
            httpClient = new HttpClient() { BaseAddress = new Uri(baseAddress)};
            this.destinationUrl = destinationUrl;

            httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", appKey);
        }

        public async Task<HttpResponseMessage> PutAsync(string jsonRequest)
        {
            try
            {
                var httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
                var httpResponse = await httpClient.PutAsync(destinationUrl, httpContent);

                // Throw exception for malformed/unauthorized http requests
                if (httpResponse.StatusCode == HttpStatusCode.BadRequest || httpResponse.StatusCode == HttpStatusCode.Unauthorized)
                {
                    var errorJson = await httpResponse.Content.ReadAsStringAsync();
                    var errDetail = JSONProcessor.ParseInkRecognitionError(errorJson);
                    throw new HttpRequestException(errDetail.ToString());
                }
                return httpResponse;
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
