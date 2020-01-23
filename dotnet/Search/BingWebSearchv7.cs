// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Newtonsoft.Json;
using System;
using System.Text;
using System.Net;
using System.IO;
using System.Collections.Generic;

/* This sample uses the Bing Web Search API v7 to retrieve different kinds of media from the web.
 * Web Search includes the most types of web content, whereas other Search APIs get more specific about a type.
 * Web Search API:
 * https://dev.cognitive.microsoft.com/docs/services/f40197291cd14401b93a478716e818bf/operations/56b4447dcf5ff8098cef380d/console
 */

namespace BingWebSearch
{
    class Program
    {
        // Add your Azure Bing Search V7 subscription key and endpoint to your environment variables
        static string subscriptionKey = Environment.GetEnvironmentVariable("BING_SEARCH_V7_SUBSCRIPTION_KEY");
        static string endpoint = Environment.GetEnvironmentVariable("BING_SEARCH_V7_ENDPOINT") + "/bing/v7.0/search";

        const string query = "Microsoft Cognitive Services";

        static void Main()
        {
            // Create a dictionary to store relevant headers
            Dictionary<String, String> relevantHeaders = new Dictionary<String, String>();

            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("Searching the Web for: " + query);

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

            // Show headers
            Console.WriteLine("\nRelevant HTTP Headers:\n");
            foreach (var header in relevantHeaders)
                Console.WriteLine(header.Key + ": " + header.Value);

            Console.WriteLine("\nJSON Response:\n");
            dynamic parsedJson = JsonConvert.DeserializeObject(json);
            Console.WriteLine(JsonConvert.SerializeObject(parsedJson, Formatting.Indented));
        }
    }
}
