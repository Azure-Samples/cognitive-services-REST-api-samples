// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

import java.io.*;
import java.net.*;
import javax.net.ssl.HttpsURLConnection;
import org.json.*;

/**
 * This sample uses the Bing Spell Check v7 API to check spelling of a sentence.
 *
 * Include library (for pretty print), add jar to a lib folder:
 *   https://github.com/stleary/JSON-java
 *
 * Add your subscription key and endpoint to your environment variables.
 *
 * Build/run from command line: 
 *   javac BingSpellCheckv7.java -cp .;lib\*
 *   java -cp .;lib\* BingSpellCheckv7
 */

public class BingSpellCheckv7 {

    // Or using the generic endpoint https://api.cognitive.microsoft.com is OK too.
    static String endpoint = System.getenv("BING_SPELL_CHECK_ENDPOINT") + "/bing/v7.0/spellcheck";

    // NOTE: Replace this example key with a valid subscription key.
    static String subscriptionKey = System.getenv("BING_SPELL_CHECK_SUBSCRIPTION_KEY");

    static String mkt = "en-US";
    static String mode = "proof";
    static String text = "Hollo, wrld!";

    public static void main(String[] args) {
        try {
            String params = "?mkt=" + mkt + "&mode=" + mode;
            URL url = new URL(endpoint + params);
            HttpsURLConnection connection = (HttpsURLConnection) url.openConnection();
            connection.setRequestMethod("POST");
            connection.setRequestProperty("Content-Type", "application/x-www-form-urlencoded");
            connection.setRequestProperty("Content-Length", "" + text.length() + 5);
            connection.setRequestProperty("Ocp-Apim-Subscription-Key", subscriptionKey);
            connection.setDoOutput(true);

            // Optional
            DataOutputStream wr = new DataOutputStream(connection.getOutputStream());
            wr.writeBytes("text=" + text);
            wr.flush();
            wr.close();

            // Get results
            BufferedReader in = new BufferedReader(new InputStreamReader(connection.getInputStream()));

            String line;
            StringBuilder sb = new StringBuilder();
            while ((line = in.readLine()) != null) {
                sb.append(line);
            }
            // Pretty print
            System.out.println((new JSONObject(sb.toString())).toString(4));

            in.close();
        } catch (Exception e) {
            System.out.println(e);
        }
    }
}
