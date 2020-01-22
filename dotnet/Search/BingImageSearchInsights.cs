using Newtonsoft.Json;
using System;
using System.Text;
using System.Net;

/* This sample makes a call to the Bing Image Search API with a query image and returns visually similar images.
 * Details about each similar image, also known as "insights", are returned in the JSON response.
 * Documentation: https://docs.microsoft.com/en-us/azure/cognitive-services/bing-web-search/
 */

namespace BingImageSearchInsights
{
    class Program
    {
        // Add your Azure Bing Search v7 subscription key to your environment variables
        static string subscriptionKey = Environment.GetEnvironmentVariable("BING_SEARCH_V7_SUBSCRIPTION_KEY");
        // Add your Azure Bing Search v7 endpoint to your environment variables  
        static string endpoint = Environment.GetEnvironmentVariable("BING_SEARCH_V7_ENDPOINT") + "/bing/v7.0/images/details";

        // Place an image (for example a jpg or png) in your bin\Debug\netcoreapp3.0 folder.
        const string imageFile = "YOUR-IMAGE.jpg";

        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;

            WebClient client = new WebClient();
            client.Headers["Ocp-Apim-Subscription-Key"] = subscriptionKey;
            client.Headers["ContentType"] = "multipart/form-data";
            // Returns all insights
            byte[] response = client.UploadFile(endpoint + "?modules=All", imageFile);
            var json = Encoding.Default.GetString(response);

            // Pretty print the result
            dynamic parsedJson = JsonConvert.DeserializeObject(json);
            Console.WriteLine("\nBing Image Insights JSON Response:\n");
            Console.WriteLine(JsonConvert.SerializeObject(parsedJson, Formatting.Indented));

            Console.ReadLine();
        }
    }
}
