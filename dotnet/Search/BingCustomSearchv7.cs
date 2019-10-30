//Copyright (c) Microsoft Corporation. All rights reserved.
//Licensed under the MIT License.
// <using>
using System;
using System.Net.Http;
using System.Web;
using Newtonsoft.Json;
// </using>

namespace bing_custom_search_example_dotnet
{
    class Program
    {
        static void Main(string[] args)
        {
            // <vars>
            // Add your Azure Bing Custom Search subscription key and endpoint to your environment variables.
            // Your endpoint will have the form: https://<your-custom-subdomain>.cognitiveservices.azure.com/bingcustomsearch/v7.0
            var subscriptionKey = Environment.GetEnvironmentVariable("BING_CUSTOM_SEARCH_SUBSCRIPTION_KEY");
            var endpoint = Environment.GetEnvironmentVariable("BING_CUSTOM_SEARCH_ENDPOINT");

            var customConfigId = "YOUR-CUSTOM-CONFIG-ID"; // you can also use "1"
            var searchTerm = args.Length > 0 ? args[0]: "microsoft";            
            // </vars>
            // </url>
            // Use your Azure Bing Custom Search endpoint to create the full request URL.
            var url = endpoint + "/search?" + "q=" + searchTerm + "&customconfig=" + customConfigId;
            // </url>
            // <client>
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);
            // </client>
            // <sendRequest>
            var httpResponseMessage = client.GetAsync(url).Result;
            var responseContent = httpResponseMessage.Content.ReadAsStringAsync().Result;
            BingCustomSearchResponse response = JsonConvert.DeserializeObject<BingCustomSearchResponse>(responseContent);
            // </sendRequest>
            // <iterateResponse>
            for(int i = 0; i < response.webPages.value.Length; i++)
            {                
                var webPage = response.webPages.value[i];

                Console.WriteLine("name: " + webPage.name);
                Console.WriteLine("url: " + webPage.url);                
                Console.WriteLine("displayUrl: " + webPage.displayUrl);
                Console.WriteLine("snippet: " + webPage.snippet);
                Console.WriteLine("dateLastCrawled: " + webPage.dateLastCrawled);
                Console.WriteLine();
            } 
            //</iterateResponse>           
        }
    }
    // <repsonseClasses>
    public class BingCustomSearchResponse
    {        
        public string _type{ get; set; }            
        public WebPages webPages { get; set; }
    }

    public class WebPages
    {
        public string webSearchUrl { get; set; }
        public int totalEstimatedMatches { get; set; }
        public WebPage[] value { get; set; }        
    }

    public class WebPage
    {
        public string name { get; set; }
        public string url { get; set; }
        public string displayUrl { get; set; }
        public string snippet { get; set; }
        public DateTime dateLastCrawled { get; set; }
        public string cachedPageUrl { get; set; }
        public OpenGraphImage openGraphImage { get; set; }        
    }

    public class OpenGraphImage
    {
        public string contentUrl { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }
    // <repsonseClasses>
}
