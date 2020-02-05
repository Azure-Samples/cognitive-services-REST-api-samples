import java.io.*;
import java.net.*;
import java.util.*;
import javax.net.ssl.HttpsURLConnection;
import com.google.gson.Gson;
import com.google.gson.GsonBuilder;
import com.google.gson.JsonObject;
import com.google.gson.JsonParser;

/**
 * This sample uses Bing Entity Search to search the web for an entity (restaurant).
 *
 * Gson: https://github.com/google/gson
 * Maven info:
 *   groupId: com.google.code.gson
 *   artifactId: gson
 *   version: x.x.x
 *
 * From the command line, compile and run:
 *   javac EntitySearch.java -cp .;gson-2.8.6.jar
 *   java -cp .;gson-2.8.6.jar EntitySearch
 */

public class BingEntitySearch {

    // Add your Bing Entity Search subscription key to your environment variables.
    static String subscriptionKey = System.getenv("BING_ENTITY_SEARCH_SUBSCRIPTION_KEY");
    // Add your Bing Entity Search endpoint to your environment variables.
    static String endpoint = System.getenv("BING_ENTITY_SEARCH_ENDPOINT") + "/bing/v7.0/entities";

    static String mkt = "en-US";
    static String query = "italian restaurant near me";

    public static String search() throws Exception {
        String encoded_query = URLEncoder.encode(query, "UTF-8");
        String params = "?mkt=" + mkt + "&q=" + encoded_query;
        URL url = new URL(endpoint + params);

        HttpsURLConnection connection = (HttpsURLConnection) url.openConnection();
        connection.setRequestMethod("GET");
        connection.setRequestProperty("Ocp-Apim-Subscription-Key", subscriptionKey);
        connection.setDoOutput(true);

        StringBuilder response = new StringBuilder();
        BufferedReader in = new BufferedReader(new InputStreamReader(connection.getInputStream()));
        String line;
        while ((line = in.readLine()) != null) {
            response.append(line);
        }
        in.close();

        return response.toString();
    }

    // Pretty-printer for JSON; uses GSON parser to parse and re-serialize
    public static String prettify(String jsonText) {
        JsonObject json = JsonParser.parseString(jsonText).getAsJsonObject();
        Gson gson = new GsonBuilder().setPrettyPrinting().create();
        return gson.toJson(json);
    }

    public static void main(String[] args) {
        try {
            String response = search();
            System.out.println(prettify(response));
        } catch (Exception e) {
            System.out.println(e);
        }
    }
}
