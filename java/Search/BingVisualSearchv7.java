import java.util.*;
import java.io.*;

import com.google.gson.Gson;
import com.google.gson.GsonBuilder;
import com.google.gson.JsonObject;
import com.google.gson.JsonParser;

import org.apache.http.HttpEntity;
import org.apache.http.HttpResponse;
import org.apache.http.client.methods.HttpPost;
import org.apache.http.entity.ContentType;
import org.apache.http.entity.mime.MultipartEntityBuilder;
import org.apache.http.impl.client.CloseableHttpClient;
import org.apache.http.impl.client.HttpClientBuilder;

/**
 * Add the Gson, HttpComponents, and HttpClient Mime jars to a lib folder: 
 *   Gson: https://github.com/google/gson 
 *   HttpComponents (HttpClient and HttpCore): http://hc.apache.org/downloads.cgi 
 *   HttpClient Mime: https://mvnrepository.com/artifact/org.apache.httpcomponents/httpmime
 *   Commons Logging: https://mvnrepository.com/artifact/commons-logging/commons-logging
 *
 * Add your Bing Search V7 key and endpoint to your environment variables.
 *
 * Maven info: 
 *   groupId: com.google.code.gson 
 *   artifactId: gson 
 *   version: x.x.x
 *
 * Compile and run from the command line: 
 *   javac BingVisualSearch.java -cp .;lib\* -encoding UTF-8 
 *   java -cp .;lib\* BingVisualSearch
 */
public class BingVisualSearch {

    // Add your Bing Search V7 key endpoint to your environment variables.
    static String subscriptionKey = System.getenv("BING_SEARCH_V7_SUBSCRIPTION_KEY");
    static String endpoint = System.getenv("BING_SEARCH_V7_ENDPOINT") + "/bing/v7.0/images/visualsearch";
    // Use your own URL image if desired.
    static String imagePath = "https://raw.githubusercontent.com/Azure-Samples/cognitive-services-sample-data-files/master/ComputerVision/Images/objects.jpg";

    public static void main(String[] args) {
        
        CloseableHttpClient httpClient = HttpClientBuilder.create().build();
        
        try {
            HttpEntity entity = MultipartEntityBuilder
                .create()
                .addBinaryBody("image", new File(imagePath))
                .build();

            HttpPost httpPost = new HttpPost(endpoint);
            httpPost.setHeader("Ocp-Apim-Subscription-Key", subscriptionKey);
            httpPost.setEntity(entity);
            HttpResponse response = httpClient.execute(httpPost);

            InputStream stream = response.getEntity().getContent();
            String json = new Scanner(stream).useDelimiter("\\A").next();

            System.out.println("\nJSON Response:\n");
            System.out.println(prettify(json));
        }
        catch (IOException e)
        {
            e.printStackTrace(System.out);
            System.exit(1);
        }
        catch (Exception e) {
            e.printStackTrace(System.out);
            System.exit(1);
        }
    }
    
    // Pretty-printer for JSON; uses GSON parser to parse and re-serialize
    public static String prettify(String json_text) {
        JsonObject json = JsonParser.parseString(json_text).getAsJsonObject();
        Gson gson = new GsonBuilder().setPrettyPrinting().create();
        return gson.toJson(json);
    }
    
}
