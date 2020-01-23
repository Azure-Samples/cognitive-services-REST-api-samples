using Newtonsoft.Json;
using System;
using System.Net.Http;

/* This sample makes a call to the Bing Entity Search v7 API with a query and returns details about it.
 * Details about each similar image, also known as "insights", are returned in the JSON response.
 * Bing Entity Search API:
 * https://westus2.dev.cognitive.microsoft.com/docs/services/7a3fb374be374859a823b79fd938cc65/operations/52069701a465405ab3286f82
 */

namespace BingEntitySearch
{
    class Program
    {
        // Add your key and endpoint to your environment variables.
        static string key = Environment.GetEnvironmentVariable("BING_ENTITY_SEARCH_SUBSCRIPTION_KEY");
        static string endpoint = Environment.GetEnvironmentVariable("BING_ENTITY_SEARCH_ENDPOINT");
        static string path = "/bing/v7.0/entities/";

        static string market = "en-US";

        static string query = "italian restaurant near me";

        async static void Search()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", key);

            string uri = endpoint + path + "?mkt=" + market + "&q=" + System.Net.WebUtility.UrlEncode(query);

            HttpResponseMessage response = await client.GetAsync(uri);

            string contentString = await response.Content.ReadAsStringAsync();
            dynamic parsedJson = JsonConvert.DeserializeObject(contentString);

            Console.WriteLine(parsedJson);
        }

        static void Main(string[] args)
        {
            Search();
            Console.ReadLine();
        }

    }
}

