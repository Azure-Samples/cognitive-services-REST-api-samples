---
topic: sample
languages:
  - java
products:
  - azure
  - cognitive services
---

# Ink Recognizer REST API samples

These samples are quickstarts that show how to use the Ink Recognizer API.

## Prerequisites

- An [Azure Ink Recognizer resource](https://portal.azure.com/#blade/Microsoft_Azure_Marketplace/MarketplaceOffersBlade/selectedMenuItemId/home/searchQuery/ink%20recognizer) 
- For the sample in the quickstart folder: 
  * copy/paste the `.java` file into your IDE or text editor,
  * add the `example-ink-strokes.json` file as a resource to your project or working directory, 
  * create a lib folder in your working directory (or IDE project) and add the Java jar libraries needed: <br>
    httpclient-4.5.11+ <br>
    slf4j-jdk14-1.7.28+  <br>
    httpcore-4.4.13+  <br>
    commons-logging-1.2+ <br>
    jackson-databind-2.10.2+ <br>
    jackson-annotations-2.10.2+ <br>
    jackson-core-2.10.2+ <br>
- Add your key and endpoint from your Azure resource to your environment variables with the variable names suggested in the sample.

## Running the samples
- If trying the Android app, upload the Recognizer repo into Android Studio, add environment variables for key/endpoint, and run.
- For the quickstart, run from your IDE or using the below commands from the command line: <br>
  `javac RecognizeInk.java -cp .;lib\* -encoding UTF-8` <br>
  `java -cp .;lib\* RecognizeInk`

## Resources
- Ink Recognizer documentation: <br>
https://docs.microsoft.com/en-us/azure/cognitive-services/ink-recognizer/index
- Ink Recognizer API:<br> 
https://dev.cognitive.microsoft.com/docs/services/inkrecognizer/operations/inkRecognizerPUT
