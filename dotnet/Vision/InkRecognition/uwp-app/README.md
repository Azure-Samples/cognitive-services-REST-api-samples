---
topic: sample
languages:
  - C#
products:
  - UWP
  - azure
---

# Ink Recognizer Cognitive Service C# UWP Sample

![Build passing](https://img.shields.io/badge/build-passing-brightgreen.svg) ![License](https://img.shields.io/badge/license-MIT-green.svg)

Ink Recognizer Cognitive Service provides recognition of digital ink. It takes the digital ink stroke data as input and provides a document tree with individual recognition units as output. This project has sample code to demonstrate a few ways developers can take advantage of the service.

## Features

This sample provides the following features:

* Collect ink strokes
* Create JSON request with Ink Recognizer Service's schema.
* Call the Ink Recognizer REST APIs with the JSON payload
* Parse the JSON response from the service and build the document tree.

## Contents

| File/folder | Description |
|-------------|-------------|
| `cs`       | Sample source code. |
| `README.md` | This README file. |
| `LICENSE`   | The license for the sample. |

## Prerequisites

- Install [Visual Studio](https://docs.microsoft.com/en-us/visualstudio/install/install-visual-studio) 
- Requires subscription key from [Azure Cognitive Services](https://docs.microsoft.com/en-us/azure/cognitive-services/authentication) 

## Build the sample

1. Clone or download this sample repository
2. Start Visual Studio and select **File > Open > Project/Solution**
3. Replace "[YOUR SUBSCRIPTION KEY]" in NoteTaker.xaml.cs with a valid subscription key  
4. Select **Build > Build Solution**

## Run the sample

* To debug the sample, select Debug > Start Debugging. To run the sample without debugging, select Debug > Start Without Debugging
* Write some text / Draw a shape on the inking area
* After 1 second of inactivity, the ink will be recognized and the result will be displayed at the bottom of the app

## Resources

Additional resources related the project are located below

* [Learn more about Ink Recognizer](http://go.microsoft.com/fwlink/?LinkID=2084782)
* [Ink Recognizer API Reference](http://go.microsoft.com/fwlink/?LinkID=2085147)
* [Ink Recognizer JavaScript sample](https://github.com/Azure-Samples/cognitive-services-REST-api-samples/tree/master/javascript/InkRecognition/javascript-app)
* [Ink Recognizer WPF sample](https://github.com/Azure-Samples/cognitive-services-REST-api-samples/tree/master/dotnet/InkRecognition/wpf-app)
* [Ink Recognizer Java sample](https://github.com/Azure-Samples/cognitive-services-REST-api-samples/tree/master/java/InkRecognition/android-sample-app)
* [Ink Recognizer Swift sample](https://github.com/Azure-Samples/cognitive-services-REST-api-samples/tree/master/swift/InkRecognition)
