// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

/*
 * This sample uses the Azure Bing Spell Check API to check the spelling of a query.
 * It then offers suggestions for corrections.
 * Bing Spell Check API: 
 * https://docs.microsoft.com/en-us/rest/api/cognitiveservices-bingsearch/bing-spell-check-api-v7-reference
 */

namespace BingSpellCheck
{
    class Program
    {
        // Add your Azure Bing Spell Check key and endpoint to your environment variables.
        static string subscriptionKey = Environment.GetEnvironmentVariable("BING_SPELL_CHECK_SUBSCRIPTION_KEY");
        static string endpoint = Environment.GetEnvironmentVariable("BING_SPELL_CHECK_ENDPOINT");
        static string path = "/bing/v7.0/spellcheck?";

        // For a list of available markets, go to:
        // https://docs.microsoft.com/rest/api/cognitiveservices/bing-autosuggest-api-v7-reference#market-codes
        static string market = "en-US";

        static string mode = "proof";

        static string query = "Hollo, wrld!";

        // These properties are used for optional headers (see below).
        // static string ClientId = "<Client ID from Previous Response Goes Here>";
        // static string ClientIp = "999.999.999.999";
        // static string ClientLocation = "+90.0000000000000;long: 00.0000000000000;re:100.000000000000";

        public async static Task Main(string[] args)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);

            // The following headers are optional, but it is recommended they be treated as required.
            // These headers help the service return more accurate results.
            // client.DefaultRequestHeaders.Add("X-Search-Location", ClientLocation);
            // client.DefaultRequestHeaders.Add("X-MSEdge-ClientID", ClientId);
            // client.DefaultRequestHeaders.Add("X-MSEdge-ClientIP", ClientIp);

            HttpResponseMessage response = new HttpResponseMessage();
            string uri = endpoint + path;

            List<KeyValuePair<string, string>> values = new List<KeyValuePair<string, string>>();
            values.Add(new KeyValuePair<string, string>("mkt", market));
            values.Add(new KeyValuePair<string, string>("mode", mode));
            values.Add(new KeyValuePair<string, string>("text", query));

            using (FormUrlEncodedContent content = new FormUrlEncodedContent(values))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
                response = await client.PostAsync(uri, content);
            }

            // Get the client ID header from your request (optional)
            // string client_id;
            // if (response.Headers.TryGetValues("X-MSEdge-ClientID", out IEnumerable<string> header_values))
            // {
            //     client_id = header_values.First();
            //     Console.WriteLine("Client ID: " + client_id);
            // }

            string contentString = await response.Content.ReadAsStringAsync();
            //Deserialize the JSON response from the API
            dynamic jsonObj = JsonConvert.DeserializeObject(contentString);
            Console.WriteLine(jsonObj);
        }
    }
}
