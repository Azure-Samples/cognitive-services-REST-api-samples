//Copyright (c) Microsoft Corporation. All rights reserved.
//Licensed under the MIT License.

using System;
using System.Net;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace BingSearchApisQuickstart
{

    class Program
    {

        // Replace this string value with your valid access key.
        const string subscriptionKey = "Enter your subscription key here";

        // Verify the endpoint URI. If you encounter unexpected authorization errors,
        // double-check this value against the Bing search endpoint in your Azure dashboard.
        const string uriBase = "https://api.cognitive.microsoft.com/bing/v7.0/images/search";

        const string searchTerm = "tropical ocean";

        // A struct to return image search results seperately from headers
        struct SearchResult
        {
            public string  jsonResult;
            public Dictionary<String, String> relevantHeaders;
        }

        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("Searching images for: " + searchTerm + "\n");
            //send a search request using the search term
            SearchResult result = BingImageSearch(searchTerm);
            //deserialize the JSON response from the Bing Image Search API
            dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(result.jsonResult);

            var firstJsonObj = jsonObj["value"][0];
            Console.WriteLine("Title for the first image result: " + firstJsonObj["name"]+"\n");
            //After running the application, copy the output URL into a browser to see the image. 
            Console.WriteLine("URL for the first image result: " + firstJsonObj["webSearchUrl"]+"\n");

            Console.Write("\nPress Enter to exit ");
            Console.ReadLine();
        }

        /// <summary>
        /// Performs a Bing Image search and return the results as a SearchResult.
        /// </summary>
        static SearchResult BingImageSearch(string searchQuery)
        {
            // Construct the URI of the search request
            var uriQuery = uriBase + "?q=" + Uri.EscapeDataString(searchQuery);

            // Perform the Web request and get the response
            WebRequest request = WebRequest.Create(uriQuery);
            request.Headers["Ocp-Apim-Subscription-Key"] = subscriptionKey;
            HttpWebResponse response = (HttpWebResponse)request.GetResponseAsync().Result;
            string json = new StreamReader(response.GetResponseStream()).ReadToEnd();

            // Create result object for return
            var searchResult = new SearchResult()
            {
                jsonResult = json,
                relevantHeaders = new Dictionary<String, String>()
            };

            // Extract Bing HTTP headers
            foreach (String header in response.Headers)
            {
                if (header.StartsWith("BingAPIs-") || header.StartsWith("X-MSEdge-"))
                    searchResult.relevantHeaders[header] = response.Headers[header];
            }

            return searchResult;
        }

    }
}
