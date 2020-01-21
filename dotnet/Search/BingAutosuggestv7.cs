// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Newtonsoft.Json;
using System;
using System.Net.Http;

namespace BingAutosuggest
{
    class Program
    {
        // Add your Azure Bing Autosuggest subscription key to your environment variables.
        static string key = Environment.GetEnvironmentVariable("BING_AUTOSUGGEST_SUBSCRIPTION_KEY");
        // Add your Azure Bing Autosuggest endpoint to your environment variables.
        static string endpoint = Environment.GetEnvironmentVariable("BING_AUTOSUGGEST_ENDPOINT");
        static string path = "/bing/v7.0/Suggestions/";

        // For a list of available markets, go to:
        // https://docs.microsoft.com/rest/api/cognitiveservices/bing-autosuggest-api-v7-reference#market-codes
        static string market = "en-US";

        static string query = "sail";

        // These properties are used for optional headers (see below).
        //static string ClientId = "<Client ID from Previous Response Goes Here>";
        //static string ClientIp = "999.999.999.999";
        //static string ClientLocation = "+90.0000000000000;long: 00.0000000000000;re:100.000000000000";

        async static void Autosuggest()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", key);

            // The following headers are optional, but it is recommended they be treated as required.
            // These headers help the service return more accurate results.
            //client.DefaultRequestHeaders.Add("X-Search-Location", ClientLocation);
            //client.DefaultRequestHeaders.Add("X-MSEdge-ClientID", ClientId);
            //client.DefaultRequestHeaders.Add("X-MSEdge-ClientIP", ClientIp);

            string uri = endpoint + path + "?mkt=" + market + "&query=" + System.Net.WebUtility.UrlEncode(query);

            HttpResponseMessage response = await client.GetAsync(uri);

           string contentString = await response.Content.ReadAsStringAsync();
            dynamic parsedJson = JsonConvert.DeserializeObject(contentString);
            Console.WriteLine(JsonConvert.SerializeObject(parsedJson, Formatting.Indented));
        }

        static void Main(string[] args)
        {
            Autosuggest();
            Console.ReadLine();
        }
    }
}
