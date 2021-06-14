// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

/* This sample makes a call to the Bing Video Search API with a query and returns data about it.
 * Bing Video Search API: 
 * https://docs.microsoft.com/en-us/azure/cognitive-services/bing-video-search/quickstarts/csharp
 */

namespace BingVideoSearch
{
    class Program
    {
        // Replace the accessKey string value with your valid access key.
        static string _accessKey = Environment.GetEnvironmentVariable("BING_SEARCH_V7_SUBSCRIPTION_KEY");

        // Or use the custom subdomain endpoint displayed in the Azure portal for your resource.
        static string _uriBase = Environment.GetEnvironmentVariable("BING_SEARCH_V7_ENDPOINT") + "/bing/v7.0/videos/search";

        const string _searchTerm = "kittens";

        static async Task Main(string[] args)
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri(_uriBase);
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _accessKey);

            var response = await client.GetAsync($"?q={Uri.EscapeDataString(_searchTerm)}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<SearchResult>(json);

                foreach (var video in result.Videos)
                {
                    Console.WriteLine($"Name: {video.Name}");
                    Console.WriteLine($"ContentUrl: {video.ContentUrl}");
                    Console.WriteLine();
                }
            }
        }
    }

    class SearchResult
    {
        [JsonPropertyName("totalEstimatedMatches")]
        public int TotalEstimatedMatches { get; set; }

        [JsonPropertyName("value")]
        public List<Video> Videos { get; set; }
    }

    class Video
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("thumbnailUrl")]
        public string ThumbnailUrl { get; set; }

        [JsonPropertyName("contentUrl")]
        public string ContentUrl { get; set; }
    }
}
