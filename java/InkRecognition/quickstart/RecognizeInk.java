// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

import org.apache.http.HttpEntity;
import org.apache.http.client.methods.CloseableHttpResponse;
import org.apache.http.client.methods.HttpPost;
import org.apache.http.entity.StringEntity;
import org.apache.http.impl.client.CloseableHttpClient;
import org.apache.http.impl.client.HttpClients;
import org.apache.http.util.EntityUtils;
import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Paths;

public class RecognizeInk {

    // Replace the subscriptionKey string value with your valid subscription key.
    private static final String subscriptionKey = "YOUR_SUBSCRIPTION_KEY";
    // Replace the dataPath string with a path to the JSON formatted ink stroke data file.
    public static final String rootUrl = "https://api.cognitive.microsoft.com";
    public static final String inkRecognitionUrl = "/inkrecognizer/v1.0-preview/recognize";
    private static final String dataPath = "PATH_TO_INK_STROKE_DATA";

    public static void main(String[] args) throws Exception {

        String requestData = new String(Files.readAllBytes(Paths.get(dataPath)), "utf-8");
        recognizeInk(requestData);
    }

    static void recognizeInk(String requestData) {
        System.out.println("Sending an Ink recognition request.");

        String result = sendRequest(rootUrl, inkRecognitionUrl, subscriptionKey, requestData);
        System.out.println(result);
    }

    static String sendRequest(String endpoint, String apiAddress, String subscriptionKey, String requestData) {
        try (CloseableHttpClient client = HttpClients.createDefault()) {
            HttpPost request = new HttpPost(endpoint + apiAddress);
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
}
