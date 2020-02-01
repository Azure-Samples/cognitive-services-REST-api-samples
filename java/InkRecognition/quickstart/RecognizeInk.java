// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
// <imports>
import org.apache.http.HttpEntity;
import org.apache.http.client.methods.CloseableHttpResponse;
import org.apache.http.client.methods.HttpPut;
import org.apache.http.entity.StringEntity;
import org.apache.http.impl.client.CloseableHttpClient;
import org.apache.http.impl.client.HttpClients;
import org.apache.http.util.EntityUtils;
import com.fasterxml.jackson.core.JsonParseException;
import com.fasterxml.jackson.databind.JsonMappingException;
import com.fasterxml.jackson.databind.ObjectMapper;
import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Paths;
import java.util.HashMap;
import java.util.Map;
// </imports>

/**
 * Library jars needed (add to a lib folder): 
 *   httpclient-4.5.11+
 *   slf4j-jdk14-1.7.28+ 
 *   httpcore-4.4.13+ 
 *   commons-logging-1.2+
 *   jackson-databind-2.10.2+
 *   jackson-annotations-2.10.2+
 *   jackson-core-2.10.2+
 */

public class RecognizeInk {
    // <vars>
    // Add your Azure Ink Recognition subscription key to your environment variables.
    private static final String subscriptionKey = System.getenv("INK_RECOGNITION_SUBSCRIPTION_KEY");
    
    // Add your Azure Ink Recognition endpoint to your environment variables.
    public static final String rootUrl = System.getenv("INK_RECOGNITION_ENDPOINT");
    public static final String inkRecognitionUrl = "/inkrecognizer/v1.0-preview/recognize";
    // Replace the dataPath string with a path to the JSON formatted ink stroke data file.
    private static final String dataPath = "PATH_TO_INK_STROKE_DATA";
    // </vars> 
    // <main>
    public static void main(String[] args) throws Exception {

        String requestData = new String(Files.readAllBytes(Paths.get(dataPath)), "utf-8");
        recognizeInk(requestData);
    }
    // </main>
    // <recognizeInk>
    static void recognizeInk(String requestData) {
        System.out.println("Sending an Ink recognition request.");

        String result = sendRequest(rootUrl, inkRecognitionUrl, subscriptionKey, requestData);
        
        // Pretty-print the JSON result
        try {
            ObjectMapper objectMapper = new ObjectMapper();
            Map<String, Object> response = objectMapper.readValue(result, HashMap.class);
            System.out.println(objectMapper.writerWithDefaultPrettyPrinter().writeValueAsString(response));
        } catch (JsonParseException e) {
            e.printStackTrace();
        } catch (JsonMappingException e) {
            e.printStackTrace();
        } catch (IOException e) {
            e.printStackTrace();
        }  
    }
    // </recognizeInk>
    // <sendRequest>
    static String sendRequest(String endpoint, String apiAddress, String subscriptionKey, String requestData) {
        try (CloseableHttpClient client = HttpClients.createDefault()) {
            HttpPut request = new HttpPut(endpoint + apiAddress);
            // Request headers.
            request.setHeader("Content-Type", "application/json");
            request.setHeader("Ocp-Apim-Subscription-Key", subscriptionKey);
            request.setEntity(new StringEntity(requestData));
            try (CloseableHttpResponse response = client.execute(request)) {
                HttpEntity respEntity = response.getEntity();
                if (respEntity != null) {
                    return EntityUtils.toString(respEntity, "utf-8");
                }
            } catch (Exception respEx) {
                respEx.printStackTrace();
            }
        } catch (IOException ex) {
            System.err.println("Exception on Anomaly Detector: " + ex.getMessage());
            ex.printStackTrace();
        }
        return null;
    }
    // </sendRequest>
}
