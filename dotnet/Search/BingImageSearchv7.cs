// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Newtonsoft.Json;
using System;
using System.Text;
using System.Net;
using System.IO;
using System.Collections.Generic;

/* This sample makes a call to the Bing Search API with a text query and returns relevant data from the web.
 * Documentation: https://docs.microsoft.com/en-us/azure/cognitive-services/bing-web-search/
 */

namespace BingImageSearch
{
    class Program
    {
        // Add your Azure Bing Search V7 subscription key and endpoint to your environment variables.
        static string subscriptionKey = Environment.GetEnvironmentVariable("BING_SEARCH_V7_SUBSCRIPTION_KEY");
        static string endpoint = Environment.GetEnvironmentVariable("BING_SEARCH_V7_ENDPOINT") + "/bing/v7.0/images/search";

        const string query = "puppies";

        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Dictionary<String, String> relevantHeaders = new Dictionary<String, String>();

            Console.WriteLine("Searching images for: " + query);

            // Construct the URI of the search request
            var uriQuery = endpoint + "?q=" + Uri.EscapeDataString(query);

            // Perform the Web request and get the response
            WebRequest request = HttpWebRequest.Create(uriQuery);
            request.Headers["Ocp-Apim-Subscription-Key"] = subscriptionKey;
            HttpWebResponse response = (HttpWebResponse)request.GetResponseAsync().Result;
            string json = new StreamReader(response.GetResponseStream()).ReadToEnd();

            // Extract Bing HTTP headers
            foreach (String header in response.Headers)
            {
                if (header.StartsWith("BingAPIs-") || header.StartsWith("X-MSEdge-"))
                    relevantHeaders[header] = response.Headers[header];
            }

            Console.WriteLine("\nRelevant HTTP Headers:\n");
            foreach (var header in relevantHeaders)
                Console.WriteLine(header.Key + ": " + header.Value);

            Console.WriteLine("\nJSON Response:\n");
            dynamic parsedJson = JsonConvert.DeserializeObject(json);
            Console.WriteLine(JsonConvert.SerializeObject(parsedJson, Formatting.Indented));
        }
    }
}
