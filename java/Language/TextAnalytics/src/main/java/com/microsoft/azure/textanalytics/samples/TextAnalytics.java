// Copyright (c) Microsoft Corporation. All rights reserved. MIT License. 
// See License.txt in the project root for license information.

import java.io.*;
import java.net.*;
import java.util.*;
import javax.net.ssl.HttpsURLConnection;

import com.google.gson.Gson;

/**
 * This sample uses the Text Analytics API to analyze text for: sentiment,
 * language detection, entity recognition, and key phrase extraction.
 * 
 * Use this library: https://github.com/google/gson
 * If running from the command line, add the gson jar to a lib folder.
 * 
 * Add your subscription key and endpoint to your environment variables.
 *
 * Download the project and run in an IDE, or build/run this file from the command line: 
 *  javac TextAnalytics.java -encoding utf-8 -cp .;lib\*
 *  java -cp .;lib\* TextAnalytics
 */

public class TextAnalytics {
    // The API subscription key and endpoint generated when you created an Azure Cognitive Services resource.
    static String subscriptionKey = System.getenv("TEXT_ANALYTICS_SUBSCRIPTION_KEY");
    static String endpoint = System.getenv("TEXT_ANALYTICS_ENDPOINT");

    public static void main(String[] args) {
        /**
         * For sentiment, keyphrase, and entity recognition your input documents must
         * specify language so that the proper model is used. If a language isn't
         * specified, we assume english is the document language. For language
         * detection, you need not specify a language.
         */

        // Sentiment Analysis
        Documents multiLanguageSentimentDocuments = new Documents();
        multiLanguageSentimentDocuments.add("1", "I had the best day of my life.", "en");
        multiLanguageSentimentDocuments.add("2", "This was a waste of my time. The speaker put me to sleep.", "en");
        multiLanguageSentimentDocuments.add("3", "No tengo dinero ni nada que dar...", "es");
        multiLanguageSentimentDocuments.add("4",
                "L'hotel veneziano era meraviglioso. È un bellissimo pezzo di architettura.", "it");

        String multiLanguageSentimentText = new Gson().toJson(multiLanguageSentimentDocuments);

        String sentimentResult = SentimentAnalysis.AnalyzeSentiment(endpoint, subscriptionKey, multiLanguageSentimentText);
        System.out.println("SENTIMENT RESPONSE:");
        System.out.println(sentimentResult);
        System.out.println();

        // For language detection your input documents don't have to specify language
        Documents languageDetectionDocuments = new Documents();
        languageDetectionDocuments.add("1", "This is a document written in English.");
        languageDetectionDocuments.add("2", "Este es un document escrito en Español.");
        languageDetectionDocuments.add("3", "这是一个用中文写的文件");

        String languageDetectionText = new Gson().toJson(languageDetectionDocuments);

        String languageResult = LanguageDetection.DetectLanguage(endpoint, subscriptionKey, languageDetectionText);
        System.out.println("LANGUAGE RESPONSE:");
        System.out.println(languageResult);
        System.out.println();

        // Entity Recognition 
        Documents multiLanguageNerDocuments = new Documents();
        multiLanguageNerDocuments.add("1",
                "Microsoft was founded by Bill Gates and Paul Allen on April 4, 1975, to develop and sell BASIC interpreters for the Altair 8800.",
                "en");
        multiLanguageNerDocuments.add("2",
                "La sede principal de Microsoft se encuentra en la ciudad de Redmond, a 21 kilómetros de Seattle.",
                "es");

        String multiLanguageNerText = new Gson().toJson(multiLanguageNerDocuments);
        String entityRecognitionResult = EntityRecognition.DetectEntities(endpoint, subscriptionKey, multiLanguageNerText);
        System.out.println("ENTITY RECOGNITION RESPONSE:");
        System.out.println(entityRecognitionResult);
        System.out.println();

        // KeyPhrase Extraction 
        Documents multiLanguageKpeDocuments = new Documents();
        multiLanguageKpeDocuments.add("1", "猫は幸せ", "ja");
        multiLanguageKpeDocuments.add("2", "Fahrt nach Stuttgart und dann zum Hotel zu Fu.", "de");
        multiLanguageKpeDocuments.add("3", "My cat might need to see a veterinarian.", "en");
        multiLanguageKpeDocuments.add("4", "A mi me encanta el fútbol!", "es");

        String multiLanguageKpeText = new Gson().toJson(multiLanguageKpeDocuments);

        String keyPhraseExtractionResult = KeyPhraseExtraction.DetectKeyPhrases(endpoint, subscriptionKey,
                multiLanguageKpeText);
        System.out.println("KEY PHRASE RESPONSE:");
        System.out.println(keyPhraseExtractionResult);
        System.out.println();
    }

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
    public static String DetectLanguage(String endpoint, String subscriptionKey, String text) {
        String path = "/text/analytics/v2.1/languages";

        try {
            URL serviceUrl = new URL(endpoint + path);
            String response = TextAnalytics.callTextAnalyticsService(serviceUrl, text);
            return response;
        } catch (Exception e) {
            return e.getMessage();
        }
    }
}

class SentimentAnalysis {
    public static String AnalyzeSentiment(String endpoint, String subscriptionKey, String text) {
        String path = "/text/analytics/v2.1/sentiment";

        try {
            URL serviceUrl = new URL(endpoint + path);
            String response = TextAnalytics.callTextAnalyticsService(serviceUrl, text);
            return response;
        } catch (Exception e) {
            return e.getMessage();
        }
    }
}

class KeyPhraseExtraction {
    public static String DetectKeyPhrases(String endpoint, String subscriptionKey, String text) {
        String path = "/text/analytics/v2.1/keyPhrases";

        try {
            URL serviceUrl = new URL(endpoint + path);
            String response = TextAnalytics.callTextAnalyticsService(serviceUrl, text);
            return response;
        } catch (Exception e) {
            return e.getMessage();
        }
    }
}

class EntityRecognition {
    public static String DetectEntities(String endpoint, String subscriptionKey, String text) {
        String path = "/text/analytics/v2.1/entities";

        try {
            URL serviceUrl = new URL(endpoint + path);
            String response = TextAnalytics.callTextAnalyticsService(serviceUrl, text);
            return response;
        } catch (Exception e) {
            return e.getMessage();
        }
    }
}
