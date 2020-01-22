// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
// <using>
using System;
using System.Net.Http;
using System.Web;
using Newtonsoft.Json;
// </using>

namespace BingCustomSearch
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

            var customConfigId = Environment.GetEnvironmentVariable("BING_CUSTOM_CONFIG"); // you can also use "1"
            var searchTerm = args.Length > 0 ? args[0] : "microsoft";
            // </vars>
            // <url>
            // Use your Azure Bing Custom Search endpoint to create the full request URL.
            var url = endpoint + "/bingcustomsearch/v7.0/images/search?" + "q=" + searchTerm + "&customconfig=" + customConfigId;
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
            for (int i = 0; i < response.value.Length; i++)
            {
                var webPage = response.value[i];

                Console.WriteLine("Name: " + webPage.name);
                Console.WriteLine("WebSearchUrl: " + webPage.webSearchUrl);
                Console.WriteLine("HostPageUrl: " + webPage.hostPageUrl);
                Console.WriteLine("Thumbnail: " + webPage.thumbnail.width + " width, " + webPage.thumbnail.height + " height");
                Console.WriteLine();
            }
            //</iterateResponse>           
        }
    }
    // <responseClasses>
    public class BingCustomSearchResponse
    {
        public string _type { get; set; }
        public WebPage[] value { get; set; }
    }

    public class WebPage
    {
        public string name { get; set; }
        public string webSearchUrl { get; set; }
        public string hostPageUrl { get; set; }
        public OpenGraphImage thumbnail { get; set; }
    }

    public class OpenGraphImage
    {
        public int width { get; set; }
        public int height { get; set; }
    }
    // </responseClasses>
}
