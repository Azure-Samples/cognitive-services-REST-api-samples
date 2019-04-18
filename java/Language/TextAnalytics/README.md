# Quickstart for Text Analytics API with Java
<a name="HOLTop"></a>

This article shows you how to detect language, analyze sentiment, and extract key phrases using the [Text Analytics APIs](//go.microsoft.com/fwlink/?LinkID=759711) with Java. 

Refer to the [API definitions](//go.microsoft.com/fwlink/?LinkID=759346) for technical documentation for the APIs.

## Prerequisites

You must have a [Cognitive Services API account](https://docs.microsoft.com/azure/cognitive-services/cognitive-services-apis-create-account) with **Text Analytics API**. You can use the **free tier for 5,000 transactions/month** to complete this quickstart.

You must also have the [endpoint and access key](../How-tos/text-analytics-how-to-access-key.md) that was generated for you during sign up.


### Quickstart

To get these samples running locally, simply get the pre-requisites above, then:

1. git clone https://github.com/Azure-Samples/cognitive-services-java-sdk-samples.git
2. cd cognitive-services-java-sdk-samples/TextAnalytics
3. mvn compile
4. set env variable AZURE_TEXTANALYTICS_API_KEY to your cognitive services API key.
5. mvn exec:java -Dexec.mainClass="com.microsoft.azure.textanalytics.samples.Samples"

## See also 

 [Text Analytics overview](../overview.md)  
 [Frequently asked questions (FAQ)](../text-analytics-resource-faq.md)
