// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

import java.io.*;
import java.net.*;
import java.util.*;
import javax.net.ssl.HttpsURLConnection;
import com.google.gson.Gson;
import com.google.gson.GsonBuilder;
import com.google.gson.JsonObject;
import com.google.gson.JsonParser;

/*
 * Download the Gson library: https://github.com/google/gson
 * Maven info:
 *     groupId: com.google.code.gson
 *     artifactId: gson
 *     version: x.x.x
 *
 * Place the Gson jar in the same folder as this file (BingAutosuggest.java), 
 * then compile and run from the command line:
 *   javac BingAutosuggest.java -classpath .;gson-2.8.6.jar -encoding UTF-8
 *   java -cp .;gson-2.8.6.jar BingAutosuggest
 */

public class BingAutosuggest {

    // Add your Bing Autosuggest subscription key to your environment variables.
    static String subscriptionKey = System.getenv("BING_AUTOSUGGEST_SUBSCRIPTION_KEY");

    static String endpoint = System.getenv("BING_AUTOSUGGEST_ENDPOINT") +  "/bing/v7.0/Suggestions";

    static String mkt = "en-US";
    static String query = "sail";

    public static String get_suggestions () throws Exception {
        String encoded_query = URLEncoder.encode (query, "UTF-8");
        String params = "?mkt=" + mkt + "&q=" + encoded_query;
        URL url = new URL (endpoint + params);

        HttpsURLConnection connection = (HttpsURLConnection) url.openConnection();
        connection.setRequestMethod("GET");
        connection.setRequestProperty("Ocp-Apim-Subscription-Key", subscriptionKey);
        connection.setDoOutput(true);

        StringBuilder response = new StringBuilder ();
        BufferedReader in = new BufferedReader(
        new InputStreamReader(connection.getInputStream()));
        String line;
        while ((line = in.readLine()) != null) {
            response.append(line);
        }
        in.close();

        return response.toString();
    }

    public static String prettify (String json_text) {
        JsonObject json = JsonParser.parseString(json_text).getAsJsonObject();
        Gson gson = new GsonBuilder().setPrettyPrinting().create();
        return gson.toJson(json);
    }

    public static void main(String[] args) {
        try {
            String response = get_suggestions ();
            System.out.println (prettify (response));
        }
        catch (Exception e) {
            System.out.println (e);
        }
    }
}
