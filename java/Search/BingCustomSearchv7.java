// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. 
// <imports>
import javax.net.ssl.HttpsURLConnection;
import java.io.InputStream;
import java.net.URL;
import java.net.URLEncoder;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.Scanner;

import com.google.gson.Gson;
import com.google.gson.GsonBuilder;
import com.google.gson.JsonObject;
import com.google.gson.JsonParser;
// </imports>

/**
 * This sample uses BingCustomSearch to do a web search with a text query and
 * return custom-designed results.
 * 
 * Add the Bing Custom Search key, endpoint, and custom configuration ID to your
 * environment variables.
 * 
 * Include the Gson library jar in your project folder:
 * https://github.com/google/gson
 *
 * To compile/run from command line: 
 *   javac BingCustomSearch.java -cp .;gson-2.8.6.jar 
 *   java -cp .;gson-2.8.6.jar BingCustomSearch
 */

public class BingCustomSearch {
    // <vars>
    // Add your Bing Custom Search subscription key and endpoint to your environment variables.
    // Example endpoint: https://<your-custom-subdomain>.cognitiveservices.azure.com
    static String subscriptionKey = System.getenv("BING_CUSTOM_SEARCH_SUBSCRIPTION_KEY");
    static String endpoint = System.getenv("BING_CUSTOM_SEARCH_ENDPOINT") + "/bingcustomsearch/v7.0/search";
    
    static String customConfigId = System.getenv("BING_CUSTOM_CONFIG"); //you can also use "1"
    static String searchTerm = "Microsoft";  // Replace with another search term, if you'd like.
    // </vars>

    // <main>
    public static void main (String[] args) {
        try {
            System.out.println("Searching the Web for: " + searchTerm);

            SearchResults result = SearchWeb(searchTerm);

            System.out.println("\nRelevant HTTP Headers:\n");
            for (String header : result.relevantHeaders.keySet())
                System.out.println(header + ": " + result.relevantHeaders.get(header));

            System.out.println("\nJSON Response:\n");
            System.out.println(prettify(result.jsonResponse));
        }
        catch (Exception e) {
            e.printStackTrace(System.out);
            System.exit(1);
        }
    }
    // </main>

    // <searchWeb>
    public static SearchResults SearchWeb(String searchQuery) throws Exception {
        // Construct URL of search request (endpoint + query string)
        URL url = new URL(endpoint + "?q=" +  URLEncoder.encode(searchTerm, "UTF-8") + "&CustomConfig=" + customConfigId);
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
        scanner.close();
        return results;
    }
    // </searchWeb>

    // <prettify>
    // Pretty-printer for JSON; uses GSON parser to parse and re-serialize
    public static String prettify(String jsonText) {
        JsonObject json = JsonParser.parseString(jsonText).getAsJsonObject();
        Gson gson = new GsonBuilder().setPrettyPrinting().create();
        return gson.toJson(json);
    }
    // </prettify>
}

// <searchResultsClass>
// Container class for search results encapsulates relevant headers and JSON data
class SearchResults{
    HashMap<String, String> relevantHeaders;
    String jsonResponse;
    SearchResults(HashMap<String, String> headers, String json) {
        relevantHeaders = headers;
        jsonResponse = json;
    }
}
// </searchResultsClass>
