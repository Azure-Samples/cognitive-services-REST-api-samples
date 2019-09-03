using Newtonsoft.Json;
using System;
using System.Net.Http;

namespace EntitySearchSample
{
    class Program
    {
        // Add your Bing Entity Search endpoint to your environment variables.
        static string host = System.getenv("BING_ENTITY_SEARCH_ENDPOINT");
        static string path = "/bing/v7.0/entities";

        static string market = "en-US";

        // Add your Bing Entity Search subscription key to your environment variables.
        static string key = System.getenv("BING_ENTITY_SEARCH_SUBSCRIPTION_KEY");

        static string query = "italian restaurant near me";

        async static void Search()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", key);

            string uri = host + path + "?mkt=" + market + "&q=" + System.Net.WebUtility.UrlEncode(query);

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
