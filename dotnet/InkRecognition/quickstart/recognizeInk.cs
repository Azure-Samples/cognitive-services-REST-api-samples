using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ConsoleApp7
{
    class Program
    {
        // Replace the subscriptionKey string with your valid subscription key.
        const string subscriptionKey = "YOUR_SUBSCRIPTION_KEY";

        // Replace the dataPath string with a path to the JSON formatted ink stroke data.
        const string dataPath = @"PATH_TO_INK_STROKE_DATA";

        // URI information for ink recognition:
        const string endpoint = "https://api.cognitive.microsoft.com";
        const string inkRecognitionUrl = "/inkrecognizer/v1.0-preview/recognize";

        static async Task<string> Request(string apiAddress, string endpoint, string subscriptionKey, string requestData)
        {

            using (HttpClient client = new HttpClient { BaseAddress = new Uri(apiAddress) })
            {
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);

                var content = new StringContent(requestData, Encoding.UTF8, "application/json");
                var res = await client.PutAsync(endpoint, content);
                if (res.IsSuccessStatusCode)
                {
                    return await res.Content.ReadAsStringAsync();
                }
                else
                {
                    return $"ErrorCode: {res.StatusCode}";
                }
            }

        }

        static void recognizeInk(string requestData)
        {

            //construct the request
            var result = Request(
                endpoint,
                inkRecognitionUrl,
                subscriptionKey,
                requestData).Result;

            dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(result);
            System.Console.WriteLine(jsonObj);
        }

        public static JObject LoadJson(string fileLocation)
        {

            var jsonObj = new JObject();

            using (StreamReader file = File.OpenText(fileLocation))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                jsonObj = (JObject)JToken.ReadFrom(reader);
            }
            return jsonObj;
        }

        static void Main(string[] args)
        {

            var requestData = LoadJson(dataPath);
            string requestString = requestData.ToString(Newtonsoft.Json.Formatting.None);
            recognizeInk(requestString);
            System.Console.WriteLine("\nPress any key to exit ");
            System.Console.ReadKey();
        }
    }
}
