---
topic: sample
languages:
  - java
products:
  - azure
  - cognitive services
---

# Language REST API samples

These samples are quickstarts that show how to use various Language APIs, such as Bing Spell Check and Text Analytics.

## Prerequisites

- Create an [Azure resource](https://portal.azure.com) for the service you'd like to try, for example a Bing Spell Check resource.
- Add your key and endpoint from your resource to your environment variables with the variable names suggested in the sample.
- Copy/paste the `.java` file into your project or text editor.
- For Text Analytics an entire Java project is available for download.
- Include the [JSON](https://github.com/stleary/JSON-java) library into a lib folder. 

## Running the samples
- Run Text Analytics in your IDE
- Build/run Bing Spell Check from the command line with these commands: <br>

  `javac BingSpellCheck.java -cp .;lib\* -encoding UTF-8` <br>
  `java -cp .;lib\* BingSpellCheck`

## Resources
#### Bing Spell Check: [Documentation](https://docs.microsoft.com/en-us/azure/cognitive-services/bing-spell-check/overview), [API](https://dev.cognitive.microsoft.com/docs/services/5f7d486e04d2430193e1ca8f760cd7ed/operations/57855119bca1df1c647bc358)

#### Text Analytics: [Documentation](https://docs.microsoft.com/en-us/azure/cognitive-services/text-analytics/index), [API](https://westus.dev.cognitive.microsoft.com/docs/services/TextAnalytics-v3-0-Preview-1/operations/Languages) 
