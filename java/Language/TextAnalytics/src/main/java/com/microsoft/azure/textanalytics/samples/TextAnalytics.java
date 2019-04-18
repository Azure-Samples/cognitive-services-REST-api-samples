/**
 * Copyright (c) Microsoft Corporation. All rights reserved.
 * Licensed under the MIT License. See License.txt in the project root for
 * license information.
 */

package com.microsoft.azure.textanalytics.samples;

import java.io.*;
import java.net.*;
import java.util.*;
import javax.net.ssl.HttpsURLConnection;

import com.google.gson.Gson;

public class TextAnalytics {
    /**
     * The API Subscription Key that was generated when you created a cogntiive
     * services resource in Azure. Make sure to change this.
     */
    private static final String subscriptionKey = "enter-your-key-here";
    /**
     * The host of the cognitive services service. We will then append the Text
     * Analytics service path to this to generate the URI against which we will make
     * requests. Please make sure to enter the region to match the Azure region in
     * which your cognitive services resource was deployed to.
     */
    private static final String host = "https://<region>.api.cognitive.microsoft.com";

    public static void addAuthenticationHeader(HttpsURLConnection connection) {
        connection.setRequestProperty("Ocp-Apim-Subscription-Key", subscriptionKey);
    }

    public static String callTextAnalyticsService(URL serviceUrl, String text) {
        try {
            byte[] encoded_input;
            encoded_input = text.getBytes("UTF-8");

            HttpsURLConnection connection = (HttpsURLConnection) serviceUrl.openConnection();
            connection.setRequestMethod("POST");
            connection.setRequestProperty("Content-Type", "application/json");
            addAuthenticationHeader(connection);
            connection.setDoOutput(true);

            DataOutputStream wr = new DataOutputStream(connection.getOutputStream());
            wr.write(encoded_input, 0, encoded_input.length);
            wr.flush();
            wr.close();

            StringBuilder response = new StringBuilder();
            BufferedReader in = new BufferedReader(new InputStreamReader(connection.getInputStream()));
            String line;
            while ((line = in.readLine()) != null) {
                response.append(line);
            }
            in.close();

            return response.toString();
        } catch (Exception e) {
            return e.getMessage();
        }
    }

    public static void main(String[] args) {
        /* For language detection your input documents don't have to specify language */
        Documents documents = new Documents();
        documents.add("1", "This is a document written in English.");
        documents.add("2", "Este es un document escrito en Español.");
        documents.add("3", "这是一个用中文写的文件");

        String text = new Gson().toJson(documents);

        String languageResult = LanguageDetection.DetectLanguage(host, subscriptionKey, text);
        System.out.println("LANGUAGE RESPONSE:");
        System.out.println(languageResult);

        /*
         * For sentiment, keyphrase, and entity recognition your input documents must
         * specify language so that the proper model is used. If a language isn't
         * specified, we assume english is the document language.
         */
        Documents multiLanguageDocuments = new Documents();
        multiLanguageDocuments.add("1", "I love Seattle. It's a great city.", "en");
        multiLanguageDocuments.add("2", "Este clima me encanta. Esta muy soleado hoy en Buenos Aires.", "es");

        String multiLanguageText = new Gson().toJson(multiLanguageDocuments);

        String sentimentResult = SentimentAnalysis.AnalyzeSentiment(host, subscriptionKey, multiLanguageText);
        System.out.println("SENTIMENT RESPONSE:");
        System.out.println(sentimentResult);

        String keyPhraseExtractionResult = KeyPhraseExtraction.DetectKeyPhrases(host, subscriptionKey,
                multiLanguageText);
        System.out.println("KEY PHRASE RESPONSE:");
        System.out.println(keyPhraseExtractionResult);

        String entityRecognitionResult = EntityRecognition.DetectEntities(host, subscriptionKey, multiLanguageText);
        System.out.println("ENTITY RECOGNITION RESPONSE:");
        System.out.println(entityRecognitionResult);
    }
}

class Document {
    public String id, text, language;

    public Document(String id, String text) {
        this.id = id;
        this.text = text;
    }

    public Document(String id, String text, String language) {
        this.id = id;
        this.text = text;
        this.language = language;
    }
}

class Documents {
    public List<Document> documents;

    public Documents() {
        this.documents = new ArrayList<Document>();
    }

    public void add(String id, String text) {
        this.documents.add(new Document(id, text));
    }

    public void add(String id, String text, String language) {
        this.documents.add(new Document(id, text, language));
    }
}

class LanguageDetection {
    public static String DetectLanguage(String host, String subscriptionKey, String text) {
        String path = "/text/analytics/v2.1/languages";

        try {
            URL serviceUrl = new URL(host + path);
            String response = TextAnalytics.callTextAnalyticsService(serviceUrl, text);
            return response;
        } catch (Exception e) {
            return e.getMessage();
        }
    }
}

class SentimentAnalysis {
    public static String AnalyzeSentiment(String host, String subscriptionKey, String text) {
        String path = "/text/analytics/v2.1/sentiment";

        try {
            URL serviceUrl = new URL(host + path);
            String response = TextAnalytics.callTextAnalyticsService(serviceUrl, text);
            return response;
        } catch (Exception e) {
            return e.getMessage();
        }
    }
}

class KeyPhraseExtraction {
    public static String DetectKeyPhrases(String host, String subscriptionKey, String text) {
        String path = "/text/analytics/v2.1/keyPhrases";

        try {
            URL serviceUrl = new URL(host + path);
            String response = TextAnalytics.callTextAnalyticsService(serviceUrl, text);
            return response;
        } catch (Exception e) {
            return e.getMessage();
        }
    }
}

class EntityRecognition {
    public static String DetectEntities(String host, String subscriptionKey, String text) {
        String path = "/text/analytics/v2.1/entities";

        try {
            URL serviceUrl = new URL(host + path);
            String response = TextAnalytics.callTextAnalyticsService(serviceUrl, text);
            return response;
        } catch (Exception e) {
            return e.getMessage();
        }
    }
}
