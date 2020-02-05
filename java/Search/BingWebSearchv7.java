// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

import java.net.*;
import java.util.*;
import java.io.*;
import javax.net.ssl.HttpsURLConnection;

import com.google.gson.Gson;
import com.google.gson.GsonBuilder;
import com.google.gson.JsonObject;
import com.google.gson.JsonParser;

/**
 * This sample uses the Bing Web Search API with a text query to return relevant results from the web.
 * 
 * Include the Gson jar library with your project:
 * Gson: https://github.com/google/gson
 * 
 * Maven info:
 *     groupId: com.google.code.gson
 *     artifactId: gson
 *     version: 2.8.6
 *
 * Compile and run from the command line (change Gson version if needed):
 *   javac BingWebSearch.java -cp .;gson-2.8.6.jar -encoding UTF-8
 *   java -cp .;gson-2.8.6.jar BingWebSearch
 */
public class BingWebSearch {

    // Add your Bing Search V7 subscription key to your environment variables.
    static String subscriptionKey = System.getenv("BING_SEARCH_V7_SUBSCRIPTION_KEY");
    static String endpoint = System.getenv("BING_SEARCH_V7_ENDPOINT") + "/bing/v7.0/search";
    // Add your own search terms, if desired.
    static String searchTerm = "Microsoft Cognitive Services";

    public static void main(String[] args) {
        try {
            System.out.println("Searching the Web for: " + searchTerm);

            SearchResults result = SearchWeb(searchTerm);

            System.out.println("\nRelevant HTTP Headers:\n");
            for (String header : result.relevantHeaders.keySet())
                System.out.println(header + ": " + result.relevantHeaders.get(header));

            System.out.println("\nJSON Response:\n");
            System.out.println(prettify(result.jsonResponse));
        } catch (Exception e) {
            e.printStackTrace(System.out);
            System.exit(1);
        }
    }

    public static SearchResults SearchWeb (String searchQuery) throws Exception {
        // Construct URL of search request (endpoint + query string)
        URL url = new URL(endpoint + "?q=" +  URLEncoder.encode(searchQuery, "UTF-8"));
        HttpsURLConnection connection = (HttpsURLConnection)url.openConnection();
        connection.setRequestProperty("Ocp-Apim-Subscription-Key", subscriptionKey);

        // Receive JSON body
        InputStream stream = connection.getInputStream();
        Scanner scanner = new Scanner(stream);
        String response = scanner.useDelimiter("\\A").next();

        // Construct result object for return
        SearchResults results = new SearchResults(new HashMap<String, String>(), response);

        // Extract Bing-related HTTP headers
        Map<String, List<String>> headers = connection.getHeaderFields();
        for (String header : headers.keySet()) {
            if (header == null) continue;      // may have null key
            if (header.startsWith("BingAPIs-") || header.startsWith("X-MSEdge-")) {
                results.relevantHeaders.put(header, headers.get(header).get(0));
            }
        }
        stream.close();
        scanner.close();

        return results;
    }

    // Pretty-printer for JSON; uses GSON parser to parse and re-serialize
    public static String prettify(String json_text) {
        JsonObject json = JsonParser.parseString(json_text).getAsJsonObject();
        Gson gson = new GsonBuilder().setPrettyPrinting().create();
        return gson.toJson(json);
    }
}

// Container class for search results encapsulates relevant headers and JSON data
class SearchResults{
    HashMap<String, String> relevantHeaders;
    String jsonResponse;
    SearchResults(HashMap<String, String> headers, String json) {
        relevantHeaders = headers;
        jsonResponse = json;
    }
}
